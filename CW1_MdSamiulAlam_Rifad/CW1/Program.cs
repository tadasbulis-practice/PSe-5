using CW1After.Interfaces;
using CW1After.Services;
using CW1After.UI;

// ─── Task 3: parse command-line arguments ───
bool useStub = args.Contains("--stub");

// ─── Conditional injection (only here, not inside services) ───
IStudentRepository repository = useStub
    ? new StubStudentRepository()
    : new MemoryStudentRepository();

// ─── Tell the user which repository is active ───
if (useStub)
    Console.WriteLine("[INFO] Using StubStudentRepository (--stub).");
else
    Console.WriteLine("[INFO] Using MemoryStudentRepository (default).");

// ─── Composition root: wire up all services ───
var averageCalculator = new AverageCalculator();
var validator         = new StudentValidator();
var studentService    = new StudentService(repository, averageCalculator, validator);
var reportService     = new ReportService(repository, averageCalculator);
var menu              = new ConsoleMenu(studentService, reportService, averageCalculator);

// ─── Start the app ───
menu.Run();
