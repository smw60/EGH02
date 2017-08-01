using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Primitives;

namespace EGH01.Models.EGHGEA
{
    public class WaterPollutionCategoriesView
    {
        public enum REGIM { INIT, ERROR, RUNERROR, REPORT };

        public REGIM Regim { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public float? min { get; set; }
        public float? max { get; set; }
        public int list_cadastre { get; set; }



        public static bool Handler(GEAContext context, NameValueCollection parms)
        {
            bool rc = false;
            WaterPollutionCategoriesView viewcontext = null;
            string menuitem = parms["menuitem"] ?? "Empty";
            if ((viewcontext = context.GetViewContext("WaterPollutionCategoriesCreate") as WaterPollutionCategoriesView) != null)
            {
                viewcontext.Regim = REGIM.INIT;

                string Name = parms["name"];
                if (String.IsNullOrEmpty(Name)) viewcontext.Regim = REGIM.ERROR;
                else
                {

                    viewcontext.name = Name;

                }
                string Min = parms["min"];
                if (String.IsNullOrEmpty(Min)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float m = 0.0f;
                    if (Helper.FloatTryParse(Min, out m)) { viewcontext.min = m; }


                }
                string Max = parms["max"];
                if (String.IsNullOrEmpty(Max)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float mx = 0.0f;
                    if (Helper.FloatTryParse(Max, out mx)) { viewcontext.max = mx; }


                }


            }
            return rc;
        }
    }
}
