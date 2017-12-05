using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGH01DB;
using System.Collections.Specialized;
using EGH01DB.Objects;

namespace EGH01.Models.EGHRGE
{
    public class ChoiceRiskObjectContext
    {

        public enum REGIM { INIT, CHOICE, SET, ERROR };

        public REGIM Regim { get; set; }
        public string Template { get; set; }
        public int RiskObjectID { get; set; }
        public RiskObject riskobject;
        public const string VIEWNAME = "_ChoiceRiskObject";
        public ChoiceRiskObjectContext()
        {
            this.Regim = REGIM.INIT;
            this.Template = string.Empty;
            this.RiskObjectID = -1;



        }
        public static ChoiceRiskObjectContext Handler(RGEContext context, NameValueCollection parms)
        {
           
            ChoiceRiskObjectContext viewcontext = null;
            if ((viewcontext = context.GetViewContext(VIEWNAME) as ChoiceRiskObjectContext) != null)
            {
                string choicefind = parms["ChoiceRiskObject.choicefind"];
                if  (!string.IsNullOrEmpty(choicefind))
                { 
                        switch (choicefind)
                        {
                                case "init": viewcontext.Regim = ChoiceRiskObjectContext.REGIM.INIT;
                                        break;
                                case "choice":
                                        string template = parms["ChoiceRiskObject.template"];
                                        if (!string.IsNullOrEmpty(template))
                                        {
                                            viewcontext.Regim = ChoiceRiskObjectContext.REGIM.CHOICE;
                                            viewcontext.Template = template;
                                        }
                                        break;
                                case "set":
                                        int id = 0;
                                        string formid = parms["ChoiceRiskObject.id"];
                                        if (!string.IsNullOrEmpty(formid) && int.TryParse(formid, out id))
                                        {
                                            viewcontext.Regim = ChoiceRiskObjectContext.REGIM.SET;
                                            viewcontext.RiskObjectID = id;
                                            if (viewcontext.riskobject == null || viewcontext.riskobject.id != id)
                                            {
                                                viewcontext.riskobject = new RiskObject();
                                                if (!RiskObject.GetById(context, id, ref viewcontext.riskobject)) viewcontext.Regim = REGIM.ERROR;
                                            }
                                        }
                                        break;
                        }
                }
                
            }
            return (viewcontext);
        }
    }
}

  





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