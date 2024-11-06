using System.ComponentModel.DataAnnotations.Schema;

namespace ConnektAPI_Core.Entities;

public class UserFollower : BaseClass
{
    [ForeignKey("FollowerId")] public string FollowerId { get; set; }

    public ApplicationUser Follower { get; set; }

    [ForeignKey("FollowingId")] public string FollowingId { get; set; }


    public ApplicationUser Following { get; set; }
}