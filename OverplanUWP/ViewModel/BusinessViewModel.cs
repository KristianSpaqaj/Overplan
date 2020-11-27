using OverplanUWP.Commands;
using OverplanUWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.ViewModel
{
    class BusinessViewModel
    {
        public ObservableCollection<Business> postVirksomhed { get; set; }

        public int medarbejderID { get; set; }
        public string navn { get; set; }
        public string adresse { get; set; }
        public int telefon { get; set; }

        public RelayCommand AddVirksomhed { get; set; }

        public BusinessViewModel()
        {
            
            postVirksomhed = new ObservableCollection<Business>();

            //AddVirksomhed = new RelayCommand(PostVirksomhed);

        }
    }
}
