using AutoMapper;
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
    public class AppSettingController : Controller
    {
        private readonly IAppSettingsRepository _appSettingsRepo;
        private readonly IMapper _mapper;

        public AppSettingController(IAppSettingsRepository appSettingsRepository, IMapper mapper)
        {
            _appSettingsRepo = appSettingsRepository;
            _mapper = mapper;
        }
        // GET: Setting
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var result =  await _appSettingsRepo.GetAllAsync();
          
            return View(_mapper.Map<AppSettingViewModel>(result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppSettingViewModel vm, HttpPostedFileBase Logo)
        {
            var response = new ResponseContainer<string>();

            if (Logo != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(Logo.FileName);
                string extension = Path.GetExtension(Logo.FileName);
                fileName = fileName + extension;
                vm.Logo = "/Image/" + fileName;
                Logo.SaveAs(Path.Combine(Server.MapPath("~/Image"), fileName));
            }

            var result = await _appSettingsRepo.CreateOrUpdateAsync(_mapper.Map<AppSettings>(vm));

            return Json(response);
        }

        public async Task<ActionResult> LoadInfo()
        {
            var result = await _appSettingsRepo.GetAllAsync();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}