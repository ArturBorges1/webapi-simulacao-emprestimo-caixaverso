using Caixaverso.Backend.Infrastructure.Sqlite.Context;
using Microsoft.EntityFrameworkCore;

namespace Caixaverso.Backend.CrossCutting.Database
{
    public static class SqliteConfiguration
    {
        public static IServiceCollection AddSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = $"Data Source={configuration["ConnectionStrings:Database"]}";
            services.AddDbContext<SqliteContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            return services;
        }
    }
}
