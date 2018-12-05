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
    public class AppSettingsRepository : IAppSettingsRepository
    {

        public async Task<AppSettings> CreateOrUpdateAsync(AppSettings model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", model.Id);
            parameters.Add("@Name", model.Name);
            parameters.Add("@Description", model.Description);
            parameters.Add("@Logo", model.Logo);
            parameters.Add("@Company", model.Company);
            parameters.Add("@Address", model.Address);
            parameters.Add("@Phone", model.Phone);
            parameters.Add("@Email", model.Email);
            parameters.Add("@Website", model.Website);

            var result = await AdapterPattern.ExecuteSingle<AppSettings>("usp_AppSettings_CreateOrUpdate", parameters);
            return result;
        }

        public async Task<AppSettings> GetAllAsync()
        {
            var result = await AdapterPattern.ExecuteList<AppSettings>("usp_AppSettings_ReadAll");
            return result.SingleOrDefault();
        }
    }
}
