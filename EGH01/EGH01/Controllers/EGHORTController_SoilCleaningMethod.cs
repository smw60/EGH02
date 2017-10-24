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
        public ActionResult SoilCleaningMethod()
        {
            ORTContext db = null;
            ActionResult view = View("Index");
            ViewBag.EGHLayout = "ORT.SoilCleaningMethod";
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("SoilCleaningMethod", db);

                if (menuitem.Equals("SoilCleaningMethod.Create"))
                {
                    view = View("SoilCleaningMethodCreate");
                }
                else if (menuitem.Equals("SoilCleaningMethod.Delete"))
                {
                    string type_code = this.HttpContext.Request.Params["type_code"];
                    if (type_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code, out c))
                        {
                            EGH01DB.Types.SoilCleaningMethod scm = new EGH01DB.Types.SoilCleaningMethod();
                            if (EGH01DB.Types.SoilCleaningMethod.GetByCode(db, c, out scm))
                            {
                                view = View("SoilCleaningMethodDelete", scm);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("SoilCleaningMethod.Update"))
                {
                    string type_code = this.HttpContext.Request.Params["type_code"];

                    if (type_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code, out c))
                        {
                            SoilCleaningMethod scm = new EGH01DB.Types.SoilCleaningMethod();

                            if (EGH01DB.Types.SoilCleaningMethod.GetByCode(db, c, out scm))
                            {
                                view = View("SoilCleaningMethodUpdate", scm);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("SoilCleaningMethod.Excel"))
                {
                    EGH01DB.Types.SoilCleaningMethodList list = new EGH01DB.Types.SoilCleaningMethodList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/SoilCleaningMethod.xml"));
                    view = View("Index");
                    view = File(Server.MapPath("~/App_Data/SoilCleaningMethod.xml"), "text/plain", "Методы ликвидации загрязнения почвогрунтов.xml");

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
        public ActionResult SoilCleaningMethodCreate(SoilCleaningMethodView scmv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.SoilCleaningMethod";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                view = View("SoilCleaningMethod", db);
                if (menuitem.Equals("SoilCleaningMethod.Create.Create"))
                {
                    int id = -1;
                    if (EGH01DB.Types.SoilCleaningMethod.GetNextCode(db, out id))
                    {
                        int type_code = scmv.type_code;
                        string method_description = scmv.method_description;
                        SoilCleaningMethod scm = new SoilCleaningMethod(type_code, method_description);

                        if (EGH01DB.Types.SoilCleaningMethod.Create(db, scm))
                        {
                            view = View("SoilCleaningMethod", db);
                        }
                        else if (menuitem.Equals("SoilCleaningMethod.Create.Cancel")) 
                            view = View("SoilCleaningMethod", db);
                    }
                }
                else if (menuitem.Equals("SoilCleaningMethod.Create.Cancel")) 
                    view = View("SoilCleaningMethod", db);
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
        public ActionResult SoilCleaningMethodDelete(int type_code)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.SoilCleaningMethod";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("SoilCleaningMethod.Delete.Delete"))
                {
                    if (EGH01DB.Types.SoilCleaningMethod.DeleteByCode(db, type_code))
                        view = View("SoilCleaningMethod", db);
                }
                else if (menuitem.Equals("SoilCleaningMethod.Delete.Cancel"))
                    view = View("SoilCleaningMethod", db);

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
        public ActionResult SoilCleaningMethodUpdate(SoilCleaningMethodView scmv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.SoilCleaningMethod";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("SoilCleaningMethod.Update.Update"))
                {

                    int type_code = scmv.type_code;
                    string method_description = scmv.method_description;
                    SoilCleaningMethod scm = new SoilCleaningMethod(type_code, method_description);

                    if (EGH01DB.Types.SoilCleaningMethod.Update(db,scm))
                        view = View("SoilCleaningMethod", db);
                }
                else if (menuitem.Equals("SoilCleaningMethod.Update.Cancel"))
                    view = View("SoilCleaningMethod", db);
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