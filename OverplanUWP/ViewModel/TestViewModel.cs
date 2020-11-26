using OverplanUWP.Commands;
using OverplanUWP.Model;
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
    public partial class Virksomhed
    {
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public int Telefon { get; set; }

    }
    public class TestViewModel
    {

        const string serverUrl = "https://overplanwebservice20201120130529.azurewebsites.net/";

        public ObservableCollection<EmployeeOverview> postMedarbejdersplan { get; set; }
        public ObservableCollection<Virksomhed> postVirksomhed { get; set; }
        public ObservableCollection<EmployeeOverview> getMedarbejdersplan { get; set; }
        public ObservableCollection<EmployeeOverview> getMedarbejderCL { get; set; }

        public int medarbejderID { get; set; }
        public string navn { get; set; }
        public string adresse { get; set; }
        public int telefon { get; set; }

        public RelayCommand AddMedarbejder { get; set; }
        public RelayCommand AddVirksomhed { get; set; }
        public RelayCommand HentMedarbejder { get; set; }


        public TestViewModel()
        {
            postMedarbejdersplan = new ObservableCollection<EmployeeOverview>();
            postVirksomhed = new ObservableCollection<Virksomhed>();
            getMedarbejdersplan = new ObservableCollection<EmployeeOverview>();

            AddMedarbejder = new RelayCommand(PostMedarbejdersplan);
            AddVirksomhed = new RelayCommand(PostVirksomhed);
            HentMedarbejder = new RelayCommand(GetMedarbejder);

        }



        public void PostMedarbejdersplan()
        {
            EmployeeOverview oMedarbejder = new EmployeeOverview();

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
                    EmployeeOverview fo = new EmployeeOverview() { Navn = navn, Adresse = adresse, Telefon = telefon };
                    //Get all the flower orders from the database
                    var MedarbejdersplanResponse = client.PostAsJsonAsync<EmployeeOverview>("api/Medarbejdersplans", fo).Result;

                    //Check response -> throw exception if NOT successful
                    MedarbejdersplanResponse.EnsureSuccessStatusCode();

                    //Get the hotels as a ICollection
                    var Medarbejdersplan = MedarbejdersplanResponse.Content.ReadAsAsync<EmployeeOverview>().Result;


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
        /// <summary>
        /// Henter en json fil fra disken 
        /// </summary>
        public void GetMedarbejder()
        {
            getMedarbejdersplan.Clear();
            //List<Medarbejdersplan> nyListe = new List<Medarbejdersplan>();
            //nyListe = await PersistencyService.HentDataFraDiskAsyncPS();

            getMedarbejderCL = new ObservableCollection<EmployeeOverview>();

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
                    //Get all the flower orders from the database
                    var medarbejdersplanResponse = client.GetAsync("api/medarbejdersplans").Result;

                    //Check response -> throw exception if NOT successful
                    medarbejdersplanResponse.EnsureSuccessStatusCode();

                    //Get the hotels as a ICollection
                    var orders = medarbejdersplanResponse.Content.ReadAsAsync<ICollection<EmployeeOverview>>().Result;

                    foreach (var order in orders)
                    {

                        getMedarbejdersplan.Add(order);
                    }
                }
                catch
                {

                }
            }
            //StorageFile file = await localfolder.GetFileAsync(filnavn);
            //string jsonText = await FileIO.ReadTextAsync(file);
            //this.OC_blomster.Clear();
            //IndsætJson(jsonText);
        }
    }
}
