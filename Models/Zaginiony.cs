using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zaginionki.Models
{
    public class Zaginiony
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data Zaginiecia")]
        public DateTime DataZaginiecia { get; set; }
        public string Wojewodztwo { get; set; }

        public string Opis { get; set; }
        public string Plec { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Zdjecie { get; set; }
    }
}
