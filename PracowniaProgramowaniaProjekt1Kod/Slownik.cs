using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PracowniaProgramowaniaProjekt1
{
    public class Slownik
    {
        private static List<Slownik> lista_slownikow = new List<Slownik>();
        public string jezyk { get; set; }
        public List<string> slowa { get; set; }
        public Slownik(string _jezyk, List<string> _slowa)
        {
            jezyk = _jezyk;
            slowa = _slowa;
            lista_slownikow.Add(this);
        }
        public static List<Slownik> Lista_slownikow
        {
            get { return lista_slownikow; }
            set { lista_slownikow = value; }
        }
    }
}