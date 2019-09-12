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

        public List<SeatModel> GetSeats()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.GetStringAsync(url + "getAll").Result;

            List<SeatModel> ls = JsonConvert.DeserializeObject<List<SeatModel>>(response);

            return ls;
        }

        public string BuySeat(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.GetStringAsync(url + "buyTicket/" + id).Result;

            var resp = JsonConvert.DeserializeObject(response);

            return resp.ToString();
        }

        public string Reservation(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.PutAsync(url + "reservation/" + id, null).Result;

            var resString = response.Content.ReadAsStringAsync().Result;

            return resString;
        }

        public HttpResponseMessage ClearReservation(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.PutAsync(url + "clearReservation/" + id, null).Result;

            return response;
        }

        public bool CheckIfReserved(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.GetAsync(url + "checkReservation/" + id);

            var v = JsonConvert.DeserializeObject<List<bool>>(response.ToString());
            
            return v[0];
        }

        public bool CheckIfSold(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.GetAsync(url + "checkSold/" + id);

            var v = JsonConvert.DeserializeObject<List<bool>>(response.ToString());

            return v[0];
        }

        public HttpResponseMessage ClearAll()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.PutAsync(url + "releaseAll", null).Result;

            return response;
        }
    }   
}
