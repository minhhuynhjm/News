using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class AppSettingViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is not empty.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is not empty.")]
        public string Description { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "Company is not empty.")]
        public string Company { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is not empty.")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone is not empty.")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is not empty.")]
        public string Email { get; set; }

        [Display(Name = "Website")]
        [Required(ErrorMessage = "Website is not empty.")]
        public string Website { get; set; }
    }
}