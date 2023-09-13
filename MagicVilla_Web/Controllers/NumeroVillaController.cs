using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_Web.Models.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Text.Json.Nodes;
using MagicVilla_Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Controllers
{
    public class NumeroVillaController : Controller
    {
        private readonly INumeroVillaService _numeroVillaService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public NumeroVillaController(INumeroVillaService numeroVillaService, IVillaService villaService, IMapper mapper)
        {
            _numeroVillaService = numeroVillaService;
            _villaService = villaService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexNumeroVilla()
        {
            List<NumeroVillaDto> numeroVillaList = new();
            var response = await _numeroVillaService.ObtenerTodos<APIResponse>();
            if (response != null && response.IsExistoso)
            {

                numeroVillaList = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado));
            }
            return View(numeroVillaList);
        }
        public async Task<IActionResult> CrearNumeroVilla()
        {

            NumeroVillaViewModel numeroVillaVM = new();
            var response = await _villaService.ObtenerTodos<APIResponse>();
            if (response != null && response.IsExistoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado)).
                                                                        Select(v => new SelectListItem
                                                                        { Text = v.Nombre,
                                                                            Value = v.Id.ToString() });
            }
            return View(numeroVillaVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response= await _numeroVillaService.Crear<APIResponse>(modelo.NumeroVilla);
                if (response != null && response.IsExistoso)
                {
                    TempData["exitoso"] = "Numero de Villa creada exitosamente";
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
                var res = await _villaService.ObtenerTodos<APIResponse>();
                if (res != null && res.IsExistoso)
                {
                    modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Resultado)).
                                                                            Select(v => new SelectListItem
                                                                            {
                                                                                Text = v.Nombre,
                                                                                Value = v.Id.ToString()
                                                                            });
                }                
            }
            return View(modelo);
        }
        public async Task<IActionResult> ActualizarNumeroVilla(int VillaNo)
        {
            NumeroVillaUpdateViewModel numeroVillaVM = new();
            var response = await _numeroVillaService.Obtener<APIResponse>(VillaNo);
            if (response!=null && response.IsExistoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaVM.NumeroVilla = _mapper.Map<NumeroVillaUpdateDto>(modelo);
            }
            response = await _villaService.ObtenerTodos<APIResponse>();
            if (response != null && response.IsExistoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado)).
                                                                        Select(v => new SelectListItem
                                                                        {
                                                                            Text = v.Nombre,
                                                                            Value = v.Id.ToString()
                                                                        });
                return View(numeroVillaVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarNumeroVilla(NumeroVillaUpdateViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Actualizar<APIResponse>(modelo.NumeroVilla);
                if (response != null && response.IsExistoso)
                {
                    TempData["exitoso"] = "Numero de Villa actualizada exitosamente";
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
                var res = await _villaService.ObtenerTodos<APIResponse>();
                if (res != null && res.IsExistoso)
                {
                    modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Resultado)).
                                                                            Select(v => new SelectListItem
                                                                            {
                                                                                Text = v.Nombre,
                                                                                Value = v.Id.ToString()
                                                                            });
                }
            }
            return View(modelo);
        }

        public async Task<IActionResult> RemoverNumeroVilla(int VillaNo)
        {
            NumeroVillaDeleteViewModel numeroVillaVM = new();
            var response = await _numeroVillaService.Obtener<APIResponse>(VillaNo);
            if (response != null && response.IsExistoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaVM.NumeroVilla = modelo;
            }
            response = await _villaService.ObtenerTodos<APIResponse>();
            if (response != null && response.IsExistoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Resultado)).
                                                                        Select(v => new SelectListItem
                                                                        {
                                                                            Text = v.Nombre,
                                                                            Value = v.Id.ToString()
                                                                        });
                return View(numeroVillaVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverNumeroVilla(NumeroVillaDeleteViewModel modelo)
        {
            var response = await _numeroVillaService.Remover<APIResponse>(modelo.NumeroVilla.VillaNo);
            if (response!=null && response.IsExistoso)
            {
                TempData["exitoso"] = "Numero de Villa eliminado exitosamente";
                return RedirectToAction(nameof(IndexNumeroVilla));
            }
            TempData["error"] = "Ocurrio un error al remover";
            return View(modelo);
        }

    }
}
