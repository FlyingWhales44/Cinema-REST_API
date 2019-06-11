using Newtonsoft.Json;
using RESTAPI.DAL;
using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace RESTAPI.Controllers
{
    


    public class SeatsController : ApiController
    {
        private static readonly object SyncObject = new object();

        public static DB db;

        [HttpGet]
        [Route("seats/getAll")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            lock (SyncObject)
            {
                HttpResponseMessage resp = new HttpResponseMessage();

                var ls = db.seatsList.ToList();
                var json = JsonConvert.SerializeObject(ls);

                resp.Content = new StringContent(json, Encoding.UTF8, "application/json");

                return resp;
            }            
        }
        
        [HttpPost]
        [Route("seats/add")]
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            lock (SyncObject)
            {                           
                var json = request.Content.ReadAsStringAsync().Result;
                var m = JsonConvert.DeserializeObject<SeatModel>(json);

                db.seatsList.Add(m);
                          
                return request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPut]
        [Route("buySpot/{id}")]
        public void Buy(int? id)
        {            
            
        }

        [HttpPut]
        [Route("setFree/{id}")]
        public void SetFree(int? id)
        {
                    
        }
    }
}
