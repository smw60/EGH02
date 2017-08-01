using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EGH01.Models.EGHRGE
{
    public class AnchorPointView
    {
        public int      id {get; set;}   
        public string   name { get; set;}        
        public float waterdeep { get; set; }         
        public float height { get; set; }   
        public int list_cadastre { get; set; }  
        public int list_groundType { get; set; }
        public int Lat_d { get; set; }
        public int lngitude { get; set; }
        public int lat_m { get; set; }
        public int lng_m { get; set; }
        public float lat_s { get; set; }
        public float lng_s { get; set; }
        public float angel { get; set; }
        public float distance { get; set; }
    }

    


}