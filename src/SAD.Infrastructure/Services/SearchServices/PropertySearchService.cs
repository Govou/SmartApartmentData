using Nest;
using SAD.Core.Models;
using SAD.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Infrastructure.Services.SearchServices
{
    public class PropertySearchService : IPropertySearchService
    {
        private readonly IElasticClient _elasticClient;
        public PropertySearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<IEnumerable<MainProperty>> Search(string searchText, int pageSize)
        {
            var response = await _elasticClient.SearchAsync<MainProperty>(s => s
                        .Index("smartpropertiesdata_v2")
                        .Query(q =>
                        q.Match(m => m.Field(f => f.property.market)
                         .Query(searchText)
                         .Boost(5)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))

                        ||

                         q.Match(m => m.Field(f => f.property.name)
                         .Query(searchText)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))

                        ||

                        q.Match(m => m.Field(f => f.property.formerName)
                         .Query(searchText)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))

                        ||

                        q.Match(m => m.Field(f => f.property.streetAddress)
                         .Query(searchText)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))

                        ||

                        q.Match(m => m.Field(f => f.property.city)
                         .Query(searchText)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))

                        ||

                        q.Match(m => m.Field(f => f.property.state)
                         .Query(searchText)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))
                        )
                         .Size(25));

            return response?.Documents?.ToList();

        }
    }
}
