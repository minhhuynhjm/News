using AutoMapper;
using Microsoft.AspNet.Identity;
using News.Common;
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
    public class CommentsController : Controller
    {
        private readonly ICommentsRepository _commentRepo;
        private readonly IUsersRepository _usersRepo;
        private readonly IMapper _mapper;

        public CommentsController(ICommentsRepository commentRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _commentRepo = commentRepository;
            _usersRepo = usersRepository;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region LoadData
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

                var result = _commentRepo.GetPagingAsync(pageSize, skip, searchValue, sortColumn, sortColumnDir);

                recordsTotal = result.Item2;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result.Item1 });
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        #endregion

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CommentViewModel vm)
        {
            var id = int.Parse(User.Identity.GetUserId());
            var user = await _usersRepo.FindByIdAsync(id);

            var model = new Comments
            {
                UserId = user.Id,
                CommentAuthor = user.FullName,
                CommentAuthorEmail = user.Email,
                CommentDate = DateTime.Now.ToString(),
                CommentContent = vm.CommentContent,
                Status = false,
                CommentParent = 0,
                PostId = vm.PostId
            };

            var result = _commentRepo.CreateAsync(model, out int Id);

            return Json(new { model, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            var response = new ResponseContainer<string>();
            try
            {
                var result = await _commentRepo.FindByIdAsync(id);
                result.Status = true;

                var update = await _commentRepo.UpdateAsync(result);

                return Json(response);
            }
            catch (Exception)
            {
                response.success = false;
                return Json(response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var response = new ResponseContainer<string>();
            try
            {
                var result = await _commentRepo.FindByIdAsync(id);

                return View(_mapper.Map<CommentViewModel>(result));
            }
            catch (Exception)
            {
                response.success = false;
                return Json(response);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = new ResponseContainer<string>();

            var delete = await _commentRepo.DeleteAsync(id);

            response.success = delete;

            return Json(response);
        }
    }
}