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
    [RoutePrefix("hall")]
    public class HallController : ApiController
    {

        private static readonly object SyncObject = new object();

        public static DB db;

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            lock (SyncObject)
            {
                HttpResponseMessage resp = new HttpResponseMessage();

                var ls = db.hallList.ToList();
                var json = JsonConvert.SerializeObject(ls);

                resp.Content = new StringContent(json, Encoding.UTF8, "application/json");

                return resp;
            }
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit(HttpRequestMessage request, int id)
        {
            lock (SyncObject)
            {
                var h = db.hallList.FirstOrDefault(x => x.Id == id);

                var json = request.Content.ReadAsStringAsync().Result;
                var newModel = JsonConvert.DeserializeObject<HallModel>(json);

                h = newModel;

                return request.CreateResponse(HttpStatusCode.OK);
            }           
        }
    }
}
