using AutoMapper;
using Microsoft.AspNet.Identity;
using News.DTO;
using News.Interface;
using News.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class ClientController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostsRepository _postRepo;
        private readonly ICommentsRepository _commentRepo;
        private readonly IUsersRepository _usersRepo;


        public ClientController(
            IPostsRepository postRepository,
            ICommentsRepository commentRepository,
            IUsersRepository usersRepository,
            IMapper mapper)
        {
            _postRepo = postRepository;
            _commentRepo = commentRepository;
            _usersRepo = usersRepository;
            _mapper = mapper;
        }

        // GET: Client
        public async Task<ActionResult> Index()
        {
            // Two image 750x450
            var background = ConfigurationManager.AppSettings["Background"];
            var technical = ConfigurationManager.AppSettings["Technical"];

            var news = ConfigurationManager.AppSettings["News"];

            var data = await _postRepo.GetAllAsync();
            var result = data.Where(p => p.PostStatus == true);

            ViewBag.Background = result.Where(p => p.CategoryName == background).Take(2);
            ViewBag.Technical = result.Where(p => p.CategoryName == technical).Take(6);
            ViewBag.News = result.Where(p => p.CategoryName == news).Take(1);
            ViewBag.Slider = result.Where(p => p.CategoryName == news).Skip(1).Take(6);
            ViewBag.Featured = result.Skip(4).Take(3);
            ViewBag.Recent = result.Take(4);

            return View();
        }

        public async Task<ActionResult> Detail(int id)
        {
            var get = await _postRepo.FindByIdAsync(id);

            return View(_mapper.Map<PostViewModel>(get));
        }

        public async Task<ActionResult> _RightLayout()
        {
            var result = await _postRepo.GetAllAsync();
            ViewBag.Recent = result.Take(4);

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

            ViewBag.Tag = listTag.Take(10);

            return PartialView("_RightLayout");
        }

        public ActionResult Tags(string id, int? page)
        {
            int pageNumber = (page ?? 1);
            var pageSize = 5;
            ViewBag.Tag = id;

            var result = _postRepo.GetByTagAsync(id, pageNumber, pageSize, out int totalRows);


            var get = _mapper.Map<IEnumerable<PostList>, IEnumerable<PostViewModel>>(result);

            IPagedList<PostViewModel> pageOrders = new StaticPagedList<PostViewModel>(get, pageNumber, pageSize, totalRows);

            return View(pageOrders);
        }

        public ActionResult Search(string title, int? page)
        {
            int pageNumber = (page ?? 1);
            var pageSize = 5;
            ViewBag.Title = title;

            var result = _postRepo.GetByTitleAsync(title, pageNumber, pageSize, out int totalRows);


            var get = _mapper.Map<IEnumerable<PostList>, IEnumerable<PostViewModel>>(result);

            IPagedList<PostViewModel> pageOrders = new StaticPagedList<PostViewModel>(get, pageNumber, pageSize, totalRows);

            return PartialView(pageOrders);
        }

        public ActionResult Categories(int id, int? page)
        {
            int pageNumber = (page ?? 1);
            var pageSize = 5;
            ViewBag.CategoryId = id;

            var result = _postRepo.GetByCategoryIdAsync(id, pageNumber, pageSize, out int totalRows);


            var get = _mapper.Map<IEnumerable<PostList>, IEnumerable<PostViewModel>>(result);

            IPagedList<PostViewModel> pageOrders = new StaticPagedList<PostViewModel>(get, pageNumber, pageSize, totalRows);

            return View(pageOrders);
        }


        [HttpGet]
        public async Task<ActionResult> ShowUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = int.Parse(User.Identity.GetUserId());

                var user = await _usersRepo.FindByIdAsync(id);

                return Json(user, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        #region ProfileClient
        [HttpGet]
        public async Task<ActionResult> ProfileClient()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = int.Parse(User.Identity.GetUserId());

                var user = await _usersRepo.FindByIdAsync(id);

                var model = new UserViewModel();

                model = _mapper.Map<UserViewModel>(user);

                return View(model);

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ProfileClient(UserViewModel vm, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                string extension = Path.GetExtension(Image.FileName);
                fileName = fileName + DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss") + extension;
                vm.Image = "/Image/" + fileName;
                Image.SaveAs(Path.Combine(Server.MapPath("~/Image"), fileName));
            }

            var model = _mapper.Map<Users>(vm);

            var post = await _usersRepo.UpdateAsync(model);

            return View(vm);
        }
        #endregion

        #region Load Comment Detail
        public async Task<ActionResult> DetailCommentPartial(int id)
        {
            var data = await _commentRepo.FindByPostIdAsync(id);
            var model = new List<CommentViewModel>();
            foreach (var item in data)
            {
                var temp = new CommentViewModel
                {
                    CommentId = item.CommentId,
                    CommentAuthor = item.CommentAuthor,
                    CommentAuthorEmail = item.CommentAuthorEmail,
                    CommentContent = item.CommentContent,
                    CommentDate = item.CommentDate,
                    CommentParent = item.CommentParent,
                    Image = item.Image
                };
                model.Add(temp);
            }
            return PartialView("DetailCommentPartial", model);
        }
        #endregion

        #region Comment Parent Partial
        public async Task<ActionResult> AddCommentParentPartial(CommentViewModel vm)
        {
            if (User.Identity.IsAuthenticated)
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
                var create = _commentRepo.CreateAsync(model, out int Id);

                vm.CommentId = Id;
                vm.CommentAuthor = user.FullName;
                vm.CommentDate = DateTime.Now.ToString();
                vm.Image = user.Image;
                return PartialView("AddCommentParentPartial", vm);
            }
            else
            {
                return Json(null);
            }

        }
        #endregion

        #region Comment Child Partial
        public async Task<ActionResult> AddCommentChildPartial(CommentViewModel vm)
        {
            if (User.Identity.IsAuthenticated)
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
                    CommentParent = vm.CommentParent,
                    PostId = vm.PostId
                };
                var create = _commentRepo.CreateAsync(model, out int Id);

                vm.CommentId = Id;
                vm.CommentAuthor = user.FullName;
                vm.CommentDate = DateTime.Now.ToString();
                vm.Image = user.Image;

                return PartialView("AddCommentChildPartial", vm);
            }
            else
            {
                return Json(null);
            }

        }
        #endregion
    }
}