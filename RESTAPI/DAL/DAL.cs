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

        public List<HallModel> hallList;

        public DB()
        {

            seatsList = new List<SeatModel>();

            hallList = new List<HallModel>();

            hallList.AddRange(new[]
            {
                new HallModel{Id=1,name="Pierwsza sala"},
                new HallModel{Id=2,name="Druga sala"},
                new HallModel{Id=3,name="Trzecia sala"}
            });

            seatsList.AddRange(new[]
            {
                new SeatModel{Id=1,Sold=false,Reservation=false,HallId=1},
                new SeatModel{Id=2,Sold=false,Reservation=false,HallId=1},
                new SeatModel{Id=3,Sold=false,Reservation=false,HallId=1}
            });
        }
    }       
}