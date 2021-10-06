using DamEnovaWebApi.Models.Base;
using Soneta.CRM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DamEnovaWebApi.Models
{
    public class DamKontrahent : DamModelBase
    {
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string EuVAT { get; set; }
        public string NIP { get; set; }
        public bool JestIncydentalny { get; set; }

        //[ForeignKey("AdresDoKorespondencji")]
        //public int AdresDoKorespondencjiId { get; set; }
        //public DamAdres AdresDoKorespondencji { get; set; }

        [ForeignKey("DamAdres")]
        public int DamAdresId { get; set; }        
        public DamAdres DamAdres { get; set; }

        ////public StatusPodmiotu StatusPodmiotu { get; set; }
        public bool PodatnikVAT { get; set; }
        ////public RodzajPodmiotu RodzajPodmiotuZakup { get; set; }
        ////public RodzajPodmiotu RodzajPodmiotu { get; set; }
        /////public DefKategKth Kategoria { set; }
        ////public View DzialnoscPrzewazajacaKontrahenta { get; }
        ////public View DzialalnosciKontrahenta { get; }
        ////public View PrzecenyKontrahenta { get; }
        ////public bool BlokadaSprzedazy { get; set; }
        ////public EFaktura EFaktura { get; set; }
        ////public FormaPlatnosci SposobZaplaty { get; set; }
        public bool NieWindykowac { get; set; }
        //public bool PrzeterminowanieNieograniczone { get; set; }
        //public bool LimitNieograniczony { get; set; }
        ////public RachunekBankowyPodmiotu DomyslnyRachunek { get; }
        ////[Description("Kod kraju kontrahenta wyliczony na podstawie NIP/EuVAT.")]
        public string KodKraju { get; set; }
        //public string NazwaFormatowana { get; set; }
        //public string Segment { get; set; }
        public bool IsAllowed { get; set; }
        //public bool KontrolaAktywna { get; set; }
        //public bool ZgodnoscGIODOPotwierdzona { get; set; }
        ////public View OsobyZOsobyKontrahent { get; }
        //public string DomyślnyAdresWWW { get; set; }
        ////public Currency KontrolaKwota { get; set; }
        ////public Currency LimitKredytu { get; set; }
        //public TypLimituKredytowego TypPrzeterminowania { get; set; }
        //public TypLimituKredytowego TypLimituKredytowego { get; set; }
        //public StatusNumeruVAT AktualnyStatusVATMF { get; set; }
        //public FormaPrawna FormaPrawna { get; set; }
        //public StatusNumeruVAT AktualnyStatusVATVies { get; set; }
        //public List<Kontrahent> PodmiotyZastąpione { get; set; }
        ////public ListWithView KontrahenciPodrzedni { get; }
        ////public IPodmiot PodmiotNadrzedny { get; }
        //public Kontrahent Zamiennik { get; set; }
        //public DateTime? AktStatusVATDataVIES { get; set; }
        //public DateTime? AktStatusVATDataMF { get; set; }
        //public DateTime? AktStatusVATData { get; set; }
        public string EMAIL { get; set; }
        public bool Blokada { get; set; }
        ////public IPodmiotKasowy Platnik { get; }
        public StatusNumeruVAT AktualnyStatusVAT { get; set; }
        //public string NazwaPierwszaLinia { get; set; }
        //public bool PodmiotPowiazany { get; set; }
        public string MailTo { get; set; }



        public void MapEnovaObject(Kontrahent kontrahent)
        {
            //PK
            this.ID = kontrahent.ID;

            this.Kod = kontrahent.Kod;
            this.Nazwa = kontrahent.Nazwa;
            this.EuVAT = kontrahent.EuVAT;
            this.NIP = kontrahent.NIP;
            this.JestIncydentalny = kontrahent.JestIncydentalny;
            //DamAdres adres = new DamAdres();
            //adres.MapEnovaObject(kontrahent.Adres);
            //this.AdresDoKorespondencji = new DamAdres(kontrahent.AdresDoKorespondencji);            
            this.DamAdres = new DamAdres(kontrahent.Adres);
            this.DamAdresId = this.DamAdres.ID;
            //public StatusPodmiotu StatusPodmiotu { get; set; }
            this.PodatnikVAT = kontrahent.PodatnikVAT;
            //public RodzajPodmiotu RodzajPodmiotuZakup { get; set; }
            //public RodzajPodmiotu RodzajPodmiotu { get; set; }
            ///public DefKategKth Kategoria { set; }
            //public View DzialnoscPrzewazajacaKontrahenta { get; }
            //public View DzialalnosciKontrahenta { get; }
            //public View PrzecenyKontrahenta { get; }
            //public bool BlokadaSprzedazy { get; set; }
            //public EFaktura EFaktura { get; set; }
            //public FormaPlatnosci SposobZaplaty { get; set; }
            this.NieWindykowac = kontrahent.NieWindykowac;
            //this.PrzeterminowanieNieograniczone = kontrahent.PrzeterminowanieNieograniczone;
            //this.LimitNieograniczony = kontrahent.LimitNieograniczony;
            //public RachunekBankowyPodmiotu DomyslnyRachunek { get; }
            //[Description("Kod kraju kontrahenta wyliczony na podstawie NIP/EuVAT.")]
            this.KodKraju = kontrahent.KodKraju;
            //this.NazwaFormatowana = kontrahent.NazwaFormatowana;
            //this.Segment = kontrahent.Segment;
            //this.IsAllowed = kontrahent.IsAllowed;
            //this.KontrolaAktywna = kontrahent.KontrolaAktywna;
            //this.ZgodnoscGIODOPotwierdzona = kontrahent.ZgodnoscGIODOPotwierdzona;
            //public View OsobyZOsobyKontrahent { get; }
            //this.DomyślnyAdresWWW = kontrahent.DomyślnyAdresWWW;
            //public Currency KontrolaKwota { get; set; }
            //public Currency LimitKredytu { get; set; }
            //this.TypPrzeterminowania = kontrahent.TypPrzeterminowania;
            //this.TypLimituKredytowego = kontrahent.TypLimituKredytowego;
            //this.AktualnyStatusVATMF = kontrahent.AktualnyStatusVATMF;
            //public FormaPrawna FormaPrawna { get; set; }
            //StatusNumeruVAT AktualnyStatusVATVies 
            //public List<Kontrahent> PodmiotyZastąpione { get; }
            //public ListWithView KontrahenciPodrzedni { get; }
            //public IPodmiot PodmiotNadrzedny { get; }
            //public Kontrahent Zamiennik { get; set; }
            //this.AktStatusVATDataVIES = kontrahent.AktStatusVATDataVIES;
            //this.AktStatusVATDataMF = kontrahent.AktStatusVATDataMF;
            //this.AktStatusVATData = kontrahent.AktStatusVATData;
            this.EMAIL = kontrahent.EMAIL;
            //this.Blokada = kontrahent.Blokada;
            //public IPodmiotKasowy Platnik { get; }
            //public StatusNumeruVAT AktualnyStatusVAT { get; }
            //this.NazwaPierwszaLinia = kontrahent.NazwaPierwszaLinia;
            //this.PodmiotPowiazany = kontrahent.PodmiotPowiazany;
            this.MailTo = kontrahent.MailTo;

        }
    }
}