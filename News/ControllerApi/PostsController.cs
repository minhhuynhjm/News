using Microsoft.AspNet.Identity;
using News.Common;
using News.DTO;
using News.Interface;
using News.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace News.ControllerApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Posts")]
    public class PostsController : ApiController
    {
        private readonly IPostsRepository _postsRepo;
        private readonly ICategoriesRepository _categoriesRepo;

        public PostsController(IPostsRepository postsRepository, ICategoriesRepository categoriesRepository)
        {
            _postsRepo = postsRepository;
            _categoriesRepo = categoriesRepository;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var host = ConfigurationManager.AppSettings["serverUrl"];

            var result = await _postsRepo.GetAllMobileAsync();

            var data = result.Select(item => new PostList
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                PostId = item.PostId,
                PostTitle = item.PostTitle,
                PostDecription = item.PostDecription,
                PostModify = item.PostModify,
                Image = host + item.Image.Substring(1)
            }).ToList();

            return Ok(data);
        }

        // GET: api/Posts/1
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var host = ConfigurationManager.AppSettings["serverUrl"];
            var result = await _postsRepo.FindByIdMobileAsync(id);
            result.Image = host + result.Image.Substring(1);
            return Ok(result);
        }

        [Route("Category/{id}/{page}")]
        [HttpGet]
        public IHttpActionResult Category(int id, int? page)
        {
            int pageNumber = (page ?? 1);
            var pageSize = 5;

            var listPosts = _postsRepo.GetByCategoryIdAsync(id, pageNumber, pageSize, out int totalRows);

            var result = new
            {
                listPosts,
                totalRows
            };
            return Ok(result); 
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            //var value = HttpContext.Current.Request.Form["PostTitle"];
            //NameValueCollection form = HttpContext.Current.Request.Form;
            PostViewModel vm = new PostViewModel
            {
                PostTitle = HttpContext.Current.Request.Form["PostTitle"],
                PostDecription = HttpContext.Current.Request.Form["PostDecription"],
                PostContent = HttpContext.Current.Request.Form["PostContent"],
                CategoryId = int.Parse(HttpContext.Current.Request.Form["CategoryId"]),
                PostStatus = Convert.ToBoolean(HttpContext.Current.Request.Form["PostStatus"]),
                Tag = HttpContext.Current.Request.Form["Tag"]
            };
            // Form to Model
            //Helper.FormToModel(form, vm);

            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var Image = httpRequest.Files[file];
                    string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension = Path.GetExtension(Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss") + extension;
                    vm.Image = "~/Image/" + fileName;
                    Image.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Image"), fileName));
                }
            }

            // set automatic datetime
            vm.PostDate = DateTime.Now.ToString();
            vm.PostModify = DateTime.Now.ToString();
            vm.PostAuthorId = 2;//int.Parse(User.Identity.GetUserId());
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
            var create = await _postsRepo.CreateAsync(model);

            return Ok(create);
        }

        // GET All Cagetories : api/Posts/Categories
        //[Route("Categories")]
        //[HttpGet]
        //public async Task<IHttpActionResult> Categories()
        //{
        //    var result = await _categoriesRepo.GetAllMobileAsync();

        //    return Ok(result);
        //}

        // GET All Posts by Cagetories : api/Posts/Categories/1
        [Route("Categories/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Categories(int id)
        {
            var host = ConfigurationManager.AppSettings["serverUrl"];

            var result = await _postsRepo.FindByCategoryIdAsync(id);

            var data = result.Select(item => new PostList
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                PostId = item.PostId,
                PostTitle = item.PostTitle,
                PostDecription = item.PostDecription,
                PostModify = item.PostModify,
                Image = host + item.Image.Substring(1)
            }).ToList();

            return Ok(data);
        }

        [Route("Categories")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(Categories model)
        {
            var result = await _categoriesRepo.CreateAsync(model);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
