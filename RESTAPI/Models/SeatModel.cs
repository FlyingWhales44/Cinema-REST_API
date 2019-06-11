using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAPI.Models
{
    public class SeatModel
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public bool Reservation { get; set; }
        public bool Sold { get; set; }
    }
}