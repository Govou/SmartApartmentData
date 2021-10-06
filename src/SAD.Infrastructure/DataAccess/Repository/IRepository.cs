using System;
using System.Collections.Generic;
using System.Text;

namespace SAD.Infrastructure.DataAccess.Repository
{
    public interface IRepository
    {
        string LoadData(string filePath = null);
    }
}
