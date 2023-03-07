# Driving School Management System

## Table of Contents

* [General Info](#general-info)
* [Features](#features)
* [Technologies](#technologies)
* [Database Model](#database-model)
* [Credits](#credits)

## General Info

Driving School Management System is a web application that is used by driving school students, lecturers, instructors and administration. It handles driving school processes step-by-step - enrollment, course appointments and attendance tracking, and driving lessons scheduling. 🚗📝  

> Project created as a college seminar:  
> *SRC136 - C# Programming*  
> *University of Split - University Department of Professional Studies*

## Features

### User Management

- Roles:
    - Student - Atendee
    - Lecturer
    - Instructor
    - Admin
- Registration
    - Medical profession checkbox
    - Documentation upload (ID card (required), driving license for other category, medical certificate (required))
- Authentication (login/logout)

### Driving School Processes Management

- Anonymous user registers on the site, and enrolls in the driving school by selecting a category:
    - A
        - If student already possesses a driving license for B category, he directly enrolls in UV course
            - Skipping PPSP & PPP courses - they are tracked as passed
    - B
        - Student enrolls in one of the upcoming PPSP courses

#### Stage 1 - Prometni propisi i sigurnosna pravila (PPSP)

- Predetermined appointments (x10 - 2 weeks on working days)
- Max `num` students on course
- Course lecturer tracks students that are present
- Student can see their attendance statistics  

- Students with 70% attendance completed the course and are sent to PPSP exam  

- Administration tracks if student passed the PPSP exam (external information - provided by HAK)

#### Stage 2 - Pružanje prve pomoći osobama ozlijeđenim u prometnoj nesreći (PPP)

* Note: If student has a medical profession he doesn't enroll in PPP, but directly in UV course - PPP exam tracked as passed  

- PPP enrollment requirements:
    - passed PPSP exam

- Predetermined appointments (x3 - 2 theoretical, 1 practical)
- Max `num` students on course
- Course lecturer tracks students that are present
- Student can see their attendance statistics  

- Students with 100% attendance completed the course and are sent to PPP exam  

- Administration tracks if student passed the PPP exam (external information - provided by HAK)

#### Stage 3 - Upravljanje vozilom (UV)

- UV enrollment requirements:
    - passed PPSP & PPP exam

- Student chooses his driving instructor
    - Student can change his instructor later if he is not satisfied
- Instructor enters his working hours for upcoming period
- Students pick available driving lesson appointments (1 h) with their instructors
    - Student and instructor can cancel reserved appointment
- Instructor tracks completed driving appointments (x35 mandatory)  

- Students who attended all 35 driving appointments and paid all expenses to driving school are sent to UV exam  

- Administration tracks if student passed the UV exam (external information - provided by HAK)

##### Instructor Feedback

- On driving instructor's profile his students can leave ratings and reviews (anonymous or not)

### Bonus Features - If Time Allows

- Mail sending
    - 'Welcome' email with total receipt sent to student when he registers and enrolls in driving school

- Payment processing integration with [Stripe](https://stripe.com/)
    - Driving school enrollment receipt handling
    - Additional driving lessons (no. 35+) are to be charged
    - Late canceled/no show driving lesson appointments (student-side) are to be charged

## Technologies

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)

## Database Model

`TO-DO`

## Credits

 ✍️ **no-ap-team** members: 

* [Nikola Bošnjak](https://github.com/LunarStrain94)
* [Nikola Occidentale](https://github.com/nikolaoccid)
* [Anamarija Papić](https://github.com/anamarijapapic)
