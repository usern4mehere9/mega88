using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Kalkulator
{
    class ProgramKalkulatorUFajl
    {
        public static void inicializacija(){
            //Proveravam da li fajl postoji ako ne napreavi ga
            try
            {
                StreamReader init_T = new StreamReader("db.txt");
                init_T.Close();
            }
            catch (Exception e)
            {
                //pravim fajl

                File.CreateText("db.txt").Close();

                //Unosim broj predmeta koji je skladisten na pocetku fajla

                int n;
                StreamWriter init_C = new StreamWriter("db.txt");

                Console.Write(" Fajl ne postoji \v Nov db.txt kreiran \v Unesite broj predmeta: ");

                n = int.Parse(Console.ReadLine());

                init_C.WriteLine(n);
                init_C.WriteLine("0");

                init_C.Close();
            }
        }

        public static void unos()
        {
            // Unosim novog ucenika

            string writer,temp;
            int n_predmeta, n_ucenika,k, n_ucenika_radni;
            string[] prepisivac; 

            StreamReader in_R = new StreamReader("db.txt");

            n_predmeta=int.Parse(in_R.ReadLine());
            n_ucenika=int.Parse(in_R.ReadLine());

            k = -1;
            n_ucenika_radni = n_ucenika + 1;

            prepisivac = new string[n_ucenika+3];

            //Upisujem broj ucenika u string koji cu uneti u fajl
            
            n_ucenika++;
            writer = n_ucenika.ToString();

            //Isto samo sa imenom

            Console.Write(" Unesite [Ime] ucenika: ");
            temp = Console.ReadLine();
            writer = writer + "," + temp;

            //Isto samo sa prezimenom

            Console.Write(" Unesite [Prezime] ucenika: ");
            temp = Console.ReadLine();
            writer = writer + "," + temp;

            //Unos ocena od 1 do n

            for(int i = 0; i < n_predmeta; i++)
            {
                Console.Write(" Unesite {0}. ocenu: ",i);
                temp = Console.ReadLine();
                writer = writer + "," + temp;
            }

            in_R.Close();
            in_R = new StreamReader("db.txt");


            //Citam sve sto je u fajlu i stavljam ga u prepisivac

            do
            {
                k++;
                prepisivac[k] = in_R.ReadLine();
       
            } while (prepisivac[k] != null);

            in_R.Close();


            //zaqvrsavam sa string reader i pocinjem da upisujem scve iz prepisivaca

            StreamWriter in_W = new StreamWriter("db.txt");

            //na zadnju poziciju prepisivaca upisujem novog ucenika

            prepisivac[k]=writer;


            //Povecavam broj ucenika u fajlu

            prepisivac[1]= n_ucenika_radni.ToString();


            //sve upisujem u fajl

            for (int i = 0; i < prepisivac.Length; i++)
            {
                in_W.WriteLine(prepisivac[i]);

            }

            in_W.Close();

            //kraj dodavanja novog seljaka (ovaj ucenika)
        }

        public static void ispis()
        {
            int n_predmeta, n_ucenika, k;
            string[] temp;
            char com;

            StreamReader in_R = new StreamReader("db.txt");

            n_predmeta = int.Parse(in_R.ReadLine());
            n_ucenika = int.Parse(in_R.ReadLine());

            k = -1;

            temp = new string[n_ucenika + 3];


            //Citam sve sto je u fajlu i stavljam ga u temp

            do
            {
                k++;
                temp[k] = in_R.ReadLine();

            } while (temp[k] != null);
            
            in_R.Close();


            //Brisem konzolu {cisto da izgleda bolje}
            Console.Clear();

inp:
            //Ispisujem osnovne informacije o db.txt
            Console.WriteLine("Broj predmeta: {0}",n_predmeta);
            Console.WriteLine("Broj Ucenika: {0}", n_ucenika);

            //Unosim komandu za range ili individual
            Console.Write("Komanda (r za domen | i za pojedinca): ");
            com=char.Parse(Console.ReadLine());

            //pauziram na 2 sec {da natera korisnika da cita sta pise}
            Thread.Sleep(2000);

            //ako korisnik unese r ispisujemo range ucenika
            if(com == 'r'|| com == 'R')
            {
                int beg, end;

                beg = 0;
                end = 0;

                Console.Write("Unesite pocetak domena: ");
                beg = int.Parse(Console.ReadLine());


                Console.Write("Unesite kraj domena: ");
                end = int.Parse(Console.ReadLine());

                if(beg<=0||beg>n_ucenika|| end <= 0 || end > n_ucenika)
                {
                    //cistim konzolu u pripremi za ispis grske
                    Console.Clear();

                    //ispis greske
                    Console.WriteLine("Uneliste vrednost van opsega");
                    Console.WriteLine("Pokusajte ponovo");

                    //cekanje da korisnik procita gresku
                    Thread.Sleep(5000);

                    //ispisujem konzolu u pripremi za ponovni unos
                    Console.Clear();

                    //skacem na unos
                    goto inp;
                }

                

                for(int i = beg-1; i < end; i++)
                {
                    Console.WriteLine(temp[i]);   
                }
            }
            //ako unese i ispisujemo individualca
            else if(com == 'i' || com == 'I')
            {
                int n;

                n = 0;

                Console.Write("Unesite redni broj ucenika: ");
                n = int.Parse(Console.ReadLine());

                if (n <= 0 || n > n_ucenika)
                {
                    //cistim konzolu u pripremi za ispis grske
                    Console.Clear();

                    //ispis greske
                    Console.WriteLine("Uneliste vrednost van opsega");
                    Console.WriteLine("Pokusajte ponovo");

                    //cekanje da korisnik procita gresku
                    Thread.Sleep(5000);

                    //ispisujem konzolu u pripremi za ponovni unos
                    Console.Clear();

                    //skacem na unos
                    goto inp;
                }



                Console.WriteLine(temp[n-1]);
            }
            //ako unese glupost zaustavljam program i vracam se na unos komande
            else
            {
                //cistim konzolu u pripremi za ispis grske
                Console.Clear();

                //ispis greske
                Console.WriteLine("Uneliste vrednost van opsega");
                Console.WriteLine("Pokusajte ponovo");

                //cekanje da korisnik procita gresku
                Thread.Sleep(5000);

                //ispisujem konzolu u pripremi za ponovni unos
                Console.Clear();

                //skacem na unos
                goto inp;
            }
        }

        public static void statistika()
        {
            int n_ucenika, k, n_predmeta, is_predmet, zbir_ocene,index,is_ucenik;
            string[] temp;
            char com1;

            //Pitamo korisnika za komandu
sp:         Console.Write("Unesite zeljenju operaciju(p za predmet | u za ucenika): ");
            com1 = char.Parse(Console.ReadLine());


            //ucitavamo ucenike iz fajla (bukvalno kopirano od pre)
            StreamReader in_R = new StreamReader("db.txt");

            n_predmeta = int.Parse(in_R.ReadLine());
            n_ucenika = int.Parse(in_R.ReadLine());

            k = 0;
            zbir_ocene = 0;

            temp = new string[n_ucenika+1];


            //Citam sve sto je u fajlu i stavljam ga u temp

            do
            {
                
                temp[k] = in_R.ReadLine();
                k++;

            } while (temp[k-1] != null);

            in_R.Close();




            //Trazim prosek za predmet

            if(com1=='p'|| com1 == 'P')
            {
                Console.Write("Iz kojeg predmeta zelite prosek(1-{0}): ",n_predmeta);
                is_predmet=int.Parse(Console.ReadLine());

                if (is_predmet<=0||is_predmet>n_predmeta)
                {
                    //cistim konzolu u pripremi za ispis grske
                    Console.Clear();

                    //ispis greske
                    Console.WriteLine("Uneliste vrednost van opsega");
                    Console.WriteLine("Pokusajte ponovo");

                    //cekanje da korisnik procita gresku
                    Thread.Sleep(5000);

                    //ispisujem konzolu u pripremi za ponovni unos
                    Console.Clear();

                    //skacem na unos
                    goto sp;
                }

                index = is_predmet + 2;

                for (int i = 0; i < n_ucenika; i++)
                { 

                //Ovo izgleda strasno komplikovano ali stvarno nije. Ajde da kazemo da je temp od i "1,Nikola,Popovic,1,2,3,4,5" split ce vratiti '1' 'Nikola' 'Popovic' '1' '2' '3' '4' '5'
                //Zatim pomocu GetValue uzimamo vrenos koja se nalazi na poziviji index stoga ako je korisnik uneo 1 index ce po formuli index=is_predmet+2 biti 3 a na trecem polozaju se nalazi prva ocena
                //GetValue nam je vratio '1' taj jedan moramo da pretvorimo iz char u string i to radimo sa .ToString() kada smo sve to zavrsili to pretvaramo u int sa int.parse i dodajemo na zbir


                   zbir_ocene += int.Parse(temp[i].Split(',').GetValue(index).ToString());

                }
                Console.WriteLine(zbir_ocene / (double)n_ucenika);
            }
            else if (com1 == 'u' || com1 == 'U')
            {
                Console.Write("Unesite redni broj ucenika ciji prosek zelite(1-{0}): ", n_ucenika);
                is_ucenik=int.Parse(Console.ReadLine());

                if (is_ucenik<=0||is_ucenik>n_ucenika)
                {
                    //cistim konzolu u pripremi za ispis grske
                    Console.Clear();

                    //ispis greske
                    Console.WriteLine("Uneliste vrednost van opsega");
                    Console.WriteLine("Pokusajte ponovo");

                    //cekanje da korisnik procita gresku
                    Thread.Sleep(5000);

                    //ispisujem konzolu u pripremi za ponovni unos
                    Console.Clear();

                    //skacem na unos
                    goto sp;
                }


                for(int i = 0; i < n_predmeta; i++)
                {
                    index = i + 3;

                    //Ako ne kontas ovo vrati se na prosli deo za uneto p kao com

                    zbir_ocene += int.Parse(temp[is_ucenik - 1].Split(',').GetValue(index).ToString());
                }
                Console.WriteLine("Prosek tog ucenika je: {0}",zbir_ocene/(double)n_predmeta);
                
            }
            else
                {
                    //cistim konzolu u pripremi za ispis grske
                    Console.Clear();

                    //ispis greske
                    Console.WriteLine("Uneliste vrednost van opsega");
                    Console.WriteLine("Pokusajte ponovo");

                    //cekanje da korisnik procita gresku
                    Thread.Sleep(5000);

                    //ispisujem konzolu u pripremi za ponovni unos
                    Console.Clear();

                    //skacem na unos
                    goto sp;
                }
            


        }

        static void Main(string[] args)
        {

            int aux; //iz inata je aux

            inicializacija();
        //pravim UI jednom u zivotu
        //jk jk naterao sam rodjaku ovo da radi (bas sam divna osoba)


        p:
            Console.Clear();


            Console.Write("Unesite Zeljenu operaciju(1-unos | 2-ispis | 3-statistika): ");
            aux = int.Parse(Console.ReadLine());

            switch (aux) {
            
                case 1:
                    Console.Clear();
                    unos();
                    break;

                case 2:
                    Console.Clear();
                    ispis();
                    break;

                case 3:
                    Console.Clear();
                    statistika();
                    break;

                default:
                     //cistim konzolu u pripremi za ispis grske
                        Console.Clear();

                        //ispis greske
                        Console.WriteLine("Uneliste vrednost van opsega");
                        Console.WriteLine("Pokusajte ponovo");

                        //cekanje da korisnik procita gresku
                        Thread.Sleep(5000);

                        //ispisujem konzolu u pripremi za ponovni unos
                        Console.Clear();

                        //skacem na unos
                        goto p;

                    break;
            }

            
        }
    }
}
