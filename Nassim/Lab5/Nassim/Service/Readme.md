# LAB 5 

Nassim Barad  
Erasmus  

Variant 1

# interface patern

The Strategy pattern allows selecting a printing algorithm at runtime
without modifying GroupService. Each printer is a strategy injected
via constructor.

## Runtime switching
Program.cs loops over all strategies and calls the same
GroupService.DisplayGroup() 

## Architecture
Program → IStudentPrinter → GroupService → IStudentPrinter.Print()

## How to Run
dotnet run