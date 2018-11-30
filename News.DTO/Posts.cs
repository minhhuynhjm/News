using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DTO
{
    public class Posts
    {
        public int PostId { get; set; }

        public string PostDate { get; set; }

        public string PostModify { get; set; }

        public string PostTitle { get; set; }

        public string PostDecription { get; set; }

        public string PostContent { get; set; }

        public bool PostStatus { get; set; }

        public bool CommentStatus { get; set; }

        public string Image { get; set; }

        public string Tag { get; set; }

        public int PostAuthorId { get; set; }

        public int CategoryId { get; set; }
    }


    public class PostList
    {
        public int PostId { get; set; }

        public string PostDate { get; set; }

        public string PostModify { get; set; }

        public string PostTitle { get; set; }

        public string PostDecription { get; set; }

        public string PostContent { get; set; }

        public bool PostStatus { get; set; }

        public bool CommentStatus { get; set; }

        public string Image { get; set; }

        public string Tag { get; set; }

        public int PostAuthorId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string UserName { get; set; }
    }

    public class PostTag
    {
        public string Tag { get; set; }
    }

}
