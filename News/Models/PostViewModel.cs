using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        [Display(Name = "Date")]
        public string PostDate { get; set; }

        [Display(Name = "Date")]
        public string PostModify { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is not empty.")]
        public string PostTitle { get; set; }

        [Display(Name = "Decription")]
        [Required(ErrorMessage = "Decription is not empty.")]
        public string PostDecription { get; set; }

        [Display(Name = "Content")]
        [AllowHtml]
        [Required(ErrorMessage = "Content is not empty.")]
        public string PostContent { get; set; }

        [Display(Name = "Status")]
        public bool PostStatus { get; set; }

        [Display(Name = "Allow Comment")]
        public bool CommentStatus { get; set; }

        public string Image { get; set; }

        [Display(Name = "Tag")]
        [Required(ErrorMessage = "Tag is not empty.")]
        public string Tag { get; set; }

        [Display(Name = "Author")]
        public int PostAuthorId { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is not empty.")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Author")]
        public string UserName { get; set; }

        public PostViewModel()
        {
            Image = "~/image/addimage.png";
        }

    }
}