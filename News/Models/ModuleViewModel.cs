using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class ModuleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is not empty.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Link is not empty.")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Sort is not empty.")]
        public int Sort { get; set; }

        public bool Isactive { get; set; }
    }
}