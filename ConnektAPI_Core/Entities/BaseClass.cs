namespace ConnektAPI_Core.Entities;

public class BaseClass
{
    /// <summary>
    ///     The Id
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    ///     Date Created
    /// </summary>
    public DateTimeOffset Created { get; set; } = DateTime.Now;

    /// <summary>
    ///     The Date The record was Updated
    /// </summary>
    public DateTimeOffset Updated { get; set; } = DateTime.Now;
}