using Nest;
using SAD.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Infrastructure.Services.IndexingServices
{
    public class PropertyIndexingService : IPropertyIndexingService
    {

        private readonly IElasticClient _elasticClient;
        private readonly IPropertiesRepository _propRepo;
        public PropertyIndexingService(IElasticClient elasticClient, IPropertiesRepository propRepo)
        {
            _elasticClient = elasticClient;
            _propRepo = propRepo;
        }

        public async Task UploadDocuments(string filePath)
        {
            var properties = _propRepo.GetAll(filePath);

            _elasticClient.Indices.Create("smartpropertiesdata_v2", b => b.
            Settings(s => s.Analysis(d => d.Analyzers(a => a.
            Standard("std_english", sw => sw.StopWords("_english_")))))
            .Map(p => p.AutoMap()));

            var response = await _elasticClient.IndexManyAsync(properties, "smartpropertiesdata_v2");
        }
    }
}
