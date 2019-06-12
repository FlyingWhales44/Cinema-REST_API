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

        public async Task<SeatModel> CreateSeat(int hallId)
        {
            SeatModel s = new SeatModel();

            s.Reservation = false;
            s.Sold = false;
            s.HallId = hallId;

            string json = JsonConvert.SerializeObject(s);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url + "add", httpContent);
            response.EnsureSuccessStatusCode();


            var resultString = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<SeatModel>(resultString);

            return result;
        }




    }   
}
