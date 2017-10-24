using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EGH01.Models.EGHRGE
{
    public class CadastreTypeView
    {
        public int      type_code {get; set;}    
        public string   name { get; set;}       
        public int pdk_coef { get; set; }  
        public float water_pdk_coef { get; set; }
        public string ground_doc_name { get; set; }   
        public string water_doc_name { get; set; } 



    }

    


}