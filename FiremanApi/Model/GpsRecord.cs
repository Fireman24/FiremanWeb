﻿using System;

namespace FiremanModel
{
    public class GpsRecord
    {
        public GpsRecord()
        {
            
        }

        public GpsRecord(FireCar fireCar, Fire fire, double lat, double lon)
        {
            FireCar = fireCar;
            Fire = fire;
            GpsPoint = new GpsPoint()
                       {
                           Lat = lat,
                           Lon = lon
                       };
            DateTime = DateTime.Now;

        }

        public int Id { get; set; }

        public FireCar FireCar { get; set; }

        public Fire Fire { get; set; }

        public DateTime DateTime { get; set; }

        public GpsPoint GpsPoint { get; set; } 
    }
}
