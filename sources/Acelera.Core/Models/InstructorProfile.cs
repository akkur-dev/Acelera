using Acelera.Core.Abstractions;
using System.Text.Json.Serialization;

namespace Acelera.Core.Models;

/// <summary>
/// Driving instructor profile
/// </summary>
public sealed class InstructorProfile : ProfileBase
{
    /// <summary>
    /// Driving experience in years
    /// </summary>
    [JsonPropertyName("experience")]
    public short Experience { get; set; }

    /// <summary>
    /// Has the legal entity status been confirmed
    /// </summary>
    [JsonPropertyName("tax_status")]
    public bool IsTaxStatusVerified { get; set; }

    /// <summary>
    /// Has the driver's status been confirmed
    /// </summary>
    [JsonPropertyName("driver_status")]
    public bool IsDriverStatusVerified { get; set; }

    /// <summary>
    /// Has the instructor's status been confirmed
    /// </summary>
    [JsonPropertyName("instructor_status")]
    public bool IsInstructorStatusVerified { get; set; }
}
