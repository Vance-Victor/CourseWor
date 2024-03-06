Project Description 

 

Project goals: 

The project aims to efficiently manage and organize data related to a Tech organization, including information about departments, duties, projects, skills, staff, and their respective relationships. 

The system is designed to be scalable, accommodating the growth of the organization by handling an increasing number of departments, staff, projects, and skills. 

 The project focuses on maintaining a well-structured relational database design to ensure data integrity and effective querying. 

 

 

Functionalities: 

Department Management:  Create, read, update, and delete (CRUD) operations for managing departments. Departments have attributes such as Name, Email, and Location. 

Role/Function Management:  CRUD operations for managing Roles/Functions, including attributes like Name, Description, Responsibility, and association with a specific project and staff. 

Project Management:  CRUD operations for managing projects with attributes such as Deadline, Description, Duration, and association with a specific department. Projects can have multiple roles associated with them. 

Skill Management:  CRUD operations for managing skills, including attributes like Type, Level, and Description. 

Staff Management:  CRUD operations for managing staff members with attributes like Name, Email, Address, and association with a specific department. Staff members can have multiple skills associated with them. 

Skill Assignment:  Ability to associate skills with staff members using the StaffSkill table. 

 

 

Technologies Used: 

Programming Language: Implementation using C#. 

Framework: Developed using .NET Core, a cross-platform, high-performance framework for building modern, cloud-based, and internet-connected applications. 

RESTful API: Implementation of a RESTful API architecture to expose endpoints for interacting with the backend service. 

Entity Framework: Utilizes Entity Framework Core for object-relational mapping (ORM), simplifying database interactions and providing a seamless integration with the C# application. 

Dependency Injection: Leverages dependency injection for managing and injecting application services. 

 

Summary: 

This provides a high-level overview of the project's objectives, functionalities, and the technologies used to implement a .NET Core (C#) RESTful backend service for Harvoxx Tech hub organization. The actual implementation details, code structure, and additional features were depended on the specific requirements and design decisions that was made during the development process. 

 

 

 

Database Design 

 

-- Department Table 

CREATE TABLE Department ( 

    Id INT PRIMARY KEY, 

    Name VARCHAR(255), 

    Email VARCHAR(255), 

    Location VARCHAR(255) 

); 

  

-- Staff Table 

CREATE TABLE Staff ( 

    StaffId INT PRIMARY KEY, 

    Name VARCHAR(255), 

    Email VARCHAR(255), 

    Address VARCHAR(255), 

    DepartmentId INT, 

    FOREIGN KEY (DepartmentId) REFERENCES Department(Id) 

); 

  

-- Skill Table 

CREATE TABLE Skill ( 

    SkillId INT PRIMARY KEY, 

    Type VARCHAR(255), 

    Level VARCHAR(255), 

    Description VARCHAR(255) 

); 

  

-- StaffSkill Table 

CREATE TABLE StaffSkill ( 

    StaffSkillId INT PRIMARY KEY, 

    StaffId INT, 

    SkillId INT, 

    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId), 

    FOREIGN KEY (SkillId) REFERENCES Skill(SkillId) 

); 

  

-- Project Table 

CREATE TABLE Project ( 

    Id INT PRIMARY KEY, 

    Deadline VARCHAR(255), 

    Description VARCHAR(255), 

    Duration INT, 

    DepartmentId INT, 

    FOREIGN KEY (DepartmentId) REFERENCES Department(Id) 

); 

  

-- Function Table 

CREATE TABLE Function ( 

    Id INT PRIMARY KEY, 

    Name VARCHAR(255), 

    Description VARCHAR(255), 

    Responsibility VARCHAR(255), 

    ProjectId INT, 

    StaffId INT, 

    FOREIGN KEY (ProjectId) REFERENCES Project(Id), 

    FOREIGN KEY (StaffId) REFERENCES Staff(StaffId) 

); 

 

 

 

 

 

 

USER AUTHENTICATION 

 

In other to add a JWT token, I installed ‘Microsoft.AspNetCore.Authentication.JwtBearer’ dependency before adding the code below to the account controller. 

 

Token Genaration: 

 

	private string GenerateJwtToken(IdentityUser user, IList<string> roles)  

{  

var claims = new List<Claim>  

{  

new Claim(JwtRegisteredClaimNames.Sub, user.Email),  

new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  

};  

// Add roles as claims  

foreach (var role in roles)  

{  

claims.Add(new Claim(ClaimTypes.Role, role));  

}  

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  

var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  

var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));  

var token = new JwtSecurityToken(  

_configuration["Jwt:Issuer"],  

_configuration["Jwt:Issuer"],  

claims,  

expires: expires,  

signingCredentials: creds  

);  

return new JwtSecurityTokenHandler().WriteToken(token);  

} 

 

 

 

2. Token Validation: 

 

options.TokenValidationParameters = new TokenValidationParameters  

{  

ValidateIssuer = true,  

ValidateAudience = true,  

ValidateLifetime = true,  

ValidateIssuerSigningKey = true,  

ValidIssuer = builder.Configuration["Jwt:Issuer"],  

ValidAudience = builder.Configuration["Jwt:Issuer"],  

IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))  

}; 

 

 

3.  Expiry Login handling: 

[HttpPost("login")]  

public async Task<IActionResult> Login(AuthModel model)  

{  

var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);  

if (result.Succeeded)  

{  

var user = await _userManager.FindByEmailAsync(model.Email);  

var roles = await _userManager.GetRolesAsync(user);  

var token = GenerateJwtToken(user,roles);  

return Ok(new { Token = token });  

}  

return Unauthorized("Invalid login attempt.");  

} 
