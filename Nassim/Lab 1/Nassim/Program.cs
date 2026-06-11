class Program
{
    static void Main()
    {
        Student s1 = new Student(1, "Alice", "Martin", "alice.martin@email.com", 17.5);
        Student s2 = new Student(2, "Bob", "Dupont", "bob.dupont@email.com", 14.0);
        Student s3 = new Student(3, "Clara", "Bernard", "clara.bernard@email.com", 18.2);

        Group group = new Group("CS101");
        group.AddStudent(s1);
        group.AddStudent(s2);
        group.AddStudent(s3);

        group.PrintAll();
    }
}