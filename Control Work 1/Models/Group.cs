namespace CW1Friend.Models;

public class Group
{
    public string Code { get; set; }
    public string GroupName { get; set; }

    public Group()
    {
        Code = string.Empty;
        GroupName = string.Empty;
    }
}
