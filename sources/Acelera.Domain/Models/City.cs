using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acelera.Core.Models;

/// <summary>
/// A city
/// </summary>
[Table("cities")]
public sealed class City
{
    /// <summary>
    /// Unique city ID
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// The name of city
    /// </summary>
    [Required]
    [MaxLength(64)]
    [Column]
    public string? Name { get; set; }

    /// <summary>
    /// List of city districts
    /// </summary>
    public List<District> Districts { get; set; } = new(); 
}
