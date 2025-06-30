
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace ElasticSearch_Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<MasterContext>(x => x.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRESQL_URI")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            using (IServiceScope serviceScope = app.Services.CreateScope())
            {
                MasterContext context = serviceScope.ServiceProvider.GetRequiredService<MasterContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
