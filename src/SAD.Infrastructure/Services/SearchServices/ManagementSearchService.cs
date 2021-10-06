using Nest;
using SAD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Infrastructure.Services.SearchServices
{
    public class ManagementSearchService : IManagementSearchService
    {

        private readonly IElasticClient _elasticClient;
        public ManagementSearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<IEnumerable<Management>> Search(string searchText, int pageSize)
        {
            var response = await _elasticClient.SearchAsync<Management>(s => s
                        .Index("smartmanagementdata_v2")
                        .Query(q =>
                        q.Match(m => m.Field(f => f.mgmt.market)
                         .Query(searchText)
                         .Boost(5)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))

                        ||

                         q.Match(m => m.Field(f => f.mgmt.name)
                         .Query(searchText)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))

                        ||

                        q.Match(m => m.Field(f => f.mgmt.state)
                         .Query(searchText)
                         .Fuzziness(Fuzziness.AutoLength(3, 6))
                        .Lenient()
                        .FuzzyTranspositions()
                        .MinimumShouldMatch(2)
                        .Operator(Operator.Or)
                        .Analyzer("std_english"))
                        )
                         .Size(pageSize));

            return response?.Documents?.ToList();
        }
    }
}
