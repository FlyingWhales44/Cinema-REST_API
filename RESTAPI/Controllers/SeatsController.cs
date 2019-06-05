using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTAPI.Controllers
{
    public class SeatsController : ApiController
    {
        private DAL.AppContext db = new DAL.AppContext();

        [HttpGet]
        [Route("seats/getAll")]
        public IEnumerable<Seat> Get()
        {
            return db.Seats.ToList();
        }
        

        [HttpPut]
        [Route("seats/buy/{id}")]
        public void Buy(int? id)
        {            
            Seat s = db.Seats.Find(id);
            s.Sold = true;
            db.SaveChanges();
        }

        [HttpPut]
        [Route("seats/setFree/{id}")]
        public void SetFree(int? id)
        {
            Seat s = db.Seats.Find(id);
            s.Sold = false;
            db.SaveChanges();           
        }
    }
}
