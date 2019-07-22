# ObserveIt App By Asaf Epelbaum

This app includes a client (Angular) and a server (ASP.NET Web API using VB.NET)

* The server app is using localhost:4444
* The client app is using localhost:4200

The app is separated into layers so each layer has its own responsibility.
The controller works with a ISchedulerService which provides it with the needed scheduler data.
I used dependency injection so each class can be injected the implementation instead of stronly typing the responsibilities inside the classes

#### Important notes: 

There are several parts in the project which can be done differently:
1) The error handling and display was a bit lacking in this test. I would have added a much more detailed errors and messages, while each function returns specific data of what was wrong so the consumer can know what happened and action accordingly

For example:

The SchedulerService could return a StatusResult object instead of nulls or other messages.
This could give more information to the SchedulerController about what happened and do the correct action (ex: appointment not created)

Also, the client app shoul have received the error messages and show a propper error message but didn't work as expected and was left off to do something else

2) Bonus 1 was done

3) Bonus 2 didn't get there because lack of time, but i would have added a IsBest in each SchedulerRowViewModel that says if it is the best or not. Or maybe putting this value once in the response request along with the scheduler rows list

