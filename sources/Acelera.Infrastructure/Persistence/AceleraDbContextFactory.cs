using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Acelera.Infrastructure.Persistence;

/// <summary>
/// A factory for creating a database context 
/// during the migration process.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AceleraDbContext>
{
    /// <summary>
    /// Environment filename
    /// </summary>
    private const string ENV_FILE_NAME = ".env";

    /// <summary>
    /// The name of the variable containing the database name.
    /// </summary>
    private const string DATABASE_VAR_NAME = "POSTGRES_DB";

    /// <summary>
    /// The name of the variable containing the user's name.
    /// </summary>
    private const string USER_VAR_NAME = "POSTGRES_USER";

    /// <summary>
    /// The name of the variable containing the password.
    /// </summary>
    private const string PASSWORD_VAR_NAME = "POSTGRES_PASSWORD";

    /// <summary>
    /// Creates a new context <see cref="AceleraDbContext"/>
    /// </summary>
    /// <param name="args">
    /// Command line arguments
    /// </param>
    /// <returns>
    /// A new instance of <see cref="AceleraDbContext"/> 
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// It is not possible to create a context.
    /// </exception>
    public AceleraDbContext CreateDbContext(string[] args)
    {
        var projectDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        var envDirectory = projectDir.Parent?.Parent?.FullName ?? Directory.GetCurrentDirectory();
        var envFilePath = Path.Combine(envDirectory, ENV_FILE_NAME);

        var creds = LoadCredentials(envFilePath);

        if (!creds!.IsValid)
        {
            throw new InvalidOperationException("Cannot load variables POSTGRES_USER, POSTGRES_PASSWORD, POSTGRES_DB from .env file");
        }

        var connectionString = $"Host=localhost;Port=5432;Database={creds.DatabaseName};Username={creds.UserName};Password={creds.Password}";

        var optionsBuilder = new DbContextOptionsBuilder<AceleraDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AceleraDbContext(optionsBuilder.Options);
    }

    /// <summary>
    /// Loads environment variables 
    /// from the specified file.
    /// </summary>
    /// <param name="environmentFilePath">
    /// Path to environment file
    /// </param>
    /// <returns>
    /// A new instance <see cref="AceleraDbCredentials"/> 
    /// or <see langword="null"/> if the variables not found
    /// </returns>
    private static AceleraDbCredentials? LoadCredentials(string? environmentFilePath)
    {
        var variables = Env.Load(environmentFilePath);

        if (!variables.Any())
        {
            return null;
        }

        return new AceleraDbCredentials
        {
            DatabaseName = Environment.GetEnvironmentVariable(DATABASE_VAR_NAME),
            UserName = Environment.GetEnvironmentVariable(USER_VAR_NAME),
            Password = Environment.GetEnvironmentVariable(PASSWORD_VAR_NAME)
        };
    }
}
