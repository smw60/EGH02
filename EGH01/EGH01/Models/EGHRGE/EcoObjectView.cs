using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EGH01.Models.EGHRGE
{
    public class EcoObjectView
    {
        public int      id {get; set;}   
        public string   name { get; set;}
        public float waterdeep { get; set; }   // глубина грунтовых вод    (м)
        public float height { get; set; }
        public int latitude { get; set; }
        public int lngitude { get; set; }
        public int lat_m { get; set; }
        public int lng_m { get; set; }
        public float lat_s { get; set; }
        public float lng_s { get; set; }
        public float angel { get; set; }
        public float distance{ get; set; }
        public int list_groundType { get; set; }
        public int list_cadastre { get; set; }  
        public int list_ecoType { get; set; }
        public  int list_WaterProtectionArea { get; set; }
        public bool iswaterobject { get;  set; }

    }

    


}