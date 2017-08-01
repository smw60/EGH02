using System;
using System.Web.Mvc;
using EGH01DB;
using System.Xml;
using EGH01.Models.EGHORT;
using EGH01DB.Types;

namespace EGH01.Controllers
{
    public partial class EGHORTController : Controller
    {
        public ActionResult WaterCleaningMethod()
        {
            ORTContext db = null;
            ActionResult view = View("Index");
            ViewBag.EGHLayout = "ORT.WaterCleaningMethod";
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("WaterCleaningMethod", db);

                if (menuitem.Equals("WaterCleaningMethod.Create"))
                {
                    view = View("WaterCleaningMethodCreate");
                }
                else if (menuitem.Equals("WaterCleaningMethod.Delete"))
                {
                    string type_code = this.HttpContext.Request.Params["type_code"];
                    if (type_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code, out c))
                        {
                            EGH01DB.Types.WaterCleaningMethod scm = new EGH01DB.Types.WaterCleaningMethod();
                            if (EGH01DB.Types.WaterCleaningMethod.GetByCode(db, c, out scm))
                            {
                                view = View("WaterCleaningMethodDelete", scm);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("WaterCleaningMethod.Update"))
                {
                    string type_code = this.HttpContext.Request.Params["type_code"];

                    if (type_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code, out c))
                        {
                            WaterCleaningMethod scm = new EGH01DB.Types.WaterCleaningMethod();

                            if (EGH01DB.Types.WaterCleaningMethod.GetByCode(db, c, out scm))
                            {
                                view = View("WaterCleaningMethodUpdate", scm);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("WaterCleaningMethod.Excel"))
                {
                    EGH01DB.Types.WaterCleaningMethodList list = new EGH01DB.Types.WaterCleaningMethodList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/WaterCleaningMethod.xml"));
                    view = View("Index");
                    view = File(Server.MapPath("~/App_Data/WaterCleaningMethod.xml"), "text/plain", "Методы ликвидации загрязнения грунтовых вод.xml");

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
        public ActionResult WaterCleaningMethodCreate(WaterCleaningMethodView scmv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.WaterCleaningMethod";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                view = View("WaterCleaningMethod", db);
                if (menuitem.Equals("WaterCleaningMethod.Create.Create"))
                {
                    int id = -1;
                    if (EGH01DB.Types.WaterCleaningMethod.GetNextCode(db, out id))
                    {
                        int type_code = scmv.type_code;
                        string method_description = scmv.method_description;
                        WaterCleaningMethod scm = new WaterCleaningMethod(type_code, method_description);

                        if (EGH01DB.Types.WaterCleaningMethod.Create(db, scm))
                        {
                            view = View("WaterCleaningMethod", db);
                        }
                        else if (menuitem.Equals("WaterCleaningMethod.Create.Cancel"))
                            view = View("WaterCleaningMethod", db);
                    }
                }
                else if (menuitem.Equals("WaterCleaningMethod.Create.Cancel"))
                    view = View("WaterCleaningMethod", db);
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
        public ActionResult WaterCleaningMethodDelete(int type_code)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.WaterCleaningMethod";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("WaterCleaningMethod.Delete.Delete"))
                {
                    if (EGH01DB.Types.WaterCleaningMethod.DeleteByCode(db, type_code))
                        view = View("WaterCleaningMethod", db);
                }
                else if (menuitem.Equals("WaterCleaningMethod.Delete.Cancel"))
                    view = View("WaterCleaningMethod", db);

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
        public ActionResult WaterCleaningMethodUpdate(WaterCleaningMethodView scmv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.WaterCleaningMethod";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("WaterCleaningMethod.Update.Update"))
                {

                    int type_code = scmv.type_code;
                    string method_description = scmv.method_description;

                    WaterCleaningMethod scm = new EGH01DB.Types.WaterCleaningMethod(type_code, method_description);
                    if (EGH01DB.Types.WaterCleaningMethod.Update(db, scm))
                        view = View("WaterCleaningMethod", db);
                }
                else if (menuitem.Equals("WaterCleaningMethod.Update.Cancel"))
                    view = View("WaterCleaningMethod", db);
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