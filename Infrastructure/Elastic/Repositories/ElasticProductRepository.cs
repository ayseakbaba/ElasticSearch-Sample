using Application.Dtos;
using Nest;

namespace Infrastructure.Elastic.Repositories
{
    public class ElasticProductRepository : ElasticRepository<ProductDto>
    {
        public ElasticProductRepository(Nest.IElasticClient elasticClient)
            : base(elasticClient)
        {
        }

        public async Task<IEnumerable<ProductDto>> SearchByCategoryAsync(string category)
        {
            var response = await _elasticClient.SearchAsync<ProductDto>(s => s
                .Index("products")
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Category)
                        .Query(category)
                    )
                ));

            return response.IsValid ? response.Documents : Enumerable.Empty<ProductDto>();
        }

    }
}
