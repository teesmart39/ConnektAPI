using ConnektAPI_Core.ApiModels.Photos;
using ConnektAPI_Core.ApiModels.Post;
using ConnektAPI_Core.ApiModels.UserFollowers;

namespace ConnektAPI_Core.ApiModels.User;

public class UserProfileApiModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }

    public string Nationality { get; set; }

    /// <summary>
    ///     The user posts
    /// </summary>
    public List<PostApiModel> Posts { get; set; }

    /// <summary>
    ///     The User photos
    /// </summary>
    public List<PhotosApiModel> Photos { get; set; }

    /// <summary>
    ///     The user likes
    /// </summary>
    //public List<Like> Likes { get; set; }

    public List<UserFollowersApiModel> Followers { get; set; }

    public List<UserFollowersApiModel> Following { get; set; }
}

public class UpdateUserProfileApiModel
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; } 
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Nationality { get; set; }
    
    public string DateOfBirth { get; set; }
    
    public string PhotoUrl { get; set; }
}