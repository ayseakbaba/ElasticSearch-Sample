using Application;
using Business;
using Infrastructure;
using Infrastructure.Persistence.Context;
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
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddBusiness(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // ?? Migration iþlemi burada yapýlmalý
            using (IServiceScope serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MasterContext>();
                context.Database.Migrate(); 
                var pendingMigrations = context.Database.GetPendingMigrations();
                Console.WriteLine(string.Join(",", pendingMigrations));
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elastic Sample API V1");
                    c.RoutePrefix = string.Empty;
                });
                //app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
