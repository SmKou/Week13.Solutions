# Week 13 Solutions

By: Stella Marie

## Technologies Used

- C# 12
- ASP.NET Core 7
  - EntityFrameworkCore
  - Identity
  - MySQL
  - RestSharp
  - Newtonsoft.Json

## Description

### ApiCall

**ConsoleApiCall**

Sample app demonstrating how to make an api call from the console using RestSharp to make the call and Newtonson.Json to deserialize the response.

**MvcApiCall**

Sample app demonstrating how to incorporate an api call with RestSharp and Newtonsoft.Json. Instead of an EnvironmentVariables.cs, appsettings.json holds the API key and is passed implicitly from the Mvc builder object when constructing a controller. As in the usual format, after the data is called, in this case by a static method of the Article class, it is held by a model and passed into the view, where its properties can be displayed.

### CretaceousParkApi

Sample app demonstrating how to setup an api with controllers and models.

#### Api Documentation

GET     http://localhost:5042/api/animals/      Return all animals
GET     http://localhost:5042/api/animals/{id}  Return animal details
POST    http://localhost:5042/api/animals/      Create animal
PUT     http://localhost:5042/api/animals/{id}  Update animal by id
DELETE  http://localhost:5042/api/animals/{id}  Delete animal by id

| **Parameter**     | **Type**  | **Required**  | **Description**               |
| ----------------- | --------- | ------------- | ----------------------------- |
| species           | string    | not required  | Returns animals with matching species value |
| name              | string    | not required  | Returns animals with matching name value |
| minimumAge        | number    | not required  | Returns animals with age greater than or equal to specified value |

**Example**: Return all animals with a species of "Dinosaur"
GET http://localhost:5042/api/animals?species=dinosaur

**Example**: Return all animals with the name "Matilda"
GET http://localhost:5042/api/animals?name=Matilda

**Example**: Return all animals with an age of 10 or older
GET http://localhost:5042/api/animals?minimumAge=10

**Example**: Chain queries
GET http://localhost:5042/api/animals?species=dinosaur&minimumAge=10

**How to Format a POST Request**
```json
{
    "species": "Tyrannosaurus Rex",
    "name": "Elizabeth",
    "age": 8
}
```

**How to Format a PUT Request**
```json
{
    "animalId": 1,
    "species": "Tyrannosaurus Rex",
    "name": "Lizzy",
    "age": 9
}
```
**Example**: PUT request
PUT http://localhost:5042/api/animals/1

### MessageBoardApi

MessageBoardApi is an api for managing messages and groups. A user can GET all messages from a specified group, POST messages to a specified group, GET a list of all groups, input date parameters to retrieve only messages in a given timeframe, and PUT and DELETE messages as long as they were the ones to write it.

Messages
- int MessageId
- int GroupId < Groups
- int UserId < Users
- string Message
- DateTime SentAt

Groups
- int GroupId
- string Name

Users
- int UserId
- string Name
- string UserName

[X] GET     .../api/groups/                 Return all groups
[X] GET     .../api/groups/{id}             Return group details
[X] POST    .../api/groups/                 Create group
[X] PUT     .../api/groups/{id}             Update group by id
[X] DELETE  .../api/groups/{id}             Delete group by id

[X] GET     .../api/messages/               Return all messages
[X] GET     .../api/messages/{id}           Return message
[X] GET     .../api/messages/group/{id}     Return messages of group
[X] GET     .../api/messages/user/{id}      Return messages of user
[X] POST    .../api/messages/               Create message
[X] PUT     .../api/messages/{id}           Update message by id
[X] DELETE  .../api/messages/{id}           Delete message by id

[X] GET     .../api/users/                  Return all users
[X] GET     .../api/users/{id}              Return user details
[X] POST    .../api/users/                  Create user
[X] PUT     .../api/users/{id}              Update user by id
[X] DELETE  .../api/users/{id}              Delete user by id

**Queries for:** .../api/messages?

Parameter: (date: yyyy-mm-dd) fromDate
Not Required - Returns messages sent after the specified date

Parameter: (date: yyyy-mm-dd) toDate
Not Required - Returns messages sent before the specified date

## Complete Setup

This app requires use of a database. It is suggested to use migrations for ensuring the smooth setup of Identity.

### Database Schemas

To setup the database, follow the directions in [Connecting the Database](#connecting-the-database) and then implement migrations as detailed in [Migrations](#migrations).

### Connecting the Database

In your IDE:
- Create a file in the root of the assembly: appsettings.json
  - Do not remove the mention of this file from .gitignore
- Add this code:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=[hostname];Port=[port_number];database=[database_name];uid=[username];pwd=[password]"
    }
}
```

Replace the values accordingly and brackets are not to be included.

### Migrations

- In the terminal, run ```dotnet build --project AssemblyName```
  - Or navigate into the assembly subdirectory of the project and run ```dotnet build```

This command will install the necessary dependencies. For it to work though, appsettings.json should already be setup. As migrations are included in the clone or fork, you should only need to run:

```dotnet ef database update```

However, since the models are already set up, if update does not work, then do:

```bash
dotnet ef migrations add Initial
dotnet ef database update
```

### Run the App

Once you have a database setup and the connection string included in the appsettings.json, you can run the app:

- Navigate to main page of repo
- Either fork or clone project to local directory
- Bash or Terminal: ```dotnet run --project AssemblyName```
  - If you navigate into the main assembly, use ```dotnet run```

If the app does not launch in the browser:
- Run the app
- Open a browser
- Enter the url: https://localhost:5001/

**CretaceousParkApi and CretaceousClient**

To run these two apps, you'll first need to launch the api in a terminal. It will render at http://localhost:5042/.

Then open another terminal and run the client, which will render at http://localhost:5002/.

## Known Bugs

Please report any issues in using the app.

## License

[MIT](https://choosealicense.com/licenses/mit/)