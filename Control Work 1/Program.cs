using CW1Friend.Interfaces;
using CW1Friend.Services;
using CW1Friend.UI;

bool useStub = args.Contains("--stub");

IStudentRepository repo = useStub
    ? new StubStudentRepository()
    : new MemoryStudentRepository();

Console.WriteLine(useStub
    ? "[INFO] Using StubStudentRepository (--stub)."
    : "[INFO] Using MemoryStudentRepository (default).");

var calc      = new AverageCalculator();
var validator = new StudentValidator();
var stuService = new StudentService(repo, calc, validator);
var repService = new ReportService(repo, calc);
var menu       = new ConsoleMenu(stuService, repService, calc);

menu.Start();
