using Acelera.Core.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acelera.Core.Models;

/// <summary>
/// A review for training process
/// </summary>
[Table("reviews")]
public sealed class Review : ITimeTrackable
{
    /// <summary>
    /// Unique review ID
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Unique instructor's ID
    /// </summary>
    [Required]
    [Column("instructor_id")]
    public Guid InstructorId { get; set; }

    /// <summary>
    /// Unique reviewer's ID
    /// </summary>
    [Required]
    [Column("reviewer_id")]
    public Guid ReviewerId { get; set; }

    /// <summary>
    /// The score of car state
    /// </summary>
    [Required]
    [Column("car_score")]
    public int CarScore { get; set; } = 0;

    /// <summary>
    /// The score of the psychological atmosphere
    /// </summary>
    [Required]
    [Column("psychology_score")]
    public int PsychologyScore { get; set; } = 0;

    /// <summary>
    /// The score of respect for the student's time
    /// </summary>
    [Required]
    [Column("punctuality_score")]
    public int PunctualityScore { get; set; } = 0;

    /// <summary>
    /// The score of benefit for driving skills
    /// </summary>
    [Required]
    [Column("benefit_score")]
    public int BenefitScore { get; set; } = 0;

    /// <summary>
    /// Feedback text
    /// </summary>
    [Required]
    [MaxLength(1024)]
    [Column("feedback")]
    public string Feedback { get; set; } = String.Empty;

    /// <inheritdoc/>
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc/>
    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Overall rating score
    /// </summary>
    [NotMapped]
    public int RatingScore => (CarScore + PsychologyScore + PunctualityScore + BenefitScore) / 4;
}
