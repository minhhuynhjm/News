using AutoMapper;
using Microsoft.AspNet.Identity;
using News.Common;
using News.DTO;
using News.Interface;
using News.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostsRepository _postRepo;
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoryRepo;
        public PostsController(IPostsRepository postRepository, ICategoriesRepository categoryRepository, IMapper mapper)
        {
            _postRepo = postRepository;
            _categoryRepo = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            string path;
            string saveloc = "~/Image/";
            string relativeloc = "/Image/";
            string filename = file.FileName;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    path = Path.Combine(HttpContext.Server.MapPath(saveloc), Path.GetFileName(filename));
                    file.SaveAs(path);
                }
                catch (Exception e)
                {
                    return Json(new { location = Url.Content("~/Image/" + filename) }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { location = Url.Content("~/Image/" + filename) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { location = Url.Content("~/Image/" + filename) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var result = _postRepo.GetByPaging(searchValue, pageSize, skip, out int output);

                recordsTotal = output;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result });
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        #region Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var category = await _categoryRepo.GetAllAsync();
            ViewBag.CategoryID = new SelectList(category, "CategoryID", "CategoryName");

            PostViewModel model = new PostViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel vm, HttpPostedFileBase Image)
        {
            try
            {
                ModelState.Clear();
                var response = new ResponseContainer<string>();
                if (Image != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension = Path.GetExtension(Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss") + extension;
                    vm.Image = "~/Image/" + fileName;
                    Image.SaveAs(Path.Combine(Server.MapPath("~/Image"), fileName));
                }

                // set automatic datetime
                vm.PostDate = DateTime.Now.ToString();
                vm.PostModify = DateTime.Now.ToString();
                vm.PostAuthorId = int.Parse(User.Identity.GetUserId());

                var model = new Posts
                {
                    PostContent = vm.PostContent,
                    PostAuthorId = vm.PostAuthorId,
                    PostDecription = vm.PostDecription,
                    PostDate = vm.PostDate,
                    PostModify = vm.PostModify,
                    PostTitle = vm.PostTitle,
                    PostStatus = vm.PostStatus,
                    Image = vm.Image,
                    CategoryId = vm.CategoryId,
                    Tag = vm.Tag

                };
                if (!TryValidateModel(model))
                {
                    response.success = false;
                    return Json(response);
                }
                var create = await _postRepo.CreateAsync(model);

                response.success = create;

                return Json(response);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryRepo.GetAllAsync();
            ViewBag.CategoryID = new SelectList(category, "CategoryID", "CategoryName");

            var model = await _postRepo.FindByIdAsync(id);

            PostViewModel vm = new PostViewModel
            {
                PostId = model.PostId,
                PostContent = model.PostContent,
                PostAuthorId = int.Parse(User.Identity.GetUserId()),
                PostDecription = model.PostDecription,
                PostDate = model.PostDate,
                PostModify = model.PostModify,
                PostTitle = model.PostTitle,
                PostStatus = model.PostStatus,
                Image = model.Image,
                Tag = model.Tag,
                CategoryId = model.CategoryId
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PostViewModel vm, HttpPostedFileBase Image)
        {
            try
            {
                ModelState.Clear();
                var response = new ResponseContainer<string>();

                if (Image != null)
                {
                    string fullPath = Request.MapPath(vm.Image);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension = Path.GetExtension(Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss") + extension;
                    vm.Image = "~/Image/" + fileName;
                    Image.SaveAs(Path.Combine(Server.MapPath("~/Image"), fileName));
                }

                var model = new Posts
                {
                    PostId = vm.PostId,
                    PostContent = vm.PostContent,
                    PostAuthorId = 1,
                    PostDecription = vm.PostDecription,
                    PostDate = vm.PostDate,
                    PostModify = DateTime.Now.ToString(),
                    PostTitle = vm.PostTitle,
                    PostStatus = vm.PostStatus,
                    CategoryId = vm.CategoryId,
                    Image = vm.Image,
                    Tag = vm.Tag
                };
                if (!TryValidateModel(model))
                {
                    response.success = false;
                    return Json(response);
                }

                var update = await _postRepo.UpdateAsync(model);

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = new ResponseContainer<string>();

                var model = await _postRepo.FindByIdAsync(id);
                string fullPath = Request.MapPath(model.Image);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                var delete = await _postRepo.DeleteAsync(id);

                response.success = delete;

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public async Task<ActionResult> GetAllTag()
        {
            var get = await _postRepo.GetAllTagAsync();

            List<string> lst = new List<string>();
            int i = 0;
            foreach (var line in get)
            {
                if (line.Tag != null)
                {
                    string[] parts = line.Tag.Split(',');
                    lst.AddRange(parts);
                    i++;
                }
            }
            var listTag = lst.Distinct();

            return Json(listTag, JsonRequestBehavior.AllowGet);
        }
    }
}