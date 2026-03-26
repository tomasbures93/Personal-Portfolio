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
- I have Blog section
- I show my skills and Tools I can use

### Versions
- Version 1
  - Whole frontend is done
  - I can add Technologies in my list and it will be shown on frontend
  - I can add Project to my list and it will be shown on frontend
  - Blog section will be done and I can post blogs
  - User can contact me
  - User can subscribe to newsletter
- Version 2
  - Added Monitoring and proper logging

## Frontend
We will have couple of pages and that will be  
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
- WebsiteService
### Repository
- ~~BlogRepository~~
- ~~ProjectRepository~~
- ~~TechnologyRepositorye~~ 
- ~~WebsiteRepository~~
### Controllers
- BlogController
- ProjectController
- ~~TechnologyController~~ 
- WebsiteController
### Endpoints
Every controller will need CRUD except WebsiteController this one will be only Update, 
while it be created with default values while deploying

## Database
Database will be done over OR-Mapper - EntityFrameworkCore

## API Contracts
Which endpoints need Auth?

## TODO
- Updating logic for Technologies
- AuthN/AuthZ - PasswordHasher
- Config / Environment 
- Data seeding (Admin User)
- ~~Error Handling - Global Exceptions~~ 
- ~~Git Strategy~~
- Tests
- CI/CD pipeline