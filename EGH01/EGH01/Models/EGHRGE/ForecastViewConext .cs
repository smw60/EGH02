using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGH01DB;
using EGH01DB.Types;
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
        //public int?      Petrochemical_type_code  {get; set;}
       // public int?      RiskObjectId            {get; set;}
        public int?      Lat_degree               {get; set;}
        public int?      Lat_min                  {get; set;}
        public float?    Lat_sec                  {get; set;}
        public int?      Lng_degree               {get; set;}
        public int?      Lng_min                  {get; set;}
        public float?    Lng_sec                  {get; set;}
        public float?    Temperature              {get; set;}
        public PetrochemicalType petrochemicaltype;
        public RiskObject        riskobject;         
        public RGEContext.ECOForecast ecoforecast {get; set;}
        public const string VIEWNAME = "Forecast";
        
        //public ForecastViewConext()
        //{
        //    this.petrochemicaltype = null;
        //    this.riskobject = null;
        //}


        public static ForecastViewConext Handler(RGEContext context, NameValueCollection parms)
        {
            ForecastViewConext  viewcontext = null;
          //  string menuitem  = parms["menuitem"];
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

                        string parmpetrochemicaltype = parms["petrochemicaltype"];
                        if (String.IsNullOrEmpty(parmpetrochemicaltype)) viewcontext.Regim = REGIM.ERROR;
                        else
                        {
                            int code = -1;
                            if (int.TryParse(parmpetrochemicaltype, out code))
                            {
                                if (viewcontext.petrochemicaltype == null || viewcontext.petrochemicaltype.code_type != code)
                                {
                                    viewcontext.petrochemicaltype = new PetrochemicalType();
                                    if (!PetrochemicalType.GetByCode(context, code, ref viewcontext.petrochemicaltype)) viewcontext.Regim = REGIM.ERROR;
                                }
                            }
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
                        else
                        {
                            float v = 0.0f;
                            if (float.TryParse(volume, out v)) viewcontext.Volume = (float?)v;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        string temperature = parms["temperature"];
                        if (String.IsNullOrEmpty(temperature)) viewcontext.Regim = REGIM.ERROR;
                        else
                        {
                            float t = 0.0f;
                            if (float.TryParse(temperature, out t)) viewcontext.Temperature = (float?)t;
                            else viewcontext.Regim = REGIM.ERROR;
                        }

                        //string riskobjectid = parms["riskobjectid"];
                        //if (String.IsNullOrEmpty(riskobjectid)) viewcontext.Regim = REGIM.ERROR;
                        //else
                        //{
                        //    int id = 0;
                        //    if (int.TryParse(riskobjectid, out id))
                        //    {
                        //        if (viewcontext.riskobject == null || viewcontext.riskobject.id != id) 
                        //        {
                        //            viewcontext.riskobject = new RiskObject();
                        //            if (!RiskObject.GetById(context, id, ref viewcontext.riskobject)) viewcontext.Regim = REGIM.ERROR;
                        //        }
                        //    }
                        //    else viewcontext.Regim = REGIM.ERROR;
                        //}

                        
           }
            return viewcontext;
        }
    }
}

