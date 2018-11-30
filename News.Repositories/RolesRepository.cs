using Dapper;
using News.DTO;
using News.Interface;
using News.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        public async Task<IEnumerable<Roles>> GetAllAsync()
        {
            var result = await AdapterPattern.ExecuteList<Roles>("sp_AspNetRoles_ReadAll");
            return result;
        }

        public async Task<bool> CreateUserRolesAsync(UserRoles model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@UserId", model.UserId);
            parameters.Add("@RoleId", model.RoleId);

            var result = await AdapterPattern.Execute("sp_AspNetUserRoles_Create", parameters);
            return result;
        }

        public async Task<bool> DeleteUserRolesAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", id);

            var result = await AdapterPattern.Execute("sp_AspNetUserRoles_Delete", parameters);
            return result;
        }
    }
}
