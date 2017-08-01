using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Objects;

namespace EGH01.Models.EGHRGE
{
    public class ForecastViewConext
    {
        public enum REGIM {INIT, ERROR, RUNERROR, REPORT};
        
        public REGIM Regim                        {get; set;}
        public DateTime? Incident_date            {get; set;}
        public DateTime? Incident_date_message    {get; set;}
        public int?      Incident_type_code       {get; set;}
        public float?    Volume                   {get; set;}
        public int?      Petrochemical_type_code  {get; set;}
        public int?      RiskObjectId             {get; set;}
        public int?      Lat_degree               {get; set;}
        public int?      Lat_min                  {get; set;}
        public float?    Lat_sec                  {get; set;}
        public int?      Lng_degree               {get; set;}
        public int?      Lng_min                  {get; set;}
        public float?    Lng_sec                  {get; set;}
        public RGEContext.ECOForecast ecoforecast {get; set;}
        public const string VIEWNAME = "Forecast";

        public static bool Handler(RGEContext context, NameValueCollection parms)
        {
            bool rc = false;
            ForecastViewConext  viewcontext = null;
            string menuitem  = parms["menuitem"];
            if ((viewcontext = context.GetViewContext(VIEWNAME) as ForecastViewConext) != null)
            {
                        viewcontext.Regim = REGIM.INIT; 
                        string date = parms["date"];
                        if (String.IsNullOrEmpty(date)) viewcontext.Regim = REGIM.ERROR;
                        else
                        {
                            DateTime incident_date = DateTime.MinValue;
                            if (DateTime.TryParse(date, out incident_date)) viewcontext.Incident_date = (DateTime?)incident_date;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        string date_message = parms["date_message"];
                        if (String.IsNullOrEmpty(date_message)) viewcontext.Regim = REGIM.ERROR;
                        else
                        {
                            DateTime incident_date_message = DateTime.MinValue;
                            if (DateTime.TryParse(date_message, out incident_date_message)) viewcontext.Incident_date_message = (DateTime?)incident_date_message;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        string petrochemicaltype = parms["petrochemicaltype"];
                        if (String.IsNullOrEmpty(petrochemicaltype)) viewcontext.Regim = REGIM.ERROR;
                        else
                        {
                            int code = -1;
                            if (int.TryParse(petrochemicaltype, out code)) viewcontext.Petrochemical_type_code = (int?)code;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        string incidenttype = parms["incidenttype"];
                        if (String.IsNullOrEmpty(incidenttype)) viewcontext.Regim = REGIM.ERROR;
                        else
                        {
                            int code = -1;
                            if (int.TryParse(incidenttype, out code)) viewcontext.Incident_type_code = (int?)code;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        string volume = parms["volume"];
                        if (String.IsNullOrEmpty(volume)) viewcontext.Regim = REGIM.ERROR;
                        {
                            float v = 0.0f;
                            if (float.TryParse(volume, out v)) viewcontext.Volume = (float?)v;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        string riskobjectid = parms["riskobjectid"];
                        if (String.IsNullOrEmpty(riskobjectid)) viewcontext.Regim = REGIM.ERROR;
                        {
                            int id = 0;
                            if (int.TryParse(riskobjectid, out id)) viewcontext.RiskObjectId = (int?)id;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        if (menuitem != null)
                        {
                            rc = menuitem.Equals("Forecast.Forecast") || menuitem.Equals("Forecast.Cancel")|| menuitem.Equals("Forecast.Save"); 
                        }
                        else viewcontext.Regim = REGIM.INIT;
           }       
           return rc;
        }
    }
}

