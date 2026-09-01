using Acelera.Core.Abstractions;
using Acelera.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acelera.Core.Models;

/// <summary>
/// The user's account
/// </summary>
[Table("user_accounts")]
public sealed class UserAccount : ITimeTrackable
{
    /// <summary>
    /// Unique account ID
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the associated user
    /// </summary>
    [Required]    
    [Column("user_id")]
    public Guid UserId { get; set; }

    /// <summary>
    /// Class of provider
    /// </summary>
    [Required]
    [MaxLength(16)]
    [Column("provider")]
    public AccountProvider Provider { get; set; }

    /// <summary>
    /// The account ID for the specified provider
    /// </summary>
    [Required]
    [MaxLength (32)]
    [Column ("account_id")]
    public string AccountId { get; set; } = String.Empty;    

    /// <inheritdoc/>
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc/>
    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Reference to the user
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
