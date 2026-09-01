namespace Acelera.Infrastructure.Persistence;

/// <summary>
/// Credentials for connecting to the database
/// </summary>
public sealed class AceleraDbCredentials
{
    /// <summary>
    /// Database name (aka Maintenance database)
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    /// Database username
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Database password
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Checks a credentials validity
    /// </summary>
    public bool IsValid => !(
        String.IsNullOrEmpty(DatabaseName) || 
        String.IsNullOrEmpty(UserName) || 
        String.IsNullOrEmpty(Password));
}
