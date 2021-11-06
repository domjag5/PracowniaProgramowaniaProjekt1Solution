using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PracowniaProgramowaniaProjekt1
{
    public static class SlownikFactory
    {
        public static void pobierz_slowniki_z_folderu(DirectoryInfo folder_Slowniki)
        {
            foreach (FileInfo plik_ze_slownikiem in folder_Slowniki.GetFiles("*.json"))
            {
                //Console.WriteLine(plik_ze_slownikiem.FullName);
                using (StreamReader z_pliku = File.OpenText(plik_ze_slownikiem.FullName))
                {
                    JsonSerializer serializator = new JsonSerializer();
                    Slownik nowy_slownik = (Slownik)serializator.Deserialize(z_pliku, typeof(Slownik));
                    //Slownik.Lista_slownikow.Add(nowy_slownik); - niepotrzebne bo mamy to w konstruktorze
                    z_pliku.Close();
                }
            }
        }
        public static void dodaj_slownik_z_klawiatury(DirectoryInfo folder_do_zapisu_slownikow)
        {
            Console.WriteLine("Podaj nazwę języka:");
            string podany_jezyk = Console.ReadLine();
            Console.WriteLine("Pisz słowa i zatwierdzaj Enter. Żeby skończyć wybierz Ctrl+D i zatwierdź Enter.");
            List<string> podane_slowa = new List<string>();
            while (true)
            {
                string podane_slowo = Console.ReadLine().Trim();
                if (podane_slowo.Contains((char)4))     //Ctrl+D to w ASCI 004
                    break;
                podane_slowa.Add(podane_slowo);
            }
            //serializuj do podanego folderu z naza pliku taka jak nazwa jezyka .json
            //!jesli istnieje juz tkai plik to zostanie on nadpisany!
            using (StreamWriter do_pliku = File.CreateText(folder_do_zapisu_slownikow.FullName+"\\"+podany_jezyk+".json"))
            {
                JsonSerializer serializator = new JsonSerializer();
                serializator.Serialize(do_pliku, new Slownik(podany_jezyk, podane_slowa));
                do_pliku.Close();
                Slownik.Lista_slownikow.ForEach(Console.WriteLine);
            }
        }
    }
}
