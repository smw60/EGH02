using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EGH01DB.Types;

namespace EGH01.Models.EGHCCO
{
    public class PetrochemicalTypeView
    {
        public int    code_type { get; set; }           // код типа нефтепродукта
        public string name { get; set; }                // название нефтепродукта
        public float  boilingtemp { get; set; }         // температура кипения (С)
        public float  density { get; set; }             // плотность (г/см<sup>3</sup>)
        public float  viscosity { get; set; }           // кинематическая вязкость (мм2/с)
        public float  solubility { get; set; }          // растворимость (мг/дм<sup>3</sup>)
        public float  tension { get; set; }             // коэффициент поверхностного натяжения (кг/с2)
        public float  dynamicviscosity { get; set; }    // динамическая вязкость (кг/м*с)
        public float  diffusion { get; set; }           // коэффициент диффузии (м2/с)
        public int list_PetrochemicalCategories { get; set; }
        //public PetrochemicalCategories petrochemicalcategories { get; set; }

    }
}