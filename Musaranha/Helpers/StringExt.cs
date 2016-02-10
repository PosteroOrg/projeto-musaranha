using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Musaranha
{
    public static class StringExt
    {
        public static bool IsNullOrEmpty(params string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
                if (String.IsNullOrEmpty(strs[i]))
                    return true;
            return false;
        }

        public static bool IsNullOrWhiteSpace(params string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
                if (String.IsNullOrWhiteSpace(strs[i]))
                    return true;
            return false;
        }

        public static string SomenteNumeros(this string str)
        {
            return new Regex("\\D").Replace(str, ""); 
        }

        public static string MaskTelefone(this string str)
        {
            Int64 telefone = 0;
            if (Int64.TryParse(str, out telefone))
            {
                if (str.Length == 11)
                {
                    return telefone.ToString("'('##') '#####'-'####", CultureInfo.InvariantCulture);
                }
                else
                {
                    return telefone.ToString("'('##') '####'-'####", CultureInfo.InvariantCulture);
                }
            }
            return str;
        }

        public static string MaskCPF(this string str)
        {
            Int64 cpf = 0;
            if (Int64.TryParse(str, out cpf))
            {
                return cpf.ToString("###'.'###'.'###'-'##", CultureInfo.InvariantCulture);
            }
            return str;
        }           
        
        public static string MaskCNPJ(this string str)
        {
            Int64 cnpj = 0;
            if (Int64.TryParse(str, out cnpj))
            {
                return cnpj.ToString("##'.'###'.'###'/'####'-'##", CultureInfo.InvariantCulture);
            }
            return str;
        }

        public static string MaskCEP(this string str)
        {
            int cep = 0;
            if (int.TryParse(str, out cep))
            {
                return cep.ToString("#####'-'###", CultureInfo.InvariantCulture);
            }
            return str;
        }
    }
}