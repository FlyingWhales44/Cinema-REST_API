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

            for(int i = 1; i < 65; i++)
                seatsList.Add(new SeatModel { Id = i, HallId = 1, Sold = false, Reservation = false });

            for (int i = 65; i < 166; i++)
                seatsList.Add(new SeatModel { Id = i, HallId = 2, Sold = false, Reservation = false });

            for (int i = 166; i < 392; i++)
                seatsList.Add(new SeatModel { Id = i, HallId = 3, Sold = false, Reservation = false });

        }
    }       
}