using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Core.Entidades;

namespace WebMotors.Core.Application
{
    public interface IAnuncioApplication
    {
        Task<Dictionary<string, string>> Incluir(Anuncio anuncio);
        Task<Dictionary<string, string>> Atualizar(Anuncio anuncio);
        Task<Dictionary<string, string>> Remover(Anuncio anuncio);
        Task<Anuncio> ObterPorId(int id);
        Task<List<Anuncio>> ObterTodos();
        Task<List<Marca>> ObterMarcas();
        Task<List<Modelo>> ObterModelosPorMarca(int marcaId);
        Task<List<Versao>> ObterVersoesPorModelo(int modeloId);
    }
}
