using System;
using System.Web.Mvc;
using EGH01.Models.EGHORT;
using EGH01DB;
using EGH01DB.Types;
using System.Xml;

namespace EGH01.Controllers
{
    public partial class EGHORTController : Controller
    {
        public ActionResult WaterProtectionArea()
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.WaterProtectionArea";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("WaterProtectionArea", db);


                if (menuitem.Equals("WaterProtectionArea.Create"))
                {

                    view = View("WaterProtectionAreaCreate");

                }
                else if (menuitem.Equals("WaterProtectionArea.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.WaterProtectionArea pt = new WaterProtectionArea();
                            if (EGH01DB.Types.WaterProtectionArea.GetByCode(db, c, out pt))
                            {
                                view = View("WaterProtectionAreaDelete", pt);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("WaterProtectionArea.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            WaterProtectionArea pt = new WaterProtectionArea();
                            if (EGH01DB.Types.WaterProtectionArea.GetByCode(db, c, out pt))
                            {
                                view = View("WaterProtectionAreaUpdate", pt);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("WaterProtectionArea.Excel"))
                {
                    EGH01DB.Types.WaterProtectionAreaList list = new WaterProtectionAreaList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/WaterProtectionArea.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/WaterProtectionArea.xml"), "text/plain", "Категории водоохранной территории.xml");

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
        public ActionResult WaterProtectionAreaCreate(WaterProtectionAreaView pcv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.WaterProtectionArea";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                view = View("WaterProtectionArea", db);
                if (menuitem.Equals("WaterProtectionArea.Create.Create"))
                {
                    int id = -1;
                    if (EGH01DB.Types.WaterProtectionArea.GetNextCode(db, out id))
                    {
                        int type_code = pcv.type_code;
                        string name = pcv.name;

                        WaterProtectionArea pc = new WaterProtectionArea(type_code, name);
                        if (EGH01DB.Types.WaterProtectionArea.Create(db, pc))
                        {
                            view = View("WaterProtectionArea", db);
                        }
                        else if (menuitem.Equals("WaterProtectionArea.Create.Cancel")) view = View("WaterProtectionArea", db);
                    }
                }
                else if (menuitem.Equals("WaterProtectionArea.Create.Cancel")) view = View("WaterProtectionArea", db);
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
        public ActionResult WaterProtectionAreaDelete(int type_code)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.WaterProtectionArea";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("WaterProtectionArea.Delete.Delete"))
                {
                    if (EGH01DB.Types.WaterProtectionArea.DeleteByCode(db, type_code)) view = View("WaterProtectionArea", db);
                }
                else if (menuitem.Equals("WaterProtectionArea.Delete.Cancel")) view = View("WaterProtectionArea", db);

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
        public ActionResult WaterProtectionAreaUpdate(WaterProtectionAreaView pcv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.WaterProtectionArea";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("WaterProtectionArea.Update.Update"))
                {

                    int type_code = pcv.type_code;
                    string name = pcv.name;

                    WaterProtectionArea pc = new WaterProtectionArea(type_code, name);
                    if (EGH01DB.Types.WaterProtectionArea.Update(db, pc))
                        view = View("WaterProtectionArea", db);
                }
                else if (menuitem.Equals("WaterProtectionArea.Update.Cancel")) view = View("WaterProtectionArea", db);
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