using SAD.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Infrastructure.Services.SearchServices
{
    public interface IManagementSearchService
    {
        Task<IEnumerable<Management>> Search(string searchText, int pageSize);

    }
}
