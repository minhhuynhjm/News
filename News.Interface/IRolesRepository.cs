using News.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Roles>> GetAllAsync();

        Task<bool> CreateUserRolesAsync(UserRoles model);

        Task<bool> DeleteUserRolesAsync(int id);
    }
}
