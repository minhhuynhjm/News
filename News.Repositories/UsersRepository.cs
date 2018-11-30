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
    public class UsersRepository : IUsersRepository
    {

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            var result = await AdapterPattern.ExecuteList<Users>("sp_AspNetUsers_ReadAll");
            return result;
        }

        public async Task<Users> FindByIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var result = await AdapterPattern.ExecuteSingle<Users>("sp_AspNetUsers_ReadById", parameters);
            return result;
        }

        public async Task<bool> UpdateAsync(Users model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", model.Id);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@UserName", model.UserName);
            parameters.Add("@Email", model.Email);
            parameters.Add("@PhoneNumber", model.PhoneNumber);
            parameters.Add("@Image", model.Image);

            var result = await AdapterPattern.ExecuteSingle<Users>("sp_AspNetUsers_Update", parameters);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var result = await AdapterPattern.Execute("sp_AspNetUsers_Delete", parameters);
            return result;
        }
    }
}
