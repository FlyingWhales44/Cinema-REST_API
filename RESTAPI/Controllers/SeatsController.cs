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
    [RoutePrefix("seats")]
    public class SeatsController : ApiController
    {
        private static readonly object SyncObject = new object();

        public static DB db;

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage Get()
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

        [HttpGet]
        [Route("check/{id}")]
        public bool Check(HttpRequestMessage request,int id)
        {
            lock (SyncObject)
            {
                var m = db.seatsList.FirstOrDefault(x => x.Id == id);

                if (m != null)
                {
                    if (m.Sold)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }
        
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            lock (SyncObject)
            {                           
                var json = request.Content.ReadAsStringAsync().Result;
                var m = JsonConvert.DeserializeObject<SeatModel>(json);

                m.Id = db.seatsList.Count + 1;

                db.seatsList.Add(m);
                          
                return request.CreateResponse(HttpStatusCode.OK, m);
            }
        }

        [HttpPut]
        [Route("buyTicket/{id}")]
        public HttpResponseMessage BuyTicket(HttpRequestMessage request,int id)
        {
            lock (SyncObject)
            {
                var m = db.seatsList.FirstOrDefault(x => x.Id == id);

                if (m != null)
                {
                    if (!m.Sold)
                    {
                        m.Sold = true;
                    }
                    else
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent(
                                  "Place already sold", Encoding.UTF8
                          )
                        };
                    }
                }


                return new HttpResponseMessage(){
                    Content = new StringContent(
                                "Have a nice movie", Encoding.UTF8
                        )};
            }         
        }

        [HttpPut]
        [Route("releaseSeat/{id}")]
        public HttpResponseMessage ReleaseSeat(HttpRequestMessage request, int id)
        {
            lock (SyncObject)
            {
                var m = db.seatsList.FirstOrDefault(x => x.Id == id);

                if(m != null)
                    m.Sold = false;

                return request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
