using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// adicionar referencia ao Entity framework
using System.Data.Entity;
using GestaoVendas.Cap03.Models;

namespace GestaoVendas.Cap03.Data
{
    public class ClientesDao
    {
        public static void IncluirCliente(Cliente cliente)
        {
            // é recomendavel sempre instaciar o objeto de acesso a dados dentro de um objeto USING
            using (var ctx = new DB_VENDASEntities())
            {
                // para fazer um INSERT, adicionamos um objeto Clientes ao objeto de contexto 
                ctx.TBClientes.Add(cliente);
                //para executar o Entity executar o INSERT no banco de Dados, basta executar o comando SAVECHANGES()
                ctx.SaveChanges();

            }
        }

        public static Cliente BuscarClient(string documento)
        {
            using (var ctx = new DB_VENDASEntities())
            {
                // vamos utilizar o LINq para fazer um filtro dos dados retornados da base depois retornamos o resultado já filtrado
                return ctx.TBClientes.FirstOrDefault(p => p.Documento.Equals(documento));
            }
        }

        public static IEnumerable<Cliente> ListarClientes()
        {
            using (var ctx = new DB_VENDASEntities())
               {
                // faz um SELECT * FROM <Table>
                 return ctx.TBClientes.ToList();
               }
        }

        public static void AlterarCliente(Cliente cliente)
        {
            using (var ctx = new DB_VENDASEntities())
            {
                 //para executar o UPDATE, precisamos indicar para ao ENTITY q a tabela esta em ESTADo de MODIFICAÇAO, usando ENUMERADOR
                ctx.Entry<Cliente>(cliente).State = EntityState.Modified;
                ctx.SaveChanges();
                
            }
            
        }

        /// <summary> responsavel por excuir um registro de cliente
        public static void Remove(Cliente cliente)
        {
            using (var ctx = new DB_VENDASEntities())
            {
                ctx.Entry<Cliente>(cliente).State = EntityState.Deleted;

                ctx.SaveChanges(); 
            }
        }

        internal static object BuscarCliente(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Remover(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}