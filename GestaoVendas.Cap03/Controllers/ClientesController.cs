using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// importar a pasta Model
using GestaoVendas.Cap03.Models;
using GestaoVendas.Cap03.Data;
using GestaoVendas.Cap03.Utils;

namespace GestaoVendas.Cap03.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost] //verbo post
        [ValidateAntiForgeryToken] //validar o objeto criptocrafado  formulario
        public ActionResult Incluir(Cliente cliente)//parametro
        {
            /// validar o estado do objeto que recebemos, caso ele esteja invalido entao retornamos o cliente para pagina
            if (!ModelState.IsValid)// se o estado da minha pagina for invalido
            {
                return View();
            }

            // colocamos dentro do TRY-CATCH todo tipo de processamento
            try
            {
                cliente.Documento = cliente.Documento.SemFormatacao(); //metodo de instenção

                ClientesDao.IncluirCliente(cliente);

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return RedirectToAction("Error");
            }

        }

        public ActionResult Listar() //metodos
        {
            try
            {
                
                return View(ClientesDao.ListarClientes());
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = "Ocoreu uma falha, verifique: \n" + ex.Message;
                return View();
            }
        }

        private ActionResult Buscar(string id, string viewName)
        {
            try
            {
                // verificamos se esta nulo, porq alguem pode estar tentando derrubar sua API ou  tentando invasão.
                if (id == null)
                {
                    throw new Exception("O Documento não foi informado corretamente");
                }

                var cliente = ClientesDao.BuscarCliente(id);

                if (cliente == null)
                {
                    throw new Exception("Nenhum cliente encontrado");
                }
                return View(viewName, cliente);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Error");
            }
        }
        public ActionResult Alterar(string id)
        {
            return Buscar(id, "Alterar");
        }
        public ActionResult Detalhes(string id)
        {
            return Buscar(id, "Detalhes");
        }
        public ActionResult Remover(string id)
        {
            return Buscar(id, "Remover");
        }

        [HttpPost]
        public ActionResult Alterar(Cliente cliente)
        {
            try
            {
                ClientesDao.AlterarCliente(cliente);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Remover(Cliente cliente)
        {
            try
            {
                //vamos validar o estado do objeto que recebemos caso ele esteja invalido entao retornamos o cliente para a pagina
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ClientesDao.Remover(cliente);

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Error");
            }
        }
    }
}
    