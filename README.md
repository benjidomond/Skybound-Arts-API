## Skybound Arts API

This repository contains the Skybound Arts API, which Skybound Arts utilizes to store and access crucial website information such as characters, events, players, and tags!

# Built With
* ASP .Net Core
* Entity Framework
* Microsoft SQL Server

# Local Installation

After cloning a local version of this repository, you may create a replica of the database by using the SQL queries found in DBQuery.sql (main directory file). After this has been done and a connection string to the database has been supplied in the appsettings.json, run
`dotnet build`, followed by `dotnet run`

# API Command List

*Currently, this app doesn't include PUT / UPDATE requests. This functionality intends to be added in later on, as only verified users will be able to update video information.* 

Characters
*The methods in this class are used to retrieve, modify, and add characters to the database.*

* **GET** api/characters/ - Gets all characters
* **GET** api/characters/{id} - Retrieve individual character
* **POST** api/characters/ - Adds all characters to the database when action is accessed with a POST request.
* **DELETE** api/characters/{id} - Deletes a character from the database when action is accessed with a DELETE request.

Events
*The methods in this class are used to retrieve, delete, and add events to the database.*

* **GET** api/events/ - Retrieves all the events
* **GET** api/events/{id} - Retrieve individual event
* **POST** api/events/ - Adds an event to the database when action is accessed with a POST request.
* **DELETE** api/events/{id} - Deletes a character from the database when action is accessed with a DELETE request.

MapVideoTag
*The methods in this class are used on the MapVideoTag table, which is used to pair tags to video IDs.*

* **GET** api/mapvideotag/ - Retrieves all the mapped tag relations
* **GET** api/mapvideotag/{id} - Get single video / tag association
* **POST** api/mapvideotag/ - Adds a new video / tag association to the database when action is accessed with a POST request.
* **GET** api/mapvideotag/search/{searchTagID} - Allows you to search for videoIDs by passing in the ID of a tag
* **GET** api/mapvideotag/findVideo/{searchTagID}/{searchVideoIDs} - By passing in an array of tags and videos, you're able to check and see if all the videos passed include a tag within the database.

Players
*The methods in this class are used to retrieve, delete, and add players to the database.*

* **GET** api/players/ - Retrieves all the players
* **GET** api/players/{id} - Retrieving individual player
* **POST** api/players - Adds a player to the database when action is accessed with a POST request.
* **DELETE** api/players/{id} - Removes a player from the database when action is accessed with a DELETE request.

Videos
*The methods in this class are used to retrieve, delete, and add videos to the database.*

* **GET** api/videos/ - Retrieving all the videos
* **GET** api/videos/find/{videoIDs} - Gets a collection of all the video IDs passed. Used after players conduct a search and are given a set of IDs.
* **GET** api/videos/{id} - Retrieving a single video
* **POST** api/videos/ - Adds a video to the database when action is accessed with a POST request.
* **DELETE** api/videos/{id} - Removes a video from the database when action is accessed with a DELETE request.

VideoTags
*The methods in this class are used to add, retrieve, and delete video / tag relations*

* **GET** api/videotags/ - Retrieves all the video / tag pairs
* **GET** api/videotags/{id} - Retrieves a single video / tag pair
* **POST** api/videotags/ - Creates a new video / tag relation when action is accessed with a POST request.
* **DELETE** api/videotags/{id} - Removes a video / tag relation when action is accessed with a DELETE request.