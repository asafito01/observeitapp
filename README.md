# ObserveIt App By Asaf Epelbaum

This app includes a client (Angular) and a server (ASP.NET Web API using VB.NET)

Important notes: 

There are several parts in the project which can be done differently:

1) For better understanding the SchedulerService could return a StatusResult object instead of nulls or other messages.
This could give more information to the SchedulerController about what happened and do the correct action (ex: appointment not created)

2) Bonus 1 was done

3) Bonus 2 didn't get there because lack of time, but i would have added a IsBest in each SchedulerRowViewModel that says if it is the best or not. Or maybe putting this value once in the response request along with the scheduler rows list

4) I used a shared object for the scheduler data. And I SyncLock to lock the write actions so there are no races between two users who want to add an appointment.
Maybe it can be done differently, using memory cache or other data provider so it can handle the thread issues