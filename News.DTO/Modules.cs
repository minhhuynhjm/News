using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DTO
{
    public class Modules
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public int Sort { get; set; }

        public bool Isactive { get; set; }
    }
}
