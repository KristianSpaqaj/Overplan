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

        public static void PostShiftOverview(ShiftOverview shiftOverview)
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
                    var VirksomhedsResponse = client.PostAsJsonAsync<ShiftOverview>("api/ShiftOverviews", shiftOverview).Result;

                    //Check response -> throw exception if NOT successful
                    VirksomhedsResponse.EnsureSuccessStatusCode();

                    //Get the hotels as a ICollection
                    var virksomhed = VirksomhedsResponse.Content.ReadAsAsync<ShiftOverview>().Result;


                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// Henter en json fil fra disken 
        /// </summary>
        public static List<ShiftOverview> GetShiftOverview()
        {
            List<ShiftOverview> shifts = new List<ShiftOverview>();
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
                    //Get all the values from the database
                    var shiftOverviewResponse = client.GetAsync("api/ShiftOverviews").Result;

                    //Check response -> throw exception if NOT successful
                    shiftOverviewResponse.EnsureSuccessStatusCode();

                    //Get the shifts as a ICollection
                    var orders = shiftOverviewResponse.Content.ReadAsAsync<ICollection<ShiftOverview>>().Result;

                    foreach (var order in orders)
                    {

                        shifts.Add(order);
                    }
                }

                catch
                {

                }

                return shifts;
            }
        }
            
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
            
        }
    }

}
