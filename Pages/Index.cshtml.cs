using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Day6Lab1.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;

        private Lingo GiocoLingo;
        public string ParolaFinoAdOra { get; set; }

        public bool Vittoria = false;

        public int NumeroTentativi;

        public bool Sconfitta = false;

        public List<bool> ListaBooleaniIndovinati { get; set; }

        public string ListaAppareDaQualcheParte = "";

        public List<string> ListaParoleTentate { get; set; } //

        public IndexModel(ILogger<IndexModel> logger,Lingo Gioco) {

            _logger = logger;
            
            GiocoLingo = Gioco;

            ParolaFinoAdOra = Gioco.ParolaFinoAdOra;

            ListaAppareDaQualcheParte = Gioco.StringaContiene;

            NumeroTentativi = Gioco.TentativiRimanenti - 1;

            ListaParoleTentate = Gioco.ListaParoleProvate; //

         ///   ListaBooleaniIndovinati = new List<bool>();

        }

        public void OnGet() {

            GiocoLingo.SettaParolaDaIndovinare();
            ParolaFinoAdOra = GiocoLingo.ParolaFinoAdOra;

        }

        public  void OnPost(string? ParolaProvata) {

            if (ParolaProvata != null) {

                ListaBooleaniIndovinati = GiocoLingo.QualiLettereGiusteENon(ParolaProvata);
                ListaAppareDaQualcheParte = GiocoLingo.ContieneLetteraDaQualcheParte(ParolaProvata);
                ParolaFinoAdOra = GiocoLingo.ParolaFinoAdOra;

                ListaParoleTentate.Add(ParolaProvata); // non è giocolingo.parolaprovata!

                if (!ListaBooleaniIndovinati.Contains(false))
                {
                    Vittoria = true;
                }

                GiocoLingo.TentativiRimanenti--;
                if (GiocoLingo.TentativiRimanenti == 0)
                {
                    Sconfitta = true;
                }

            }
        }
    }
}