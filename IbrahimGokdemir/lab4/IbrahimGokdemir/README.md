# OOP Lab 4 – Dependency Isolation

Name: Ibrahim Gokdemir
Group: Erasmus
Date: 27/03/2026

## What was isolated
In this lab, the average calculation mechanism was isolated using the `IAverageStrategy` interface.

## Why it was isolated
Average calculation rules may change in the future. The business logic should not depend on one concrete implementation.

## Constructor Injection
`StudentReportService` receives `IAverageStrategy` through the constructor instead of creating it with `new`.

## Implementations
- `SimpleAverageStrategy` – real implementation
- `FakeAverageStrategy` – stable fake result
- `StubAverageStrategy` – controlled result through constructor

## Demonstrated branches
- PASSED branch
- FAILED branch

## Architecture
- `Program.cs` only wires objects together
- `StudentReportService` contains business logic
- Dependency behavior is replaced by injection without changing business logic