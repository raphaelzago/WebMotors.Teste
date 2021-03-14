using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMotors.Core.Application;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMotors.Core.Entidades;
using System.Collections.Generic;

namespace WebMotors.Web.Controllers
{
    public class AnuncioController : Controller
    {
        private readonly IAnuncioApplication anuncioApplication;
        public AnuncioController(IAnuncioApplication anuncioApplication)
        {
            this.anuncioApplication = anuncioApplication;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var anuncios = await anuncioApplication.ObterTodos();

                return View(anuncios);
            }
            catch (System.Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            try
            {
                var marcas = await anuncioApplication.ObterMarcas();
                ViewBag.ListaDeMarcas = marcas.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });
            }
            catch (System.Exception)
            {
                //TODO tratar exceção e logar erro.
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Anuncio anuncio)
        {
            try
            {
                Dictionary<string, string> erros = await anuncioApplication.Incluir(anuncio);

                if (erros.Any())
                {
                    foreach (var item in erros)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }

                    var marcas = await anuncioApplication.ObterMarcas();
                    ViewBag.ListaDeMarcas = marcas.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                    var modelos = await anuncioApplication.ObterModelosPorMarca(anuncio.MarcaId);
                    ViewBag.ListaDeModelos = modelos.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                    var versoes = await anuncioApplication.ObterVersoesPorModelo(anuncio.ModeloId);
                    ViewBag.ListaDeVersoes = versoes.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                    return View(anuncio);
                }

            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Ocorreu um erro inesperado.");
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            try
            {
                var anuncio = await anuncioApplication.ObterPorId(id);

                var marcas = await anuncioApplication.ObterMarcas();
                var marca = marcas.FirstOrDefault(m => m.Name == anuncio.Marca);
                ViewBag.ListaDeMarcas = marcas.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                var modelos = await anuncioApplication.ObterModelosPorMarca(marca.ID);
                var modelo = modelos.FirstOrDefault(m => m.Name == anuncio.Modelo);
                ViewBag.ListaDeModelos = modelos.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                var versoes = await anuncioApplication.ObterVersoesPorModelo(modelo.ID);
                var versao = versoes.FirstOrDefault(m => m.Name == anuncio.Versao);
                ViewBag.ListaDeVersoes = versoes.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                anuncio.MarcaId = marca.ID;
                anuncio.ModeloId = modelo.ID;
                anuncio.VersaoId = versao.ID;

                return View(anuncio);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Ocorreu um erro inesperado.");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(Anuncio anuncio)
        {
            try
            {
                Dictionary<string, string> erros = await anuncioApplication.Atualizar(anuncio);

                if (erros.Any())
                {
                    foreach (var item in erros)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }

                    var marcas = await anuncioApplication.ObterMarcas();
                    ViewBag.ListaDeMarcas = marcas.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                    var modelos = await anuncioApplication.ObterModelosPorMarca(anuncio.MarcaId);
                    ViewBag.ListaDeModelos = modelos.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                    var versoes = await anuncioApplication.ObterVersoesPorModelo(anuncio.ModeloId);
                    ViewBag.ListaDeVersoes = versoes.Select(m => new SelectListItem { Text = m.Name, Value = m.ID.ToString() });

                    return View(anuncio);
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Ocorreu um erro inesperado.");
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var anuncio = await anuncioApplication.ObterPorId(id);
                return View(anuncio);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Ocorreu um erro inesperado.");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Remover(Anuncio anuncio)
        {
            try
            {
                Dictionary<string, string> erros = await anuncioApplication.Remover(anuncio);

                if (erros.Any())
                {
                    foreach (var item in erros)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }

                    return View(anuncio);
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Ocorreu um erro inesperado.");
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ObterModelos(int MarcaId)
        {
            var modelos = await anuncioApplication.ObterModelosPorMarca(MarcaId);

            return Json(new { modelos });
        }

        [HttpGet]
        public async Task<IActionResult> ObterVersoes(int ModeloId)
        {
            var versoes = await anuncioApplication.ObterVersoesPorModelo(ModeloId);

            return Json(new { versoes });
        }
    }
}
