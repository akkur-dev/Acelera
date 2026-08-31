namespace Acelera.Core.Abstractions;

/// <summary>
/// Interface for time tracking
/// </summary>
public interface ITimeTrackable
{
    /// <summary>
    /// The creation time of the model
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// The update time of the model
    /// </summary>
    DateTime UpdatedAt { get; set; }
}
