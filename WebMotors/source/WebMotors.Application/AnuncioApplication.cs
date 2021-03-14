using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotors.Core.Application;
using WebMotors.Core.Entidades;
using WebMotors.Core.Repositorios;
using WebMotors.Core.Servicos;

namespace WebMotors.Application
{
    public class AnuncioApplication : IAnuncioApplication
    {
        private readonly IAnuncioRepository anuncioRepository;
        private readonly IMarcaServico marcaServico;
        private readonly IModeloServico modeloServico;
        private readonly IVersaoServico versaoServico;
        public AnuncioApplication(IAnuncioRepository anuncioRepository,
                                  IMarcaServico marcaServico,
                                  IModeloServico modeloServico,
                                  IVersaoServico versaoServico)
        {
            this.anuncioRepository = anuncioRepository;
            this.marcaServico = marcaServico;
            this.modeloServico = modeloServico;
            this.versaoServico = versaoServico;
        }

        public async Task<Dictionary<string, string>> Incluir(Anuncio anuncio)
        {
            Dictionary<string, string> erros = await ValidarAnuncio(anuncio);

            if (!erros.Any())
            {
                await anuncioRepository.Incluir(anuncio);
            }

            return erros;
        }
        public async Task<Dictionary<string, string>> Atualizar(Anuncio anuncio)
        {
            Dictionary<string, string> erros = await ValidarAnuncio(anuncio);

            if (!erros.Any())
            {
                var anuncioAtualizar = await anuncioRepository.ObterPorId(anuncio.Id);

                if (anuncioAtualizar == null)
                {
                    erros.Add("MarcaId", "Anúncio não encontrado.");
                    return erros;
                }

                anuncioAtualizar.Marca = anuncio.Marca;
                anuncioAtualizar.Modelo = anuncio.Modelo;
                anuncioAtualizar.Versao = anuncio.Versao;
                anuncioAtualizar.Ano = anuncio.Ano;
                anuncioAtualizar.Quilometragem = anuncio.Quilometragem;
                anuncioAtualizar.Observacao = anuncio.Observacao;

                await anuncioRepository.Atualizar(anuncioAtualizar);
            }

            return erros;
        }
        public async Task<Dictionary<string, string>> Remover(Anuncio anuncio)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();

            var anuncioRemover = await anuncioRepository.ObterPorId(anuncio.Id);
            if (anuncioRemover != null)
            {
                await anuncioRepository.Remover(anuncioRemover);
                return erros;
            }

            erros.Add("MarcaId", "Anúncio não encontrado.");
            return erros;
        }
        public async Task<List<Anuncio>> ObterTodos()
        {
            return await anuncioRepository.ObterTodos();
        }
        public async Task<Anuncio> ObterPorId(int id)
        {
            return await anuncioRepository.ObterPorId(id);
        }
        public async Task<List<Marca>> ObterMarcas()
        {
            return await marcaServico.ObterMarcas();
        }
        public async Task<List<Modelo>> ObterModelosPorMarca(int marcaId)
        {
            return await modeloServico.ObterModelosPorMarca(marcaId);
        }
        public async Task<List<Versao>> ObterVersoesPorModelo(int modeloId)
        {
            return await versaoServico.ObterVersoesPorModelo(modeloId);
        }
        private async Task<Dictionary<string, string>> ValidarAnuncio(Anuncio anuncio)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();

            var marcaList = await marcaServico.ObterMarcas();
            var modeloList = await modeloServico.ObterModelosPorMarca(anuncio.MarcaId);
            var versaoList = await versaoServico.ObterVersoesPorModelo(anuncio.ModeloId);

            var marca = marcaList.FirstOrDefault(m => m.ID == anuncio.MarcaId && m.Name == anuncio.Marca);
            var modelo = modeloList.FirstOrDefault(m => m.ID == anuncio.ModeloId && m.Name == anuncio.Modelo);
            var versao = versaoList.FirstOrDefault(m => m.ID == anuncio.VersaoId && m.Name == anuncio.Versao);
            var anuncioJaExiste = await anuncioRepository.ObterPorMarcaModeloVersao(anuncio.Marca,
                                                                                    anuncio.Modelo,
                                                                                    anuncio.Versao,
                                                                                    anuncio.Ano,
                                                                                    anuncio.Quilometragem);

            if (marca == null)
            {
                erros.Add("MarcaId", "Marca não encontrada.");
            }
            else if (anuncio.Marca?.Length > 45)
            {
                erros.Add("MarcaId", "Marca deve conter até 45 caracteres.");
            }
            else if (anuncioJaExiste != null)
            {
                erros.Add("MarcaId", "Anuncio já esta cadastrado.");
            }

            if (modelo == null)
            {
                erros.Add("ModeloId", "Modelo não encontrado.");
            }
            else if (anuncio.Modelo?.Length > 45)
            {
                erros.Add("ModeloId", "Modelo deve conter até 45 caracteres.");
            }

            if (versao == null)
            {
                erros.Add("VersaoId", "Versão não encontrada.");
            }
            else if (anuncio.Versao?.Length > 100)
            {
                erros.Add("VersaoId", "Versao deve conter até 45 caracteres.");
            }

            return erros;
        }
    }
}
