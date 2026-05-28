# Lab 3 - Service Abstraction Pattern

## Overview
This lab demonstrates the **Service Abstraction Pattern** through multiple implementations of the `IMenuService` interface. The pattern isolates user interaction concerns from business logic, enabling different user interfaces while maintaining the same core functionality.

## Service Abstraction Pattern

### What is Service Abstraction?
Service Abstraction is a design pattern that separates the user interface layer from the business logic layer through interfaces. This allows different user interaction implementations (console, web, debug, etc.) to work with the same business logic.

### How it Isolates User Interaction from Business Logic

#### 1. **Interface Segregation**
- `IMenuService` defines the contract for user interaction
- Business logic classes (`Student`, `StudentService`, etc.) have no knowledge of how users interact with the system
- User interface concerns are completely separated from data processing concerns

#### 2. **Multiple Implementations**
The same business logic works with different user interfaces:

- **ConsoleMenuService**: Traditional console-based interaction
- **DebugMenuService**: Debug-focused interface with detailed logging
- **WebMenuSimulationService**: Web-like interface simulation in console
- **AlternativeMenuService**: Enhanced console interface with statistics

#### 3. **Dependency Injection**
All menu services receive the same business logic dependencies:
- `IStudentFinder` - for searching students
- `IStudentPrinter` - for displaying students
- `IStudentValidator` - for input validation
- `IAverageStrategy` - for grade calculations

#### 4. **Runtime Flexibility**
The application can switch between different user interfaces at runtime without changing business logic:

```bash
# Production console interface
dotnet run

# Debug interface with logging
dotnet run -- debug

# Web simulation interface
dotnet run -- web

# Alternative enhanced interface
dotnet run -- alternative
```

## Benefits of Service Abstraction

### 1. **Separation of Concerns**
- User interaction logic is separate from business rules
- Changes to UI don't affect business logic
- Business logic can be tested independently of UI

### 2. **Testability**
- Business logic can be tested with mock UI implementations
- UI implementations can be tested with fake business logic
- Each layer can be unit tested in isolation

### 3. **Flexibility**
- Easy to add new user interfaces (GUI, REST API, etc.)
- Can support multiple platforms with same business logic
- Runtime switching between different UI modes

### 4. **Maintainability**
- UI changes don't require business logic modifications
- Business logic changes don't break existing UIs
- Clear boundaries between different system layers

## Pattern Implementation Details

### Interface Design
```csharp
public interface IMenuService
{
    void ShowMainMenu();
    int GetMenuChoice();
    void ExecuteChoice(int choice);
}
```

### Business Logic Isolation
The menu services only handle:
- User input/output
- Menu navigation
- Input validation display

Business logic handles:
- Student data management
- Search algorithms
- Grade calculations
- Data validation rules

### Example: Adding a Student
1. **UI Layer** (`IMenuService`): Prompts for name, age, grades
2. **Business Logic Layer**: Validates data, creates student, calculates averages
3. **UI Layer**: Displays success/failure messages

This separation ensures that changing the UI (e.g., from console to web) doesn't affect how students are created or validated.

## Running Different UI Implementations

```bash
# Standard console interface
dotnet run

# Debug interface (shows detailed logging)
dotnet run -- debug

# Web simulation (formatted like a web app)
dotnet run -- web

# Alternative interface (with statistics)
dotnet run -- alternative

# Test mode (with fake implementations)
dotnet run -- test
```

Each mode demonstrates how the same business logic can work with completely different user interaction patterns, proving the effectiveness of the Service Abstraction pattern.
	- clean solution and/or project and run it again to make sure it will run and work for your team members also.
	- make sure that your feature branch and development branch doesn't has conflicts: 
		We switch to develop, Pull it to your local repository, and merge deveop into your feature branch to be sure there is no code conflicts between develop and your feature branch.
Then we make feature final commit and push changes to remote feature branch.		
5. Making Pull Request (PR):
	- make sure you have Pushed your feature branch with your code changes to origin (cloud).
	- Go with browser to your GitHub repository.
	- Select your feature branch or go to Pull Requests
	- create New Pull Request 
		- select destination branch (always develop in our case)
		- select our feature branch which we gonna make PR for.
		- optional: we can check and compare code changes which are between develop and our feature.
		- Press Create Pull.
	- IMPORTNAT: Here we need to select peaople who gonna do Code reviews - Assignees: and add your whole team, but in our case it's enought to add me (Tadas Bulis)
	- Press Create Pull Request again.
6.Code Review:
	- to be added later by all team.

# PSe-5
PSe-5 workshops

Rules how we gonna work with Labs, how to deliver them to our Group repository
1. Everybody creates folder FirstameLastname
2. In our own forlder for each Lab we create folder like Lab-1, Lab-2, Lab-3.....
	For clasic GitFlow for each Lab (like a task) we create feature branch  feature/Lab3.
3. When I finish some code changes or I finish work that day, that lecture or ect. I'm making commit. Than not to loose code changes, I push  changes to remote (if branch was not pushed yet, client command is Publish.).
4. When all code code changes of the Lab is done, you commit them, push to remote.
	- feature works
	- tests passed (you have tested your feature)
	- code is cleaned.
	- checked that all classes and files in propper folders (Model classes in Models and ect. - depends on your products structure).
	- clean solution and/or project and run it again to make sure it will run and work for your team members also.
	- make sure that your feature branch and development branch doesn't has conflicts: 
		We switch to develop, Pull it to your local repository, and merge deveop into your feature branch to be sure there is no code conflicts between develop and your feature branch.
Then we make feature final commit and push changes to remote feature branch.		
5. Making Pull Request (PR):
	- make sure you have Pushed your feature branch with your code changes to origin (cloud).
	- Go with browser to your GitHub repository.
	- Select your feature branch or go to Pull Requests
	- create New Pull Request 
		- select destination branch (always develop in our case)
		- select our feature branch which we gonna make PR for.
		- optional: we can check and compare code changes which are between develop and our feature.
		- Press Create Pull.
	- IMPORTNAT: Here we need to select peaople who gonna do Code reviews - Assignees: and add your whole team, but in our case it's enought to add me (Tadas Bulis)
	- Press Create Pull Request again.
6.Code Review:
	- to be added later by all team.
