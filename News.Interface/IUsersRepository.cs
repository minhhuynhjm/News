using News.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetAllAsync();

        Task<Users> FindByIdAsync(int id);

        Task<bool> UpdateAsync(Users model);

        Task<bool> DeleteAsync(int id);
    }
}
