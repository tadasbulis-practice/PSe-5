# OOP Lab 8

Name: Mohamed Faouzi  
Group: PSe-5 

## Topic
Inheritance and Abstract Classes.

## Implemented
- GraduateStudent inherits from Student
- Student.Info() is virtual
- GraduateStudent overrides Info()
- StudentRepositoryBase abstract class created
- Shared repository logic moved to base class
- MemoryStudentRepository inherits from StudentRepositoryBase
- LoggedStudentRepository added as bonus

## Explanation
Lab 8 removes duplicated repository logic using inheritance.

StudentRepositoryBase contains shared fields and shared helper methods such as RegisterStudent and BuildGroupCode.

MemoryStudentRepository reuses this base logic and only overrides required repository methods.

GraduateStudent demonstrates inheritance, base constructor call, virtual method, and override.