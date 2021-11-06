using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PracowniaProgramowaniaProjekt1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sciezka_do_folderu_projektu = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //jesli nie istnieje stworz folder z pdfami
            string sciezka_do_folderu_Pdfy = sciezka_do_folderu_projektu + "\\Pdfy";
            Directory.CreateDirectory(sciezka_do_folderu_Pdfy);
            DirectoryInfo folder_Pdfy = new DirectoryInfo(sciezka_do_folderu_Pdfy);
            //jesli nie istnieje stworz folder ze slownikami
            string sciezka_do_folderu_Slowniki = sciezka_do_folderu_projektu + "\\Slowniki";
            Directory.CreateDirectory(sciezka_do_folderu_Slowniki);
            DirectoryInfo folder_Slowniki = new DirectoryInfo(sciezka_do_folderu_Slowniki);
            //wczytaj pliki i utworz z nich obiekty
            SlownikFactory.pobierz_slowniki_z_folderu(folder_Slowniki);
            //wyswietl slowniki
            /*foreach (Slownik i in Slownik.Lista_slownikow)
            {
                Console.WriteLine(i.jezyk);
                i.slowa.ForEach(Console.WriteLine);
            }*/
            //stworz szukacza ktory bedzie szukal w liscie slownikow
            SzukaczSlow szukaczSlow = new SzukaczSlow(Slownik.Lista_slownikow);
            //slownik zapamietujacy wszystkie szukane slowa i jezyki ktore je mialy
            Dictionary<string, List<string>> wyniki_szukania = new Dictionary<string, List<string>>();
            //glowna petla programu
            bool program_ma_dzialac = true;
            while (program_ma_dzialac)
            {
                Console.Clear();
                Console.WriteLine("Menu główne");
                Console.WriteLine("Wybierz:");
                Console.WriteLine("1 - Sprawdź w jakich językach występuje słowo \n2 - Dodaj słownik z klawiatury\nEscape - wyjdź z programu");
                ConsoleKeyInfo wybor = Console.ReadKey();
                Console.Clear();
                if (wybor.KeyChar == '1') //szukanie jezykow ze slowem
                {
                    while (true)
                    {
                        Console.WriteLine("Wpisz słowo, którego szukasz (uwaga na wielkie litery):");
                        string szukane_slowo = Console.ReadLine();
                        List<string> znalezione_jezyki = szukaczSlow.znajdz_jezyki_zawierajace_slowo(szukane_slowo);
                        //szukane slowo zapisujemy raz
                        if (!wyniki_szukania.ContainsKey(szukane_slowo))
                            wyniki_szukania.Add(szukane_slowo, znalezione_jezyki);
                        if (znalezione_jezyki.Count > 0)
                        {
                            Console.WriteLine("\nSłowo wystepuje w następujących słownikach:");
                            znalezione_jezyki.ForEach(Console.WriteLine);
                        }
                        else
                            Console.WriteLine("\nSłowo nie występuje w żadnym słowniku.");
                        Console.WriteLine("\nWybierz:\nEscape - zakończ wpisywanie\nEnter - szukaj następnego słowa");
                        ConsoleKeyInfo wcisniety_klawisz = Console.ReadKey();
                        Console.Clear();
                        if (wcisniety_klawisz.Key == ConsoleKey.Escape)
                            break;
                    }
                }
                else if (wybor.KeyChar == '2')
                    SlownikFactory.dodaj_slownik_z_klawiatury(folder_Slowniki);
                else if (wybor.Key == ConsoleKey.Escape)
                    program_ma_dzialac = false;
            }
            //stworz pdf
            FabrykaPdf.stworz_pdf(wyniki_szukania, folder_Pdfy);
        }
    }
}