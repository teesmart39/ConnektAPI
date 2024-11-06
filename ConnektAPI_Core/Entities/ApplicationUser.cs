using Microsoft.AspNetCore.Identity;

namespace ConnektAPI_Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string DateOfBirth { get; set; }

    public string Nationality { get; set; }

    /// <summary>
    ///     The user posts
    /// </summary>
    public List<Post> Posts { get; set; }

    /// <summary>
    ///     The User photos
    /// </summary>
    public List<Photo> Photos { get; set; }

    /// <summary>
    ///     The user likes
    /// </summary>
    public List<Like> Likes { get; set; }

    public List<UserFollower> Followers { get; set; }

    public List<UserFollower> Following { get; set; }
}