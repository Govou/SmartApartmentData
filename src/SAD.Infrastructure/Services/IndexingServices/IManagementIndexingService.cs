using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Infrastructure.Services.IndexingServices
{
    public interface IManagementIndexingService
    {
        Task UploadDocuments(string filePath);
    }
}
