# Lab-8 Docker API Update

Updates the `SimpleStudentApi` from Lab-6 by adding a new `/api/faculty` endpoint.

## What changed from Lab-6

The original `Program.cs` only had `/api/students` CRUD.  
This version adds:

```
GET /api/faculty
```

Returns the full faculty hierarchy — Faculty → Groups → Students — derived dynamically by grouping students on `StudyProgram + EnrollmentYear`.

Also added:
- `Models/FacultyResponse.cs` — two response DTOs (`FacultyResponse`, `GroupResponse`)
- `/api/info` updated to version `1.1` listing all endpoints

## Why this endpoint exists

Lab-8's `ApiStudentRepository` calls `/api/faculty` to get the complete hierarchy in one request, then organises it internally using `Dictionary<int, Student>` and `Dictionary<string, Group>` for O(1) lookups.

## How to apply

Replace `SimpleStudentApi/Program.cs` in your lab-6 Docker project with this file.  
Add `Models/FacultyResponse.cs` to the same project.

The rest of the Docker project (Dockerfile, docker-compose, AppDbContext, Student model) is unchanged.
