using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PracowniaProgramowaniaProjekt1
{
    public class SzukaczSlow
    {
        public List<Slownik> lista_slownikow { get; set; }
        public SzukaczSlow (List<Slownik> _lista_slownikow)
        {
            lista_slownikow = _lista_slownikow;
        }
        public List<string> znajdz_jezyki_zawierajace_slowo(string slowo)
        {
            List<string> znalezione_jezyki =
            lista_slownikow
            .Where(x => x.slowa.Contains(slowo))
            .Select(x => x.jezyk)
            .ToList();
            return znalezione_jezyki;
        }
        public List<string> znajdz_jezyki_zawierajace_slowo_ignoruj_wielkosc_liter(string slowo)
        {
            // wszystkie slowa we wszystkich jezykach do malej litery
            // https://www.devcurry.com/2009/01/easiest-way-to-convert-list-to-lower.html
            var lista_slownikow_male_litery = lista_slownikow;
            foreach (Slownik i in lista_slownikow_male_litery)
                    i.slowa = i.slowa.ConvertAll(x => x.ToLowerInvariant());

            List<string> znalezione_jezyki =
            lista_slownikow_male_litery
            .Where(x => x.slowa.Contains(slowo.ToLower()))
            .Select(x => x.jezyk)
            .ToList();
            return znalezione_jezyki;
        }
    }
}