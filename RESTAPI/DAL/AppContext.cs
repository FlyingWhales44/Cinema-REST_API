using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RESTAPI.DAL
{
    public class AppContext : DbContext
    {

        public AppContext() : base("name=DBContext") { }

        public virtual DbSet<Seat> Seats { get; set; }
        
    }
}