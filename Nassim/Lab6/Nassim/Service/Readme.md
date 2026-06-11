# LAB 6 
Nassim Barad  
Erasmus  


## Containers use
List<Student>  private inside MemoryStudentRepository.


## Patterns extended
| Pattern    | Interface            | Implementations                              |
| Repository | IStudentRepository   | MemoryStudentRepository                      |
| Strategy   | IAverageStrategy     | Simple, Weighted, Median                     |
| Strategy   | IStudentPrinter      | Console, Short, Json, File                   |
| Validator  | IStudentValidator    | StrictValidator, LenientValidator            |

## Full flow
1. Add students via StudentService
2. Repository stores them in private List<Student>
3. Validator filters valid students
4. Strategy calculates group average
5. Printer displays results

## Design decisions
- Container is private encapsulation respected
- No logic in Program.cs 
- Constructor injection everywhere
- Runtime switching demonstrated with 2 different StudentService configs

## How to Run
dotnet run