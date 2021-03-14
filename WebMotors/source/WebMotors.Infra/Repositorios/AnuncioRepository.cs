using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotors.Core.Entidades;
using WebMotors.Core.Repositorios;
using WebMotors.Infra.Contexto;

namespace WebMotors.Infra.Repositorios
{
    public class AnuncioRepository : IAnuncioRepository
    {
        private readonly WebMotorsContexto contexto;
        public AnuncioRepository(WebMotorsContexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> Incluir(Anuncio anuncio)
        {
            contexto.Anuncios.Add(anuncio);

            return await contexto.SaveChangesAsync() == 1;
        }
        public async Task<bool> Atualizar(Anuncio anuncio)
        {
            contexto.Entry(anuncio).State = EntityState.Modified;
            return await contexto.SaveChangesAsync() == 1;
        }
        public async Task<bool> Remover(Anuncio anuncio)
        {
            contexto.Entry(anuncio).State = EntityState.Deleted;
            return await contexto.SaveChangesAsync() == 1;
        }
        public async Task<Anuncio> ObterPorId(int id)
        {
            return await contexto.Anuncios.FindAsync(id);
        }
        public async Task<List<Anuncio>> ObterTodos()
        {
            return await contexto.Anuncios.ToListAsync();
        }
        public async Task<Anuncio> ObterPorMarcaModeloVersao(string marca, string modelo, string versao, int ano, int quilometragem)
        {
            var anuncioList = await contexto.Anuncios.Where(a => a.Marca == marca &&
                                                            a.Modelo == modelo &&
                                                            a.Versao == versao &&
                                                            a.Ano == ano &&
                                                            a.Quilometragem == quilometragem)
                                                            .ToListAsync();
            return anuncioList?.FirstOrDefault();
        }
    }
}
