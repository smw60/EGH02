using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EGH01.Models.EGHRGE;
using EGH01DB;
using EGH01DB.Primitives;
using EGH01DB.Types;

namespace EGH01.Controllers
{
    public partial class EGHRGEController: Controller
    {
        public ActionResult EcoObjectType()
        {
            ViewBag.EGHLayout = "RGE.EcoObjectType";
            RGEContext db = null;
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"]??"Empty";
            try
            {
                db = new RGEContext();
                ViewBag.msg = "Соединение с базой данных установлено"; 
                view = View("EcoObjectType", db);
                if (menuitem.Equals("EcoObjectType.Create"))
                {

                    view = View("EcoObjectTypeCreate");
                
                }
                else if (menuitem.Equals("EcoObjectType.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c)) 
                        {
                            EcoObjectType it = new EcoObjectType();
                            if (EGH01DB.Types.EcoObjectType.GetByCode(db, c, out it))
                            {
                                view = View("EcoObjectTypeDelete", it);
                            }
                        }                           
                    }                
                }
                else if (menuitem.Equals("EcoObjectType.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EcoObjectType it = new EcoObjectType();
                            if (EGH01DB.Types.EcoObjectType.GetByCode(db, c, out it))
                            {
                                view = View("EcoObjectTypeUpdate", it);
                            }
                        }
                    } 
                 }
                else if (menuitem.Equals("EcoObjectType.Excel"))
                {
                    EGH01DB.Types.EcoObjectTypeList list = new EcoObjectTypeList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/EcoObjectType.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/EcoObjectType.xml"), "text/plain", "Типы природоохранных объектов.xml");


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
        public ActionResult EcoObjectTypeCreate(EcoObjectTypeView itv)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObjectType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"]??"Empty";
            try
            {
                db = new RGEContext();
                view = View("EcoObjectType", db);
                if (menuitem.Equals("EcoObjectType.Create.Create"))
                {
                    EGH01DB.Types.WaterProtectionArea water = new WaterProtectionArea();
                    if (EGH01DB.Types.WaterProtectionArea.GetByCode(db, itv.list_water, out water)) {
                        if (EGH01DB.Types.EcoObjectType.Create(db, new EcoObjectType(0, itv.name, water)))
                        {
                            view = View("EcoObjectType", db);
                        }
                        else if (menuitem.Equals("EcoObjectType.Create.Cancel")) view = View("EcoObjectType", db);
                    } }
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
        public ActionResult EcoObjectTypeDelete(int type_code)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObjectType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("EcoObjectType.Delete.Delete"))
                {
                    if (EGH01DB.Types.EcoObjectType.DeleteByCode(db, type_code)) view = View("EcoObjectType", db);
                }
                else if (menuitem.Equals("EcoObjectType.Delete.Cancel")) view = View("EcoObjectType", db);

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
        public ActionResult EcoObjectTypeUpdate(EcoObjectTypeView itv)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObjectType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("EcoObjectType.Update.Update"))
                {
                    EGH01DB.Types.WaterProtectionArea water = new WaterProtectionArea();
                    if (EGH01DB.Types.WaterProtectionArea.GetByCode(db, itv.list_water, out water))
                    {
                        if (EGH01DB.Types.EcoObjectType.Update(db, new EGH01DB.Types.EcoObjectType(itv.type_code, itv.name, water))) view = View("EcoObjectType", db); 
                    }
                }
                else if (menuitem.Equals("EcoObjectType.Update.Cancel")) view = View("EcoObjectType", db);

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

