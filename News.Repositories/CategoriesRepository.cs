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
    public class CategoriesRepository : ICategoriesRepository
    {
        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            var result = await AdapterPattern.ExecuteList<Categories>("usp_Categories_ReadAll");
            return result;
        }
        public async Task<Categories> FindByIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", id);

            var result = await AdapterPattern.ExecuteSingle<Categories>("usp_Categories_ReadById", parameters);
            return result;
        }

        public async Task<bool> CreateAsync(Categories model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CategoryName", model.CategoryName);
            parameters.Add("@Decription", model.Decription);
            parameters.Add("@Parent", model.Parent);

            var result = await AdapterPattern.Execute("usp_Categories_Create", parameters);
            return result;
        }

        public async Task<Categories> UpdateAsync(Categories model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CategoryId", model.CategoryId);
            parameters.Add("@CategoryName", model.CategoryName);
            parameters.Add("@Decription", model.Decription);
            parameters.Add("@Parent", model.Parent);

            var result = await AdapterPattern.ExecuteSingle<Categories>("usp_Categories_Update", parameters);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", id);

            var result = await AdapterPattern.Execute("usp_Categories_Delete", parameters);
            return result;
        }

        public async Task<IEnumerable<Categories>> GetAllMobileAsync()
        {
            var result = await AdapterPattern.ExecuteList<Categories>("usp_m_Categories_ReadAll");
            return result;
        }
    }
}
