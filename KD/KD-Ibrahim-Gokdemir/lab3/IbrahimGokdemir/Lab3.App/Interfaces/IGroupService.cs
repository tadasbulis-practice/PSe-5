using System.Collections.Generic;

public interface IGroupService
{
    void RegisterStudent(Group group, Student student);
    List<Student> GetAllStudents(Group group);
}