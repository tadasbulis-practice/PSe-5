# Control Work 1

**Author:** Edgar Clerc

**Date** 28/05/2026

## Result

Task 1 finished, rework of project to multiple files

Task 2 finished Used an interface to separate report with and without linq

Task 3 finished: Added argument in program.cs to disable linq or use stub Repository instead of default one
Added Print in the console to know which report or repository is used

Drills : finished : available in file LINQ_Drills.cs

## Instalation / Launch

To launch the project use :
```dotnet run```

Some Argument can be use to change some properties of the project.


### Arguments available : 

```--nolinq```
To disable the use of Linq in the report Service

```--stub```
To start the project with a stub repository instead of default one

Exemple : 

```dotnet run --nolinq --stub```

Will launch project with a stub repository and disable Linq in the report service