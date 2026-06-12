# OOP Lab 7

Name: Mohamed Faouzi  
Group: PSe5

## Topic
Generics, Dictionary, LINQ, and IRepository<T>.

## Implemented
- IEntity interface
- Generic IRepository<T>
- Generic MemoryRepository<T>
- Dictionary<int, T> for O(1) lookup
- IReadOnlyList<T> for safe read-only access
- LINQ used in repository and strategy logic

## Explanation
Lab 7 improves the previous architecture by adding a generic repository.

MemoryRepository<T> can store any model that implements IEntity.

This avoids duplicated repository code and follows:
- Encapsulation
- DRY principle
- Open/Closed Principle
- Dependency Inversion Principle