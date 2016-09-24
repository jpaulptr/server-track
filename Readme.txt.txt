Server track is an .Net WebApi 2 project called ServerTrack. 

End Points:
It has two end points

Get
/api/server/{server name}

where {server name} is a string indicating the server you would like the stats for.

Post
/api/server

application/json

{

	"CPULoad" : 12.2, 
	"RAMLoad" : 35.6, 
	"ServerName" : "name of server"

}


Set Up:

The repo has everything that is needed to run it. A build in visula studio is not necessary. 
(Typically I wouldn't install binaries, but to simplify the demo they are there).

To run it you need IIS. 
Clone the repo and set the IIS physical path to server-track\ServerTrack\ServerTrack.
You can leave IIS in default settings for the rest of the settings.


Design:

There are three main components to the application:
1. Api controller. The controller does the request routing, and insertion and retrival of the requested data. 
   Dependency injection is used so that the data store is injected at the construction of the controller. This ensures that
   the data store could be a singleton so that all the server stats end up in the same store. It would also facilitate unit testing.

2. MemoryManager. The memory manager is the data store. It is created at application start time by Ninject, the DI framework. 
   While the class is not a singleton Ninject creates it only once (SingletonScope). 

   Because the api calls could be concurrent, the manager must ensure that the List it stores the data in is thread safe. Any call to the list
   is wrapped in a lock to ensure thread safety. The GetList method returns a copy of the List. If it returned the list itself you would be accessing
   a list that might change during use.

3. StatAgregationGenerator. This class takes a list of raw server stats and returns a collection of averaged stats for the last hour and the last day.
   It has two constructors so that unit tests can be run with reliable dates that the tester can set. 


Project Components:
There are 3 components to the project:

1. SeverTrack. This is the api project
2. LoadTester. A small console app for posting multiple concurrent server stats to the api. It is used to simulate load conditions.
3. ServerTrack.Tests. These are the unit tests for SeverTrack.


Future Inhancements:

1. Add more unit tests. The controllers and memory manager are untested due to time constraints.
2. Remove the data store and replace it with a distributed store. The current implementation is problematic because the manager loses its data during server restart, 
   and limits the data collection to one server.
3. Secure the api. Right now anyone can send results. The api should only accept post and gets from authorized entities. It should also run under TLS.
4. Add error checking. The api relies on .Net's model binding to insure the data is useable, but it should be checking for invalid values too, 
   such as negative CPU, empty server names, etc.







