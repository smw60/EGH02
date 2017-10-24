using EGH01.Models.EGHORT;
using EGH01DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace EGH01.Controllers
{
    public partial class EGHORTController: Controller
    {
     
        public ActionResult RehabilitationMethods()
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.RehabilitationMethods";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext(this);
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("RehabilitationMethods", db);

                if (menuitem.Equals("RehabilitationMethods.Create"))
                {

                    view = View("RehabilitationMethodsCreate");

                }
                else if (menuitem.Equals("RehabilitationMethods.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["id"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.RehabilitationMethod cd = new EGH01DB.Types.RehabilitationMethod();
                            if (EGH01DB.Types.RehabilitationMethod.GetByCode(db, c, out cd))
                            {
                                view = View("RehabilitationMethodsDelete", cd);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("RehabilitationMethods.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["id"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.RehabilitationMethod cd = new EGH01DB.Types.RehabilitationMethod();
                            if (EGH01DB.Types.RehabilitationMethod.GetByCode(db, c, out cd))
                            {
                                view = View("RehabilitationMethodsUpdate", cd);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("RehabilitationMethods.Excel"))
                {
                    EGH01DB.Types.RehabilitationMethodList list = new EGH01DB.Types.RehabilitationMethodList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/RehabilitationMethod.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/RehabilitationMethod.xml"), "text/plain", "Классификатор методов реабилитации.xml");


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
        public ActionResult RehabilitationMethodsCreate(RehabilitationMethodsView rm)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.RehabilitationMethods";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                view = View("RehabilitationMethods", db);
                if (menuitem.Equals("RehabilitationMethods.Create.Create"))
                {

                    int type_code = 2;
                    if (EGH01DB.Types.RehabilitationMethod.GetNextCode(db, out type_code))
                    {
                        EGH01DB.Types.CadastreType list_cadastre = new EGH01DB.Types.CadastreType();
                        if (EGH01DB.Types.CadastreType.GetByCode(db, rm.list_cadastre, out list_cadastre))
                        {
                            EGH01DB.Types.RiskObjectType list_risk = new EGH01DB.Types.RiskObjectType();
                            if (EGH01DB.Types.RiskObjectType.GetByCode(db, rm.list_type, out list_risk))
                            {
                                EGH01DB.Types.PetrochemicalCategories list_petrochemical = new EGH01DB.Types.PetrochemicalCategories();
                                if (EGH01DB.Types.PetrochemicalCategories.GetByCode(db, rm.list_petrochemical, out list_petrochemical))
                                {
                                    EGH01DB.Types.EmergencyClass list_emergency = new EGH01DB.Types.EmergencyClass();
                                    if (EGH01DB.Types.EmergencyClass.GetByCode(db, rm.list_emergency, out list_emergency))
                                    {
                                        EGH01DB.Types.PenetrationDepth list_penetration = new EGH01DB.Types.PenetrationDepth();
                                        if (EGH01DB.Types.PenetrationDepth.GetByCode(db, rm.list_penetration, out list_penetration))
                                        {
                                            EGH01DB.Types.SoilPollutionCategories list_soil = new EGH01DB.Types.SoilPollutionCategories();
                                            if (EGH01DB.Types.SoilPollutionCategories.GetByCode(db, rm.list_soil, out list_soil))
                                            {
                                                bool waterachieved = rm.waterachieved;
                                                EGH01DB.Types.WaterPollutionCategories list_water = new EGH01DB.Types.WaterPollutionCategories();
                                                if (EGH01DB.Types.WaterPollutionCategories.GetByCode(db, rm.list_water, out list_water))
                                                {
                                                    EGH01DB.Types.WaterProtectionArea list_waterArea = new EGH01DB.Types.WaterProtectionArea();
                                                    if (EGH01DB.Types.WaterProtectionArea.GetByCode(db, rm.list_waterArea, out list_waterArea))
                                                    {
                                                        EGH01DB.Types.SoilCleaningMethod list_soilCleaning = new EGH01DB.Types.SoilCleaningMethod();
                                                        if (EGH01DB.Types.SoilCleaningMethod.GetByCode(db, rm.list_soilCleaning, out list_soilCleaning))
                                                        {
                                                            EGH01DB.Types.WaterCleaningMethod list_waterCleaning = new EGH01DB.Types.WaterCleaningMethod();
                                                            if (EGH01DB.Types.WaterCleaningMethod.GetByCode(db, rm.list_waterCleaning, out list_waterCleaning))
                                                            {
                                                                EGH01DB.Types.RehabilitationMethod rehabilitationMethod = new EGH01DB.Types.RehabilitationMethod(type_code, list_risk, list_cadastre, list_petrochemical, list_emergency, list_penetration, list_soil, waterachieved, list_water, list_waterArea, list_soilCleaning, list_waterCleaning);
                                                                if (EGH01DB.Types.RehabilitationMethod.Create(db, rehabilitationMethod))
                                                                {
                                                                    view = View("RehabilitationMethods", db);
                                                                }
                                                            }

                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
             }
                else if (menuitem.Equals("RehabilitationMethods.Create.Cancel")) view = View("RehabilitationMethods", db);
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
        public ActionResult RehabilitationMethodsDelete(int type_code)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();

                if (menuitem.Equals("RehabilitationMethods.Delete.Delete"))
                {
                    if (EGH01DB.Types.RehabilitationMethod.DeleteByCode(db, type_code)) view = View("RehabilitationMethods", db);
                }
                else if (menuitem.Equals("RehabilitationMethods.Delete.Cancel")) view = View("RehabilitationMethods", db);

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
        public ActionResult RehabilitationMethodsUpdate(RehabilitationMethodsView rm)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.RehabilitationMethods";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                view = View("RehabilitationMethods", db);
                if (menuitem.Equals("RehabilitationMethods.Update.Update"))
                {

                    int type_code =rm.type_code;

                  
                        EGH01DB.Types.CadastreType list_cadastre = new EGH01DB.Types.CadastreType();
                        if (EGH01DB.Types.CadastreType.GetByCode(db, rm.list_cadastre, out list_cadastre))
                        {
                            EGH01DB.Types.RiskObjectType list_risk = new EGH01DB.Types.RiskObjectType();
                            if (EGH01DB.Types.RiskObjectType.GetByCode(db, rm.list_type, out list_risk))
                            {
                                EGH01DB.Types.PetrochemicalCategories list_petrochemical = new EGH01DB.Types.PetrochemicalCategories();
                                if (EGH01DB.Types.PetrochemicalCategories.GetByCode(db, rm.list_petrochemical, out list_petrochemical))
                                {
                                    EGH01DB.Types.EmergencyClass list_emergency = new EGH01DB.Types.EmergencyClass();
                                    if (EGH01DB.Types.EmergencyClass.GetByCode(db, rm.list_emergency, out list_emergency))
                                    {
                                        EGH01DB.Types.PenetrationDepth list_penetration = new EGH01DB.Types.PenetrationDepth();
                                        if (EGH01DB.Types.PenetrationDepth.GetByCode(db, rm.list_penetration, out list_penetration))
                                        {
                                            EGH01DB.Types.SoilPollutionCategories list_soil = new EGH01DB.Types.SoilPollutionCategories();
                                            if (EGH01DB.Types.SoilPollutionCategories.GetByCode(db, rm.list_soil, out list_soil))
                                            {
                                                bool waterachieved = rm.waterachieved;
                                                EGH01DB.Types.WaterPollutionCategories list_water = new EGH01DB.Types.WaterPollutionCategories();
                                                if (EGH01DB.Types.WaterPollutionCategories.GetByCode(db, rm.list_water, out list_water))
                                                {
                                                    EGH01DB.Types.WaterProtectionArea list_waterArea = new EGH01DB.Types.WaterProtectionArea();
                                                    if (EGH01DB.Types.WaterProtectionArea.GetByCode(db, rm.list_waterArea, out list_waterArea))
                                                    {
                                                        EGH01DB.Types.SoilCleaningMethod list_soilCleaning = new EGH01DB.Types.SoilCleaningMethod();
                                                        if (EGH01DB.Types.SoilCleaningMethod.GetByCode(db, rm.list_soilCleaning, out list_soilCleaning))
                                                        {
                                                            EGH01DB.Types.WaterCleaningMethod list_waterCleaning = new EGH01DB.Types.WaterCleaningMethod();
                                                            if (EGH01DB.Types.WaterCleaningMethod.GetByCode(db, rm.list_waterCleaning, out list_waterCleaning))
                                                            {
                                                                EGH01DB.Types.RehabilitationMethod rehabilitationMethod = new EGH01DB.Types.RehabilitationMethod(type_code, list_risk, list_cadastre, list_petrochemical, list_emergency, list_penetration, list_soil, waterachieved, list_water, list_waterArea, list_soilCleaning, list_waterCleaning);

                                                                if (EGH01DB.Types.RehabilitationMethod.Update(db, rehabilitationMethod))
                    {
                        view = View("RehabilitationMethods", db);
                                                                }
                                                            }

                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                
                else if (menuitem.Equals("RehabilitationMethods.Update.Cancel")) view = View("RehabilitationMethods", db);
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