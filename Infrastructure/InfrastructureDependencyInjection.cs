using Application.Dtos;
using Application.Interfaces;
using Infrastructure.Elastic.Configuration;
using Infrastructure.Elastic.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Elasticsearch Configuration
            var elasticUri = Environment.GetEnvironmentVariable("ELASTIC_URI");
            var defaultIndex = configuration["Elastic:DefaultIndex"] ?? "products";

            services.AddDbContext<MasterContext>(x => x.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRESQL_URI")));
            services.AddSingleton<IElasticClient>(sp =>
                ElasticClientFactory.CreateElasticClient(elasticUri, defaultIndex));
            services.AddScoped(typeof(Application.Interfaces.IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IElasticRepository<>), typeof(ElasticRepository<>));
            services.AddScoped<IElasticRepository<ProductDto>, ElasticProductRepository>();


            return services;
        }
    }
}
