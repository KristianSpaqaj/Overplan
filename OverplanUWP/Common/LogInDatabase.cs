﻿using Newtonsoft.Json;
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
    public static class LogInDatabase
    {
        static string serverUrl = "https://weblogintest20201207140615.azurewebsites.net/";
        private static JsonSerializerSettings settings = new JsonSerializerSettings();

        public static async Task Post<T>(T obj)
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
                var js = JsonConvert.SerializeObject(obj);
                string tableName = typeof(T).Name;
                var content = new StringContent(js, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/" + tableName + "s", content);

                //Get all the values from the database
                //Check response -> throw exception if NOT successful
                response.EnsureSuccessStatusCode();

                //Get the employees as a ICollection
                await response.Content.ReadAsAsync<T>();
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
        public static async Task Delete<T>(T obj) where T : LoginOverview
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
                var js = JsonConvert.SerializeObject(obj);
                string tableName = typeof(T).Name;
                string link = "api/" + tableName + "s/" + obj.Username.ToString();
                var response = await client.DeleteAsync(link);

                //Get all the values from the database
                //Check response -> throw exception if NOT successful
                response.EnsureSuccessStatusCode();

                //Get the values as a ICollection
                await response.Content.ReadAsAsync<T>();
            }
        }
    }
}
