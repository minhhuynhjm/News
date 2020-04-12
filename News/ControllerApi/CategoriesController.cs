using News.DTO;
using News.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace News.ControllerApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesRepository _categoriesRepo;
        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepo = categoriesRepository;
        }

        // GET All Cagetories : api/Posts/Categories
        [HttpGet]
        public async Task<IHttpActionResult> Categories()
        {
            var result = await _categoriesRepo.GetAllMobileAsync();

            return Ok(result);
        }

        // GET All Posts by Cagetories : api/Categories/1
        [HttpGet]
        public async Task<IHttpActionResult> Categories(int id)
        {
            var host = ConfigurationManager.AppSettings["serverUrl"];

            var result = await _categoriesRepo.FindByIdAsync(id);

            //var data = result.Select(item => new PostList
            //{
            //    CategoryId = item.CategoryId,
            //    CategoryName = item.CategoryName,
            //    PostId = item.PostId,
            //    PostTitle = item.PostTitle,
            //    PostDecription = item.PostDecription,
            //    PostModify = item.PostModify,
            //    Image = host + item.Image.Substring(1)
            //}).ToList();

            return Ok(result);
        }

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
