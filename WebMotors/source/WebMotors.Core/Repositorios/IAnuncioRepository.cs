using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Core.Entidades;

namespace WebMotors.Core.Repositorios
{
    public interface IAnuncioRepository 
    {
        Task<bool> Incluir(Anuncio anuncio);
        Task<bool> Atualizar(Anuncio anuncio);
        Task<bool> Remover(Anuncio anuncio); 
        Task<Anuncio> ObterPorId(int id);
        Task<List<Anuncio>> ObterTodos();
        Task<Anuncio> ObterPorMarcaModeloVersao(string marca, string modelo, string versao, int ano, int quilometragem);
    }
}
