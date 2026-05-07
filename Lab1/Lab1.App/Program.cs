using System;
using System.Linq;

Random rnd = new Random();
string[] argsList = Environment.GetCommandLineArgs();
bool allowChallenge = argsList.Contains("--challenge");

int max = allowChallenge ? 5 : 4;
int task = rnd.Next(1, max + 1);

Console.WriteLine("## Lab 1 Submission Details");
Console.WriteLine("Name Surname: Muhammad Abdul Quddous");
Console.WriteLine("Group: PSe-5");
Console.WriteLine($"Lecture date: {DateTime.Now:yyyy-MM-dd}");
Console.WriteLine($"Random task: {task}");

if (task == 5)
{
    int fallback = rnd.Next(1, 5);
    Console.WriteLine($"Fallback (if #5 is too hard): {fallback}");
}

Console.WriteLine("\n### Run Instructions");
Console.WriteLine("dotnet run --project Lab1.App");