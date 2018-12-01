using News.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetAllAsync();

        Task<IEnumerable<Categories>> GetAllMobileAsync();

        Task<Categories> FindByIdAsync(int id);

        Task<bool> CreateAsync(Categories model);

        Task<bool> DeleteAsync(int id);

        Task<Categories> UpdateAsync(Categories model);
    }
}
