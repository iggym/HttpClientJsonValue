using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientJsonValue
{
    class Program
    {
        private static string _address = "http://api.worldbank.org/countries?format=json";

        private static async void RunClient()
        {
            // Create an HttpClient instance
            HttpClient client = new HttpClient();

            // Send a request asynchronously and continue when complete
            HttpResponseMessage response = await client.GetAsync(_address);

            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();

            // Read response asynchronously as JToken and write out top facts for each country
            JArray content = await response.Content.ReadAsAsync<JArray>();

            Console.WriteLine("First 50 countries listed by The World Bank...");
            foreach (var country in content[1])
            {
                Console.WriteLine("   {0}, Country Code: {1}, Capital: {2}, Latitude: {3}, Longitude: {4}",
                    country.Value<string>("name"),
                    country.Value<string>("iso2Code"),
                    country.Value<string>("capitalCity"),
                    country.Value<string>("latitude"),
                    country.Value<string>("longitude"));
            }

            Console.WriteLine("Completed!");
        }

        static void Main(string[] args)
        {
            RunClient();

            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }
    }
}
