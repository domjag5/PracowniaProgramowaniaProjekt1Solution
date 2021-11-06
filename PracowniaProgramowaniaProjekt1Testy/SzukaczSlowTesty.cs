using NUnit.Framework;
using PracowniaProgramowaniaProjekt1;
using System.Collections.Generic;

namespace PracowniaProgramowaniaProjekt1_TestyJednostkowe
{
    [TestFixture]
    public class SzukaczSlowTesty
    {
        private SzukaczSlow szukaczSlow;
        [SetUp]
        public void Setup()
        {
            List<Slownik> lista_slownikow = new List<Slownik>
            {
                new Slownik("Jêzyk1", new List<string> { "a", "b", "c", "xcv", "do dn" }),
                new Slownik("Jêzyk2", new List<string> { "a", "b", "cc", "Xcv", "Do dn" }),
                new Slownik("Jêzyk3", new List<string> { "a", "D", "D", "XCV", "Do dN", "dO Dn" })
            };
            szukaczSlow = new SzukaczSlow(lista_slownikow);
        }

        [Test]
        public void znajdz_jezyki_zawierajace_slowo__jest_w_0__zwraca_0()
        {
            List<string> wynik = szukaczSlow.znajdz_jezyki_zawierajace_slowo("z");
            CollectionAssert.IsEmpty(wynik);
        }
        [Test]
        public void znajdz_jezyki_zawierajace_slowo__jest_w_3__zwraca_3()
        {
            List<string> wynik = szukaczSlow.znajdz_jezyki_zawierajace_slowo("a");
            CollectionAssert.AreEquivalent(wynik, new List<string> { "Jêzyk1", "Jêzyk2", "Jêzyk3" });
        }
        [Test]
        public void znajdz_jezyki_zawierajace_slowo__jest_w_1_dwa_razy__zwraca_1_jeden_raz()
        {
            List<string> wynik = szukaczSlow.znajdz_jezyki_zawierajace_slowo("D");
            CollectionAssert.AreEquivalent(wynik,new List<string>{"Jêzyk3"});
        }
        [Test]
        public void znajdz_jezyki_zawierajace_slowo_ignoruj_wielkosc_liter__jest_w_1__zwraca_1()
        {
            List<string> wynik = szukaczSlow.znajdz_jezyki_zawierajace_slowo("xcv");
            CollectionAssert.AreEquivalent(wynik, new List<string> { "Jêzyk1"});
        }
        [Test]
        public void znajdz_jezyki_zawierajace_slowo_ignoruj_wielkosc_liter__jest_w_3__zwraca_3()
        {
            List<string> wynik = szukaczSlow.znajdz_jezyki_zawierajace_slowo_ignoruj_wielkosc_liter("xcv");
            CollectionAssert.AreEquivalent(wynik, new List<string> { "Jêzyk1", "Jêzyk2", "Jêzyk3" });
        }
        [Test]
        public void znajdz_jezyki_zawierajace_slowo_ignoruj_wielkosc_liter__jest_w_3__zwraca_3__spacje()
        {
            List<string> wynik = szukaczSlow.znajdz_jezyki_zawierajace_slowo_ignoruj_wielkosc_liter("DO DN");
            CollectionAssert.AreEquivalent(wynik, new List<string> { "Jêzyk1", "Jêzyk2", "Jêzyk3" });
        }
    }
}