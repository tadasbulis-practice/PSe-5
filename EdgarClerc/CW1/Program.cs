// =============================================================================
//  CW-1 / KD-1  (Kontrolinis darbas / Control Work — Starter)
//
//  LT:  Sis projektas yra "viskas viename" Program.cs faile.
//       Visa logika (modeliai, validacija, vidurkio skaiciavimas, spausdinimas,
//       meniu, duomenu saugykla) sukista cia. Programa veikia, bet pazeidzia
//       Single Responsibility Principle bei daugiasluoksnes architekturos
//       principus. Jusu uzduotis — perdaryti ja i svaria OOP struktura su
//       Modeliu, Servisu ir UI sluoksniais.
//
//       PAPILDOMA (Task 2): meniu punktai 7, 8, 9 yra paraseti TIK su LINQ.
//       Jusu CW-1-after kiekvienai is siu funkciju turi but DVI versijos:
//          (a) su LINQ — kaip parodyta cia,
//          (b) be LINQ — naudojant tik for/foreach/if (jokio Where, OrderBy,
//              Sum, Average, Count, Any, All ar pan.).
//       Tai parodo, kad zinote, KA LINQ daro po kapotu.
//
//  EN:  This is the "everything in one file" Program.cs version.
//       All logic (models, validation, average, printing, menu, in-memory
//       repository) lives here. The program runs, but it breaks SRP and the
//       layered-architecture principles. Your task: refactor it into clean
//       OOP layers — Models, Services and UI — with at least one Interface.
//
//       BONUS (Task 2): menu items 7, 8 and 9 are written ONLY with LINQ.
//       In CW-1-after each of these three features must exist in TWO versions:
//          (a) the LINQ version — exactly as shown here,
//          (b) the no-LINQ version — using only for/foreach/if (no Where,
//              OrderBy, Sum, Average, Count, Any, All, etc.).
//       This proves you understand WHAT LINQ does under the hood.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using CW1.Interface;
using CW1.Models;
using CW1.Repository;
using CW1.Services;
using CW1.UI;

namespace CW1;

public static class Program
{
    public static void Main()
    {
        bool reportWithLinq = false;

        IStudentRepository repository = new StudentRepository();
        AverageStategy averageStategy = new AverageStategy();
        StudentValidator validator = new StudentValidator(repository);
        IReportService report = reportWithLinq
            ? new ReportServiceLinq(repository, averageStategy)
            : new ReportServiceWithoutLinq(repository, averageStategy);

        var studentService = new StudentService(repository, averageStategy, validator, report);

        ConsoleMenu menu = new ConsoleMenu(studentService);
        menu.StartMenu();
    }
}
