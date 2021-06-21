using GestaoVendas.Cap03.Data;
using GestaoVendas.Cap03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoVendas.Cap03.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produtos/Create
        public ActionResult Incluir()
        {
                ViewBag.ListaDeCategorias = new SelectList(ProdutosDao.ListarCategorias(), "Id", "Descricao");

                return View();
        }

        // POST: Produtos/Incluir
        [HttpPost]
        public ActionResult Incluir(Produto produto, HttpPostedFileBase image)
        {
           
            try
            {

                if (!ModelState.IsValid)//verifica se o estado da pagina é valido
                {
                    return Incluir();
                }
                if (image is null) // verifica se a image for diferente de nulo
                {
                    throw new ArgumentNullException(nameof(image));// se a image nao for carrgada lança um exception
                }

                //se a image diferente de null entao fazer a captura de stream de bytes
                produto.MimeType = image.ContentType;
                       //um tipo           tipo
                byte[] bytes = new byte[image.ContentLength];
                                               // tamanho da imagem/ possa calcular o tamanhoem bites
                image.InputStream.Read(bytes, 0, image.ContentLength);
                //inputStream colocando dentro da VAR bytes[array] a imagem transformando em bytes alocando dentro de "bytes" e o tamanho informado 'contentlenght'

                //armazenar os bytes
                produto.Foto = bytes;

                ProdutosDao.IncluirProduto(produto);

                // TODO: Add insert logic here
                return RedirectToAction("Listar");
            }
            catch
            {
                return View();
            }
        }

        public FileResult BuscarFoto(int id)
        {
            var produto = ProdutosDao.BuscarProduto(id);
            return File(produto.Foto, produto.MimeType);
        }
        public ActionResult Listar()
        {
            return View(ProdutosDao.ListarProdutos());
        }

        // GET: Produtos/Edit/5
        [HttpGet]
        public ActionResult Alterar(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Nenhum código fornecido");
                }
                var produto = ProdutosDao.BuscarProduto((int)id);
                if (produto == null)
                {
                    throw new
                    Exception("Nenhum produto encontrado com este código");
                }
                ViewBag.ListaDeCategorias = new SelectList(ProdutosDao.ListarCategorias(), "Id", "Descricao");
                return View(produto);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Error");
            }
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Alterar(Produto produto, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return Alterar(produto.Id);
            }
            try
            {
                if (image != null)
                {
                    produto.MimeType = image.ContentType;
                    byte[] bytes = new byte[image.ContentLength];
                    image.InputStream.Read(bytes, 0, image.ContentLength);
                    produto.Foto = bytes;
                }
                ProdutosDao.AlterarProduto(produto);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Error");
            }
        }
    }

     /*   // GET: Produtos/Delete/5
        public ActionResult Excluir(int id)
        {
            return View();
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }*/
}
   

