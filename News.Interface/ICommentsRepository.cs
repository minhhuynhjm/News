using News.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comments>> GetAllAsync();

        Tuple<IEnumerable<Comments>, int> GetPagingAsync(int take, int skip, string keyword, string sortColumn, string sortColumnDirection, int output = 0);

        Task<Comments> FindByIdAsync(int id);

        Task<IEnumerable<Comments>> FindByPostIdAsync(int id);

        bool CreateAsync(Comments model, out int CommentId);

        Task<bool> DeleteAsync(int id);

        Task<Comments> UpdateAsync(Comments model);
    }
}
