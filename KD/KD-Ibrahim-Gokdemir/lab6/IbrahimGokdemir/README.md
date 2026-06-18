# Student Docker Lab (.NET 8)

This repository contains two separate .NET 8 applications:

- **SimpleStudentApi** - backend API returning a simple JSON response
- **SimpleStudentFrontend** - frontend Razor Pages application that calls the API and shows the response in HTML

## Architecture

```text
Browser
   |
   v
localhost:6011
   |
   v
Frontend container (SimpleStudentFrontend)
   |
   v
http://simplestudentapi:8080/api/info
   |
   v
API container (SimpleStudentApi)
```

## Projects

### 1. SimpleStudentApi
Endpoint:

- `GET /api/info`

Returns JSON like:

```json
{
  "service": "SimpleStudentApi",
  "version": "1.0",
  "status": "ok",
  "message": "Hello from .NET 8 Minimal API",
  "timestampUtc": "2026-03-16T16:51:12.9296743Z",
  "environment": "Production"
}
```

### 2. SimpleStudentFrontend
Frontend page:

- `GET /`

This page calls the backend API and displays the response.

## Run with Docker Compose

Open terminal in repository root and run:

```bash
docker compose up --build
```

## Test URLs

API directly:

```text
http://localhost:6001/api/info
```

Frontend page:

```text
http://localhost:6011/
```

## Stop containers

```bash
docker compose down
```

## Important note

The frontend container does **not** call `localhost:6001`.
Inside Docker network it calls the API by service name:

```text
http://simplestudentapi:8080/api/info
```

This is an important Docker networking concept.

## Learning goals

Students should understand:

- how to separate frontend and backend
- how to run two .NET apps in separate containers
- how Docker Compose connects services
- how containers communicate using service names
- difference between host port and container port
