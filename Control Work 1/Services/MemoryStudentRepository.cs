using CW1Friend.Interfaces;
using CW1Friend.Models;

namespace CW1Friend.Services;

public class MemoryStudentRepository : IStudentRepository
{
    private List<Student> _studentList;
    private List<Group> _groupList;

    public MemoryStudentRepository()
    {
        _groupList = new List<Group>
        {
            new Group { Code = "ENG-1", GroupName = "English Studies 1" },
            new Group { Code = "LIT-2", GroupName = "Literature 2"       },
            new Group { Code = "MATH-3", GroupName = "Mathematics 3"     }
        };

        _studentList = new List<Student>
        {
            new Student { Id = 1, FullName = "Samiaul alam",  EmailAddress = "samiaul.k@uni.lt",   GroupCode = "ENG-1",  Grades = new List<int> { 7, 8, 9, 6  } },
            new Student { Id = 2, FullName = "Ali khan",  EmailAddress = "khan.p@uni.lt",   GroupCode = "LIT-2",  Grades = new List<int> { 10, 9, 10, 9 } },
            new Student { Id = 3, FullName = "Tomas Jankauskas",  EmailAddress = "tomas.j@uni.lt",   GroupCode = "MATH-3", Grades = new List<int> { 5, 6, 4, 7  } },
            new Student { Id = 4, FullName = "shani Stankeviciene", EmailAddress = "shani.s@uni.lt",   GroupCode = "ENG-1",  Grades = new List<int> { 8, 9, 8, 10 } },
            new Student { Id = 5, FullName = "kashi khan",   EmailAddress = "kashi.v@uni.lt",   GroupCode = "LIT-2",  Grades = new List<int> { 6, 5, 7, 6  } }
        };
    }

    public List<Student> FetchAllStudents() => _studentList;

    public Student? FetchStudentById(int id)
    {
        for (int i = 0; i < _studentList.Count; i++)
        {
            if (_studentList[i].Id == id)
                return _studentList[i];
        }
        return null;
    }

    public void SaveStudent(Student student)
    {
        _studentList.Add(student);
    }

    public void SaveGrade(int studentId, int grade)
    {
        var found = FetchStudentById(studentId);
        if (found != null)
            found.Grades.Add(grade);
    }

    public List<Group> FetchAllGroups() => _groupList;

    public void SaveGroup(Group group)
    {
        _groupList.Add(group);
    }
}
