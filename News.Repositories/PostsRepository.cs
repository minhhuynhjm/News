using Dapper;
using News.DTO;
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
    public class PostsRepository : IPostsRepository
    {
        public async Task<IEnumerable<PostList>> GetAllAsync()
        {
            var result = await AdapterPattern.ExecuteList<PostList>("usp_Posts_ReadAll");
            return result;
        }

        public async Task<Posts> FindByIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PostId", id);

            var result = await AdapterPattern.ExecuteSingle<Posts>("usp_Posts_ReadById", parameters);
            return result;
        }

        public async Task<bool> CreateAsync(Posts model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@PostAuthorId", model.PostAuthorId);
            parameters.Add("@PostDate", model.PostDate);
            parameters.Add("@PostContent", model.PostContent);
            parameters.Add("@PostTitle", model.PostTitle);
            parameters.Add("@PostStatus", model.PostStatus);
            parameters.Add("@CommentStatus", model.CommentStatus);
            parameters.Add("@PostModify", model.PostModify);
            parameters.Add("@PostDecription", model.PostDecription);
            parameters.Add("@Image", model.Image);
            parameters.Add("@Tag", model.Tag);
            parameters.Add("@CategoryId", model.CategoryId);

            var result = await AdapterPattern.Execute("usp_Posts_Create", parameters);
            return result;
        }

        public async Task<Posts> UpdateAsync(Posts model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@PostId", model.PostId);
            parameters.Add("@PostAuthorId", model.PostAuthorId);
            parameters.Add("@PostDate", model.PostDate);
            parameters.Add("@PostContent", model.PostContent);
            parameters.Add("@PostTitle", model.PostTitle);
            parameters.Add("@PostStatus", model.PostStatus);
            parameters.Add("@CommentStatus", model.CommentStatus);
            parameters.Add("@PostModify", model.PostModify);
            parameters.Add("@PostDecription", model.PostDecription);
            parameters.Add("@Image", model.Image);
            parameters.Add("@Tag", model.Tag);
            parameters.Add("@CategoryId", model.CategoryId);

            var result = await AdapterPattern.ExecuteSingle<Posts>("usp_Posts_Update", parameters);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PostId", id);

            var result = await AdapterPattern.Execute("usp_Posts_Delete", parameters);
            return result;
        }

        public async Task<IEnumerable<PostTag>> GetAllTagAsync()
        {
            var result = await AdapterPattern.ExecuteList<PostTag>("usp_Posts_ReadAllTag");

            return result;
        }

        public IEnumerable<PostList> GetByCategoryIdAsync(int categoryId, int pageNumber, int pageSize, out int totalRows)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", categoryId);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteListNonAsync<PostList>("usp_Posts_ReadByCategoryId", parameters);

            totalRows = parameters.Get<int>("@TotalRows");

            return result;
        }

        public IEnumerable<PostList> GetByTagAsync(string tag, int pageNumber, int pageSize, out int totalRows)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Tag", tag);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteListNonAsync<PostList>("usp_Posts_ReadByTag", parameters);

            totalRows = parameters.Get<int>("@TotalRows");

            return result;
        }

        public IEnumerable<PostList> GetByTitleAsync(string title, int pageNumber, int pageSize, out int totalRows)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PostTitle", title);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteListNonAsync<PostList>("usp_Posts_ReadByTitle", parameters);

            totalRows = parameters.Get<int>("@TotalRows");

            return result;
        }

        public IEnumerable<PostList> GetByPaging(string search, int pageSize, int skip, out int output)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Title", search);
            //parameters.Add("@Sort", sort);
            //parameters.Add("@SortColumn", sortColumn);
            parameters.Add("@Take", pageSize);
            parameters.Add("@Skip", skip);
            parameters.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteListNonAsync<PostList>("usp_Posts_ReadByPaging", parameters);

            output = parameters.Get<int>("@Output");

            return result;
        }

        public async Task<IEnumerable<PostList>> FindByCategoryIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", id);

            var result = await AdapterPattern.ExecuteList<PostList>("usp_m_Posts_ReadByCategoryId", parameters);

            return result;
        }

        public async Task<Posts> FindByIdMoblieAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PostId", id);

            var result = await AdapterPattern.ExecuteSingle<Posts>("usp_m_Posts_ReadById", parameters);
            return result;
        }
    }
}
