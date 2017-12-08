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

            
            //new Menu.MenuItem("Географическая точка", "Forecast.Point", true);
              
        public RiskObject   riskobject;

        public const string VIEWNAME = "_ChoiceRiskObject";

        public ChoiceRiskObjectViewContext()
        {
            this.Regim        = REGIM.INIT;
            this.Template     = string.Empty;
            this.RiskObjectID = -1;
          
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
                                    case "geopinit":     viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.INIT;
                                            break;
                                    case "geopchoice":
                             viewcontext.Regim = ChoiceRiskObjectViewContext.REGIM.CHOICE; 
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