using Dapper;
using News.Interface;
using News.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repositories
{
    public class ChartsRepository : IChartsRepository
    {
        public bool GetAll(out int user, out int post, out int comment)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@User", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Post", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Comment", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteNoneAsync("usp_Chart_ReadAll", parameters);

            user = parameters.Get<int>("@User");
            post = parameters.Get<int>("@Post");
            comment = parameters.Get<int>("@Comment");

            return result;
        }

        public bool GetToday(out int post, out int comment)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Post", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Comment", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteNoneAsync("usp_Chart_ReadToday", parameters);

            post = parameters.Get<int>("@Post");
            comment = parameters.Get<int>("@Comment");

            return result;
        }
    }
}
