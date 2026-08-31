using Acelera.Core.Abstractions;
using Acelera.Core.Enums;
using System.Text.Json.Serialization;

namespace Acelera.Core.Models;

/// <summary>
/// The profile of the student
/// </summary>
public sealed class StudentProfile : ProfileBase 
{
    /// <summary>
    /// The category under study
    /// </summary>
    [JsonPropertyName("target_category")]
    public LicenseCategory TargetCategory { get; set; }
}
