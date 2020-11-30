using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        private static JsonSerializerSettings settings = new JsonSerializerSettings();
        private static IsoDateTimeConverter dateConverter = new IsoDateTimeConverter
        {
            DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss.fff"
        };

        static Database()
        {
            settings.Converters.Add(dateConverter);
        }

        public static async Task PostEmployeeOverview(EmployeeOverview employee)
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
                    //Get all the values from the database
                    var EmployeeResponse = await client.PostAsJsonAsync<EmployeeOverview>("api/EmployeeOverviews", employee);

                    //Check response -> throw exception if NOT successful
                    EmployeeResponse.EnsureSuccessStatusCode();

                    //Get the employees as a ICollection
                    var Medarbejdersplan = await EmployeeResponse.Content.ReadAsAsync<EmployeeOverview>();
                }
                catch
                {

                }
            }
        }

        public static async Task PostShiftOverview(ShiftOverview employee)
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
                var js = JsonConvert.SerializeObject(employee, dateConverter);
                var content = new StringContent(js, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/ShiftOverviews", content);

                //Get all the values from the database
                //Check response -> throw exception if NOT successful
                response.EnsureSuccessStatusCode();

                //Get the employees as a ICollection
                var Medarbejdersplan = await response.Content.ReadAsAsync<ShiftOverview>();
            }
        }


        public static async Task<List<T>> Get<T>()
        {
            List<T> results = new List<T>();
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
                    string tableName = typeof(T).Name;
                    var response = await client.GetAsync("api/" + tableName + "s");

                    //Check response -> throw exception if NOT successful
                    response.EnsureSuccessStatusCode();

                    //Get the Employees as a ICollection
                    var parsed = await response.Content.ReadAsAsync<ICollection<T>>();

                    foreach (var row in parsed)
                    {

                        results.Add(row);
                    }
                }

                catch
                {

                }

                return results;
            }

        }
    }

}
