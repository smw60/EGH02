using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Objects;
namespace EGH01.Models.EGHCAI
{
    public class RiskObject
    {
        public enum REGIM { INIT, ERROR, RUNERROR, REPORT };

        public REGIM Regim { get; set; }
        public int type_code { get; set; }    // уникальный идинтификатор
        public string name { get; set; }        // Наименование объекта
        public string adress { get; set; }
        public string RiskObjectType { get; set; }
        public int latitude { get; set; }
        public int lngitude { get; set; }
        public int lat_m { get; set; }
        public int lng_m { get; set; }
        public float lat_s { get; set; }
        public float lng_s { get; set; }
        public int selectlist { get; set; }
        public int list_groundType { get; set; }
        public int list_region { get; set; }
        public int list_district { get; set; }
        public int list_cadastre { get; set; }
        public DateTime foundationdate { get; set; }
        public DateTime reconstractiondate { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public int groundtank { get; set; }
        public int undergroundtank { get; set; }
        public float waterdeep { get; set; }   // глубина грунтовых вод    (м)
        public float height { get; set; }
        public bool watertreatment { get; set; }
        public bool watertreatmentcollect { get; set; }
        public string base64 { get; set; }
         public byte[]   maps { get; set; }




        public static bool Handler(CAIContext context, NameValueCollection parms)
        {
            bool rc = false;
            RiskObject viewcontext = null;
            string menuitem = parms["menuitem"];
            if ((viewcontext = context.GetViewContext("RiskObject") as RiskObject) != null)
            {
                viewcontext.Regim = REGIM.INIT;
                string foundationdat = parms["foundationdate"];
                if (String.IsNullOrEmpty(foundationdat)) viewcontext.Regim = REGIM.ERROR;
                else
                {
                    DateTime foundationdate = DateTime.MinValue;
                    if (DateTime.TryParse(foundationdat, out foundationdate)) viewcontext.foundationdate = (DateTime)foundationdate;
                    else viewcontext.Regim = REGIM.ERROR;
                }
            }
            return rc;
        }
    }


}
