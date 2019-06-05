using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RESTAPI.Models;

namespace RESTAPI.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<SeatsModel> Seats { get; set; }

    }
}