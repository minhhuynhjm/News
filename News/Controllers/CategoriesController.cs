using AutoMapper;
using News.DTO;
using News.Interface;
using News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private List<CategoryTreeViewModel> _categoryList = new List<CategoryTreeViewModel>();
        private readonly ICategoriesRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesRepository categoryRepository, IMapper mapper)
        {
            _categoryRepo = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var get = await _categoryRepo.GetAllAsync();

            //ViewBag.Category = data;

            List<CategoryTreeViewModel> listData = new List<CategoryTreeViewModel>();
            foreach (var item in get)
            {
                var data = new CategoryTreeViewModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Decription = item.Decription,
                    Parent = item.Parent
                };
                listData.Add(data);
            }
            var categoryList = CreateStructure(0, listData);

            return View(categoryList);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var categoryId = int.Parse(RouteData.Values["id"].ToString());
            string parentCategoryName;
            if (categoryId == 0)
            {
                parentCategoryName = "No parent category";
            }
            else
            {
                var getById = await _categoryRepo.FindByIdAsync(categoryId);
                parentCategoryName = getById?.CategoryName ?? "No parent category";
            }
            ViewBag.Parent = categoryId;
            ViewBag.ParentCategoryName = parentCategoryName;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryViewModel vm)
        {
            var model = new Categories
            {
                CategoryName = vm.CategoryName,
                Decription = vm.Decription,
                Parent = vm.Parent
            };

            var data = await _categoryRepo.CreateAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var model = await _categoryRepo.FindByIdAsync(id);
            var vm = new CategoryTreeViewModel
            {
                CategoryId = id,
                CategoryName = model.CategoryName,
                Decription = model.Decription,
                Parent = model.Parent,
                CategoryList = await GetCategoryList(id, model.Parent),
                SelectedParentCategoryId = model.Parent
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Update(CategoryTreeViewModel vm)
        {
            var model = new Categories
            {
                CategoryId = vm.CategoryId,
                CategoryName = vm.CategoryName,
                Decription = vm.Decription,
                Parent = vm.SelectedParentCategoryId
            };

            var post = await _categoryRepo.UpdateAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            var model = await _categoryRepo.FindByIdAsync(id);

            var temp = new CategoryViewModel
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Decription = model.Decription,
                Parent = model.Parent
            };

            return View(temp);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _categoryRepo.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private List<CategoryTreeViewModel> CreateStructure(int parent, List<CategoryTreeViewModel> source, int level = 0)
        {
            return (from category in source
                    where category.Parent == parent
                    select new CategoryTreeViewModel()
                    {
                        CategoryName = category.CategoryName,
                        CategoryId = category.CategoryId,
                        ChildCategory = CreateStructure(category.CategoryId, source, level + 1).ToList(),
                        Parent = category.Parent,
                        Level = level
                    }).ToList();
        }

        private async Task<SelectList> GetCategoryList(int currentId, int ParentID)
        {
            var get = await _categoryRepo.GetAllAsync();
            List<CategoryTreeViewModel> listData = new List<CategoryTreeViewModel>();
            foreach (var item in get)
            {
                var data = new CategoryTreeViewModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Decription = item.Decription,
                    Parent = item.Parent
                };
                listData.Add(data);
            }
            var CategoryList = CreateStructure(0, listData);
            var lookingFor = CategoryList.Select(item => CategoryTreeViewModel.Find(item, currentId)).FirstOrDefault(category => category != null);
            var parent = CategoryList.Select(item => CategoryTreeViewModel.Find(item, lookingFor.Parent)).FirstOrDefault(category => category != null);
            if (parent != null)
            {
                parent.ChildCategory.Remove(lookingFor);
            }
            else
            {
                CategoryList.Remove(lookingFor);
            }
            var childList = GetAllChilds(CategoryList);
            var categories = childList.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.DisplayAsCategory
            });

            var list = categories.ToList();
            SelectListItem selected;
            if (parent != null)
            {
                selected = (from category in list
                            where category.Value == parent.CategoryId.ToString()
                            select category).First();
            }
            else
            {
                selected = null;
            }

            if (selected != null)
            {
                selected.Selected = true;
                list.Insert(0, new SelectListItem { Value = "0", Text = "No parent category" });
                ParentID = int.Parse(selected.Value);
            }
            else
            {
                list.Insert(0, new SelectListItem { Value = "0", Text = "No parent category", Selected = true });
                ParentID = 0;
            }

            return new SelectList(list, "Value", "Text");
        }

        public List<CategoryTreeViewModel> GetAllChilds(List<CategoryTreeViewModel> CategoryList)
        {

            foreach (var category in CategoryList)
            {
                _categoryList.Add(category);
                if (category.ChildCategory.Count > 0)
                {
                    GetAllChilds(category.ChildCategory);
                }
            }
            return _categoryList;
        }
    }
}