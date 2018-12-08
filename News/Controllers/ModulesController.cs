using AutoMapper;
using News.Common;
using News.DTO;
using News.Interface;
using News.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace News.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class ModulesController : Controller
    {
        private readonly IModulesRepository _modulesRepo;
        private readonly IMapper _mapper;

        public ModulesController(IModulesRepository modulesRepository, IMapper mapper)
        {
            _modulesRepo = modulesRepository;
            _mapper = mapper;
        }
        // GET: Modules
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = await _modulesRepo.GetAllAsync();

            return View(_mapper.Map<IEnumerable<ModuleViewModel>>(result));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ModuleViewModel vm)
        {
            var result = await _modulesRepo.CreateOrUpdateAsync(_mapper.Map<Modules>(vm));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _modulesRepo.FindByIdAsync(id);

            return View(_mapper.Map<ModuleViewModel>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var response = new ResponseContainer<string>();

            var result = await _modulesRepo.DeleteAsync(id);

            response.success = result;

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> _ListModules()
        {
            var result = await _modulesRepo.GetAllAsync();
            var data = result.Where(p => p.Isactive == true);

            return PartialView("_ListModules", _mapper.Map<IEnumerable<ModuleViewModel>>(data));
        }
    }
}