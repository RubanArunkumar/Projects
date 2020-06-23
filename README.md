# Exercise
 
# Introduction
In this exercise you will create a basic .NET solution from scratch. Create this exercise using C# and Visual Studio (2015 or higher). This document describes the two major components, how you structure your solution is up to you.
The worker
This program retrieves the content of a JSON feed that exposes airports. The individual airports should be stored in a SQL database (local or in Azure). The only airports that should be stored are the European ones. 
*	Create a worker solution (i.e. WebJob, Azure Functions, console application)
*	Download the content using the HttpClient class from the Microsoft.Net.Http NuGet package to download the JSON.
*	The program should get all the airports from this JSON feed: https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json
*	Use Entity Framework to save the data.
*	Before saving the data, the database should be emptied.
The web application
This program shows a list of all the airports and offers the functionality to add a new airport to the database. Retrieving the list of the airports should only happen once every 5 minutes as long as no new airport has been added. A response header should be used to indicate whether the application got its data from the database.
*	The web application should be an ASP.NET Core MVC application.
*	The name of the response header should be ‘from-database’.
For extra points:
Create a view that shows all the airports in a given country. The country code is in the ‘Iso’ column.
*	The application should accept the country in the URL. 
Unit Test project
*	Create (integration) unit test which tests the retrieval of airports
*	The test should verify/assert that the expected number of airports is returned
What we will look for
*	Using common frameworks
*	Cleverness & performance
*	Separation of concerns
*	The solution should build and run on other computers
