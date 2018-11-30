using News.Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.String;

namespace News.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is not empty.")]
        public string CategoryName { get; set; }

        [Display(Name = "Decription")]
        [Required(ErrorMessage = "Decription is not empty.")]
        public string Decription { get; set; }

        public int Parent { get; set; }
    }

    public class CategoryTreeViewModel
    {
        [Display(Name = "Id")]
        public int CategoryId { get; set; }

        [NotMapped]
        public int Level { get; set; }

        [NotMapped]
        public string DisplayAsCategory => Concat("\xA0".Multiply(Level * 5), CategoryName);

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is not empty.")]
        public string CategoryName { get; set; }

        [Display(Name = "Decription")]
        [Required(ErrorMessage = "Decription is not empty.")]
        public string Decription { get; set; }

        public string Position { get; set; }

        [Display(Name = "Parent")]
        [Required(ErrorMessage = "Parent is not empty.")]
        public int Parent { get; set; }

        [NotMapped]
        public List<CategoryTreeViewModel> ChildCategory { get; set; }

        public CategoryTreeViewModel()
        {
            ChildCategory = new List<CategoryTreeViewModel>();
        }

        public static CategoryTreeViewModel Find(CategoryTreeViewModel category, int categoryId)
        {
            if (category == null) return null;
            return category.CategoryId == categoryId ? category : category.ChildCategory.Select(child => Find(child, categoryId)).FirstOrDefault(found => found != null);
        }

        public int CompareTo(object compare)
        {
            var next = compare as CategoryTreeViewModel;
            return Compare(CategoryName, next.CategoryName, StringComparison.Ordinal);
        }

        public CategoryTreeViewModel Category { get; set; }
        public SelectList CategoryList { get; set; }
        public int SelectedParentCategoryId { get; set; }
    }
}