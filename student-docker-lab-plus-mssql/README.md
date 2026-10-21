# Student Docker Lab (.NET 8 + MSSQL)

This repository contains three Docker services:

- **SimpleStudentApi** — .NET 8 Minimal API with full CRUD, backed by SQL Server
- **SimpleStudentFrontend** — .NET 8 Razor Pages app that calls the API and renders HTML
- **mssqlserver** — SQL Server 2022 Express, managed separately so it survives app rebuilds

---

## Architecture

```
Browser
   |
   v
localhost:6011
   |
   v
Frontend container (SimpleStudentFrontend)
   |
   v
http://simplestudentapi:8080/api/students  (Docker internal network)
   |
   v
API container (SimpleStudentApi)
   |
   v
mssqlserver:1433  (Docker internal network)
   |
   v
SQL Server container  <-->  mssql_data volume (persistent)
```

All three containers communicate on a shared Docker network called **`student-net`**.
The database volume (`mssql_data`) is managed by the infra compose file, so your data
is never lost when rebuilding the app.

---

## Project structure

```
.
├── docker-compose.infra.yml     <- SQL Server (start once, leave running)
├── docker-compose.yml           <- API + Frontend (rebuild freely)
├── SimpleStudentApi/
│   ├── Data/
│   │   └── AppDbContext.cs      <- EF Core DbContext
│   ├── Models/
│   │   └── Student.cs           <- Student entity
│   └── Program.cs               <- Minimal API endpoints (CRUD)
└── SimpleStudentFrontend/
    ├── Models/
    │   └── StudentDto.cs        <- DTO for API responses
    └── Pages/
        ├── Index.cshtml         <- Info / home page
        ├── Students.cshtml      <- List all students
        ├── StudentCreate.cshtml <- Add new student
        └── StudentEdit.cshtml   <- Edit existing student
```

---

## First-time setup

### Step 1 — Start the infrastructure (SQL Server)

Run this **once**. The container has `restart: unless-stopped`, so it also
comes back automatically after a machine reboot.

```bash
docker-compose -f docker-compose.infra.yml up -d
```

Wait ~15 seconds for SQL Server to finish initialising. You can verify it is ready:

```bash
docker logs mssqlserver --tail 20
```

Look for: `SQL Server is now ready for client connections.`

### Step 2 — Build and start the app

```bash
docker-compose up --build
```

The API automatically creates the `StudentDb` database and `Students` table on first
startup — no migrations needed.

---

## Daily workflow

```bash
# SQL Server is already running from Step 1 — do nothing.

# Rebuild and start the app after any code change:
docker-compose up --build

# Stop the app (SQL Server keeps running, data is safe):
docker-compose down

# --- Less common commands ---

# Check if the infra (SQL Server) is running:
docker-compose -f docker-compose.infra.yml ps

# Stop SQL Server (only when you need to):
docker-compose -f docker-compose.infra.yml down

# Start SQL Server again:
docker-compose -f docker-compose.infra.yml up -d
```

---

## URLs

| What                          | URL                              |
|-------------------------------|----------------------------------|
| Frontend (home / info)        | http://localhost:6011            |
| Frontend (students CRUD)      | http://localhost:6011/Students   |
| API — list students           | http://localhost:6001/api/students |
| API — info                    | http://localhost:6001/api/info   |
| SQL Server (host access)      | localhost,1433                   |

### Connecting from SSMS / Azure Data Studio

| Field          | Value                |
|----------------|----------------------|
| Server         | localhost,1433       |
| Authentication | SQL Server Auth      |
| Login          | sa                   |
| Password       | 1Secure*Password1    |

---

## API endpoints

| Method | Route                  | Description              |
|--------|------------------------|--------------------------|
| GET    | `/api/info`            | Service health / info    |
| GET    | `/api/students`        | List all students        |
| GET    | `/api/students/{id}`   | Get one student by ID    |
| POST   | `/api/students`        | Create a new student     |
| PUT    | `/api/students/{id}`   | Update existing student  |
| DELETE | `/api/students/{id}`   | Delete a student         |

### Example — create a student (curl)

```bash
curl -X POST http://localhost:6001/api/students \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Jonas",
    "lastName": "Jonaitis",
    "email": "jonas@university.lt",
    "studyProgram": "Computer Science",
    "enrollmentYear": 2024
  }'
```

---

## How Docker networking works here

The frontend container does **not** call `localhost:6001`.
Inside the Docker network it reaches the API by service name:

```
http://simplestudentapi:8080
```

Likewise the API reaches SQL Server as:

```
Server=mssqlserver,1433
```

These names come from the `container_name` fields in the compose files.
Both compose files attach to the same external network `student-net`, which is
why containers defined in different files can still talk to each other.

---

## Learning goals

- Separating infrastructure from application containers
- Persistent data with Docker named volumes
- Multi-file Docker Compose with a shared external network
- Container-to-container communication by service name
- .NET 8 Minimal API with Entity Framework Core
- Razor Pages frontend calling a REST API
- Full CRUD: Create, Read, Update, Delete from a browser UI
