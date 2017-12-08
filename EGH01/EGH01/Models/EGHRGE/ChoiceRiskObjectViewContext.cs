using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Objects;

namespace EGH01.Models.EGHRGE
{
    public class ChoiceRiskObjectViewContext
    {

        public enum REGIM { INIT, CHOICE, SET, ERROR };

        public REGIM Regim { get; set; }
        public string Template { get; set; }
        public int RiskObjectID { get; set; }
        public string  Latitude {get;set;}
        public string Lat_m { get; set; }
        public string Lat_s { get; set; }
        public string Lngitude { get; set; }
        public string Lng_m { get; set; }
        public string Lng_s { get; set; }
            //new Menu.MenuItem("Географическая точка", "Forecast.Point", true);

        public RiskObject   riskobject;

        public const string VIEWNAME = "_ChoiceRiskObject";

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
        
        public static ChoiceRiskObjectViewContext Handler(RGEContext context, NameValueCollection parms)
        {
           
            ChoiceRiskObjectViewContext viewcontext = HandlerRiskObject(context, parms);
                    
            return (viewcontext);
        }
        
        private static ChoiceRiskObjectViewContext HandlerRiskObject(RGEContext context, NameValueCollection parms)
        { 
            ChoiceRiskObjectViewContext viewcontext = null;
            if ((viewcontext = context.GetViewContext(VIEWNAME) as ChoiceRiskObjectViewContext) != null)
            {
                    string choicefind = parms["ChoiceRiskObject.choicefind"];
                    if  (!string.IsNullOrEmpty(choicefind))
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
                        case "geopchoice":
                            { viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.CHOICE;
                                string Latitude = parms["ChoiceRiskObject.Latitude"];

                                if (!string.IsNullOrEmpty(Latitude))
                                {
                                    viewcontext.Latitude = Latitude;
                                }
                                string Lat_m = parms["ChoiceRiskObject.Lat_m"];

                                if (!string.IsNullOrEmpty(Lat_m))
                                {
                                    viewcontext.Lat_m = Lat_m;
                                }
                                string Lat_s = parms["ChoiceRiskObject.Lat_s"];

                                if (!string.IsNullOrEmpty(Lat_s))
                                {
                                    viewcontext.Lat_s = Lat_s;
                                }
                                string Lngitude = parms["ChoiceRiskObject.Lngitude"];

                                if (!string.IsNullOrEmpty(Lngitude))
                                {
                                    viewcontext.Lngitude = Lngitude;
                                }
                                string Lng_m = parms["ChoiceRiskObject.Lng_m"];

                                if (!string.IsNullOrEmpty(Lng_m))
                                {
                                    viewcontext.Lng_m = Lng_m;
                                }
                                string Lng_s = parms["ChoiceRiskObject.Lng_s"];

                                if (!string.IsNullOrEmpty(Lng_s))
                                {
                                    viewcontext.Lng_s = Lng_s;
                                }
                            }
                                            break;
                                    case "geopset":      viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.SET; 
                                            break;
                                    default: break;
                                }
                    }
            }
            return (viewcontext);
        }
    }
}


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