using SAD.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAD.Infrastructure.DataAccess.Repository
{
    public interface IManagementRepository : IRepository
    {
        IEnumerable<Management> GetAll(string filePath = null);
    }
}
