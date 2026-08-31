using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acelera.Core.Models;

/// <summary>
/// A car for driving training
/// </summary>
[Table("cars")]
public sealed class Car
{
    /// <summary>
    /// Unique car ID
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Car brand
    /// </summary>
    /// <example>
    /// Toyota
    /// </example>
    [Required]
    [MaxLength(32)]
    [Column("make")]
    public string Make { get; set; } = String.Empty;

    /// <summary>
    /// Car model
    /// </summary>
    /// <example>
    /// Camry
    /// </example>
    [Required] 
    [MaxLength(32)]
    [Column ("model")]
    public string Model { get; set; } = String.Empty;

    /// <summary>
    /// Does the car have an automatic transmission
    /// </summary>
    [Required]
    [Column ("auto_transmission")]
    public bool HasAutoTransmission { get; set; } = false;
}
