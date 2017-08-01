using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EGH01.Models.EGHRGE
{
    public class WaterPropertiesView
    {
        public int   water_code  { get; set; }    // код показателя воды
        public float temperature { get; set; }    // температура , градусы Цельсия
        public float viscocity   { get; set; }    // вязкость , кг/м с 
        public float density     { get; set; }    // плотность, кг/м3
        public float tension     { get; set; }    // коэф. поверхностного натяжения , кг/с2 
        
	}
}