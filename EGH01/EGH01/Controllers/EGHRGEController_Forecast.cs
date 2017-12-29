using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using EGH01.Models.EGHRGE;
using EGH01DB;
//using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01DB.Points;
using EGH01DB.Objects;
namespace EGH01.Controllers
{
    public partial  class EGHRGEController: Controller
    {
       
        public ActionResult Forecast( )
        {
           ActionResult view = View("Index");
           ViewBag.EGHLayout = "RGE.Forecast";
           try
           {
                RGEContext db = new RGEContext(this);
                view = View(db);
                
                ChoiceRiskObjectViewContext coc = ChoiceRiskObjectViewContext.Handler(db, this.HttpContext.Request.Params);
                ForecastViewConext          fvc =  ForecastViewConext.Handler(db, this.HttpContext.Request.Params);

                string menuitem = this.HttpContext.Request.Params["menuitem"];
                if ( menuitem != null && menuitem.Equals("Forecast.Point"))
                {
                    if (fvc.menuitempoint == fvc.menuitemgeop)   fvc.menuitempoint = fvc.menuitemrobj;
                    else fvc.menuitempoint = fvc.menuitemgeop;
                    coc.Regim = ChoiceRiskObjectViewContext.REGIM.INIT;
                }
                else if (coc.Regim == ChoiceRiskObjectViewContext.REGIM.SET &&  fvc.Regim == ForecastViewConext.REGIM.SET)
                {

                    if (menuitem != null && menuitem.Equals("Forecast.Forecast"))
                    {
                              SpreadPoint spreadpoint = new SpreadPoint(coc.riskobject, fvc.petrochemicaltype, (float)fvc.Volume, (float)fvc.Temperature);
                              Incident incident = new Incident((DateTime)fvc.Incident_date, (DateTime)fvc.Incident_date_message, fvc.incidenttype, spreadpoint);
                             // fvc.ecoforecast = new RGEContext.ECOForecast(incident);
                              fvc.ecoforecastx = new RGEContext.ECOForecastX(db, incident);
                              fvc.Regim = ForecastViewConext.REGIM.REPORT;                                     
                    }
                    else if (menuitem != null &&  menuitem.Equals("Forecast.Cancel")) view = View("Index", db);
                    else if (menuitem != null &&  menuitem.Equals("Forecast.Save"))
                    {
                        
                        if (fvc.ecoforecastx != null)
                        {

                            //RGEContext.Report report = fvc.ecoforecastx.CreateReport();
                            XmlNode xn =  fvc.ecoforecastx.CreateReport().toXmlNode(); 
                            EGH01DB.Primitives.Report report = new EGH01DB.Primitives.Report(1000, "П", DateTime.Now, xn);
                            EGH01DB.Primitives.Report.Create(db, report);

                            RGEContext.Report rrr = new RGEContext.Report(xn); 
                           


                               // public Report(int id, string stage, DateTime date, XmlNode xmlcontetnt, string comment = "")

                           
                             // report.Save();
                        
                        } 

                            //ForecastViewConext viewtext = db.GetViewContext(ForecastViewConext.VIEWNAME) as ForecastViewConext;
                            //EGH01DB.RGEContext.ECOForecast forecast = viewcontext.ecoforecast;
                            //RGEContext.ECOForecast.Create(db, forecast, "отладка");
                     }
                }
          }
          catch (RGEContext.Exception e)
          {
                ViewBag.msg = e.message;
          }
          catch (Exception e)
          {
                ViewBag.msg = e.Message;
          }
          return view;
        }

    }
}


//XmlNode node =  forecast.toXmlNode("Отладка");
//XmlDocument doc = new XmlDocument();
//doc.AppendChild(doc.ImportNode(node, true));
//doc.Save(@"C:\Report.xml");



//[ChildActionOnly]
//public ActionResult ChoiceRiskObject()
//{
//    RGEContext context = null;
//    try
//    {
//        context = new RGEContext(this);

//    }
//    catch (RGEContext.Exception e)
//    {
//        ViewBag.msg = e.message;
//    }
//    catch (Exception e)
//    {
//        ViewBag.msg = e.Message;
//    }

//    return PartialView("_ChoiceRiskObject",context);

//}


