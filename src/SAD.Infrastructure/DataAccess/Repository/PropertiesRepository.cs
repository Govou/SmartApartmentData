using Newtonsoft.Json;
using SAD.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SAD.Infrastructure.DataAccess.Repository
{
    public class PropertiesRepository : Repository, IPropertiesRepository 
    {
     
        public IEnumerable<MainProperty> GetAll(string filePath = null)
        {
            var jsonData = LoadData(filePath);

            if (string.IsNullOrEmpty(jsonData))
            {
                return null;
            }
            var properties = JsonConvert.DeserializeObject<List<MainProperty>>(jsonData);
            return properties;
        }

        
    }
}
