using OverplanUWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Common
{
    public static class Database
    {
        static string serverUrl = "https://overplanwebservice20201120130529.azurewebsites.net/";


        public static void PostEmployeeOverview(EmployeeOverview employee)
        {
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
                    var MedarbejdersplanResponse = client.PostAsJsonAsync<EmployeeOverview>("api/EmployeeOverviews", employee).Result;

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

        public static void PostBusiness(Business business)
        {
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
                    var VirksomhedsResponse = client.PostAsJsonAsync<Business>("api/Virksomheds", business).Result;

                    //Check response -> throw exception if NOT successful
                    VirksomhedsResponse.EnsureSuccessStatusCode();

                    //Get the hotels as a ICollection
                    var virksomhed = VirksomhedsResponse.Content.ReadAsAsync<Business>().Result;


                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// Henter en json fil fra disken 
        /// </summary>
        public static List<EmployeeOverview> GetEmployeeOverview()
        {
            List<EmployeeOverview> employees = new List<EmployeeOverview>();
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
                    var medarbejdersplanResponse = client.GetAsync("api/EmployeeOverviews").Result;

                    //Check response -> throw exception if NOT successful
                    medarbejdersplanResponse.EnsureSuccessStatusCode();

                    //Get the hotels as a ICollection
                    var orders = medarbejdersplanResponse.Content.ReadAsAsync<ICollection<EmployeeOverview>>().Result;

                    foreach (var order in orders)
                    {

                        employees.Add(order);
                    }
                }

                catch
                {

                }

                return employees;
            }
            //StorageFile file = await localfolder.GetFileAsync(filnavn);
            //string jsonText = await FileIO.ReadTextAsync(file);
            //this.OC_blomster.Clear();
            //IndsætJson(jsonText);
        }
    }

}
