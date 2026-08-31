using Acelera.Core.Abstractions;
using Acelera.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acelera.Core.Models;

/// <summary>
/// User's data
/// </summary>
[Table("users")]
public sealed class User : ITimeTrackable
{
    /// <summary>
    /// Unique user ID
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// The user's name
    /// </summary>
    [Required]
    [MaxLength(32)]
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The user's age
    /// </summary>
    [Required]
    [Column("age")]
    public short Age { get; set; }

    /// <summary>
    /// The user's role on the platform
    /// </summary>
    [Required]
    [MaxLength(16)]
    [Column("role")]
    public UserRole Role { get; set; }

    /// <summary>
    /// Is the user banned
    /// </summary>
    [Column("is_banned")]
    public bool IsBanned { get; set; } = false;

    /// <summary>
    /// Profile data
    /// </summary>
    [Required]
    [Column("name", TypeName = "jsonb")]
    public ProfileBase? Profile { get; set; }    

    /// <inheritdoc/>
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc/>
    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Linked user's accounts
    /// </summary>
    public List<UserAccount>? Accounts { get; set; }
}
