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
It will be personal Portfolio, to show my skills and program something

### Main Features  
- I can show projects I worked on
- I show my skills and Tools I can use
- I have Blog section

### Versions
- Phase 1
  - Whole Backend is done
  - I can add Technologies to my list
  - I can add Project to my list
  - I can add BlogPosts
  - I can manage WebsiteConfig
  - Logging
- Phase 2
  - Frontend is done
  - User can contact me via Email
  - User can subscribe to newsletter -> Why would someone do that ? :D
- Phase 3
  - Added Monitoring with Grafana/Prometheus
- Phase 4
  - Deployment

## Frontend
We will have couple of pages  
- /Home - Whole page scrollable
- /Projects - Extra page with all projects
- /Blog - Blog posts
- /Admin - Adminpanel (Auth needed)

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

## Database
~~Database will be done over OR-Mapper - EntityFrameworkCore~~

## API Contracts
Which endpoints need Auth?
- All Post, Put, Delete

## TODO Phase 1
- ~~Error Handling - Global Exceptions~~ 
- ~~Git Strategy~~
- ~~PasswordValidation Logic~~
- ~~Update logic for adding technologies in the DB and updating them~~
- AuthN/AuthZ + PasswordHasher
- Data seeding (Admin User)
- Tests
- CI pipeline
- Config / Environment 
- Logging

## TODO Phase 2
- Pick a Frontend Framework
- Plan Frontend
- Code -> Enjoy -> Profit

## TODO Phase 3
- Update docker-compose file
- Setup Grafana 
- Setup Program.cs to export Metrics/Traces/Logs
- Test -> All good? -> You won!

## TODO Phase 4
- from CI to CI/CD with Github actions
- Deployment to Hetzen -> burest.net