using AutoMapper;
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
    }
}