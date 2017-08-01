using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EGH01DB;
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01.Models.EGHCEQ;
using EGH01.Models.EGHRGE;

namespace EGH01.Controllers
{
    public partial class EGHCEQController : Controller
    {

        public ActionResult ChoiceForecastResult()
        {
            ViewBag.EGHLayout = "CEQ";
            ActionResult rc = View("Index");
            try
            {
               CEQContext ceq  = new CEQContext(this);
               CEQViewContext context = CEQViewContext.HandlerChoiceForecast(ceq, this.HttpContext.Request.Params);
               if (context != null &&  context.RegimChoice  == CEQViewContext.REGIM_CHOICE.CHOICE)
               {

                   EGH01DB.RGEContext.ECOForecast forecast =  new EGH01DB.RGEContext.ECOForecast();
                   string comment = string.Empty;
                   if (EGH01DB.RGEContext.ECOForecast.GetById(ceq, (int) context.idforecat, out  forecast, out comment))
                   {
                     context.ecoevalution = new CEQContext.ECOEvalution(forecast); 
                     rc = View("Index",ceq);
 
                   } 

               }
               else   rc = View(ceq);

            }
            catch (EGHDBException e)
            {
                rc = View("Index");
            }
            catch (Exception e)
            {
                rc = View("Index");
            }
            return rc;
        }

        
        public ActionResult EvalutionForecast()
        {
            ViewBag.EGHLayout = "CEQ";
            CEQContext db = null; 
            ActionResult rc = View("Index");
            try
            {
                db = new CEQContext(this);
                rc = View("Index",db);
                CEQViewContext context = CEQViewContext.HandlerEvalutionForecast(db, this.HttpContext.Request.Params);
                switch (context.RegimEvalution)
                {
                    case CEQViewContext.REGIM_EVALUTION.INIT:   rc = View(db); break;
                    case CEQViewContext.REGIM_EVALUTION.CHOICE: rc = View(db); break;
                    case CEQViewContext.REGIM_EVALUTION.REPORT: rc = View(db); break;
                    case CEQViewContext.REGIM_EVALUTION.SAVE:   rc =  View("Index",db);
                                                               CEQContext.ECOEvalution.Create(db,context.ecoevalution,"отладка"); 
                                                               context.RegimEvalution = CEQViewContext.REGIM_EVALUTION.CHOICE;       
                                                               break;
                    default: rc = View("Index", db); break;
                }
                               
            }
            catch (EGHDBException e)
            {
                rc = View("Index",db);
            }
            catch (Exception e)
            {
                rc = View("Index", db);
            }
            return rc;
        }

        public ActionResult Index()
        {
            ViewBag.EGHLayout = "CEQ";
            CEQContext db = null;
            try
            {
                db = new CEQContext(this);
               
            }
            catch (RGEContext.Exception e)
            {
                ViewBag.msg = e.message;
            }
            finally
            {
                //if (db != null) db.Disconnect();
            }

            return View("Index", db);
        }
    }

}




//switch (CEQViewContext.HandlerChoiceForecast(ceq, this.HttpContext.Request.Params))
        //        {
        //            case CEQViewContext.REGIM_CHOICE.INIT:   rc = View(ceq); break;
        //            case CEQViewContext.REGIM_CHOICE.CHOICE:
        //                {  // отладка 

        //                    RGEContext rge = new RGEContext(this);    // 
        //                    ForecastViewConext forctx = (ForecastViewConext)rge.GetViewContext(ForecastViewConext.VIEWNAME);
        //                    if (forctx != null)
        //                    {
        //                        CEQViewContext evalctx = (CEQViewContext)ceq.GetViewContext(CEQViewContext.VIEWNAME);
        //                        evalctx.ecoevalution =  new CEQContext.ECOEvalution(forctx.ecoforecast);
        //                    }
        //                }
        //                rc = View(ceq); break;
        //            case CEQViewContext.REGIM_CHOICE.CANCEL: rc = View("Index",ceq); break;
        //            case CEQViewContext.REGIM_CHOICE.ERROR:  rc = View(ceq); break;
        //            case CEQViewContext.REGIM_CHOICE.REPORT: rc = View(ceq); break;
        //            default: rc = View("Index",ceq); break;
        //        }