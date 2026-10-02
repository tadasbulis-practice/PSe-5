# Student Docker Lab (.NET 8 + MSSQL)

This repository contains three Docker services:

- **SimpleStudentApi** — .NET 8 Minimal API with full CRUD, backed by SQL Server, documented with **Swagger / OpenAPI**
- **SimpleStudentFrontend** — .NET 8 Razor Pages app that calls the API through a **typed client generated from Swagger**
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
│   │   ├── Student.cs           <- Student entity (/// comments shown in Swagger)
│   │   ├── InfoResponse.cs      <- /api/info response
│   │   └── FacultyResponse.cs   <- /api/faculty response
│   └── Program.cs               <- Swagger setup + Minimal API endpoints (CRUD)
└── SimpleStudentFrontend/
    ├── ApiClient/
    │   ├── swagger.json             <- copy of the API contract (OpenAPI)
    │   ├── StudentApiClient.g.cs    <- GENERATED typed client (NSwag) – do not edit
    │   ├── ApiErrors.cs             <- turns API errors into readable messages
    │   └── generate-client.sh/.ps1  <- regenerate the client after API changes
    ├── Models/
    │   └── StudentDto.cs        <- form model (mapped to/from the API Student)
    └── Pages/
        ├── Index.cshtml         <- Info / home page
        ├── Students.cshtml      <- List all students
        ├── Student.cshtml       <- Student details (/Student?id=1)
        ├── StudentCreate.cshtml <- Add new student
        └── StudentEdit.cshtml   <- Edit existing student
```

---

## First-time setup

### Step 1 — Start the infrastructure (SQL Server)

> **Apple Silicon Mac (M1/M2/M3/M4):** SQL Server images exist only for Intel/AMD.
> In Docker Desktop → Settings → General enable **"Use Rosetta for x86_64/amd64 emulation"**.
> The compose file already sets `platform: linux/amd64`.

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
| **API — Swagger UI**          | http://localhost:6001/swagger    |
| API — OpenAPI document (JSON) | http://localhost:6001/swagger/v1/swagger.json |
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
| GET    | `/api/faculty`         | Faculty → groups → students |

Invalid input (empty name, bad email, year outside 2000–2100) returns **400 Bad Request**
with a list of field errors.

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

## Swagger (OpenAPI)

Open **http://localhost:6001/swagger** (or just http://localhost:6001 – it redirects there).

What you can do there:

- see every endpoint, its parameters, request body and possible response codes
- read the descriptions and example values (they come from `/// <summary>` and `<example>` comments)
- press **Try it out → Execute** to call the real API and see the response, status code and the equivalent `curl` command
- open `/swagger/v1/swagger.json` – the machine-readable contract other tools use

How it is added to the API (see `SimpleStudentApi/Program.cs`):

```csharp
// .csproj:  <PackageReference Include="Swashbuckle.AspNetCore" Version="10.2.3" />
//           <GenerateDocumentationFile>true</GenerateDocumentationFile>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => { o.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleStudentApi", Version = "v1" });
                                      o.IncludeXmlComments(xmlFile); });
...
app.UseSwagger();      // /swagger/v1/swagger.json
app.UseSwaggerUI();    // /swagger

students.MapPost("/", ...)
   .WithName("CreateStudent")          // operationId → method name in generated clients
   .WithSummary("Create a new student")
   .Produces<Student>(201)
   .ProducesValidationProblem();       // documents the 400 response
```

## Frontend: typed client generated from Swagger

The frontend does not write URLs or JSON by hand. `ApiClient/StudentApiClient.g.cs` was generated
from `swagger.json` with **NSwag**, and the pages use it like a normal C# service:

```csharp
// Program.cs
builder.Services.AddHttpClient<IStudentApiClient, StudentApiClient>(c => c.BaseAddress = new Uri(apiBaseUrl));

// Students.cshtml.cs
Students = (await _api.GetStudentsAsync()).ToList();   // GET    /api/students
await _api.CreateStudentAsync(student);                // POST   /api/students
await _api.UpdateStudentAsync(id, student);            // PUT    /api/students/{id}
await _api.DeleteStudentAsync(id);                     // DELETE /api/students/{id}
```

API errors arrive as `ApiException` (for 400 it contains the validation errors), see `ApiErrors.cs`.

### Regenerate the client after you change the API

```bash
docker-compose up --build -d                 # API must be running
cd SimpleStudentFrontend/ApiClient
bash generate-client.sh                      # macOS / Linux   (needs Node.js)
.\generate-client.ps1                        # Windows PowerShell
docker-compose up --build                    # rebuild frontend with the new client
```

If the API contract changed (renamed field, new endpoint), the frontend **will not compile**
until you fix it – the compiler shows you every place that must change.

## Run without Docker (from IDE / terminal)

SQL Server must run (Step 1). Then in two terminals:

```bash
cd SimpleStudentApi      && dotnet run    # http://localhost:6001/swagger (Development connection string)
cd SimpleStudentFrontend && dotnet run    # http://localhost:6011
```

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
- Documenting an API with Swagger / OpenAPI and testing it in the browser
- Generating a typed API client from the OpenAPI contract
