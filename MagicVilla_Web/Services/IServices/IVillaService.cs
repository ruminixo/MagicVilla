using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> ObtenerTodos<T>(string token);
        Task<T> Obtener <T>(int id, string token);
        Task<T> Crear<T>(VillaCreateDTO dTO, string token);
        Task<T> Actualizar<T>(VillaUpdateDTO dto, string token);
        Task<T> Remover<T>(int id, string token);
    }
}
