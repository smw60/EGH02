using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Objects;
using EGH01DB.Primitives;
using EGH01DB.Points;

namespace EGH01.Models.EGHRGE
{
    public class ChoiceRiskObjectViewContext
    {

        public enum REGIM { INIT, CHOICE, SET, ERROR };

        public REGIM Regim { get; set; }
        public string Template { get; set; }
        public Coordinates coordinates;
        public int RiskObjectID { get; set; }
        public string  Latitude {get;set;}
        public string Lat_m { get; set; }
        public string Lat_s { get; set; }
        public string Lngitude { get; set; }
        public string Lng_m { get; set; }
        public string Lng_s { get; set; }
            

        public RiskObject   riskobject;

        public static readonly string VIEWNAME = "_ChoiceRiskObject";

        public ChoiceRiskObjectViewContext()
        {
            this.Regim        = REGIM.INIT;
            this.Template     = string.Empty;
            this.RiskObjectID = -1;
            this.Latitude = string.Empty;
            this.Lat_m = string.Empty;
            this.Lat_s = string.Empty;
            this.Lngitude = string.Empty;
            this.Lng_m = string.Empty;
            this.Lng_s = string.Empty;


        }
        
        
        
       public  static ChoiceRiskObjectViewContext Handler(RGEContext context, NameValueCollection parms)
        { 
            ChoiceRiskObjectViewContext viewcontext = context.GetViewContext(VIEWNAME) as ChoiceRiskObjectViewContext;


            if (viewcontext  != null)
            {
                string choicefind = parms["ChoiceRiskObject.choicefind"];
                if (!string.IsNullOrEmpty(choicefind))
                {
                    switch (choicefind)
                    {
                        case "init": viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.INIT;
                            break;
                        case "choice":
                            string template = parms["ChoiceRiskObject.template"];
                            if (!string.IsNullOrEmpty(template))
                            {
                                viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.CHOICE;
                                viewcontext.Template = template;
                            }
                            break;
                        case "set":
                            int id = 0;
                            string formid = parms["ChoiceRiskObject.id"];
                            if (!string.IsNullOrEmpty(formid) && int.TryParse(formid, out id))
                            {
                                viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.SET;
                                viewcontext.RiskObjectID = id;
                                if (viewcontext.riskobject == null || viewcontext.riskobject.id != id)
                                {
                                    viewcontext.riskobject = new RiskObject();
                                    if (!RiskObject.GetById(context, id, ref viewcontext.riskobject)) viewcontext.Regim = REGIM.ERROR;
                                }
                            }
                            break;
                        case "geopinit": viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.INIT;
                            break;
                        case "geopchoice": viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.CHOICE;
                            viewcontext.coordinates = viewcontext.getCoordinatesParm(viewcontext, parms);
                            break;
                        case "geopset": viewcontext.Regim = REGIM.SET;
                            viewcontext.coordinates = viewcontext.getCoordinatesParm(viewcontext, parms);
                            MapePoint mp = new MapePoint(context, viewcontext.coordinates);
                            viewcontext.riskobject = new RiskObject(mp);
                            break;
                        default: break;
                    }
                }
            }
            else
            {
               viewcontext = new ChoiceRiskObjectViewContext();
               viewcontext.Regim = REGIM.INIT;
            }
            return (viewcontext);
        }
        
        private REGIM ParmParse(string parm, REGIM regim, out int iparm)
        {
           iparm = 0;
           return (string.IsNullOrEmpty(parm) || !int.TryParse(parm, out iparm))? REGIM.ERROR: regim;
        }
        
        private Coordinates getCoordinatesParm(ChoiceRiskObjectViewContext viewcontext, NameValueCollection parms)
        {
            Coordinates rc = null;

            int lat_d = 0, lat_m = 0, lat_s = 0, lng_d = 0, lng_m = 0, lng_s = 0;

            viewcontext.Regim = ParmParse(parms["ChoiceRiskObject.Latitude"], viewcontext.Regim, out lat_d);
            viewcontext.Regim = ParmParse(parms["ChoiceRiskObject.Lat_m"],    viewcontext.Regim, out lat_m);
            viewcontext.Regim = ParmParse(parms["ChoiceRiskObject.Lat_s"],    viewcontext.Regim, out lat_s);


            viewcontext.Regim = ParmParse(parms["ChoiceRiskObject.Lngitude"], viewcontext.Regim, out lng_d);
            viewcontext.Regim = ParmParse(parms["ChoiceRiskObject.Lng_m"],    viewcontext.Regim, out lng_m);
            viewcontext.Regim = ParmParse(parms["ChoiceRiskObject.Lng_s"],    viewcontext.Regim, out lng_s);

            if (viewcontext.Regim != REGIM.ERROR) rc = new Coordinates(lat_d, lat_m, lat_s, lng_d, lng_m, lng_s); 

            return rc;
        }

    }
}
//public static ChoiceRiskObjectViewContext Handler(RGEContext context, NameValueCollection parms)
//{

//    ChoiceRiskObjectViewContext viewcontext = HandlerRiskObject(context, parms);

//    return (viewcontext);
//}

//string choicefind = parms["ChoiceRiskObject.choicefind"];
//if  (!string.IsNullOrEmpty(choicefind))
//{ 
//        switch (choicefind)
//        {
//                case "init": viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.INIT;
//                        break;
//                case "choice":
//                        string template = parms["ChoiceRiskObject.template"];
//                        if (!string.IsNullOrEmpty(template))
//                        {
//                            viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.CHOICE;
//                            viewcontext.Template = template;
//                        }
//                        break;
//                case "set":
//                        int id = 0;
//                        string formid = parms["ChoiceRiskObject.id"];
//                        if (!string.IsNullOrEmpty(formid) && int.TryParse(formid, out id))
//                        {
//                            viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.SET;
//                            viewcontext.RiskObjectID = id;
//                            if (viewcontext.riskobject == null || viewcontext.riskobject.id != id)
//                            {
//                                viewcontext.riskobject = new RiskObject();
//                                if (!RiskObject.GetById(context, id, ref viewcontext.riskobject)) viewcontext.Regim = REGIM.ERROR;
//                            }
//                        }
//                        break;
//        }
//}




                     //       if (choicefind.Equals("init"))
             //       {
             //           viewcontext.Regim = ChoiceRiskObjectContext.REGIM.INIT;
             //       }
             //       else if (choicefind.Equals("choice"))
             //       {
             //           string template = parms["ChoiceRiskObject.template"];
             //           if (!string.IsNullOrEmpty(template))
             //           {
             //               viewcontext.Regim = ChoiceRiskObjectContext.REGIM.CHOICE;
             //               viewcontext.Template = template;
             //           }

             //       }
             //       else if (choicefind.Equals("set"))
             //       {
             //           int id = 0;
             //           string formid = parms["ChoiceRiskObject.id"];
             //           if (!string.IsNullOrEmpty(formid) && int.TryParse(formid, out id))
             //           {
             //               viewcontext.Regim = ChoiceRiskObjectContext.REGIM.SET;
             //               viewcontext.RiskObjectID = id;
             //               if (viewcontext.riskobject == null || viewcontext.riskobject.id != id)
             //               {
             //                  viewcontext.riskobject = new RiskObject();
             //                  if (!RiskObject.GetById(context, id, ref viewcontext.riskobject)) viewcontext.Regim = REGIM.ERROR;
             //               }
             //            }
             //            else viewcontext.Regim = REGIM.ERROR;
             //        }

             //    }
             //}