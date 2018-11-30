using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DTO
{
    public class Categories
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Decription { get; set; }

        public int Parent { get; set; }
    }
}
