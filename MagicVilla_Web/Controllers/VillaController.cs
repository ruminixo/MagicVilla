using AutoMapper;
using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> villaList = new();
            var response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExistoso)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado));
            }
            return View(villaList);
        }
        //get
        public async Task<IActionResult> CrearVilla()
        {
            return View();  
        }
        //pos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearVilla(VillaCreateDTO modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.Crear<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));
                if (response!=null && response.IsExistoso)
                {
                    TempData["exitoso"] = "Villa creada exitosamente";
                    return RedirectToAction(nameof(IndexVilla));
                }
                
            }
            return View(modelo);
        }
        public async Task<IActionResult> ActualizarVilla(int villaId)
        {
            var response=await _villaService.Obtener<APIResponse>(villaId, HttpContext.Session.GetString(DS.SessionToken));
            if (response!=null && response.IsExistoso)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Resultado));
                return View(_mapper.Map<VillaUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarVilla(VillaUpdateDTO modelo)
        {
           if (ModelState.IsValid)
            {
                var response=await _villaService.Actualizar<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));
                if (response!=null && response.IsExistoso)
                {
                    TempData["exitoso"] = "Villa actualizada exitosamente";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
           return View(modelo);
        }


        public async Task<IActionResult> RemoverVilla(int villaId)
        {
            var response = await _villaService.Obtener<APIResponse>(villaId, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExistoso)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Resultado));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverVilla(VillaDTO modelo)
        {

                var response = await _villaService.Remover<APIResponse>(modelo.Id, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExistoso)
                {
                TempData["exitoso"] = "Villa Eliminada exitosamente";
                return RedirectToAction(nameof(IndexVilla));
                }
                TempData["error"] = "Ocurrio un error al remover";
            return View(modelo);
        }

    }
}
