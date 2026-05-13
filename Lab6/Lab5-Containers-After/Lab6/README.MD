# OOP Lab 6

Name: Mohamed Faouzi
Group: PSe5

## Topic
Containers in Object-Oriented Architecture

## Implemented
- List<Student> container
- Repository supports collections
- Printer supports collections
- Strategy supports collections
- Validator supports collections

## Design decisions
The student container is private inside the repository.
Access to students is provided only through methods.

Interfaces and constructor injection are used across the architecture.

The architecture allows working with multiple students while keeping clean separation of responsibilities.