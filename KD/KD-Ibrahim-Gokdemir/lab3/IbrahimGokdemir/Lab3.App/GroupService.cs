using System.Collections.Generic;

public class GroupService : IGroupService
{
    public void RegisterStudent(Group group, Student student)
    {
        group.Students.Add(student);
    }

    public List<Student> GetAllStudents(Group group)
    {
        return group.Students;
    }
}
