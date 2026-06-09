# Lab 6 — Docker + .NET 8 + SQL Server

## Architecture

```
Browser
   |
   v
localhost:6011  (Frontend)
   |
   v
http://simplestudentapi:8080  (Docker internal)
   |
   v
localhost:6001  (API)
   |
   v
mssqlserver:1433  (Docker internal)
   |
   v
SQL Server 2022
```

All three containers run on a shared Docker network called `student-net`.

---

## How to run

### Step 1 — Start SQL Server (once)

```bash
docker compose -f docker-compose.infra.yml up -d
```

Wait ~15 seconds, then verify:

```bash
docker logs mssqlserver --tail 20
# Look for: SQL Server is now ready for client connections.
```

### Step 2 — Start the app

```bash
docker compose up --build
```

The API will automatically create the database and Students table on first startup.

---

## URLs

| What | URL |
|------|-----|
| Frontend — home | http://localhost:6011 |
| Frontend — students | http://localhost:6011/Students |
| API — info | http://localhost:6001/api/info |
| API — students | http://localhost:6001/api/students |

---

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/info` | Service health info |
| GET | `/api/students` | List all students |
| GET | `/api/students/{id}` | Get one student |
| POST | `/api/students` | Create student |
| PUT | `/api/students/{id}` | Update student |
| DELETE | `/api/students/{id}` | Delete student |

---

## Key Docker concepts demonstrated

- **Image vs Container** — image is the blueprint, container is the running instance
- **Port mapping** — `HOST:CONTAINER` (e.g. `6001:8080`)
- **Container networking** — containers talk via service names, not localhost
- **Docker Compose** — orchestrating multiple containers together
- **Volumes** — persistent SQL Server data survives container restarts
- **Multi-file Compose** — infra separated from app for safe rebuilds

---

## Daily workflow

```bash
# Rebuild app after code changes
docker compose up --build

# Stop app (SQL Server keeps running, data is safe)
docker compose down

# Stop everything including SQL Server
docker compose -f docker-compose.infra.yml down
```
