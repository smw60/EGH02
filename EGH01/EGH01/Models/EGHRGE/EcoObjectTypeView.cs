using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EGH01.Models.EGHRGE
{
    public class EcoObjectTypeView
    {
        public int type_code { get; set; }    // код типа природоохранного объекта
        public string name   { get; set; }    // название природоохранного объекта 
        public int list_water { get; set; }
        public bool iswaterobject { get; set; }
    }
}