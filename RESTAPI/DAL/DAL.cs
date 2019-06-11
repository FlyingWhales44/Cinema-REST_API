using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAPI.DAL
{
    public class DB
    {
        public List<SeatModel> seatsList;

        public DB()
        {

            seatsList = new List<SeatModel>();

            seatsList.AddRange(new[]
            {
                new SeatModel{Id=1,Sold=false,Reservation=false,HallId=1},
                new SeatModel{Id=2,Sold=false,Reservation=false,HallId=1},
                new SeatModel{Id=3,Sold=false,Reservation=false,HallId=1}
            });
        }
    }       
}