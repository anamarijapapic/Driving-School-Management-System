# Driving School Management System

## Table of Contents

* [General Info](#general-info)
* [Features](#features)
* [Technologies](#technologies)
* [Database Model](#database-model)
* [Project Dependency Diagram](#project-dependency-diagram)
* [Getting Started](#getting-started)
* [Credits](#credits)

## General Info

Driving School Management System is a web application that is used by driving school students, driving instructors and administration. It handles driving lessons scheduling and attendance tracking. 🚗📝  

> Project created as a college seminar:  
> *SRC136 - C# Programming*  
> *University of Split - University Department of Professional Studies*

![DSMS - Home](https://github.com/OSS-Csharp-Seminar/Driving-School-Management-System/assets/92815435/2fa06153-e1bc-481a-8bf8-45ae5c2095fa "Driving School Management System - Home")

## Features

### User Management

- Roles (base for authorization) ([ASP.NET Core Identity with Role services](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-6.0)):
    - Student - Atendee
    - Instructor
    - Admin
- Available operations:
    - Features from [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio) (adapted to fit our needs)
        - **Registration**
        - **Authentication (login/logout)**
        - Edit and update user data
        - Delete user
        - View all users (with search, filtering)
        - View user details for specific user
        - Display & manage current user details
     
> **Note**
> By default, every new registered user is assigned `Student` role. Then, if needed, admin can change that user's role.

### Driving School Management

> **Note**
> Available categories are: `A`, `B`.

#### Vehicles

- Available operations:
    - Add new vehicle
    - Edit and update vehicle
    - Delete vehicle
- View all vehicles (with search, filtering and pagination)

#### Enrollment

- Available operations:
    - Add new enrollment
    - Edit and update enrollment
    - Delete enrollment
- View all enrollments (with search, filtering and pagination)

#### Appointments

- Available operations:
    - Make new appointment
    - Edit and update appointment
    - Cancel appointment
- View all appointments (with search, filtering and pagination)

> **Note**
> By default, it is only available to book/reserve appointments for future 5 working days. Appointments last 1 hour, and initially there are 8 time slots each day (10:00, 11:00, 12:00, 13:00, 14:00, 15:00, 16:00, 17:00). Appointments can have statuses: `Reserved`, `Completed`, `Canceled`.

#### Feedback & Reactions

> **Note**
> It is possible to leave reviews & ratings on driving instructor's profile (Users/Details page). Other users can mark specific reviews useful or not.

- Available operations:
    - Add new feedback
    - Edit and update feedback
    - Delete feedback
    - Leave reaction (useful or not) on specific feedback
- View all reviews (with associated reactions) for specific instructor

## Technologies

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)  
![Bootstrap](https://img.shields.io/badge/bootstrap-%23563D7C.svg?style=for-the-badge&logo=bootstrap&logoColor=white)  
![NuGet](https://img.shields.io/badge/NuGet-004880?style=for-the-badge&logo=nuget&logoColor=white)  
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)  
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)  

![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)

## Database Model

![ERD For Database](https://github.com/OSS-Csharp-Seminar/Driving-School-Management-System/assets/92815435/5901b893-7e19-4184-aead-61f14efa51da "ERD For Database (From pgAdmin)")

## Project Dependency Diagram

```mermaid
---
title: src
---
flowchart TD
    DSMS.Frontend --> DSMS.Application
    DSMS.API --> DSMS.Application
    DSMS.Application --> DSMS.DataAccess
    DSMS.DataAccess --> DSMS.Core
    DSMS.DataAccess --> DSMS.Shared
```

## Getting Started

### Requirements

You should have the following installed:

- [Docker Desktop](https://www.docker.com/)
- [Visual Studio](https://visualstudio.microsoft.com/)

### Running the Application

1. Open solution `DSMS.sln` inside Visual Studio.

2. Docker Desktop should be running:
![Docker Desktop running](https://github.com/OSS-Csharp-Seminar/Driving-School-Management-System/assets/92815435/4294c40b-1ad8-42b9-b895-ba72417e6ae3)

3. Check that `docker-compose` project is set up as startup project:
![docker-compose project](https://github.com/OSS-Csharp-Seminar/Driving-School-Management-System/assets/92815435/9bb53780-ef5c-4925-9b3a-9883eb3f2fe1)

4. Launch web app using **Docker Compose** setting, it should automatically open inside your web browser. Enjoy!
![Launch Docker Compose](https://github.com/OSS-Csharp-Seminar/Driving-School-Management-System/assets/92815435/8a961868-c355-4b3f-9672-d8cb3caf746c)

#### Managing the Database

[pgAdmin](https://www.pgadmin.org/) is available at http://localhost:15432/browser/.

## Credits

 ✍️ **no-ap-team** members: 

* [Nikola Bošnjak](https://github.com/LunarStrain94)
* [Nikola Occidentale](https://github.com/nikolaoccid)
* [Anamarija Papić](https://github.com/anamarijapapic)
