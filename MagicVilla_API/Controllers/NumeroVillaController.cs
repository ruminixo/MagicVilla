using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MagicVilla_API.Repositorio.IRepositorio;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _NumeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio NumeroRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _NumeroRepo= NumeroRepo;
            _mapper = mapper;
            _response= new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult< APIResponse >> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Numero villas");
                IEnumerable<Numerovilla> NumerovillaList = await _NumeroRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(NumerovillaList);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExistoso = false;   
                _response.StatusCode=HttpStatusCode.OK;
            }
            return _response;
        }
        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero villa con la id" + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsExistoso=false;
                    return BadRequest(_response);
                }
                var Numerovilla = await _NumeroRepo.Obtener(v => v.VillaNo == id);
                if (Numerovilla == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsExistoso=false; 
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroVillaDto>(Numerovilla);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExistoso=false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _NumeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de villa ya existe!");
                    return BadRequest(ModelState);
                }
                if (await _villaRepo.Obtener(v=>v.Id==createDto.VillaId)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El id de la villa no existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                
                Numerovilla modelo = _mapper.Map<Numerovilla>(createDto);
                modelo.FechaCreacion=DateTime.Now;  
                modelo.FechaActualizacion=DateTime.Now;
                await _NumeroRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo, _response });
            }
            catch (Exception ex)
            {
                _response.IsExistoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExistoso=false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Numerovilla = await _NumeroRepo.Obtener(v => v.VillaNo == id);
                if (Numerovilla == null)
                {
                    _response.IsExistoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
               await _NumeroRepo.Remover(Numerovilla);
                _response.StatusCode=HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExistoso = false;
                _response.ErrorMessages=new List<string> { ex.ToString() } ;
            }
            return BadRequest(_response);
        }
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task< IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaDto updateDTO)
        {
            if (updateDTO == null || id!= updateDTO.VillaNo )
            {
                _response.IsExistoso=false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (await _villaRepo.Obtener(V=>V.Id  == updateDTO.VillaId )==null)
            {
                ModelState.AddModelError("Clave foranea", "El id de la villa no existe");
                return BadRequest(ModelState);
            }
            Numerovilla modelo =_mapper.Map<Numerovilla>(updateDTO);
            await _NumeroRepo.Actualizar(modelo);
            _response.StatusCode=HttpStatusCode.NoContent;
            
            return Ok(_response);

        }





    }
}
