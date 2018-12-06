using News.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface IModulesRepository
    {
        Task<IEnumerable<Modules>> GetAllAsync();
        Task<Modules> CreateOrUpdateAsync(Modules model);

        Task<Modules> FindByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

    }
}
