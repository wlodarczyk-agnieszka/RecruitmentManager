# Recruitment Manager
Application that helps people manage their applying for a new job process.
The process may be long and hard to manage. This application was created to change this. 

### Status:
In development

### Features:
- Register and log in (login and password)
- Register / login via Google or LinkedIn (temporarily disabled)
- Add a new job offer (on wchich you applied) - job title, description, company etc.
- Edit a job offer
- Add offer to archives
- Filter offers by status
- Search option
- Change offer statuses (no response, interview etc)
- History of status changes
- Add (and remove) notes (for example - with recruter phone number or message you recieve)
- Annotations (write your thoughts about companies you applied... or anything else you think is important)
- Load data from .xlsx file 
- Statistics

### Technologies:
- .Net Core 2.0 MVC
- Entity Framework (with SQL Server)
- Bootstrap 3
- JavaScript

### Installation:
#### Prepare database
- In your SQL Server DB, create 'RecruitmentManager' database (SQL -> create database RecruitmentManager)

#### Configure connection string
- Paste your connection string into appsettings.xml (`<Root><ConnectionString>Your_Conn_String_Here</ConnectionString></Root>`)

#### Run migration
- Run new_app migration

#### Initial data
- Run SQL_Statuses_init.sql script to fill DB with initial data

#### Additional settings
- Fill appsettings.xml with your ClientId's and ClientSecret's data, to allow login via Google or LinkedIn
- The file should look like:
```
<Root>
  <ConnectionString>CONN_STRING</ConnectionString>
  <Authentication>
    <Google>
      <ClientId>CLIENT_ID</ClientId>
      <ClientSecret>CLIENT_SECRET</ClientSecret>
    </Google>
    <LinkedIn>
      <ClientId>CLIENT_ID</ClientId>
      <ClientSecret>CLIENT_SECRET</ClientSecret>
    </LinkedIn>
  </Authentication>
</Root>
```
