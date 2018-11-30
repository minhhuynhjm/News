using News.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface IPostsRepository
    {
        Task<IEnumerable<PostList>> GetAllAsync();

        Task<IEnumerable<PostTag>> GetAllTagAsync();

        IEnumerable<PostList> GetByPaging(string search, int pageSize, int skip, out int output);

        IEnumerable<PostList> GetByCategoryIdAsync(int categoryId, int pageNumber, int pageSize, out int totalRows);

        IEnumerable<PostList> GetByTagAsync(string tag, int pageNumber, int pageSize, out int totalRows);

        IEnumerable<PostList> GetByTitleAsync(string title, int pageNumber, int pageSize, out int totalRows);

        Task<Posts> FindByIdAsync(int id);

        Task<IEnumerable<PostList>> FindByCategoryIdAsync(int id);

        Task<Posts> FindByIdMoblieAsync(int id);

        Task<bool> CreateAsync(Posts model);

        Task<bool> DeleteAsync(int id);

        Task<Posts> UpdateAsync(Posts model);
    }
}
