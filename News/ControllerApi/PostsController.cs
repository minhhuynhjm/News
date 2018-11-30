﻿using News.DTO;
using News.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace News.ControllerApi
{
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

        // GET: api/Posts/1
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _postsRepo.FindByIdMoblieAsync(id);

            return Ok(result);
        }

        // GET All Cagetories : api/Posts/Categories
        [Route("Categories")]
        [HttpGet]
        public async Task<IHttpActionResult> Categories()
        {
            var result = await _categoriesRepo.GetAllMoblieAsync();

            return Ok(result);
        }

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

    }
}