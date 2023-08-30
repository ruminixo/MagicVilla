﻿using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(_db.Villas.ToList());
        }
        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer villa con la id" + id);
                return BadRequest();
            }
           // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
           var villa=_db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CrearVilla([FromBody] VillaDTO villaDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.Villas.FirstOrDefault(v=>v.Nombre.ToLower() == villaDTO.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest();
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa modelo = new()
            {
                
                Nombre = villaDTO.Nombre,
                Detalle = villaDTO.Detalle,
                ImagenUrl = villaDTO.ImageUrl,
                Ocupantes = villaDTO.Ocupantes,
                Tarifa = villaDTO.Tarifa,
                MetrosCuadrados = villaDTO.MetroCuadrados,
                Amenidad = villaDTO.Amenidad

            };
            _db.Villas.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new {id=villaDTO.Id,villaDTO});
        }
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var villa=_db.Villas.FirstOrDefault(v=>v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent(); 
        }
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id!=villaDTO.Id )
            {
                return BadRequest();
            }
            // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            // villa.Nombre= villaDTO.Nombre;  
            // villa.Ocupantes= villaDTO.Ocupantes;
            //villa.MetroCuadrados=villaDTO.MetroCuadrados;
            Villa modelo = new()
            {
                Id = villaDTO.Id,
                Nombre = villaDTO.Nombre,
                Detalle = villaDTO.Detalle,
                ImagenUrl = villaDTO.ImageUrl,
                Ocupantes = villaDTO.Ocupantes,
                Tarifa = villaDTO.Tarifa,
                MetrosCuadrados = villaDTO.MetroCuadrados,
                Amenidad = villaDTO.Amenidad

            };
            _db.Villas.Update(modelo);
            _db.SaveChanges();
            return NoContent();

        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> pathDto)
        {
            if (pathDto == null || id ==0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);
            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImageUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetroCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };
            if (villaDTO == null)  return BadRequest(); 

            pathDto.ApplyTo(villaDTO, ModelState);
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }
            Villa modelo = new()
            {
                Id = villaDTO.Id,
                Nombre = villaDTO.Nombre,
                Detalle = villaDTO.Detalle,
                ImagenUrl = villaDTO.ImageUrl,
                Ocupantes = villaDTO.Ocupantes,
                Tarifa = villaDTO.Tarifa,
                MetrosCuadrados = villaDTO.MetroCuadrados,
                Amenidad = villaDTO.Amenidad
            };
            _db.Villas.Update(modelo);
            _db.SaveChanges();
            
            return NoContent();

        }



    }
}
