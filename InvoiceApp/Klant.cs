using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp
{
    internal class Klant
    {
        public int KlantId { get; set; }
        public string BedrijfNaam { get; set; }
        public int NrTva { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public int NrTel { get; set; }
    }
}
