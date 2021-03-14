using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Core.Entidades;

namespace WebMotors.Core.Servicos
{
    public interface IModeloServico
    {
        Task<List<Modelo>> ObterModelosPorMarca(int marcaId);
    }
}
