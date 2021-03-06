﻿using System.Web;

namespace Musaranha.Models
{
    public class Contexto
    {
        public static MusaranhaEntities Current
        {
            get
            {
                if (HttpContext.Current?.Session?["Contexto"] == null)
                {
                    HttpContext.Current?.Session?.Add("Contexto", new MusaranhaEntities());
                }

                return HttpContext.Current?.Session?["Contexto"] as MusaranhaEntities;
            }
            set
            {
                HttpContext.Current?.Session?.Add("Contexto", value);
            }
        }
    }
}