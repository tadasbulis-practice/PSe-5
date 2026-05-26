LAB-6 – Containers in Object-Oriented Architecture

OOP Laboratory Work Collection

Student: Djamel Hammar
Group: Pse-05
Course: Object-Oriented Programming (C#)

⸻


Goal

The goal of this laboratory work is to extend the existing architecture to support collections and containers using List<Student>.

Description

Instead of managing a single object, the application now manages multiple students through repositories and services. Collections are encapsulated inside repositories to preserve data integrity.

Dependencies:

* IStudentRepository
* IStudentPrinter
* IStudentValidator
* IAverageStrategy


Design Decisions

* repositories encapsulate collections,
* services contain business logic,
* interfaces isolate implementations,
* constructor injection is used throughout the system.