using Application.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Elastic.Configuration
{
    public static class ElasticClientFactory
    {
        public static IElasticClient CreateElasticClient(string uri, string defaultIndex = "products")
        {
            var connectionSettings = new ConnectionSettings(new Uri(uri))
                .DefaultIndex(defaultIndex)
                .EnableDebugMode()
                .PrettyJson()
                .DefaultMappingFor<Product>(m => m
                    .IndexName(defaultIndex)
                    .IdProperty(p => p.Id))
                .ThrowExceptions()
                .RequestTimeout(TimeSpan.FromSeconds(30))
                .BasicAuthentication("elastic", "root");
            return new ElasticClient(connectionSettings);
        }
    }
}
