/*
 *  Jan_Kacicnik_1002333873_n4.cs  
 *
 *  Ustvarjeno: 20/5/2018
 *  Avtor: Jan Kačičnik
 *  Identifikacijska stevilka: 1002333873
 *  Besedilo naloge:
 *  
 *  Naloga 4
Vaše rešitve pišite v predlogo za C++ ali C#. Rešitev naloge oddajte v skladu s Pravili za
oddajanje nalog.
Prototipi podanih imenskih prostorov in metod v predlogi morajo ostati nespremenjeni.
Definirate lahko poljubno število lastnih metod. Primeri testov v metodi [mM]ain() (C++:
main(), C#: Main()) so zgolj za zgled in ne predstavljajo celotnega obsega testov, katerim mora
zadostiti vaša rešitev.
1 Iskalno drevo števil
Iskalno drevo je drevesna podatkovna struktura, katere glavna prednost, v primerjavi s tabelo
ali seznamom, je v splošnem hitrejše iskanje nekega elementa. Ob tem pa njegova notranja
urejenost omogoča enostavno tvorbo urejenega zaporedja hranjenih elementov. Vaša naloga je
implementacija metod, ki omogočijo omenjeno funkcionalnost.
V predlogah je tokrat samo zametek podatkovne strukture, ki pa zadošča implementaciji teh
metod. To pomeni, da skelet razreda, ki predstavlja podatkovno strukturo iskalno drevo števil,
ne vsebuje vseh metod, ki jih poznamo iz definicije. Posledično tudi ne ponuja popolne
funkcionalnosti omenjene podatkovne strukture, pač pa vsebuje samo izbrane vsebine, ki so
potrebne za implementacijo rešitve naloge.
Predlogi vsebujeta razred, ki predstavlja zametek podatkovne strukture iskalno drevo, kjer
manjkajo implementacije nekaterih metod. Znotraj tega razreda se uporablja še drug (notranji,
vgnezden) razred, ki predstavlja posamezno vozlišče takšnega drevesa. Tudi v njem manjkajo
implementacije nekaterih metod.
Vaša naloga je implementirati vse omenjene metode (tj. vse metode, ki v telesu nosijo komentar
z začetkom „TODO”).
 */

//definicija imenskega prostora(/podrocja)
namespace PS
{
    //uporabljene knjiznice
    using System;
    using System.Collections.Generic;

    //razredi
    /// <summary>
    /// Razred IskalnoDrevo
    /// definira zametek podatkovne strukture IskalnoDrevo.
    /// Razred smo okrnili za metode, ki jih za resitev vaje ne 
    /// potrebujete.
    /// 
    /// Skelet vsebuje ob privzetem in kopirnem konstruktorju, 
    /// se metode:
    /// - osnovne funkcionalnosti
    ///   - vstavi()
    /// - razsirjene funkcionalnosti
    ///   - jePrazno()
    ///   - steviloVozlisc()
    ///	  - visinaDrevesa()
    /// - dodatne funkcionalnosti
    ///   - najvecjiElement()
    ///   - najmanjsiElement()
    ///   - narascajocaVrsta()
    ///   - padajocaVrsta()
    ///   - prefixQueue()
    ///   - infixQueue()
    ///   - postfixQueue()
    /// </summary>
    public class IskalnoDrevo
    {
        /// <summary>
        /// Vgnezden razred Vozlisce
        /// definira zametek razreda Vozlisce, namenjenega interni
        /// rabi (tj. samo znotraj razreda IskalnoDrevo).
        /// 
        /// Razred Vozlisce predstavlja tip, ki pomaga notranje 
        /// urediti drevesno podatkovno strukturo.
        /// 
        /// Skelet vsebuje ob konstruktorju, ki prejme podatek in
        /// kopirnem konstruktorju, se metode:
        /// - osnovne funkcionalnosti
        ///   - vstavi()
        /// - razsirjene funkcionalnosti
        ///   - steviloVozlisc()
        ///   - visinaDrevesa()
        /// - dodatne funkcionalnosti
        ///   - najvecji()
        ///   - najmanjsi()
        ///   - narascajocaVrsta()
        ///   - padajocaVrsta()
        ///   - prefixQueue()
        ///   - infixQueue()
        ///   - postfixQueue()
        /// </summary>
        class Vozlisce
        {
            private float podatek;
            private Vozlisce levo;
            private Vozlisce desno;

            /// <summary>
            /// Konstuktor, ki prejme podatek, ki ga nato hrani ustvarjeno Vozlisce.
            /// </summary>
            /// <remarks>
            /// Leva in desna veja kazeta v prazno (null).
            /// </remarks>
            /// <param name="podatek">
            /// Podatek (decimalno stevilo - tipa <see cref="System.Float"/>), 
            /// ki ga hrani vozlisce.
            /// </param>
            public Vozlisce(float podatek)
            {
                this.podatek = podatek;
                levo = null;
                desno = null;
            }

            //kopirni konstruktor
            public Vozlisce(Vozlisce original)
            {
                this.podatek = original.podatek;
                levo = null;
                desno = null;

                if (original.levo != null)
                    levo = new Vozlisce(original.levo);
                if (original.desno != null)
                    desno = new Vozlisce(original.desno);
            }

            /// <summary>
            /// Metoda vstavi podan podatek v Vozlisce na naslednjem nivoju,
            /// ali na katerem od visjih nivojev v drevesu vozlisc.
            /// </summary>
            /// <remarks>
            /// Ce je ustrezna veja, kamor po pravilih dvojiskega (stopnja_drevesa==2)
            /// iskalnega drevesa spada podan podatek, ze "polna", metoda "rekurzivno" klice
            /// vstavljanje podatka nad najvisjim vozliscem te veje drevesa.
            /// </remarks>
            /// <param name="podatek">
            /// Podatek (decimalno stevilo - tipa <see cref="System.Float"/>), ki se hrani 
            /// v novo Vozlisce na naslednjem nivoju ali na katerem od visjih nivojev v 
            /// drevesu vozlisc.
            /// </param>
            public void vstavi(float podatek)
            {
                if (podatek < this.podatek)
                {
                    if (levo == null)
                        levo = new Vozlisce(podatek);
                    else
                        levo.vstavi(podatek);
                }
                else
                {   //when (podatek >= this.podatek)
                    if (desno == null)
                        desno = new Vozlisce(podatek);
                    else
                        desno.vstavi(podatek);
                }
            }

            public int steviloVozlisc()
            {
                Queue<float> stevilo = new Queue<float>();

                narascajocaVrsta(stevilo);

                return stevilo.Count;
            }

            public int visinaDrevesa(int globina)
            {
                int vozliscaLevo = 0;
                int vozliscaDesno = 0;

                if (levo != null)
                {
                    vozliscaLevo = levo.visinaDrevesa(globina);
                }
                if (desno != null)
                {
                    vozliscaDesno = desno.visinaDrevesa(globina);
                }
                return Math.Max(vozliscaLevo, vozliscaDesno) + globina;
            }

            public float najvecji()
            {
                if (desno != null)
                {
                    return desno.najvecji();
                }
                else
                {
                    return podatek;
                }
            }

            public float najmanjsi()
            {
                if (levo != null)
                {
                    return levo.najmanjsi();
                }
                else
                {
                    return podatek;
                }
            }

            public void narascajocaVrsta(Queue<float> vrsta)
            {
                if (levo != null)
                {
                    levo.narascajocaVrsta(vrsta);
                }

                vrsta.Enqueue(podatek);

                if (desno != null)
                {
                    desno.narascajocaVrsta(vrsta);
                }
            }

            public void padajocaVrsta(Queue<float> vrsta)
            {
                if (desno != null)
                {
                    desno.padajocaVrsta(vrsta);
                }
                vrsta.Enqueue(podatek);

                if (levo != null)
                {
                    levo.padajocaVrsta(vrsta);
                }
            }

            public void prefixQueue(Queue<float> vrsta)
            {
                vrsta.Enqueue(podatek);
                if (levo != null)
                {
                    levo.prefixQueue(vrsta);
                }
                if (desno != null)
                {
                    desno.prefixQueue(vrsta);
                }
            }

            public void infixQueue(Queue<float> vrsta)
            {
                if (levo != null)
                    levo.infixQueue(vrsta);
                vrsta.Enqueue(podatek);
                if (desno != null)
                    desno.infixQueue(vrsta);
            }

            /// <summary>
            /// Metoda, ki vrne (vse) hranjene elemente urejene po
            /// vrstnem redu, kot ga ustvari postfix pregled 
            /// dvojiskega (stopnja_drevesa==2) drevesa.
            /// </summary>
            /// <param name="vrsta">
            /// Po izvedbi metode: Vrsta (tipa <see cref="Queue<System.Float>"/>) 
            /// hranjenih stevil v vrstnem redu, kot ga dobimo po postfixnem prehodu 
            /// dvojiskega (stopnja_drevesa==2) drevesa.
            /// </param>
            public void postfixQueue(Queue<float> vrsta)
            {
                if (levo != null)
                    levo.postfixQueue(vrsta);

                if (desno != null)
                    desno.postfixQueue(vrsta);

                vrsta.Enqueue(podatek);
            }
        }   //end class Vozlisce

        //lastnosti
        Vozlisce koren;

        //privzeti konstruktor
        public IskalnoDrevo()
        {
            this.koren = null;
        }

        //kopirni konstruktor
        public IskalnoDrevo(IskalnoDrevo original)
        {
            koren = new Vozlisce(original.koren);
        }

        /// <summary>
        /// Metoda vstavi podan podatek v koren oz. na katerega od visjih nivojev drevesa.
        /// </summary>
        /// <remarks>
        /// Ce drevo ni prazno, klice (rekurzivno) vstavljanje nad korenskim vozliscem.
        /// </remarks>
        /// <param name="podatek">
        /// Podatek (decimalno stevilo - tipa <see cref="System.Float"/>) 
        /// ki se hrani v korensko Vozlisce ali katerega od vozlisc na visjem nivoju v drevesu.
        /// </param>
        public void vstavi(float podatek)
        {
            if (jePrazen())
                koren = new Vozlisce(podatek);
            else
                koren.vstavi(podatek);
        }

        /// <summary>
        /// Metoda javi ali je drevo prazno (vrne "true") ali ne (vrne "false").
        /// </summary>
        /// <remarks>
        /// Preveri samo ali je kazalec na korensko Vozlisce null ali ne.
        /// </remarks>
        /// <returns>
        /// (tip <see cref="System.Boolean"/>) Vrne "true", ce drevo 
        /// "nima korenskega vozlisca" oz. "false", ce ga ima.
        /// </returns>
        public bool jePrazen()
        {
            return (koren == null);
        }

        /// <summary>
        /// Presteje in vrne stevilo hranjenih elementov v iskalnem drevesu.
        /// </summary>
        /// <remarks>
        /// Kadar drevo ima korensko Vozlisce, se dejanski
        /// izracun(/stetje) stevila hranjenih elementov zgodi v
        /// metodi IskalnoDrevo.Vozlisce.steviloVozlisc() zacensi nad
        /// korenskim vozliscem drevesa.
        /// </remarks>
        /// <returns>
        /// Stevilo hranjenih elementov(/vozlisc) v tem drevesu 
        /// (tipa <see cref="System.Int32"/>) oz. 0, ce drevo 
        /// nima korenskega vozlisca.
        /// </returns>
        public int steviloVozlisc()
        {
            if (koren == null/*jePrazen()*/)
                return 0;
            else
                return koren.steviloVozlisc();
        }

        /// <summary>
        /// Vrne visino drevesa (koren ima nivo 1)
        /// </summary>
        /// <remarks>
        /// Kadar drevo ima korensko Vozlisce, se dejanski
        /// stetje nivojev zgodi v metodi IskalnoDrevo.Vozlisce.visinaDrevesa(int) zacensi nad
        /// korenskim vozliscem drevesa.
        /// </remarks>
        /// <returns>
        /// Najvecji nivo drevesa oziroma -1 oz. 0, ce drevo 
        /// nima korenskega vozlisca.
        /// </returns>
        public int visinaDrevesa()
        {
            if (koren == null)
                return 0;
            else
                return koren.visinaDrevesa(1);
        }

        //metode za dodatno funkcionalnost
        public float najvecjiElement()
        {
            if (koren != null)
            {
                return koren.najvecji();
            }
            else
            {
                return float.NaN;
            }
        }

        public float najmanjsiElement()
        {
            if (koren == null)
                return float.NaN;
            return koren.najmanjsi();
        }



        public Queue<float> narascajocaVrsta()
        {
            Queue<float> narascajocaVrsta = new Queue<float>();

            if (koren != null)
            {
                koren.narascajocaVrsta(narascajocaVrsta);
            }

            return narascajocaVrsta;
        }

        /// <summary>
        /// Metoda s pomocjo pregleda iskalnega drevesa vrne vrsto (vseh) hranjenih
        /// elementov po padajocem vrstnem redu.
        /// </summary>
        /// <returns>
        /// Vrsta decimalnih stevil (tipa <see cref="Queue<System.Float>"/>) , 
        /// urejenih padajoce.
        /// </returns>
        public Queue<float> padajocaVrsta()
        {
            Queue<float> padajoca = new Queue<float>();
            if (koren != null)
                //				padajoca = koren.padajocaVrsta ();
                koren.padajocaVrsta(padajoca);
            return padajoca;
        }


        public Queue<float> prefixQueue()
        {
            Queue<float> prefixVrsta = new Queue<float>();

            if (koren != null)
            {
                koren.prefixQueue(prefixVrsta);
            }

            return prefixVrsta;
        }

        public Queue<float> infixQueue()
        {
            Queue<float> infix = new Queue<float>();
            if (koren != null)
            {
                koren.infixQueue(infix);
            }
            return infix;
        }
        public Queue<float> postfixQueue()
        {
            Queue<float> vrsta = new Queue<float>();
            if (koren != null)
            {
                koren.postfixQueue(vrsta);
            }
            return vrsta;
        }
    }   //end class IskalnoDrevo


    public class Test
    {
        static float min_element(float[] zaporedje)
        {
            if (zaporedje.Length == 0)
                return float.NaN;
            else if (zaporedje.Length == 1)
                return zaporedje[0];
            float najmanjsi = zaporedje[0];

            foreach (float vrednost in zaporedje)
                if (vrednost < najmanjsi)
                    najmanjsi = vrednost;
            return najmanjsi;
        }

        static bool test_najmanjsi_element()
        {
            string opis_scenarija = "Vrni najmanjsi element v iskalnem drevesu.";
            string ime_metode = "float IskalnoDrevo.najmanjsiElement()";
            try
            {
                //stevila
                float[] zaporedje = { 1.1f, -9.9f, 16.16f, -25.25f, -1.1f, 4.4f, 36.36f, -16.16f, -4.4f, .0f, 9.9f, 25.25f, 49.49f };
                //priprava IskalnoDrevo
                IskalnoDrevo id = new IskalnoDrevo();
                for (int i = 0; i < zaporedje.Length; i++)
                    id.vstavi(zaporedje[i]);

                //kontrola
                float pricakovana_vrednost = min_element(zaporedje);

                //testing
                if (id.najmanjsiElement() != pricakovana_vrednost)
                {
                    Console.WriteLine("V testnem scenariju \'{0}\': Metoda \'{1}\' ni vrnila pravilnega najmanjsega elementa. ", opis_scenarija, ime_metode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("V testnem scenariju \'{0}\': Metoda \'{1}\' je prozila izjemo. Opis izjeme: \'{2}\'", opis_scenarija, ime_metode, e.Message);
                return false;
            }

            //vse vredu
            return true;
        }

        //glavna metoda
        static void Main(string[] args)
        {
            bool uspesni = true;
            Console.WriteLine("--- zacetek testiranja ---");
            //izvajanje testov
            if (!test_najmanjsi_element())
                uspesni = false;
            // TODO: Tukaj dodajte svoje teste, ki bodo ustrezno preverili, da vasa
            //       implementacija deluje pravilno.



            //povzetek
            if (uspesni)
                Console.Write("Povzetek: Program JE");
            else
                Console.Write("Povzetek: Program ni");
            Console.WriteLine(" uspesno prestal testiranje.");
            Console.WriteLine("--- konec testiranja ---");
            Console.WriteLine();

            IskalnoDrevo testnoDrevo = new IskalnoDrevo();

            testnoDrevo.vstavi(5);
            testnoDrevo.vstavi(26);
            testnoDrevo.vstavi(-4);
            testnoDrevo.vstavi(18);
            testnoDrevo.vstavi(-2);
            testnoDrevo.vstavi(36);
            testnoDrevo.vstavi(22);
            testnoDrevo.vstavi(78);
            testnoDrevo.vstavi(3);
            testnoDrevo.vstavi(68);
            testnoDrevo.vstavi(17);
            testnoDrevo.vstavi(99);
            testnoDrevo.vstavi(-9);
            testnoDrevo.vstavi(6);
            testnoDrevo.vstavi(88);
            testnoDrevo.vstavi(-11);
            testnoDrevo.vstavi(92);
            testnoDrevo.vstavi(-34);
            testnoDrevo.vstavi(39);
            testnoDrevo.vstavi(15);
            testnoDrevo.vstavi(109);
            testnoDrevo.vstavi(-62);
            testnoDrevo.vstavi(7);
            testnoDrevo.vstavi(21);
            testnoDrevo.vstavi(66);
            testnoDrevo.vstavi(-198);

            Console.WriteLine("Size: " + testnoDrevo.steviloVozlisc());
            Console.WriteLine("Najvecji: " + testnoDrevo.najvecjiElement());
            Console.WriteLine("Najmanjsi: " + testnoDrevo.najmanjsiElement());
            Console.WriteLine("Višina: " + testnoDrevo.visinaDrevesa());

            Console.WriteLine("");

            Queue<float> narascujoce = testnoDrevo.narascajocaVrsta();


            Queue<float> postfix = testnoDrevo.postfixQueue();

            Console.Write("Postfix: ");
            while (postfix.Count > 0)
            {
                Console.Write(postfix.Dequeue() + " ");
            }


            Console.WriteLine("");

            Queue<float> infix = testnoDrevo.infixQueue();

            Console.Write("\nInfix: ");
            while (infix.Count > 0)
            {
                Console.Write(infix.Dequeue() + " ");
            }


            Console.WriteLine("");

            Queue<float> prefix = testnoDrevo.prefixQueue();

            Console.Write("\nPrefix: ");
            while (prefix.Count > 0)
            {
                Console.Write(prefix.Dequeue() + " ");
            }


            Console.WriteLine("");

            Console.Write("\nNaraščujoče: ");
            while (narascujoce.Count > 0)
            {
                Console.Write(narascujoce.Dequeue() + " ");
            }


            Console.WriteLine("");

            Queue<float> padajoce = testnoDrevo.padajocaVrsta();

            Console.Write("\nPadajoče: ");
            while (padajoce.Count > 0)
            {
                Console.Write(padajoce.Dequeue() + " ");
            }

            Console.Write("\n");

            Console.ReadKey();
        }
    }
}
//na koncu prazna vrstica

