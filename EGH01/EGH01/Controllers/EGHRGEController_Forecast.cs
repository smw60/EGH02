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
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01DB.Points;
using EGH01DB.Objects;
namespace EGH01.Controllers
{
    public partial  class EGHRGEController: Controller
    {
       
        public ActionResult Forecast( )
        {
           RGEContext context = null;
           ActionResult view = View("Index");
           ViewBag.EGHLayout = "RGE.Forecast";
           try
           {
                context = new RGEContext(this);
                view = View(context);

                ChoiceRiskObjectContext coc = ChoiceRiskObjectContext.Handler(context, this.HttpContext.Request.Params);
                ForecastViewConext      fvc =  ForecastViewConext.Handler(context, this.HttpContext.Request.Params);
                string menuitem = this.HttpContext.Request.Params["menuitem"];
                if (coc.Regim == ChoiceRiskObjectContext.REGIM.SET &&  fvc.Regim != ForecastViewConext.REGIM.ERROR)
                {
                   
                    if (menuitem.Equals("Forecast.Forecast"))
                    {
                        
                            
                                RiskObject riskobject = coc.riskobject;
                                PetrochemicalType petrochemicaltype = fvc.petrochemicaltype;
                                SpreadPoint spreadpoint = new SpreadPoint(riskobject, petrochemicaltype, (float)fvc.Volume, (float)fvc.Temperature);
                                
                                IncidentType incidenttype = new IncidentType();
                                if(EGH01DB.Types.IncidentType.GetByCode(context, (int)fvc.Incident_type_code, out  incidenttype))
                                {
                                    Incident incident = new Incident(
                                                                       (DateTime)fvc.Incident_date,
                                                                       (DateTime)fvc.Incident_date_message,
                                                                       incidenttype,
                                                                       spreadpoint
                                                                    );
                                        fvc.ecoforecast = new RGEContext.ECOForecast(incident);
                                        fvc.Regim = ForecastViewConext.REGIM.REPORT;
                                }
                                else fvc.Regim = ForecastViewConext.REGIM.RUNERROR;
                                                         
                       }
                       else if (menuitem.Equals("Forecast.Cancel")) view = View("Index", context); //view = Redirect("Index");
                       else if (menuitem.Equals("Forecast.Save"))
                       {
                            ForecastViewConext viewcontext = context.GetViewContext("Forecast") as ForecastViewConext;
                            EGH01DB.RGEContext.ECOForecast forecast = viewcontext.ecoforecast;
                            //XmlNode node =  forecast.toXmlNode("Отладка");
                            //XmlDocument doc = new XmlDocument();
                            //doc.AppendChild(doc.ImportNode(node, true));
                            //doc.Save(@"C:\Report.xml");

                            RGEContext.ECOForecast.Create(context, forecast, "отладка");
                         
                           
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
        

        [ChildActionOnly]
        public ActionResult ChoiceRiskObject()
        {
            RGEContext context = null;
            try
            {
                context = new RGEContext(this);
               
            }
            catch (RGEContext.Exception e)
            {
                ViewBag.msg = e.message;
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }

            return PartialView("_ChoiceRiskObject",context);

        }





    }
}