using System.ComponentModel.DataAnnotations.Schema;

namespace ConnektAPI_Core.Entities;

public class Photo : BaseClass
{
    public string PhotoId { get; set; }

    public string PhotoUrl { get; set; }

    [ForeignKey(nameof(UserId))] public string UserId { get; set; }

    public ApplicationUser User { get; set; }
}