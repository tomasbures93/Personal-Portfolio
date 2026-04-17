# PLAN
- Architecture - Monolith / Modular Monolith (Clean Architecture)
- Backend - .NET 10 ASP.NET Core Web API
- Frontend - ReactJS / Angular / Vue.JS
- Database - PostgreSQL
- Communication - REST API
- Auth - ASP.NET Identity
- Deployment - Docker Compose
- CI/CD - Github Actions
- Hosting - Hetzner
- Monitoring - Grafana + Prometheus
- Logging - Serilog

## Define the App Scope
### What the app does?  
It will be personal Portfolio, to show my skills and programs I have built.

### Main Features  
- Showcase projects I have worked on
- Display my skills and the tools I use
- Include a blog section

### Phases
- Phase 1
  - Whole Backend is done
  - ~~I can add Technologies~~ 
  - ~~I can add Projects~~
  - ~~I can add BlogPosts~~
  - ~~I can manage WebsiteConfig~~
  - ~~Logging~~
  - Test
  - CI/CD
- Phase 2
  - Frontend is done
  - User can contact me via Email
  - User can subscribe to newsletter -> Why would someone do that ? :D
- Phase 3
  - Add Monitoring with Grafana/Prometheus
- Phase 4
  - Deployment

## Frontend
I will have couple of pages  
- /Home - Classic Home page - Designing will be a challenge
- /Projects - Separate page with all projects
- /Blog - Blog posts
- /Admin - Adminpanel (authentication required)
  - /Admin/Projects - Manage Projects
  - /Admin/BlogPosts - Manage BlogPosts
  - /Admin/Technologies - Manage Technologies
  - /Admin/Website - Manage WebsiteConfig

## Backend
### Classes
- ~~BlogPost~~
- ~~Project~~ 
- ~~Technology~~ 
- ~~Website~~
### Services
- ~~BlogService~~
- ~~ProjectService~~
- ~~TechnologyService~~ 
- ~~WebsiteService~~
### Repository
- ~~BlogRepository~~
- ~~ProjectRepository~~
- ~~TechnologyRepositorye~~ 
- ~~WebsiteRepository~~
### Controllers
- ~~BlogController~~
- ~~ProjectController~~
- ~~TechnologyController~~ 
- ~~WebsiteController~~
### Endpoints
~~Every controller will need CRUD except WebsiteController this one will be only Update, 
while it be created with default values while deploying~~

### Logging
Logging is important for monitoring and debugging.
I will be using Serilog for logging (structured logs) and in future Phase 3 
I will export logs to grafana/prometheus or seq.
But I don´t want to log everything, we will log only important events like:
- ~~GlobalExceptionsHandler~~
- ~~Incoming Requests~~
- ~~Program.cs~~ 
  - ~~Application start~~
  - ~~Configuration validation~~
- ~~Services~~
  - ~~Website Service ( updates )~~
  - ~~Blog Service ( updates )~~
- ~~Auth~~
  - ~~Login~~
  - ~~Logout~~
  - ~~Failed Login Attempts~~

### Security
- ASP.NET Identity for authentication and authorization.
- HTTPS Cookie Authentication for authentication and CSRF Token Validation for security.
- CORS configuration to allow frontend to communicate with backend.
- Rate limiting for security and to prevent brute force attacks.

## Database
~~Database will be done over OR-Mapper - EntityFrameworkCore~~

## API Contracts
Which endpoints need Auth and Antiforgery?
- All Post, Put, Delete

## TODO Phase 1
- ~~Error Handling - Global Exceptions~~ 
- ~~Git Strategy~~
- ~~PasswordValidation Logic~~
- ~~Update logic for adding technologies in the DB and updating them~~
- ~~AuthN/AuthZ over ASP.NET Identity~~
  - ~~Using build in PasswordHasher~~
  - ~~HttpsCookie Authentication~~
  - ~~CSFR - Cross-Site Request Forgery - Token Validation~~
- ~~Data seeding (Admin User)~~
- Tests
- CI pipeline
- ~~Config / Environment - Validation~~
- ~~Logging~~
- Migrationen

## TODO Phase 2
- Pick a Frontend Framework
- Plan the frontend
- Backend CORS configuration
- Code -> Enjoy -> Profit
- Authentication testing

## TODO Phase 3
- Update docker-compose file
- Set up Grafana 
- Setup Program.cs to export metrics, traces, logs
- Add rate Limiting
- Test -> Is everything good? -> Enjoy -> Profit

## TODO Phase 4
- Deploy to Hetzen -> burest.net
- Upgrade from CI to CI/CD with GitHub Actions via SSH to Hetzner