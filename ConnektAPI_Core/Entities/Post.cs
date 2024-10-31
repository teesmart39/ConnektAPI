namespace ConnektAPI_Core.Entities;

public class Post : BaseClass
{
    public string Content { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    
}