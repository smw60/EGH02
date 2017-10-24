using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Primitives;

namespace EGH01.Models.EGHORT
{
    public class PenetrationDepthView
    {
        public enum REGIM { INIT, ERROR, RUNERROR, REPORT };

        public REGIM Regim { get; set; }
        public int    code { get; set; }           
        public string name { get; set; }               
        public float?  mindepth { get; set; }        
        public float?  maxdepth { get; set; }
        public const string VIEWNAME = "PenetrationDepthCreate";
        public static bool Handler(ORTContext context, NameValueCollection parms)
        {
            bool rc = false;
            PenetrationDepthView viewcontext = null;
            string menuitem = parms["menuitem"] ?? "Empty";
            if ((viewcontext = context.GetViewContext("PenetrationDepthCreate") as PenetrationDepthView) != null)
            {
                viewcontext.Regim = REGIM.INIT;

                string Name = parms["name"];
                if (String.IsNullOrEmpty(Name)) viewcontext.Regim = REGIM.ERROR;
                else
                {

                    viewcontext.name = Name;

                }
                string Min = parms["mindepth"];
                if (String.IsNullOrEmpty(Min)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float m = 0.0f;
                    if (Helper.FloatTryParse(Min, out m)) { viewcontext.mindepth = m; }


                }
                string Max = parms["maxdepth"];
                if (String.IsNullOrEmpty(Max)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    float mx = 0.0f;
                    if (Helper.FloatTryParse(Max, out mx)) { viewcontext.maxdepth = mx; }


                }


            }
             return rc;
        }


    }
}