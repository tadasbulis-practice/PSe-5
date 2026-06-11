# LAB 4

Nassim Barad  
Variant 1 

## What was isolated and why?
The printing logic was isolated from the business logic using the
IStudentPrinter interface. This means GroupService never depends
on a concrete printer. It only knows the interface.


## How to Run
dotnet run