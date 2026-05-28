full name : kamal demnati
task drills completed : 1/2/3/4/5


By default, the application runs in LINQ mode and uses the MemoryStudentRepository. You can change behavior using the following arguments:

- `--stub` → Uses StubStudentRepository instead of in-memory data
- `--linq` → Forces LINQ mode (this is also the default if no mode is specified)
- `--nolinq` → Runs the application using manual non-LINQ implementations

Examples:
- `dotnet run` → runs with LINQ and memory repository
- `dotnet run -- --stub` → runs with stub data and LINQ mode
- `dotnet run -- --nolinq` → runs with non-LINQ mode
- `dotnet run -- --stub --nolinq` → runs with stub data and non-LINQ mode

