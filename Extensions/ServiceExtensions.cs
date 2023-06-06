using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Places.Contracts.Repository;
using Places.Data;
using Places.Repository;
using System.Runtime.CompilerServices;

namespace Places.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterCors(this IServiceCollection services)
        {
            services.AddCors(policy =>
                    policy.AddPolicy("OpenCorsPolicy", opt =>
                        opt.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                    )
                );
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var host = "localhost,8082";

            if(!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_HOST"))) {
                host = Environment.GetEnvironmentVariable("DB_HOST");
            }
            var conStrBuilder = new SqlConnectionStringBuilder(
            configuration.GetConnectionString("Default"));
            conStrBuilder.DataSource = host;
            var connection = conStrBuilder.ConnectionString;

            services.AddDbContext<RequestResponseDbContext>(opt => opt.UseSqlServer(connection));
        }

        public static void RegisterHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("foursquare", c =>
            {
                c.BaseAddress = new Uri("https://api.foursquare.com");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", configuration.GetValue<string>("FourSquareApiKey"));
            });
        }
    }
}
