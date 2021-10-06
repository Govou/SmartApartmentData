using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Infrastructure.Services.IndexingServices
{
    public interface IPropertyIndexingService
    {
        Task UploadDocuments(string filePath);
    }
}
