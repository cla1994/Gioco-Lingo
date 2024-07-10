using System.Reflection.Metadata;
using System.Text;

namespace Day6Lab1 {
    public class Lingo {

        public string ParolaDaAzzeccare;
        public string ParolaFinoAdOra { get; set; } 
        private List<bool> ListaUltimaConfigBooleani { get; set; }

        public string StringaContiene;

        private string[] GruppoParolePossibili = {
            "nequizia","livore","foriero","prodromo","astruso","mendace","melologo","lutolento","pleonasmo","riverenza","propinquo",
            "pernicioso","gaglioffo","fonema","delatore","bislacco","callido"
           
        };

        public char EvitareMessaggioLettereDoppie = ' ';

        public int TentativiRimanenti = 5;

        public List<string> ListaParoleProvate { get; set; }
        

    public Lingo() {

            ListaUltimaConfigBooleani = new List<bool>();

            ListaParoleProvate = new List<string>(); // attento a mettere questa parte di codice dentro il costruttore, non fuori...

            ListaParoleProvate.Add(" "); //     se non crei l'oggetto prima con riga sopra, per forza non funziona nulla...
        } 
        public void SettaParolaDaIndovinare() {

            List<bool> ParolaAllInizio = new List<bool>();

            var MioGruppoParole = (from ParolaPossibile in GruppoParolePossibili
                        select ParolaPossibile).ToList();

            Random rnd = new Random();

            ParolaDaAzzeccare = MioGruppoParole[rnd.Next(0,16)];
            
            for (int i = 0; i < ParolaDaAzzeccare.Length; i++) 
            { 
                if (i != 0)
                {
                    ParolaAllInizio.Add(false);
                }
                else
                {
                    ParolaAllInizio.Add(true);
                }
            }

            MostraParola(ParolaAllInizio);         
        }

        public string ContieneLetteraDaQualcheParte(string ParolaTentata)                               
        {

            for (int i = 0; i < ParolaTentata.Length; i++)    
            {
                if (ParolaDaAzzeccare.Contains(ParolaTentata[i]) && ParolaTentata[i] != ParolaDaAzzeccare[i] && EvitareMessaggioLettereDoppie != ParolaTentata[i])
                {                                                               // primo &&: così lettere giuste al posto giusto non le segnala, già scritte
                    StringaContiene += ParolaTentata[i];                        // secondo &&: così per esempio non scrive 2 volte "la z è al posto sbagliato" se parola tentata ha 2 z

                    EvitareMessaggioLettereDoppie = ParolaTentata[i];
                }                                                                                                   
            }

            return StringaContiene;
        }

         public void MostraParola(List<bool> ListaBooleaniIndovinatiENon) 
         {
            
            string ParolaAQuestoTentativo = "";

            for (int j = 0; j < ListaUltimaConfigBooleani.Count; j++ )  
            {

                if (ListaUltimaConfigBooleani[j]==true) {
                    ListaBooleaniIndovinatiENon[j] = true;
                }
            } 
        
            for (int k = 0; k < ListaBooleaniIndovinatiENon.Count; k++)
            {

                if (ListaBooleaniIndovinatiENon[k] == true) 
                {
                    ParolaAQuestoTentativo = ParolaAQuestoTentativo + ParolaDaAzzeccare[k];
                }
                else
                {
                    ParolaAQuestoTentativo = ParolaAQuestoTentativo + "?";          // "□"
                }
            }

            ParolaFinoAdOra = ParolaAQuestoTentativo;                // fa doppio salvataggio di valori calcolati in funzione

            ListaUltimaConfigBooleani = ListaBooleaniIndovinatiENon;    
        }

        public List<bool> QualiLettereGiusteENon(string ParolaTentata)  
        {

            List<bool> ListaBooleaniTrovatiENon = new List<bool>();

            for (int k = 0; k < ParolaTentata.Length;k++)
            {
                ListaBooleaniTrovatiENon.Add(false); // true o false è lo stesso tanto lo cambio, importante è crearla con tot
            }                                           // elementi così posso intervenire sui singoli (necessario per lettere
                                                        // ripetute che non compaiono)

            for (int i = 0; i < ParolaTentata.Length; i++)
            {
                if (ParolaTentata[i] == ParolaDaAzzeccare[i])
                {
                    ListaBooleaniTrovatiENon[i] = true;

                 /*   for (int j = 0; j < ParolaTentata.Length; j++)            // commentata questa parte se non voglio che se parola è fama e io dico fame non metta entrambe le a
                    {
                        if (ParolaTentata[i] == ParolaDaAzzeccare[j])               // se lettera (con i che rimane fermo) compare ancora
                        {                                                                // nella parola da azzeccare, scrive anche lei
                            ListaBooleaniTrovatiENon[j] = true;
                        }
                    }   */                                                         // non c'è bisogno fare caso else a parolatentata == paroladaazzeccare; inizializzato tutto a 
                }                                                                   // false, semplicemente rende true quando trova lettera giusta in posizione giusta e poi mette true
            }                                                               //altre lettere uguali; mettendo else lista[i] = false c'era problema che sovrascriveva lettere giuste
                                                                     // che si ripetevano (tipo se azzeccavo la i in "nequizia" la seconda i veniva sovrascritta perché condizione
            MostraParola(ListaBooleaniTrovatiENon);             // if non soddisfatta

            return ListaBooleaniTrovatiENon;
        }
    }
}
