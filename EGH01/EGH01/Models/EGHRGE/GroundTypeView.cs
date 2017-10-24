using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Primitives;

namespace EGH01.Models.EGHRGE
{
    public class GroundTypeView
    {
        
        public enum REGIM { INIT, ERROR, RUNERROR, REPORT };

        public REGIM Regim { get; set; }
        public int type_code { get; set; }    // код типа инцидента 
        public string name { get; set; }        // Наименование типа 
        public float porosity { get; set; }   // пористость     >0    <1, безразмерная , доля застрявшего  в грунте нефтепродукта       
        public float soilmoisture { get; set; }   // влажность грунта (от 0 до 1)
        public float watercapacity { get; set; }   // капиллярная влагоемкость (от 0 до 1)
        public float holdmigration { get; set; }   // коэфф. задержки миграции нефтепродуктов 
        public float waterfilter { get; set; }   // коэфф. фильтрации воды  
        public float аveryanovfactor { get; set; }   // коэффициент Аверьянова (от 4 до 9)
        public float density { get; set; }
        public float distribution { get; set; }
         public float    diffusion { get; set; }
        public float sorption { get; set; }
        public float permeability { get; set; }
        static public string VIEWNAME = "GroundTypeCreate";

        public static bool Handler(RGEContext context, NameValueCollection parms)
        {
            bool rc = false;
            GroundTypeView viewcontext = null;
            string menuitem = parms["menuitem"] ?? "Empty";
            if ((viewcontext = context.GetViewContext(VIEWNAME) as GroundTypeView) != null)
            {
                viewcontext.Regim = REGIM.INIT;
                string namec = parms["name"];
                if (String.IsNullOrEmpty(namec)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    viewcontext.name = namec;

                }
                string Diffusion = parms["diffusion"];
                if (String.IsNullOrEmpty(Diffusion)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float d = 0.0f;
                    if (Helper.FloatTryParse(Diffusion, out d)) { viewcontext.diffusion = d; }
               

                }
                string Porosity = parms["porosity"];
                if (String.IsNullOrEmpty(Porosity)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float p = 0.0f;
                    if (Helper.FloatTryParse(Porosity, out p)) { viewcontext.porosity = p; }


                }
                string Soilmoisture = parms["soilmoisture"];
                if (String.IsNullOrEmpty(Soilmoisture)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float s = 0.0f;
                    if (Helper.FloatTryParse(Soilmoisture, out s)) { viewcontext.soilmoisture = s; }


                }
                string Watercapacity = parms["watercapacity"];
                if (String.IsNullOrEmpty(Watercapacity)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float w=0.0f;
                    if (Helper.FloatTryParse(Watercapacity, out w)) { viewcontext.watercapacity = w; }


                }
                string Holdmigration = parms["holdmigration"];
                if (String.IsNullOrEmpty(Holdmigration)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float h = 0.0f;
                    if (Helper.FloatTryParse(Holdmigration, out h)) { viewcontext.holdmigration = h; }


                }
                string Waterfilter = parms["waterfilter"];
                if (String.IsNullOrEmpty(Waterfilter)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float wa = 0.0f;
                    if (Helper.FloatTryParse(Waterfilter, out wa)) { viewcontext.waterfilter = wa; }


                }
                string Averyanovfactor = parms["аveryanovfactor"];
                if (String.IsNullOrEmpty(Averyanovfactor)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float a = 0.0f;
                    if (Helper.FloatTryParse(Averyanovfactor, out a)) { viewcontext.аveryanovfactor = a; }


                }
                string Density = parms["density"];
                if (String.IsNullOrEmpty(Density)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float de = 0.0f;
                    if (Helper.FloatTryParse(Density, out de)) { viewcontext.density = de; }

                }
                
                string Distribution = parms["distribution"];
                if (String.IsNullOrEmpty(Distribution)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float di = 0.0f;
                    if (Helper.FloatTryParse(Distribution, out di)) { viewcontext.distribution = di; }


                }
                string Sorption = parms["sorption"];
                if (String.IsNullOrEmpty(Sorption)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float so = 0.0f;
                    if (Helper.FloatTryParse(Sorption, out so)) { viewcontext.sorption = so; }


                }
                
                string Permeability = parms["permeability"];
                if (String.IsNullOrEmpty(Permeability)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float pe = 0.0f;
                    if (Helper.FloatTryParse(Permeability, out pe)) { viewcontext.permeability = pe; }


                }

            }
            return rc;
        }
    }


}