using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using EGH01DB;
namespace EGH01.Models.EGHORT
{
    public class ORTContextView
    {
        public enum REGIM {INIT, CHOICE, CANCEL, REPORT, SAVE}
        public const string VIEWNAME = "ORTContextView";
        public int? idclassification  { get; set; }
        public GEAContext.ECOClassification  ecoclassifiation = null;
        public ORTContext.ECORehabilitation  ecorehabilitation = null; 
        public REGIM Regim = REGIM.INIT;
  
        public static ORTContextView HandlerChoice(ORTContext db, NameValueCollection parms)
        {
               ORTContextView rc = db.GetViewContext(VIEWNAME) as ORTContextView;
               if (rc == null) db.SaveViewContext(ORTContextView.VIEWNAME, rc = new ORTContextView());
               string menuitem = parms["menuitem"];
               if (menuitem != null)
               {
                   if  (menuitem.Equals("ChoiceClassification.Choice")) 
                    {
                         rc.Regim = REGIM.CHOICE;
                          if (!string.IsNullOrEmpty(parms["ChoiceClassification.id"]))
                          {
                             int id = 0;
                             if (int.TryParse(parms["ChoiceClassification.id"], out id)) rc.ecoclassifiation = GEAContext.ECOClassification.GetById(db,(int)(rc.idclassification = id)); 
                             else rc.ecoclassifiation = null; 
                          }
                     } 
                     else  if (menuitem.Equals("ChoiceClassification.Cancel"))    rc.Regim = REGIM.CANCEL;
               } else rc.Regim = REGIM.INIT;            
 
            return rc;     
       }
       public static ORTContextView HandlerRehabilitation(ORTContext db, NameValueCollection parms)
       {
               ORTContextView rc = db.GetViewContext(VIEWNAME) as ORTContextView;
              if (rc == null) db.SaveViewContext(ORTContextView.VIEWNAME, rc = new ORTContextView());
              string menuitem = parms["menuitem"];
              if (menuitem != null)
              {
                if (menuitem.Equals("Report.Save")) 
                {
                     rc.Regim = REGIM.SAVE;

                }  
              }
              else  if (rc.ecoclassifiation != null) 
              { 
                  rc.Regim = REGIM.REPORT;
                  rc.ecorehabilitation = new  ORTContext.ECORehabilitation(rc.ecoclassifiation);
              }
              return rc;  
        }  

    }
}