# Lab 3 - Dependency Injection and Testing Isolation

## Overview
This lab demonstrates proper dependency injection patterns, interface-driven design, and testability through fake/stub implementations.

## Architecture Changes

### What Was Isolated and Why

#### 1. **Dependency Creation Isolated to ServiceFactory**
- **What**: All `new` instantiations moved from `Program.cs` to `ServiceFactory.cs`
- **Why**: Program.cs should only orchestrate, not create dependencies. This enables different configurations (production, test, alternative) without changing main application logic.

#### 2. **Fake/Stub Implementations Created**
- **FakeStudentFinder**: Always returns a predefined student for predictable testing
- **StubStudentPrinter**: Captures output instead of printing to console, enabling test verification
- **FakeStudentValidator**: Configurable to always pass/fail validation for testing edge cases
- **StubAverageStrategy**: Returns fixed averages for deterministic test results
- **Why**: Enables unit testing without external dependencies (console I/O, unpredictable data)

#### 3. **Logical Branches Demonstrated**
- **Production Mode** (default): Uses real implementations for normal operation
- **Test Mode** (`--test`): Uses fake/stub implementations for testing
- **Alternative Mode** (`--alternative`): Uses `PartialNameFinder` instead of `ExactNameFinder`
- **Why**: Shows how dependency injection enables different behaviors without code changes

#### 4. **Constructor Injection Maintained**
- All dependencies passed via constructors, no `new` keywords in business logic classes
- **Why**: Enables testability, flexibility, and adherence to SOLID principles

## Running the Application

```bash
# Production mode (default)
dotnet run

# Test mode with fake implementations
dotnet run -- test

# Alternative mode with partial name matching
dotnet run -- alternative
```

## Benefits Achieved

1. **Testability**: Fake/stub implementations allow isolated unit testing
2. **Flexibility**: Different configurations without code changes
3. **Maintainability**: Clear separation of concerns
4. **SOLID Principles**: Dependency Inversion through interfaces and injection
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
