using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EGH01.Models.EGHMAP
{
    public class MapPointView
    {
        public string Lngitude { get; set; }
        public string Lng_m { get; set; }
        public string Lng_s { get; set; }

        public string Latitude { get; set; }
        public string Lat_m { get; set; }
        public string Lat_s { get; set; }
    }




}