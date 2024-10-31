using Microsoft.AspNetCore.Identity;

namespace ConnektAPI_Core.Entities;

public class User : IdentityUser
{
    /// <summary>
    /// The user first name
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// The user last name
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// The user posts
    /// </summary>
    public List<Post> Posts { get; set; }
    
    /// <summary>
    /// The User photos
    /// </summary>
    public List<Photo> Photos { get; set; }
    
    /// <summary>
    /// The user likes
    /// </summary>
    public List<Like> Likes { get; set; }
    
    public List<User> Followers { get; set; }
    
    public List<User> Following { get; set; }
}