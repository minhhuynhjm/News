using News.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface IAppSettingsRepository
    {
        Task<AppSettings> GetAllAsync();

        Task<AppSettings> CreateOrUpdateAsync(AppSettings model);
    }
}
