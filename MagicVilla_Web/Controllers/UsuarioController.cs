﻿
using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{


    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Login(LoginRequestDto modelo)
        {
            var response = await _usuarioService.Login<APIResponse>(modelo);
            if (response != null && response.IsExistoso==true)
            {
                LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Resultado));

                //clains
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);  
                identity.AddClaim(new Claim(ClaimTypes.Name, loginResponse.Usuario.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, loginResponse.Usuario.Rol));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                //session
                HttpContext.Session.SetString(DS.SessionToken, loginResponse.Token);
                return RedirectToAction ("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                return View(modelo);
            }
        }
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Registrar(RegistroRequestDto modelo)
        {
            var response = await _usuarioService.Registrar<APIResponse>(modelo);
            if (response == null &&  response.IsExistoso)
            {
                return RedirectToAction("login");
            }
            return View();
        }   
        public async Task< IActionResult> Logout(LoginRequestDto modelo)
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(DS.SessionToken,"");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccesoDenegado(LoginRequestDto modelo)
        {
            return View();
        }

    }
}
