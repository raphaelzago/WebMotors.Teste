using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMotors.Core.Entidades;

namespace WebMotors.Core.Servicos
{
    public interface IVersaoServico
    {
        Task<List<Versao>> ObterVersoesPorModelo(int modeloId);
    }
}
