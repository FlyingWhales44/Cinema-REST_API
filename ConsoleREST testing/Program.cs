using Newtonsoft.Json;
using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleREST_testing
{
    class Program
    {
        public static Semaphore semaphore;
        public int numer { get; set; }

        static HttpClient client = new HttpClient();

        private string url = "http://localhost:58059/seats/";

        static void Main(string[] args)
        {
            semaphore = new Semaphore(0, 100);

            Thread[] threads = new Thread[100];
            for (int i = 0; i < threads.Length; i++)
            {
                Program me = new Program();
                me.numer = i;
                threads[i] = new Thread(new ThreadStart(me.test));
                threads[i].Start();
            }
            semaphore.Release(100);
            for (int i = 0; i < threads.Length; i++)
                threads[i].Join();
            Console.ReadLine();

        }


        public void test()
        {
            testClient(1);
        }


        public void testClient(int id)
        {
            semaphore.WaitOne();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Test HTTP Client");

            var response = client.GetStringAsync(url + "buyTicket/" + id).Result;

            var resp = JsonConvert.DeserializeObject(response);

            Console.WriteLine(resp.ToString());

            semaphore.Release();
        }

    }
}
