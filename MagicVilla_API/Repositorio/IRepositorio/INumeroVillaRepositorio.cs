using MagicVilla_API.Modelos;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface INumeroVillaRepositorio : IRepositorio<Numerovilla>
    {
        Task<Numerovilla> Actualizar(Numerovilla entidad);
    }
}
