using System;
using System.Collections.Generic;
using System.Linq;
using RESTAPI.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CinemaWPF
{
    public class SeatsClient
    {
        static HttpClient client;

        private string url;

        public SeatsClient()
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

        public async Task<string> Reservation(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = await client.PutAsync(url + "reservation/" + id, null);

            var resString = await response.Content.ReadAsStringAsync();            

            return resString;
        }

        public async Task<HttpResponseMessage> ClearReservation(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = await client.PutAsync(url + "clearReservation/" + id, null);

            return response;
        }

        public async Task<bool> CheckIfReserved(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = await client.GetAsync(url + "checkReservation/" + id);

            var v = JsonConvert.DeserializeObject<List<bool>>(response.ToString());
            
            return v[0];
        }

        public async Task<bool> CheckIfSold(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = await client.GetAsync(url + "checkSold/" + id);

            var v = JsonConvert.DeserializeObject<List<bool>>(response.ToString());

            return v[0];
        }

        public async Task<HttpResponseMessage> ClearAll()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = await client.PutAsync(url + "releaseAll", null);

            return response;
        }
    }   
}
