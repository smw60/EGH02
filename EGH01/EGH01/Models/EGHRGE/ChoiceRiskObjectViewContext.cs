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
        
        public enum REGIM {INIT, CHOICE, SET};
        
        public REGIM Regim { get; set; }
        public string Template { get; set; }
        public int RiskObjectID { get; set;}
        
        public ChoiceRiskObjectContext()
        {
            this.Regim = REGIM.INIT;
            this.Template = string.Empty;
            this.RiskObjectID = -1;


        }
        public static  bool Handler(RGEContext context, NameValueCollection parms)
        {
            bool rc = false;
            ChoiceRiskObjectContext viewcontext = null;
            string choicefind = parms["ChoiceRiskObject.choicefind"];
            if (!string.IsNullOrEmpty(choicefind))
            {

                if ((viewcontext = context.GetViewContext("_ChoiceRiskObject") as ChoiceRiskObjectContext) != null)
                {
                    if (rc = choicefind.Equals("init"))
                    {
                        viewcontext.Regim = ChoiceRiskObjectContext.REGIM.INIT;
                    }
                    else if (rc = choicefind.Equals("choice"))
                    {
                        string template = parms["ChoiceRiskObject.template"];
                        if (!string.IsNullOrEmpty(template))
                        {
                            viewcontext.Regim = ChoiceRiskObjectContext.REGIM.CHOICE;
                            viewcontext.Template = template;
                        }

                    }
                    else if (rc = choicefind.Equals("set"))
                    {
                        int id = 0;
                        string formid = parms["ChoiceRiskObject.id"];
                        if (!string.IsNullOrEmpty(formid) && int.TryParse(formid, out id))
                        {
                            viewcontext.Regim = ChoiceRiskObjectContext.REGIM.SET;
                            viewcontext.RiskObjectID = id;
                        }
                    }
                }

            }
            return rc;
        }

    }


}