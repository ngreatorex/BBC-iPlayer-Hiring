BBC-iPlayer-Hiring
==================

This repository contains my submission for the BBC iPlayer Hiring coding exercise.

Building
--------
The code has been implemented in C# using Visual Studio 2012. You will need to have NuGet installed as well.

The solution uses NuGet package restore, so you should be able to simply open the solution in Visual Studio 2012 and
press Build. NuGet will then download the required packages, and compile everything.

Testing
-------
The solution contains NUnit tests in the BacklogTracker.Tests project. I have included the VS wrapper for NUnit,
so you should simply be able to run the unit tests from the TEST menu inside Visual Studio.

Alternatively, you can use the NUnit GUI, which should be downloaded to packages\NUnit.Runners.2.6.3\tools within the
repo once you have built the project.

Running
-------
I have included a simple console program mock up to allow you to manually test the system. 
This is the executable that will run if you select the Run option in Visual Studio. It is pretty simple, and, I hope,
self explanatory!

Notes
-----
 * I have added an interface for the Story and Backlog classes to allow mocking and dependency injection.
 * The Backlog class has been designed to be thread-safe, so that concurrent access would be possible from a web server.
