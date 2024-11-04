using Microsoft.AspNetCore.Identity;

namespace ConnektAPI_Core.Entities;

public class ApplicationUser : IdentityUser
{
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
    
    public List<ApplicationUser> Followers { get; set; }
    
    public List<ApplicationUser> Following { get; set; }
}