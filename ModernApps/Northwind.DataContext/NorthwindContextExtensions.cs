using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.DataContext;

public static class NorthwindContextExtensions
{
    /// <summary>
    /// Adds NorthwindContext to the specified IServiceCollection. Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
        public static IServiceCollection AddNorthwindContext(this IServiceCollection services, string? connectionString)
        {
            if(connectionString is null)
            {
                SqlConnectionStringBuilder builder = new();
                builder.DataSource = "tcp:127.0.0.1\\azure-edge,14331"; // SQL Server in container.
                builder.InitialCatalog = "Northwind";
                builder.TrustServerCertificate = true;
                builder.MultipleActiveResultSets = true;
                // Because we want to fail faster. Default is 15 seconds.
                builder.ConnectTimeout = 3;
                // SQL Server authentication.
                builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
                builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");
             
                connectionString = builder.ConnectionString;
            }

            services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.LogTo(NorthwindContextLogger.WriteLine, 
                    new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting }
                    );
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);

            return services;
        }
    
}