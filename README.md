# MHA.API
This is a RESTful API made with C# and .NET 5.0 that basically allows registered users to created their owns characters as if they were in the anime of My Hero Academia, this is part of a personal project that I've made to put into practice stuff like design patterns in the back end, calls to a NoSQL database and good practices when writing an API, the other part of the project is the front end, an app made with Angular that I'll uploading soon.

This project is based in the [My Hero Academia API](https://github.com/renant/myheroacademiaapi) made by [Renan Teixeira](https://github.com/renant), you can take a look at his [documentation](https://myheroacademiaapi.com/docs) as the structure of the data required and returned for this API is pretty similar to the one from his API.

This project has two main functionalities:
- Basic CRUD operations in MongoDB
- Basic authentication using Json Web Token

## Prerequisites
1. To take a look at the project you need [Visual Studio](https://visualstudio.microsoft.com), I used the 2022 community version, but it should work with 2019 version as well. (I haven't tried any older versions of the IDE).
2. For the call to the endpoints I highly recommend the use of [Postman](https://www.postman.com) since the API requires to be authenticated and there's no configuration for Swagger to send the Bearer token... Or you can just go to the [AccountController.cs](./MHA.API/Controllers/CharacterController.cs) and comment the:
```
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
```
3. I used a free MongoDB cluster, so if you have one just copy the connection string generated into the ```appsettings.json```, it also can be configured a local MongoDB... but that just go far from what I started with.

And that's all!. The packages I used should be installed once the repository is cloned.

## How to use
As I said above, the documentation of Renan's API helps to understand what is required and what is going to be returned by the API, seriously, the structure of the data is slightly different just for the adding of the fields:
- userId
- custom

And that's because those fields are needed for the front end to use both APIs.

However, since I implemented authentication, these are the necessary steps to use the API (the steps are listed to use Postman):
1. First of all the API needs to be running, with the connection to the database set in the ```appsettings.json```
2. Only authenticated users can use the API, so use the POST endpoint ```https://localhost:your_host/api/account/login``` with the following body to obtain the Bearer Token:
```
{
  "email": "funnyname@anymail.com",
  "password": "superSecretPassword"
}
```
> Passwords are encrypted by the data protection provided by .NET
3. If there's no user, it can be created, use the POST endpoint ```https://localhost:your_host/api/account/register``` with the following body:
```
{
  "email": "funnyname@anymail.com",
  "password": "superSecretPassword",
  "name": "Yor name",
  "alias": "funnyAlias"
}
```
> Just the email and password are required. For now, there's no email verification or something like that.
4. Once you get the Bearer token, it is required for the endpoints to do the basic CRUD operations in the database with the characters. The token goes in the postman's ```Authentication``` tab, selecting the type ```Bearer Token```. The token has an expiration of 1 hour.

### Creating your own characters - POST
The main purpose for making this API, the creation and management of custom characters. To create a custom character there's not a single field required, this API was thought to be consumed by an app so, at least a name and a quirk is going to come in the body of the request. However, the entire body can be the next one:
```
{
  "name": "Test Character",
  "alias": "Alias",
  "affiliation": "Villain",
  "birthday": "1991-05-15",
  "bloodtype": "O-",
  "description": "Can do Quirk really good",
  "eye": "Green",
  "fightstyle": "Long distance",
  "gender": "male",
  "hair": "White",
  "height": "186cm",
  "kanji": null,
  "occupation": "Part time deliver",
  "quirk": "Quirk",
  "romaji": null,
  "status": "Wanted dead or alive",
  "teams": "League of villains",
  "images": [
    "https://link_to _some_illustration",
    "https://link_to _some_illustration2"
  ],
  "epithet": "Unknown",
  "ages": [
    "24 years first appereance",
    "36 years when died"
  ],
  "family": [
    "id_of_related_character",
    "id_of_related_character2",
    "id_of_related_character3"
  ]
}
```
And that's all, those are the fields that the model has but as I said, none of them are required, you can send just a name and it will be stored in the database.

### Reading the characters in the database - GET
By calling the GET endpoint ```https://localhost:your_host/api/character``` you have a response similar to the original API:
```
{
  "info": {
      "currentPage": 1,
      "count": 47,
      "pages": 3
  },
  "result": [
    {
      "id": "61b29ab1be2938b5f26a2efe",
      "name": "Test Character2",
      "alias": "Alias2",
      "affiliation": "Villain",
      "birthday": "1991-05-15",
      "bloodtype": "O-",
      "description": "Can do NewQuirk really good",
      "eye": "Green",
      "fightstyle": "Long distance",
      "gender": "male",
      "hair": "White",
      "height": null,
      "kanji": null,
      "occupation": null,
      "quirk": "Quirk2",
      "romaji": null,
      "status": null,
      "teams": null,
      "images": [],
      "epithet": null,
      "ages": [],
      "family": [],
      "custom": true
    },
    ...
    {
    ...
    }
  ]
}
```
> All characters are saved as custom by default.

It always returns the first 20 characters, the count of how many characters the user have registered and the amount of pages existing, the currentPage logically is going to be #1 since is a get call with no parameters.

### Applying filters - POST
In order to view more characters if there's mora than 20 saved in the database, use the endpoint ```https://localhost:44347/api/character``` in a POST request, the body can have any of the next parameters:
- **page:** Send an integer to say what page are you looking for.
- **characterId:** The same one as "Id" on a character object, this one allows you to get a specific character from the database.
- **filters:** This is an object that allows you to filter the character list, the parameters can be:
  - name
  - alias
  - quirk
  - accupation
  - affiliation
