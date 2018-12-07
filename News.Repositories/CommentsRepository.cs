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
    public class CommentsRepository : ICommentsRepository
    {
        public async Task<IEnumerable<Comments>> GetAllAsync()
        {
            var result = await AdapterPattern.ExecuteList<Comments>("usp_Comments_ReadAll");
            return result;
        }

        public async Task<Comments> FindByIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CommentId", id);

            var result = await AdapterPattern.ExecuteSingle<Comments>("usp_Comments_ReadById", parameters);
            return result;
        }

        public bool CreateAsync(Comments model, out int CommentId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CommentAuthor", model.CommentAuthor);
            parameters.Add("@CommentAuthorEmail", model.CommentAuthorEmail);
            parameters.Add("@CommentDate", model.CommentDate);
            parameters.Add("@CommentContent", model.CommentContent);
            parameters.Add("@Status", model.Status);
            parameters.Add("@CommentParent", model.CommentParent);
            parameters.Add("@UserId", model.UserId);
            parameters.Add("@PostId", model.PostId);
            parameters.Add("@CommentId", DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteNoneAsync("usp_Comments_Create", parameters);

            CommentId = parameters.Get<int>("@CommentId");

            return result;
        }

        public async Task<Comments> UpdateAsync(Comments model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CommentAuthor", model.CommentAuthor);
            parameters.Add("@CommentAuthorEmail", model.CommentAuthorEmail);
            parameters.Add("@CommentDate", model.CommentDate);
            parameters.Add("@CommentContent", model.CommentContent);
            parameters.Add("@Status", model.Status);
            parameters.Add("@CommentParent", model.CommentParent);
            parameters.Add("@UserId", model.UserId);
            parameters.Add("@PostId", model.PostId);
            parameters.Add("@CommentId", model.CommentId);

            var result = await AdapterPattern.ExecuteSingle<Comments>("usp_Comments_Update", parameters);
            return result;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CommentId", id);

            var result = await AdapterPattern.Execute("usp_Comments_Delete", parameters);
            return result;
        }

        public async Task<IEnumerable<Comments>> FindByPostIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var result = await AdapterPattern.ExecuteList<Comments>("usp_Comments_ReadByPostId", parameters);
            return result;
        }

        public Tuple<IEnumerable<Comments>, int> GetPagingAsync(int take, int skip, string keyword, string sortColumn, string sortColumnDirection, int output = 0)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Take", take);
            parameters.Add("@Skip", skip);
            parameters.Add("@Keyword", keyword);
            parameters.Add("@SortDataField", sortColumn);
            parameters.Add("@SortOrder", sortColumnDirection);
            parameters.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = AdapterPattern.ExecuteListNonAsync<Comments>("usp_Comments_ReadAllPaging", parameters);
            output = parameters.Get<int>("@Output");

            return Tuple.Create(result, output);
        }
    }
}
