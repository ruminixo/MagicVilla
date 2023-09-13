using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _villaUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration): base (httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<String>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "/api/villa/" + dto.Id
            });

        }

        public Task<T> Crear<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo=DS.APITipo.POST,
                Datos=dto,
                Url=_villaUrl + "/api/villa"
            } );
        }

        public Task<T> Obtener<T>(int id)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/villa/" + id
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.GET,                
                Url = _villaUrl + "/api/villa/" 
            });
        }

        public Task<T> Remover<T>(int id)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "/api/villa/" + id
            });
        }
    }
}
