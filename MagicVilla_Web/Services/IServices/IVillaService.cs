using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> ObtenerTodos<T>();
        Task<T> Obtener <T>(int  id);
        Task<T> Crear<T>(VillaCreateDTO dTO);
        Task<T> Actualizar<T>(VillaUpdateDTO dto);
        Task<T> Remover<T>(int id);
    }
}
