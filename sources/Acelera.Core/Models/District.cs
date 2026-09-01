using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Acelera.Core.Models;

/// <summary>
/// A district of city
/// </summary>
[Table("districts")]
public sealed class District
{
    /// <summary>
    /// Unique district ID
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// ID of the city
    /// </summary>
    [Required]
    [Column("city_id")]
    public int CityId { get; set; }

    /// <summary>
    /// The name of district
    /// </summary>
    [Required]
    [MaxLength(64)]
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The city where the area is located
    /// </summary>
    [ForeignKey(nameof(CityId))]
    public City? City { get; set; }

    /// <summary>
    /// Linked users
    /// </summary>
    public List<User> Users { get; set; } = new();
}
