using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interface
{
    public interface IChartsRepository
    {
        bool GetAll(out int user, out int post, out int comment);
    }
}
