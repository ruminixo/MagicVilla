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

        public Task<T> Actualizar<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "/api/villa/" + dto.Id,
                Token = token
            });

        }

        public Task<T> Crear<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo=DS.APITipo.POST,
                Datos=dto,
                Url=_villaUrl + "/api/villa",
                Token = token
            } );
        }

        public Task<T> Obtener<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/villa/" + id,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.GET,                
                Url = _villaUrl + "/api/villa/",
                Token = token
            });
        }

        public Task<T> Remover<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "/api/villa/" + id,
                Token = token
            });
        }
    }
}
