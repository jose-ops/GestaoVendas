using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// importar a model
using GestaoVendas.Cap03.Models;
using System.Data.Entity;

namespace GestaoVendas.Cap03.Data
{
    public class ProdutosDao
    {
        /// <summary>
        /// Incvluir o produto informado pelo formulario 
        /// </summary>
        /// <param name="produto">dados do produto</param>
        public static void IncluirProduto(Produto produto)
        {
            using (var db = new DB_VENDASEntities())//obtendo instancia
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
            }
        }

        //Buscar
        public static Produto BuscarProduto(int id)
        {
            using (var db = new DB_VENDASEntities())
            {
                return db.Produtos.FirstOrDefault(prod => prod.Id.Equals(id));
            }
           
        }

        //Listar
        public static IEnumerable<Produto> ListarProdutos()
        {
            using (var db = new DB_VENDASEntities())
            {
                return db.Produtos.ToList();
            }
         
        }

        //Alterar
        public static void AlterarProduto(Produto produto)
        {
            using (var db = new DB_VENDASEntities())
            {
                db.Entry<Produto>(produto).State = EntityState.Modified;
            }
        }

        //método para listar todos os produtos
        public static IEnumerable<Categoria> ListarCategorias()
        {
            using (var ctx = new DB_VENDASEntities())
            {
                return ctx.TBCategorias.ToList(); 
            }
        }
    }
}