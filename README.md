BBC-iPlayer-Hiring
==================

This repository contains my submission for the BBC iPlayer Hiring coding exercise.

Building
--------
The code has been implemented in C# using Visual Studio 2012. You will need to have NuGet installed as well. See http://docs.nuget.org/docs/start-here/installing-nuget for instructions on how to do that.

The solution uses NuGet package restore, so you should be able to simply open the solution in Visual Studio 2012 and
choose Build Solution from the Build menu. NuGet will then download the required packages, and compile everything.

Testing
-------
The solution contains NUnit tests in the BacklogTracker.Tests project. I have included the Visual Studio wrapper for NUnit, so you should simply be able to run the unit tests from the Test menu inside Visual Studio. Once you have run the tests, the results can be viewed in the Test Explorer (Test --> Windows --> Test Explorer).

Alternatively, you can use the NUnit GUI, which should be downloaded to packages\NUnit.Runners.2.6.3\tools within the
repo once you have built the project.

Running
-------
I have included a simple console program mock up to allow you to manually test the system. 
This is the executable that will run if you select the Start Debugging or Start Without Debugging options from the Debug menu in Visual Studio. It is pretty simple, and, I hope,
self explanatory!

Notes
-----
 * The solution has been separated into 3 projects. The idea is that the BacklogTracker project could be shared between a web project and a GUI or console project such as the ConsoleRunner project I have included. The 3rd project is BacklogTracker.Tests, which contain all the unit tests, and would not be included in a production build.
 * This implementation stores the stories in memory. However, a Repository pattern and IRepository interface were used in order to allow an easy change to an alternative implementation that used a relational database.
 * I have added an interface for the Story and Backlog classes to allow mocking and dependency injection.
 * The Backlog class has been designed to be thread-safe, so that concurrent access would be possible from a web server.
 * There is class called KnapsackProblemSolverSprintGenerator that has been excluded from the final solution. I was attempting to use the pseudo-code from http://en.wikipedia.org/wiki/Knapsack_problem#0.2F1_Knapsack_Problem to hopefully find a more efficient algorithm for generating the sprints. However, this class is slower, does not work correctly in all situations, and fails unit tests, so it has been excluded from the build.
