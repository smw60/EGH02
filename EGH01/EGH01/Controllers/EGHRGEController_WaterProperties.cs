using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EGH01DB;
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01.Models.EGHRGE;

namespace EGH01.Controllers
{
    public partial class EGHRGEController : Controller
    {

        public ActionResult WaterProperties()
        {
            RGEContext db = null;
            ActionResult view = View("Index");
            ViewBag.EGHLayout = "RGE.WaterProperties";
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("WaterProperties", db);

                if (menuitem.Equals("WaterProperties.Create"))
                {

                    view = View("WaterPropertiesCreate");

                }
                else if (menuitem.Equals("WaterProperties.Delete"))
                {
                    string water_code = this.HttpContext.Request.Params["water_code"];
                    if (water_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(water_code, out c))
                        {
                            EGH01DB.Primitives.WaterProperties wp = new EGH01DB.Primitives.WaterProperties();
                            if (EGH01DB.Primitives.WaterProperties.GetByCode(db, c, out wp))
                            {
                                view = View("WaterPropertiesDelete", wp);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("WaterProperties.Update"))
                {
                    string water_code = this.HttpContext.Request.Params["water_code"];

                    if (water_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(water_code, out c))
                        {
                            WaterProperties wp = new WaterProperties();
                            if (EGH01DB.Primitives.WaterProperties.GetByCode(db, c, out wp))
                            {
                                view = View("WaterPropertiesUpdate", wp);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("WaterProperties.Excel"))
                {
                    EGH01DB.Primitives.WaterPropertiesList list = new EGH01DB.Primitives.WaterPropertiesList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/WaterProperties.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/WaterProperties.xml"), "text/plain", "Свойства воды.xml");


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

        [HttpPost]
        public ActionResult WaterPropertiesCreate(WaterPropertiesView wpv)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.WaterProperties";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                view = View("WaterProperties", db);
                if (menuitem.Equals("WaterProperties.Create.Create"))
                {
                    int id = -1;
                    if (EGH01DB.Primitives.WaterProperties.GetNextCode(db, out id))
                    {
                        int water_code = wpv.water_code;

                        string strtemperature = this.HttpContext.Request.Params["temperature"] ?? "Empty";
                        float temperature;
                        Helper.FloatTryParse(strtemperature, out temperature);

                        string strviscocity = this.HttpContext.Request.Params["viscocity"] ?? "Empty";
                        float viscocity;
                        Helper.FloatTryParse(strviscocity, out viscocity);

                        string strdensity = this.HttpContext.Request.Params["density"] ?? "Empty";
                        float density;
                        Helper.FloatTryParse(strdensity, out density);

                        string strtension = this.HttpContext.Request.Params["tension"] ?? "Empty";
                        float tension;
                        Helper.FloatTryParse(strtension, out tension);

                        WaterProperties wp = new EGH01DB.Primitives.WaterProperties((int)water_code, (float)temperature, (float)viscocity, (float)density, (float)tension);
                        if (EGH01DB.Primitives.WaterProperties.Create(db, wp))
                        {
                            view = View("WaterProperties", db);
                        }
                        else if (menuitem.Equals("WaterProperties.Create.Cancel")) view = View("WaterProperties", db);
                    }
                }
                else if (menuitem.Equals("WaterProperties.Create.Cancel")) view = View("WaterProperties", db);
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

        [HttpPost]
        public ActionResult WaterPropertiesDelete(int water_code)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.WaterProperties";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("WaterProperties.Delete.Delete"))
                {
                    if (EGH01DB.Primitives.WaterProperties.DeleteByCode(db, water_code)) 
                        view = View("WaterProperties", db);
                }
                else if (menuitem.Equals("WaterProperties.Delete.Cancel")) 
                    view = View("WaterProperties", db);

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

        [HttpPost]
        public ActionResult WaterPropertiesUpdate(WaterPropertiesView wpv)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.WaterProperties";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("WaterProperties.Update.Update"))
                {

                    int water_code = wpv.water_code;

                    string strtemperature = this.HttpContext.Request.Params["temperature"] ?? "Empty";
                    float temperature;
                    Helper.FloatTryParse(strtemperature, out temperature);

                    string strviscocity = this.HttpContext.Request.Params["viscocity"] ?? "Empty";
                    float viscocity;
                    Helper.FloatTryParse(strviscocity, out viscocity);

                    string strdensity = this.HttpContext.Request.Params["density"] ?? "Empty";
                    float density;
                    Helper.FloatTryParse(strdensity, out density);

                    string strtension = this.HttpContext.Request.Params["tension"] ?? "Empty";
                    float tension;
                    Helper.FloatTryParse(strtension, out tension);

                    WaterProperties wp = new WaterProperties((int)water_code, (float)temperature, (float)viscocity, (float)density, (float)tension);
                    if (EGH01DB.Primitives.WaterProperties.Update(db, wp))
                        view = View("WaterProperties", db);
                }
                else if (menuitem.Equals("WaterProperties.Update.Cancel")) 
                    view = View("WaterProperties", db);
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
