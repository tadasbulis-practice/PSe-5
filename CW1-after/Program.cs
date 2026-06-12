using CW1.Services;
using CW1.UI;

// Entry point — wires up the three layers and starts the application.
// No business logic or Console output belongs here.

var repo    = new StudentRepository();
var service = new StudentService(repo);
var ui      = new StudentUI(service);

ui.Run();
