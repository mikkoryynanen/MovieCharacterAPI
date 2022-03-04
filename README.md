# MovieCharacterAPI

![GitHub repo size](https://img.shields.io/github/repo-size/mikkoryynanen/MovieCharacterAPI)

## Table of Contents

- [General Information](#general-information)
- [Technologies](#technologies)
- [Installation and Usage](#installation-and-usage)
- [Contributors](#contributors)

## General Information

MovieCharacterAPI is an Entity Framework Core Code First ASP.NET Core Web API written in C#.

Project has full CRUD functionality for Franchises, Movies and Characters.

- Get all Franchises, Movies and Characters from database. 
    - Franchise contains: ``Name, Description and a list of movie ids``. 
    - Movie contains: ``MovieTitle, Genre, ReleaseYear, Director, MoviePicture(url), Trailer(url)``. 
    - Character contains: ``FullName, Alias, Gender, CharacterPicture(url), Collection of movies``

- Get single Franchise, Movie or Characer by supplying it's id to the request path

- Create new Franchise, Movie or Character. Same fields that are received with GET are supplied in the request body when adding Franchise, Movie or Character

- Updating Franchise, Movie or Character. Supply the following JSON in the request bodies:

```
// For Franchise
{
  "name": "string",
  "description": "string"
}

// For Movie
{
  "movieTitle": "string",
  "genre": "string",
  "releaseYear": "string",
  "director": "string",
  "moviePicture": "string",
  "trailer": "string",
  "franchiseId": 0
}

// For Character
{
  "fullName": "string",
  "alias": "string",
  "gender": "string",
  "characterPicture": "string"
}
```

- Deleting Franchise, Movie or Character is simply done by supplying it's id to the DELETE request

In Addition to full CRUD functionality, additional features include:

- Updating Characters in a Movie. Takes an integer array of Character id's in the request body, and a Movie id in the request path

- Updating Movies in a Franchise. Takes an integer array of Movie id's in the request body, and a Franchise id in the request path


Data Transfer Objects (DTOs) are used to decouple from domain models. This is done to ensure that sensitive data is not exposed to the client. AutoMapper is used to map between domain models and DTOs.

## Technologies

The project is implemented using the following technologies:

- C#
- Entity Framework Core
- AutoMapper
- ASP.NET Core
- SQL Server
- Swagger

## Installation and Usage

__NOTE:__ You will need *SQL Server* to be connected with *SQL Server Management Studio*/*Azure Data Studio*.

1. Clone the project repository

```sh
git clone https://github.com/mikkoryynanen/MovieCharacterAPI.git
```

2. Open project in *Visual Studio*

3. Click *Start* button from top bar

4. Swagger page can be accessed at: http://localhost:5000/swagger/index.html

## Contributors

[Mikko Ryynänen (@mikkoryynanen)](https://github.com/mikkoryynanen)

[Arttu Hartikainen (@arttuhar)](https://github.com/arttuhar)
