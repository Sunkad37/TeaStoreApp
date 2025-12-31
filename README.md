 - Add Empty Solution
 - Add Solution Projects
    - APi
    - Application
    - Domain
    - Infrastructure
    
  - Add Project References
   - Application => Add Domain Ref
   - APi => Add Application Ref
   - Infrastructure => Add Application
   
  - Define Entities in Domain Project
   - Create Entities Folder and add Classes for entity
   
  - Install EntityFramework
    - dotnet add package Microsoft.EntityFrameworkCore
    - dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    - dotnet add package Microsoft.EntityFrameworkCore.Tools
    
  - Add DbContext Class to the Infrastructure Proj
   - Ex : Persistence/DbContext.cs
   - Add Db Sets - Ex : Dish
   
  - Configure Sql Server Connection
    - Override OnConfiguring Method to add Db Connection
    
  - Add Model Creating Method
    - override OnModelCreating Method
    - Add PK and FK for respective tables
    
   - Add Migrations
    - dotnet ef migrations add InitialCreate
      dotnet ef database update
-------------------------------------
- Flexible Db Connection
 - If you want to access infrastructure module in APi Project 
    - Add Infrastructure proj ref to APi Project
     - Add Db Context to Api Service(builder.Services.AddInfrastructure())
      - To Do This
       - Add ServiceCollection Extension Class to the Infrastructure Project
       - Add AddInfrastructure Method to it
       - AddInfrastructure to APi Service
       - Add Connection String to Config File and read it from config file
       - Remove onConfigure method from DbContext and pass it through constructor
       - Add Ef Tools nuget to Api Project and run update database - it should create a fresh database
       
  - Seeding Data
    - Add a folder Seeders in Infrastructure Project
        - Add a Class for seeding the Entity and inject DbContext via DI
        - Add Seed Method
        - IRestaurantSeeder add interface to service(Add it to service collection in infrastructure project)
        - Create scope and call seed method from APi Service
  
  ------------------------------
  - Create APi's 
    - Add Controller folder
    - Add Api Controller and inherit from Controller Base
    - Add Service Classes in Application Project (EntityFolderName/EntityClassService)
    - Add Interface Repository in Domain Project
    - Create and Repository Class in Infrastructure Project and Refer Implement Iinterface from Domain and register it 
  ---------------------------------------------
  
  - Returning DTO's
    - Create Dto in Application Project and implement FromEntity Method to return values     
  
  - Automapper
    - Add Automapper Dependency Injection Nuget Package to Application Project
    - Create Mappings - Add Profile classes in Dto Folder
    - Add Map in Service Class
    - Register Mapper in Service Collection of Application Project - (services.AddAutoMapper(typeof(ApplicationServiceCollections).Assembly)

- Model Validation
    - Try Using Model State
    - Or Add Validation to the Dto's
    - Or Using Fluent Validation Nuget - Add Rules
---------------------------------------------------

- CQRS
    - Install MediateR Package to the Application Project
     - Command - Write
        - Create Command Folder/Command Class for the entities
        - Add Entity properties(members) and return Type
        - Create respective Handler Class - Implement IRequestHandler interface
        - Implement the logic of handler method
        - Adjust the Auto Mapper Profile Class to map
     - Query - Read
        - Create Query Class and add return type
        - Create Handler Class and Implement IRequest Interface
        - Inject the MediateR in Application Service Collection - Register it
     - Call through MediateR in Controllers
     - Implement Delete|Update APi End Points as well
-------------------------------------------------------

- Logging
    - Using serilog package - implement logging mechanism
    - Try to log Console | ToFile
    - Try to format the logs using json config
- Swagger Doc
    - Add Swachbuckle Asp.NetCore package and register it
    - Add Produce Response Type and status code to the Controller's
    
- Exception Handling
    - Create Error Handling Middleware and implement IMiddleware
    - Register it(before build) and use(after)
    - Create Custom Exceptions - Example - In Domain Project - Not Found 
----------------------------------------------------------------------

- Authentication
    - Add Package to Domain Project - dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    - Create User Entity and inherit IdentityUser
    - Change the DbContext to -> IdentityDbContext<User>
    - Register Identity End Point's -> services.AddIdentityApiEndpoints<User>()
                                                   .AddEntityFrameworkStores<RestaurantDbContext>();
    - Register - app.MapIdentityApi<User>(); in program.cs
    - Add Migration and Update Database
    - Register and Login - Using these end points get the authentication token
    - Set Up Swagger for Authentication
        - Add Security Definition to SwaggerGen()
        - Add these to service Extension class and refer it to Program.cs
        - Add Authorize attribute to the controller - if dont want add AllowAnonymous attribute
     - Add User Context
        - Add Current User record in Application Project
        - Create UserCotext Interface and implement it
        - Register IUserCotext in Application Extension Method
        - Add services.AddHttpContextAccessor();
        - Extend User Table - Optional - if you want add any other new columns 
        - Add Identity Controller - to update newly add columns for the user table(if you added)

        
     
    

------------------------------
# Stage all modified and new files
git add .

# Or stage a specific file
git add RestaurantApp.Api/Program.cs

# Commit with a message
git commit -m "Implemented Custom Exception and Delete Controller"

# Push current branch to origin (first time, set upstream)
git push -u origin main

# After upstream is set, just use:
git push


