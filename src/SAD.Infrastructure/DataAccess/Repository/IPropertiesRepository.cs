using SAD.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAD.Infrastructure.DataAccess.Repository
{
    public interface IPropertiesRepository
    {
        IEnumerable<MainProperty> GetAll(string filePath = null);
    }
}
