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
    public class ModulesRepository : IModulesRepository
    {
        public async Task<Modules> CreateOrUpdateAsync(Modules model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", model.Id);
            parameters.Add("@Title", model.Title);
            parameters.Add("@Link", model.Link);
            parameters.Add("@Sort", model.Sort);
            parameters.Add("@Isactive", model.Isactive);

            var result = await AdapterPattern.ExecuteSingle<Modules>("usp_Modules_CreateOrUpdate", parameters);
            return result;
        }

        public async Task<IEnumerable<Modules>> GetAllAsync()
        {
            var result = await AdapterPattern.ExecuteList<Modules>("usp_Modules_ReadAll");
            return result;
        }
    }
}
