using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {

        public readonly IHttpClientFactory _httpClient;
        private String _villaUrl;
        public UsuarioService(IHttpClientFactory httpClient, IConfiguration configuration):base (httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<String>("ServiceUrls:API_URL");

        }
        public Task<T> Login<T>(LoginRequestDto dto)
        {
            return SendAsync<T>(new APIRequrest()
            {
                 APITipo= DS.APITipo.POST,
                 Datos=dto,
                 Url= _villaUrl + "/api/v1/usuario/login"
            });

        }

        public Task<T> Registrar<T>(RegistroRequestDto dto)
        {
            return SendAsync<T>(new APIRequrest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl + "/api/v1/usuario/registrar"
            });
        }
    }
}
