// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

StudentProfile student1 = new StudentProfile()
{
    FirstName = "edgar",
    LastName = "clerc",
    Group = "erasmus",
    LectureDate = new DateOnly(2026, 02, 12),
    Grades = [10,12,16,14.5,18.2]
};

StudentProfile student2 = new StudentProfile()
{
    FirstName = "nassim",
    LastName = "???",
    Group = "erasmus",
    LectureDate = new DateOnly(2026, 02, 12),
    Grades = [2,3,5,2.3]
};

StudentProfile student3 = new StudentProfile()
{
    FirstName = "ibrahim",
    LastName = "???",
    Group = "erasmus",
    LectureDate = new DateOnly(2026, 02, 12),
    Grades = [18.2,19,17,16.8,18]
};

Group group = new Group
{
    name = "group1"
};

group.addStudent(student1);
group.addStudent(student2);
group.addStudent(student3);

group.printAll();


student1.printInfo();
student2.printInfo();
student3.printInfo();
