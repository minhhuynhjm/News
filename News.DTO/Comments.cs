using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DTO
{
    public class Comments
    {
        public int CommentId { get; set; }

        public string CommentAuthor { get; set; }

        public string CommentAuthorEmail { get; set; }

        public string CommentDate { get; set; }

        public string CommentContent { get; set; }

        public bool Status { get; set; }

        public int CommentParent { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public string FullName { get; set; }

        public string Image { get; set; }
    }
}
