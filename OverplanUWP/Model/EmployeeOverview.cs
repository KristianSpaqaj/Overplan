using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Model
{
    public class EmployeeOverview
    {
        public EmployeeOverview(int medarbejderID, string navn, string adresse, int telefon)
        {
            MedarbejderID = medarbejderID;
            Navn = navn;
            Adresse = adresse;
            Telefon = telefon;
        }
        public EmployeeOverview()
        {

        }

        public int MedarbejderID { get; set; }

        public string Navn { get; set; }

        public string Adresse { get; set; }

        public int Telefon { get; set; }
    }
}
