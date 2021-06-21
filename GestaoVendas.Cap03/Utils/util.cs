using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoVendas.Cap03.Utils
{
    public static class util
    {
        public static string FormatCNPJ(this string CNPJ)
        {
            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }


        /// <summary>
        /// Retira a Formatação de uma string CNPJ/CPF
        /// </summary>
        /// <param name="Codigo">string Codigo Formatada</param>
        /// <returns>string sem formatação</returns>
        /// <example>Recebe '99.999.999/9999-99' Devolve '99999999999999'</example>

        public static string SemFormatacao(this string Codigo)
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }

        /// <summary>
        /// Formatar uma string CNPJ
        /// </summary>
        /// 
        ///<param name="CPF_CNPJ">string CNPJ sem fomratação</param>
        /// <returns>string CNPJ formatada</returns>
        /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>
        /// 
        public static string FormatCpfCnpj(this String CPF_CNPJ)
        {
            return CPF_CNPJ.Length >= 14 ? FormatCNPJ(CPF_CNPJ) : FormatCpfCnpj(CPF_CNPJ);
        }
        
    }
}