using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Core.Entidades;

namespace WebMotors.Core.Servicos
{
    public interface IMarcaServico
    {
        Task<List<Marca>> ObterMarcas();
    }
}
