using News.Common;
using News.DTO;
using News.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IChartsRepository _chartsRepo;

        public AdminController(IChartsRepository chartsRepository)
        {
            _chartsRepo = chartsRepository;
        }

        // GET: Dashboard
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Chart()
        {
            var response = new ResponseContainer<Charts>();


            var result = _chartsRepo.GetAll(out int user, out int post, out int comment);

            var model = new Charts
            {
                User = user,
                Comment = comment,
                Post = post
            };

            response.response = model;

            return Json(response);
        }
    }
}