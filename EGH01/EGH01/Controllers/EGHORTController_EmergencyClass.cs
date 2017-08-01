using System;
using EGH01DB;
using System.Web.Mvc;
using EGH01.Models.EGHORT;
using EGH01DB.Types;
using System.Xml;
using EGH01DB.Primitives;


namespace EGH01.Controllers
{
    public partial class EGHORTController : Controller
    {
        public ActionResult EmergencyClass()
        {
            ORTContext db = null;
            ActionResult view = View("Index");
            ViewBag.EGHLayout = "ORT.EmergencyClass";
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("EmergencyClass", db);

                if (menuitem.Equals("EmergencyClass.Create"))
                {
                    view = View("EmergencyClassCreate");
                }
                else if (menuitem.Equals("EmergencyClass.Delete"))
                {
                    string type_code = this.HttpContext.Request.Params["type_code"];
                    if (type_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code, out c))
                        {
                            EGH01DB.Types.EmergencyClass scm = new EGH01DB.Types.EmergencyClass();
                            if (EGH01DB.Types.EmergencyClass.GetByCode(db, c, out scm))
                            {
                                view = View("EmergencyClassDelete", scm);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("EmergencyClass.Update"))
                {
                    string type_code = this.HttpContext.Request.Params["type_code"];

                    if (type_code != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code, out c))
                        {
                            EmergencyClass scm = new EGH01DB.Types.EmergencyClass();

                            if (EGH01DB.Types.EmergencyClass.GetByCode(db, c, out scm))
                            {
                                view = View("EmergencyClassUpdate", scm);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("EmergencyClass.Excel"))
                {
                    EGH01DB.Types.EmergencyClassList list = new EGH01DB.Types.EmergencyClassList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/EmergencyClass.xml"));
                    view = View("Index");
                    view = File(Server.MapPath("~/App_Data/EmergencyClass.xml"), "text/plain", "Классификация аварий.xml");
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
        public ActionResult EmergencyClassCreate(EmergencyClassView ecv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.EmergencyClass";
            ActionResult view = View("Index");
           
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                view = View("EmergencyClass", db);
                if (menuitem.Equals("EmergencyClass.Create.Create"))
                {
                    int id = -1;
                    if (EGH01DB.Types.EmergencyClass.GetNextCode(db, out id))
                    {
                        int type_code = ecv.type_code;
                        string name = ecv.name;

                        string strminmass = this.HttpContext.Request.Params["minmass"] ?? "Empty";
                        float minmass;
                        Helper.FloatTryParse(strminmass, out minmass);

                        string strmaxmass = this.HttpContext.Request.Params["maxmass"] ?? "Empty";
                        float maxmass;
                        Helper.FloatTryParse(strmaxmass, out maxmass);

                        if (minmass < maxmass && name.Length > 0)
                        {

                            EmergencyClass scm = new EmergencyClass(type_code, name, minmass, maxmass);

                            if (EGH01DB.Types.EmergencyClass.Create(db, scm))
                            {
                                view = View("EmergencyClass", db);
                            }
                            else if (menuitem.Equals("EmergencyClass.Create.Cancel"))
                                view = View("EmergencyClass", db);
                        }
                        else if (maxmass < minmass)
                        {
                            ViewBag.Error = "Минимальное значение не должно быть больше максимального";
                            view = View("EmergencyClassCreate", db);
                            return view;
                        }
                        else if (strminmass.Length.Equals(0) || strmaxmass.Length.Equals(0) || name.Length.Equals(0))
                        {
                            ViewBag.Error = "Все поля должны быть заполнены";
                            view = View("EmergencyClassCreate", db);
                            return view;
                        }
                    }
                }
                else if (menuitem.Equals("EmergencyClass.Create.Cancel"))
                    view = View("EmergencyClass", db);
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
        public ActionResult EmergencyClassDelete(int type_code)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.EmergencyClass";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("EmergencyClass.Delete.Delete"))
                {
                    if (EGH01DB.Types.EmergencyClass.DeleteByCode(db, type_code))
                        view = View("EmergencyClass", db);
                }
                else if (menuitem.Equals("EmergencyClass.Delete.Cancel"))
                    view = View("EmergencyClass", db);

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
        public ActionResult EmergencyClassUpdate(EmergencyClassView ecv)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.EmergencyClass";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("EmergencyClass.Update.Update"))
                {

                    int type_code = ecv.type_code;
                    string name = ecv.name;

                    string strminmass = this.HttpContext.Request.Params["minmass"] ?? "Empty";
                    float minmass;
                    Helper.FloatTryParse(strminmass, out minmass);

                    string strmaxmass = this.HttpContext.Request.Params["maxmass"] ?? "Empty";
                    float maxmass;
                    Helper.FloatTryParse(strmaxmass, out maxmass);


                    EmergencyClass scm = new EGH01DB.Types.EmergencyClass(type_code, name, minmass, maxmass);
                    if (EGH01DB.Types.EmergencyClass.Update(db, scm))
                        view = View("EmergencyClass", db);
                }
                else if (menuitem.Equals("EmergencyClass.Update.Cancel"))
                    view = View("EmergencyClass", db);
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