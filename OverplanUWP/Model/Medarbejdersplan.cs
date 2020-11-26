using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Model
{
    public class Medarbejdersplan
    {
        public Medarbejdersplan(string navn, string adresse, int telefon)
        {
            Navn = navn;
            Adresse = adresse;
            Telefon = telefon;
        }
        public int MedarbejderID { get; set; }

        public string Navn { get; set; }

        public string Adresse { get; set; }

        public int Telefon { get; set; }
    }
}
