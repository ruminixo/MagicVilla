﻿using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villaList));
        }
        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer villa con la id" + id);
                return BadRequest();
            }
           // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
           var villa= await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<VillaDTO>> CrearVilla([FromBody] VillaCreateDTO createDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _db.Villas.FirstOrDefaultAsync(v=>v.Nombre.ToLower() == createDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }
            Villa modelo = _mapper.Map<Villa>(createDto);       
            
           await _db.Villas.AddAsync(modelo);
           await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new {id=modelo.Id,modelo});
        }
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <IActionResult> DeleteVilla(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var villa=await _db.Villas.FirstOrDefaultAsync(v=>v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
           await _db.SaveChangesAsync();
            return NoContent(); 
        }
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task< IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            if (updateDTO == null || id!= updateDTO.Id )
            {
                return BadRequest();
            }
            Villa modelo =_mapper.Map<Villa>(updateDTO);

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> pathDto)
        {
            if (pathDto == null || id ==0)
            {
                return BadRequest();
            }
            var villa =await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            VillaUpdateDTO villaDto=_mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)  return BadRequest(); 

            pathDto.ApplyTo(villaDto, ModelState);
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }
            Villa modelo= _mapper.Map<Villa>(villaDto);
            
            _db.Villas.Update(modelo);
           await _db.SaveChangesAsync();
            
            return NoContent();

        }



    }
}
