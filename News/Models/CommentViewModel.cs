using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class CommentViewModel
    {
        [Display(Name = "Id")]
        public int CommentId { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "Author is not empty.")]
        public string CommentAuthor { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is not empty.")]
        public string CommentAuthorEmail { get; set; }

        [Display(Name = "Date")]
        public string CommentDate { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Content is not empty.")]
        public string CommentContent { get; set; }
        public bool Status { get; set; }

        [Display(Name = "Comment Parent")]
        public int CommentParent { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "PostId is not empty.")]
        public int PostId { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Image Author")]
        public string Image { get; set; }

        [Display(Name = "Title")]
        public string PostTitle { get; set; }
    }
}