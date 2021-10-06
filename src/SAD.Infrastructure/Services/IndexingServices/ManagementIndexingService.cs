using Nest;
using SAD.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Infrastructure.Services.IndexingServices
{
    public class ManagementIndexingService : IManagementIndexingService
    {

        private readonly IElasticClient _elasticClient;
        private readonly IManagementRepository _mgmtRepo;
        public ManagementIndexingService(IElasticClient elasticClient, IManagementRepository mgmtRepo)
        {
            _elasticClient = elasticClient;
            _mgmtRepo = mgmtRepo;
        }

        public async Task UploadDocuments(string filePath)
        {
            var managements = _mgmtRepo.GetAll(filePath);

            _elasticClient.Indices.Create("smartmanagementdata_v2", b => b.
            Settings(s => s.Analysis(d => d.Analyzers(a => a.
            Standard("std_english", sw => sw.StopWords("_english_")))))
            .Map(p => p.AutoMap()));

            var response = await _elasticClient.IndexManyAsync(managements, "smartmanagementdata_v2");
        }
    }
}
