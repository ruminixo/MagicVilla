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
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio numeroRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response= new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Numero villas");

                IEnumerable<NumeroVilla> NumerovillaList = await _numeroRepo.ObtenerTodos(incluirPropiedades: "Villa");
                
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(NumerovillaList);
                _response.statusCode = HttpStatusCode.OK;
                
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExistoso = false;   
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero villa con la id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExistoso=false;
                    return BadRequest(_response);
                }
                var Numerovilla = await _numeroRepo.Obtener(v => v.VillaNo == id , incluirPropiedades: "Villa");
                if (Numerovilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExistoso=false; 
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroVillaDto>(Numerovilla);
                _response.statusCode = HttpStatusCode.OK;
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
        public async Task< ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El numero de villa ya existe!");
                    return BadRequest(ModelState);
                }
                if (await _villaRepo.Obtener(v=>v.Id==createDto.VillaId)==null)
                {
                    ModelState.AddModelError("ErrorMessages", "El id de la villa no existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                
                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);
                modelo.FechaCreacion=DateTime.Now;  
                modelo.FechaActualizacion=DateTime.Now;
                await _numeroRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExistoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpDelete("{id:int}")]
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
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Numerovilla = await _numeroRepo.Obtener(v => v.VillaNo == id);
                if (Numerovilla == null)
                {
                    _response.IsExistoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
               await _numeroRepo.Remover(Numerovilla);
                _response.statusCode=HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExistoso = false;
                _response.ErrorMessages=new List<string> { ex.ToString() } ;
            }
            return BadRequest(_response);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task< IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaDto updateDTO)
        {
            if (updateDTO == null || id!= updateDTO.VillaNo )
            {
                _response.IsExistoso=false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (await _villaRepo.Obtener(V=>V.Id  == updateDTO.VillaId )==null)
            {
                ModelState.AddModelError("ErrorMessages", "El id de la villa no existe");
                return BadRequest(ModelState);
            }
            NumeroVilla modelo =_mapper.Map<NumeroVilla>(updateDTO);
            await _numeroRepo.Actualizar(modelo);
            _response.statusCode=HttpStatusCode.NoContent;
            
            return Ok(_response);

        }





    }
}
