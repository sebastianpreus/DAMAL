using DamEnovaWebApi.Models.Base;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamZasob : DamZasobBase
    {
        [Key]
        public string Kod { get; set; }

        public void MapEnovaObject(Zasob zasob)
        {
            this.Kod = zasob.Towar.Kod;
            this.Nazwa = zasob.Towar.Nazwa;
            this.Ilosc = zasob.IlośćZasobu.Value;
            this.JednostkaMiary = zasob.IlośćZasobu.Symbol;
            this.Wartosc = zasob.Partia.Wartosc;
            this.Cena = zasob.Partia.Cena;
            this.Dokument = zasob.Partia.Dokument.Numer.NumerPelny;
            this.Data = zasob.Partia.Dokument.Data;
            //this.DaokumentPierw = zasob.Partia.WgDokument;
            //this.Data = zasob.Partia.WgDokument;
            if(zasob.PartiaPierwotna.KontrahentPartii != null)
                this.Dostawca = zasob.PartiaPierwotna.KontrahentPartii.Nazwa;
            this.Typ = zasob.Towar.Typ.ToString();
        }
    }
}