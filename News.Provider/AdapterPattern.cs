using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Provider
{
    public static class AdapterPattern
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static async Task<T> ExecuteSingle<T>(string storedprocedure, DynamicParameters parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return await conn.QuerySingleAsync<T>(storedprocedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public static async Task<IEnumerable<T>> ExecuteList<T>(string storedprocedure, DynamicParameters parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<T>(storedprocedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public static async Task<bool> Execute(string storedprocedure, DynamicParameters parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                await conn.QueryAsync(storedprocedure, parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public static bool ExecuteNoneAsync(string storedprocedure, DynamicParameters parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                conn.Query(storedprocedure, parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public static IEnumerable<T> ExecuteListNonAsync<T>(string storedprocedure, DynamicParameters parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return conn.Query<T>(storedprocedure, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static async Task<T> ExecuteSingleTest<T>(string storedprocedure, DynamicParameters parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return await conn.ExecuteScalarAsync<T>(storedprocedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
