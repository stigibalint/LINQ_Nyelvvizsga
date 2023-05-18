using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string sikeresFile = "sikeres.csv";
        string sikertelenFile = "sikertelen.csv";

        List<string[]> sikeresAdatok = BeolvasAdatok(sikeresFile);
        List<string[]> sikertelenAdatok = BeolvasAdatok(sikertelenFile);

        // Feladat 2: Legnépszerűbb nyelvek
        List<string> legnepszerubbNyelvek = LegnepszerubbNyelvek(sikeresAdatok, sikertelenAdatok);
        Console.WriteLine("Legnépszerűbb nyelvek:");
        foreach (string nyelv in legnepszerubbNyelvek)
        {
            Console.WriteLine(nyelv);
        }

        // Feladat 3: Év ellenőrzése
        int ev = EllenorizEv();

        // Feladat 4: Legnagyobb sikertelen arány
        double sikertelenArany = LegnagyobbSikertelenArany(sikertelenAdatok, ev);
        Console.WriteLine($"A(z) {ev}-es évben a legnagyobb sikertelen arány: {sikertelenArany}");


        // Feladat 5: Vizsgázatlan nyelvek ?
       

        // Feladat 6: Összesítés mentése
     //   MentesOsszesites(sikeresAdatok, sikertelenAdatok, "osszesites.csv");
    }

    static List<string[]> BeolvasAdatok(string fajlnev)
    {
        List<string[]> adatok = new List<string[]>();
        string[] sorok = File.ReadAllLines(fajlnev).Skip(1).ToArray();
        foreach (string sor in sorok)
        {
            string[] oszlopok = sor.Split(';');
            adatok.Add(oszlopok);
        }
        return adatok;
    }

    static List<string> LegnepszerubbNyelvek(List<string[]> sikeresAdatok, List<string[]> sikertelenAdatok)
    {
        var nyelvek = sikeresAdatok.Concat(sikertelenAdatok).Select(adat => adat[0]);
        var gyakorisag = nyelvek.GroupBy(nyelv => nyelv).OrderByDescending(x => x.Count());
        return gyakorisag.Select(x => x.Key).Take(3).ToList();
    }

    static int EllenorizEv()
    {
        int bekeres = 0;
        do
        {
            Console.WriteLine("Kérem, adjon meg egy évet (2009 és 2017 között): ");
            bekeres = Convert.ToInt32(Console.ReadLine());
        } while ( 2009 > bekeres || bekeres > 2017);
     
        return bekeres;

    }

    static double LegnagyobbSikertelenArany(List<string[]> sikertelenAdatok, int ev)
    {
        double legnagyobbArany = 0.0;
        foreach (string[] adat in sikertelenAdatok)
        {
            int adatEv = int.Parse(adat[2]);
            if (adatEv == ev)
            {
                int sikeresVizsgak = int.Parse(adat[1]);
                int sikertelenVizsgak = int.Parse(adat[3]);
                double arany = (double)sikertelenVizsgak / (sikeresVizsgak + sikertelenVizsgak);
                if (arany > legnagyobbArany)
                {
                    legnagyobbArany = arany;
                }
            }
        }
        return legnagyobbArany;
    }
  
    }
