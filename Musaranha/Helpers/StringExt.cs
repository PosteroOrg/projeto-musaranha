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
        public static string SomenteNumeros(this string str)
        {
            return new Regex("\\D").Replace(str, ""); 
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