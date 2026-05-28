📘 Lab 7 – Containerized Student Management System
This lab extends Lab 5 by running the entire Student Management System inside Docker containers.
The goal is to deploy the backend API and frontend UI as separate services, orchestrated with Docker Compose, while preserving all Lab 5 design patterns (Strategy, Repository, Adapter, Service Abstraction).

🎯 Objectives
By completing this lab, you will:

Convert the Lab 5 architecture into a Minimal API backend

Build a frontend container that communicates with the backend

Use Dockerfiles to containerize both projects

Use docker-compose.yml to run a multi‑container system

Demonstrate runtime switching of strategies, repositories, and validators using environment variables

Understand container‑to‑container communication using service names

🏗️ System Architecture
Code
Browser
   ↓
Frontend Container (StudentFrontend)
   ↓  calls via Docker network
Backend Container (StudentApi)
Ports
Service	Container Port	Host Port	Purpose
studentapi	8080	6001	Backend API
studentfrontend	8080	6011	Frontend UI


Important
Containers do not call localhost.
They call each other using service names:

Code
http://studentapi:8080/api/students
📂 Project Structure
Code
StudentWeek7/
│
├── StudentApi/           # Backend API (Minimal API)
│   ├── Models/
│   ├── Interfaces/
│   ├── Services/
│   ├── Strategies/
│   ├── Repositories/
│   ├── Validation/
│   ├── Program.cs
│   └── Dockerfile
│
├── StudentFrontend/      # Razor Pages frontend
│   ├── Pages/
│   ├── Program.cs
│   └── Dockerfile
│
└── docker-compose.yml    # Multi-container orchestration
🧠 How Lab 5 Patterns Are Used in Containers
✔ Strategy Pattern
The backend selects the average calculation strategy using environment variables:

Code
AVERAGE_STRATEGY=simple | median | weighted
✔ Repository Pattern
Switch between memory, file, or API repositories:

Code
REPOSITORY=memory | file | api
✔ Adapter Pattern
Legacy validator can be enabled:

Code
VALIDATOR=legacy
✔ Service Abstraction
The frontend UI is completely separated from backend logic and communicates only through HTTP.

🐳 docker-compose.yml Overview
The system runs two containers:

yaml
services:
  studentapi:
    build: ./StudentApi
    ports:
      - "6001:8080"
    environment:
      ASPNETCORE_HTTP_PORTS: "8080"
      AVERAGE_STRATEGY: "simple"
      REPOSITORY: "memory"
      VALIDATOR: "basic"
      PRINTER: "simple"

  studentfrontend:
    build: ./StudentFrontend
    ports:
      - "6011:8080"
    environment:
      ASPNETCORE_HTTP_PORTS: "8080"
      API_BASE_URL: "http://studentapi:8080"
    depends_on:
      - studentapi
▶️ How to Run the System
From the root folder:

Code
docker compose up --build
Then open:

Frontend
Code
http://localhost:6011
Backend
Code
http://localhost:6001/api/info
🔄 Runtime Behavior Switching
You can change system behavior without modifying code — just edit docker-compose.yml.

Examples:

Use median strategy:
Code
AVERAGE_STRATEGY: "median"
Use file repository:
Code
REPOSITORY: "file"
Use strict validator:
Code
VALIDATOR: "strict"
Rebuild:

Code
docker compose up --build
📚 Learning Outcomes
After completing Lab 7, you should understand:

How to containerize .NET applications

How to run multiple containers with Docker Compose

How containers communicate using service names

How to integrate SOLID and design patterns into a deployed system

How to configure runtime behavior using environment variables