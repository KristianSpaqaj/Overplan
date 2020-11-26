using OverplanUWP.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.ViewModel
{
    public partial class Medarbejdersplan
    {
        public int MedarbejderID { get; set; }

        public string Navn { get; set; }

        public string Adresse { get; set; }

        public int Telefon { get; set; }
    }
    public partial class Virksomhed
    {
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public int Telefon { get; set; }

    }
    public class TestViewModel
    {

        const string serverUrl = "https://overplanwebservice20201120130529.azurewebsites.net/";

        public ObservableCollection<Medarbejdersplan> postMedarbejdersplan { get; set; }
        public ObservableCollection<Virksomhed> postVirksomhed { get; set; }

        public int medarbejderID { get; set; }
        public string navn { get; set; }
        public string adresse { get; set; }
        public int telefon { get; set; }

        public RelayCommand AddMedarbejder { get; set; }
        public RelayCommand AddVirksomhed { get; set; }


        public TestViewModel()
        {
            postMedarbejdersplan = new ObservableCollection<Medarbejdersplan>();
            postVirksomhed = new ObservableCollection<Virksomhed>();

            AddMedarbejder = new RelayCommand(PostMedarbejdersplan);
            AddVirksomhed = new RelayCommand(PostVirksomhed);

        }



        public void PostMedarbejdersplan()
        {
            Medarbejdersplan oMedarbejder = new Medarbejdersplan();

            postMedarbejdersplan.Add(oMedarbejder);

            //Setup client handler
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                //Initialize client
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                //Request JSON format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    Medarbejdersplan fo = new Medarbejdersplan() { Navn = navn, Adresse = adresse, Telefon = telefon };
                    //Get all the flower orders from the database
                    var MedarbejdersplanResponse = client.PostAsJsonAsync<Medarbejdersplan>("api/Medarbejdersplans", fo).Result;

                    //Check response -> throw exception if NOT successful
                    MedarbejdersplanResponse.EnsureSuccessStatusCode();

                    //Get the hotels as a ICollection
                    var Medarbejdersplan = MedarbejdersplanResponse.Content.ReadAsAsync<Medarbejdersplan>().Result;


                }
                catch
                {

                }
            }
        }

        public void PostVirksomhed()
        {
            Virksomhed oMedarbejder = new Virksomhed();

            postVirksomhed.Add(oMedarbejder);

            //Setup client handler
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                //Initialize client
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                //Request JSON format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    Virksomhed fo = new Virksomhed() { Navn = navn, Adresse = adresse, Telefon = telefon };
                    //Get all the flower orders from the database
                    var VirksomhedsResponse = client.PostAsJsonAsync<Virksomhed>("api/Virksomheds", fo).Result;

                    //Check response -> throw exception if NOT successful
                    VirksomhedsResponse.EnsureSuccessStatusCode();

                    //Get the hotels as a ICollection
                    var virksomhed = VirksomhedsResponse.Content.ReadAsAsync<Virksomhed>().Result;


                }
                catch
                {

                }
            }
        }
    }
}
