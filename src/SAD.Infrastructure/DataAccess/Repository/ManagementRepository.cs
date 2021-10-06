using Newtonsoft.Json;
using SAD.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SAD.Infrastructure.DataAccess.Repository
{
    public class ManagementRepository : Repository, IManagementRepository
    {
  
        public IEnumerable<Management> GetAll(string filePath = null)
        {
            var jsonData = LoadData(filePath);

            if (string.IsNullOrEmpty(jsonData))
            {
                return null;
            }
            var properties = JsonConvert.DeserializeObject<List<Management>>(jsonData);
            return properties;
        }
    }
}
