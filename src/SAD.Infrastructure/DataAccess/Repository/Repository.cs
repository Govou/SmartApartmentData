using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SAD.Infrastructure.DataAccess.Repository
{
    public class Repository : IRepository
    {

        public string LoadData(string filePath = null)
        {
            var jsonStr = string.Empty;
            using (var str = new StreamReader(filePath))
            {
                jsonStr = str.ReadToEnd();
            }
            return jsonStr;
        }
    }
}
