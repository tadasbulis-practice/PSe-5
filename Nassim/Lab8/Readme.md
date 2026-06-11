# LAB 7 
Nassim Barad  
Erasmus  

## Architecture
Program (Composition Root)
   IMenuService (ConsoleMenuService)
       StudentService
           IStudentRepository  (MemoryStudentRepository | ApiStudentRepository)
           IStudentPrinter     (ConsoleStudentPrinter)
           IAverageStrategy    (SimpleAverageStrategy)
           IStudentValidator   (StudentValidatorAdapter)

## Patterns used

| Repository | IStudentRepository       | Abstracts data access             |
| Strategy   | IAverageStrategy         | Swappable calculation             |
| Adapter    | StudentValidatorAdapter  | Wraps LegacyStudentValidation     |
| Strategy   | IStudentPrinter          | Swappable display                 |

## Key concept
ApiStudentRepository hides: HttpClient, Dictionary<int,Student>,
Dictionary<string,Group>, DTOs, JSON mapping.


## How to Run (Memory mode)
dotnet run

## How to Run (API mode)
docker compose up
# then set useApi = true in Program.cs
dotnet run