﻿using AutoMapper;
using DSMS.Application.Common.Email;
using DSMS.Application.Exceptions;
using DSMS.Application.Helpers;
using DSMS.Application.Models;
using DSMS.Application.Models.User;
using DSMS.Application.Templates;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DSMS.Application.Services.Impl;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;

    private readonly IEmailService _emailService;

    private readonly ITemplateService _templateService;

    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IConfiguration configuration,
        IEmailService emailService,
        ITemplateService templateService,
        IMapper mapper,
        IUserRepository userRepository,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _configuration = configuration;
        _emailService = emailService;
        _templateService = templateService;
        _mapper = mapper;
        _userRepository = userRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel)
    {
        var user = _mapper.Map<ApplicationUser>(createUserModel);

        var result = await _userManager.CreateAsync(user, createUserModel.Password);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var emailTemplate = await _templateService.GetTemplateAsync(TemplateConstants.ConfirmationEmail);

        var emailBody = _templateService.ReplaceInTemplate(emailTemplate,
            new Dictionary<string, string> { { "{UserId}", user.Id }, { "{Token}", token } });

        await _emailService.SendEmailAsync(EmailMessage.Create(user.Email, emailBody, "[DSMS]Confirm your email"));

        return new CreateUserResponseModel
        {
            Id = Guid.Parse((await _userManager.FindByNameAsync(user.UserName)).Id)
        };
    }

    public async Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginUserModel.Username);

        if (user == null)
            throw new NotFoundException("Username or password is incorrect");

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginUserModel.Password, false, false);

        if (!signInResult.Succeeded)
            throw new BadRequestException("Username or password is incorrect");

        var token = JwtHelper.GenerateToken(user, _configuration);

        return new LoginResponseModel
        {
            Username = user.UserName,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
    {
        var user = await _userManager.FindByIdAsync(confirmEmailModel.UserId);

        if (user == null)
            throw new UnprocessableRequestException("Your verification link is incorrect");

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailModel.Token);

        if (!result.Succeeded)
            throw new UnprocessableRequestException("Your verification link has expired");

        return new ConfirmEmailResponseModel
        {
            Confirmed = true
        };
    }

    public async Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        var result =
            await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword,
                changePasswordModel.NewPassword);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        return new BaseResponseModel
        {
            Id = Guid.Parse(user.Id)
        };
    }

    public async Task<IEnumerable<UserResponseModel>> GetAllAsync()
    {
        var response = await _userRepository.GetAll().ToListAsync();
        var users = new List<UserResponseModel>();

        foreach (var user in response)
        {
            var userDto = _mapper.Map<UserResponseModel>(user);

            try
            {
                var role = _userManager.GetRolesAsync(user).Result.First();
                userDto.Role = (ApplicationRole)Enum.Parse(typeof(ApplicationRole), role);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            users.Add(userDto);
        }

        return users;
    }

    public async Task<IEnumerable<UserResponseModel>> GetAllUsersByRoleAsync(ApplicationRole applicationRole)
    {
        var response = await _userRepository.GetAll().ToListAsync();
        var filteredUsers = new List<UserResponseModel>();

        foreach (var user in response)
        {
            var userDto = _mapper.Map<UserResponseModel>(user);

            try
            {
                var role = _userManager.GetRolesAsync(user).Result.First();
                userDto.Role = (ApplicationRole)Enum.Parse(typeof(ApplicationRole), role);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (userDto.Role == applicationRole)
            {
                filteredUsers.Add(userDto);
            }
        }

        return filteredUsers;
    }

    public async Task<ApplicationUser> GetByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public IEnumerable<UserResponseModel> Search(IEnumerable<UserResponseModel> users, string searchString)
    {
        IEnumerable<UserResponseModel> searchedUsers = users;

        if (!string.IsNullOrEmpty(searchString))
        {
            var searchStringTrim = searchString.ToLower().Trim();
            searchedUsers = users.Where(u =>
                u.FirstName.ToLower().Contains(searchStringTrim) ||
                u.LastName.ToLower().Contains(searchStringTrim));
        }

        return searchedUsers;
    }

    public IEnumerable<UserResponseModel> Filter(IEnumerable<UserResponseModel> users, string currentFilter)
    {
        IEnumerable<UserResponseModel> filteredUsers = users;

        if (!string.IsNullOrEmpty(currentFilter))
        {
            var currentFilterTrim = currentFilter.Trim();
            filteredUsers = users.Where(v => v.Role.ToString() == currentFilter);
        }

        return filteredUsers;
    }
}
