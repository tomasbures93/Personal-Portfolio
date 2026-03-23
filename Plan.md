# PLAN
- Architecture - Monolith / Modular Monolith
- Backend - .NET 10 ASP.NET Core Web API
- Frontend - ReactJS
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
I can show project I worked on
I have Blog section
I show my skills and Tools I can use
### Versions
- Version 1
  - Whole frontend is done
  - I can add Technologies in my list and it will be shown on frontend
  - I can add Project to my list and it will be shown on frontend
  - User can contact me
- Version 2
  - Blog section will be done and I can post blogs
  - User can subscribe to newsletter
- Version 3
  - Added Monitoring and proper logging

## Frontend
We will have couple of pages and that will be  
- /Home - Whole page scrollable
- /Projects - Extra page with all projects
- /Blog - Blog posts
- /Admin - Adminpanel (Auth needed)

## Backend
### Classes
- Blogpost 
- Project  
- Technology 
- Website - Class which will have website info saved
### Services
- BlogService
- ProjectService
- TechnologyService
- WebsiteService
### Controllers
- BlogController
- ProjectController
- TechnologyController
- WebsiteController
### Endpoints
Every controller will need CRUD except WebsiteController this one will be only Update, 
while it be created with default values while deploying

## Database
Database will be done over OR-Mapper 

## API Contracts
TODO ( Endpoints, RequestsBody, ResponseBody ) Which endpoints need Auth

## Optional
- Config Planning / Environment 
- Data seeding - not needed for now 
- Error Handling - Global Exceptions ( IExceptionHandler )
- Git Strategy
- Tests - Some Unittests