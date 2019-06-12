using Newtonsoft.Json;
using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleREST_testing
{
    class Client
    {
        static HttpClient client;

        private string url;

        public Client()
        {
            client = new HttpClient();

            url = "http://localhost:58059/seats/";
        }

        public async Task<List<SeatModel>> GetSeats()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = await client.GetStringAsync(url + "getAll");

            List<SeatModel> ls = JsonConvert.DeserializeObject<List<SeatModel>>(response);

            return ls;
        }

        public async Task<HttpResponseMessage> BuySeat(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = await client.PutAsync(url + "buyTicket/" + id, null);

            return response;
        }

    }
}
