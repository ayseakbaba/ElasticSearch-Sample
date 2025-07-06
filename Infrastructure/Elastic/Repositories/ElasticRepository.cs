using Application.Interfaces;
using Nest;

namespace Infrastructure.Elastic.Repositories
{
    public class ElasticRepository<T> : IElasticRepository<T> where T : class
    {
        protected readonly IElasticClient _elasticClient;
        private readonly string _indexName;

        public ElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;

            // Index adı: T tipine göre belirlenir, örneğin "productdto" → override edilebilir
            _indexName = typeof(T).Name.ToLower();
        }

        public virtual async Task<bool> IndexAsync(T entity)
        {
            var response = await _elasticClient.IndexAsync(entity, i => i
                .Index(_indexName)
                .Refresh(Elasticsearch.Net.Refresh.True));

            return response.IsValid;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            // Id bilgisi interface'te yoksa burada zorlanabiliriz → özel çözüme gerek olabilir
            var idProperty = typeof(T).GetProperty("Id")?.GetValue(entity)?.ToString();
            if (string.IsNullOrEmpty(idProperty))
                return false;

            var response = await _elasticClient.UpdateAsync<T>(idProperty, u => u
                .Index(_indexName)
                .Doc(entity)
                .Refresh(Elasticsearch.Net.Refresh.True));

            return response.IsValid;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var response = await _elasticClient.DeleteAsync<T>(id, d => d
                .Index(_indexName)
                .Refresh(Elasticsearch.Net.Refresh.True));

            return response.IsValid;
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            var response = await _elasticClient.GetAsync<T>(id, g => g.Index(_indexName));
            return response.Found ? response.Source : null;
        }

        public virtual async Task<IEnumerable<T>> SearchAsync(string query)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s
                .Index(_indexName)
                .Query(q => q
                    .QueryString(qs => qs.Query($"*{query}*"))
                ));

            return response.IsValid ? response.Documents : Enumerable.Empty<T>();
        }
    }
}
