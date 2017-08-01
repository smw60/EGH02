using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using EGH01DB;
namespace EGH01.Models.EGHGEA
{
    public class GEAContextView
    {
        public enum REGIM {INIT, CHOICE, CANCEL, REPORT, SAVE}
        public const string VIEWNAME = "GEAContextView";
        public int? idevalution  { get; set; }
        public CEQContext.ECOEvalution       ecoevolution = null;
        public GEAContext.ECOClassification  ecoclassifiation = null;
        public REGIM Regim = REGIM.INIT;
  
        public static GEAContextView HandlerChoice(GEAContext db, NameValueCollection parms)
        {
             GEAContextView rc = db.GetViewContext(VIEWNAME) as GEAContextView;
             if (rc == null) db.SaveViewContext(GEAContextView.VIEWNAME, rc = new GEAContextView());
             string menuitem = parms["menuitem"];
             if (menuitem != null)
             {
                    if  (menuitem.Equals("ChoiceEvalutionResult.Choice")) 
                    {
                         rc.Regim = REGIM.CHOICE;
                         if (!string.IsNullOrEmpty(parms["ChoiceEvalutionResult.id"]))
                         {
                            int id = 0;
                            if (int.TryParse(parms["ChoiceEvalutionResult.id"], out id)) rc.ecoevolution = CEQContext.ECOEvalution.GetById(db,id); 
                            else rc.ecoevolution = null; 
                         }
                     } 
                    else  if (menuitem.Equals("ChoiceEvalutionResult.Cancel"))    rc.Regim = REGIM.CANCEL;
             } else rc.Regim = REGIM.INIT;            
 
             return rc;     
       }
        public static GEAContextView HandlerClassification(GEAContext db, NameValueCollection parms)
        {
             GEAContextView rc = db.GetViewContext(VIEWNAME) as GEAContextView;
             if (rc == null) db.SaveViewContext(GEAContextView.VIEWNAME, rc = new GEAContextView());
           
             string menuitem = parms["menuitem"];
             if (menuitem != null)
             {
                if (menuitem.Equals("Report.Save")) 
                {
                     rc.Regim = REGIM.SAVE;

                }  
             }
             else  if (rc.ecoevolution != null) 
             { 
               rc.Regim = REGIM.REPORT;
               rc.ecoclassifiation = new GEAContext.ECOClassification(rc.ecoevolution);
             }
             return rc;  
        }  

    }
}