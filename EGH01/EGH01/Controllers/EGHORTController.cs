using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGH01.Models.EGHORT;
using EGH01DB;

namespace EGH01.Controllers
{

    public partial class EGHORTController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.EGHLayout = "ORT";
            ORTContext db = null;
            try
            {
                db = new ORTContext(this);

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


       public ActionResult ChoiceClassificationResult()
        {
            ViewBag.EGHLayout = "ORT";
            ORTContext db = null;
            ActionResult view = View("Index", db);
            try
            {
                db = new ORTContext(this);
                view = View(db);
                ORTContextView context = ORTContextView.HandlerChoice(db,this.Request.Params);
                switch(context.Regim)
                { 
                 case ORTContextView.REGIM.INIT:   view = View(db); break;
                 case ORTContextView.REGIM.CHOICE: view = View("Index", db); context.Regim = ORTContextView.REGIM.INIT;  break;
                 case ORTContextView.REGIM.CANCEL: view = View("Index", db); context.Regim = ORTContextView.REGIM.INIT;  break; 
                 default:  view = View(db); break;
                }                 

            }
            catch (RGEContext.Exception e)   //ORTContext.Exception
            {
                ViewBag.msg = e.Message;
            }
            finally
            {
                //if (db != null) db.Disconnect();
            }

            return view;
        }

         public ActionResult Rehabilitation()
        {
            ViewBag.EGHLayout = "ORT";
            ORTContext db = null;
            ActionResult view = View("Index", db);
            try
            {
                db = new ORTContext(this);
                ORTContextView context = ORTContextView.HandlerRehabilitation(db,this.Request.Params);
                switch(context.Regim)
                { 
                  case ORTContextView.REGIM.REPORT:  view = View(db); break;
                  case ORTContextView.REGIM.SAVE:    
                                                ORTContext.ECORehabilitation.Create(db, context.ecorehabilitation);
                                                view = View("Index", db);
                                                break;       
                 case ORTContextView.REGIM.CANCEL:  view = View("Index", db); break; 
                 default:  view = View(db); break;
                }                 

               
            }
            catch (RGEContext.Exception e)   //ORTContext.Exception
            {
                ViewBag.msg = e.Message;
            }
            finally
            {
                //if (db != null) db.Disconnect();
            }

            return view;
        }


              
        //public ActionResult ClassificationEvalution()
        //{
        //  ViewBag.EGHLayout = "GEA";
        //  ActionResult view = View("Index");
        //  GEAContext db = null;
        //  try
        //  {
        //        db = new GEAContext(this);
        //        GEAContextView context = GEAContextView.HandlerClassification(db,this.Request.Params);
        //        switch(context.Regim)
        //        { 
        //         case GEAContextView.REGIM.REPORT:  view = View(db); break;
        //         case GEAContextView.REGIM.SAVE:    
        //                                        GEAContext.ECOClassification.Create(db, context.ecoclassifiation);
        //                                        view = View("Index", db);
        //                                        break;       
        //         case GEAContextView.REGIM.CANCEL:  view = View("Index", db); break; 
        //         default:  view = View(db); break;
        //        }                 

        //    }
        //    catch (RGEContext.Exception e)
        //    {
        //        ViewBag.msg = e.message;
        //    }
        //    finally
        //    {
        //        //if (db != null) db.Disconnect();
        //    } 
        //    return view;
        //}






        //public class OrtData
        //{
        //    public List<string> RGEReport = new List<string> { "АЗС 28 - 17.09.2016", "Нефтебаза - 19.09.2016", "Хранилище 4 - 21.09.2016" };
        //    public List<string> TypeObj = new List<string> { "Река", "Лес", "Болото" };
        //    public List<string> AccidentObj = new List<string> { "АЗС 28", "Нефтебаза", "Хранилище 4" };
        //    public List<string> ObjPoints = new List<string> { "АЗС 28", "Колодец", "Проходная" };
        //}
        //public ActionResult IndexDebug()
        //{
        //    EGH01DB.RGEContext db = new EGH01DB.RGEContext();
        //    OrtData oData = new OrtData();
        //    ViewBag.RGEReport = new SelectList(oData.RGEReport);
        //    //if (db.IsConnect) ViewBag.msg = "соединение  c БД установлено";
        //    //else ViewBag.msg = "соединение  c БД  не установлено";
        //    return View(oData);
        //}

        //public ActionResult Report()
        //{
        //    //if (db.IsConnect) ViewBag.msg = "соединение  c БД установлено";
        //    //else ViewBag.msg = "соединение  c БД  не установлено";

        //    return View();
        //}

	}
}
