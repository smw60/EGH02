using EGH01DB;
using System;
using EGH01.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGH01.Models.EGHORT;
using EGH01DB.Primitives;
using EGH01DB.Types;
using System.Xml;

namespace EGH01.Controllers
{
    public partial class EGHORTController : Controller
    {
        public ActionResult PenetrationDepth()
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.PenetrationDepth";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                PenetrationDepthView viewcontext = db.GetViewContext(PenetrationDepthView.VIEWNAME) as PenetrationDepthView;
             
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("PenetrationDepth", db);

                if (menuitem.Equals("PenetrationDepth.Create"))
                {

                    view = View("PenetrationDepthCreate");
                    //viewcontext.mindepth = null;
                    //viewcontext.maxdepth = null;
                    //viewcontext.name = "";

                }
                else if (menuitem.Equals("PenetrationDepth.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["id"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.PenetrationDepth pd = new EGH01DB.Types.PenetrationDepth();
                            if (EGH01DB.Types.PenetrationDepth.GetByCode(db, c, out pd))
                            {
                                view = View("PenetrationDepthDelete", pd);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("PenetrationDepth.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["id"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.PenetrationDepth pd = new EGH01DB.Types.PenetrationDepth();
                            if (EGH01DB.Types.PenetrationDepth.GetByCode(db, c, out pd))
                            {
                                view = View("PenetrationDepthUpdate", pd);
                            }
                        }
                    }
                }
              
                else if (menuitem.Equals("PenetrationDepth.Excel"))
                {
                    EGH01DB.Types.PenetrationDepthList pdlist = new EGH01DB.Types.PenetrationDepthList(db);
                    XmlNode node = pdlist.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/PenetrationDepth.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/PenetrationDepth.xml"), "text/plain", "Категории проникновения нефтепродукт.xml");


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
        public ActionResult PenetrationDepthCreate(PenetrationDepthView pd)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.PenetrationDepth";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (!PenetrationDepthView.Handler(db, this.HttpContext.Request.Params))
                {

                }
                view = View("PenetrationDepth", db);
                if (menuitem.Equals("PenetrationDepth.Create.Create"))
                {

                    int code = -1;
                    if (EGH01DB.Types.PenetrationDepth.GetNextCode(db, out code))
                    {
                        float mindepth;
                        string strmindepth = this.HttpContext.Request.Params["mindepth"] ?? "Empty";
                        if (!Helper.FloatTryParse(strmindepth, out mindepth))
                        {
                            mindepth = 0.0f;
                        }
                        float maxdepth;
                        string strmaxdepth = this.HttpContext.Request.Params["maxdepth"] ?? "Empty";
                        if (!Helper.FloatTryParse(strmaxdepth, out maxdepth))
                        {
                            maxdepth = 0.0f;
                        }
                        String name = pd.name;
                        if (mindepth < maxdepth)
                        {
                            EGH01DB.Types.PenetrationDepth penetration = new EGH01DB.Types.PenetrationDepth(code, name, mindepth, maxdepth);


                            if (EGH01DB.Types.PenetrationDepth.Create(db, penetration))
                            {
                                view = View("PenetrationDepth", db);
                            }
                        }
                        else
                        {
                                ViewBag.Error = "Проверьте введенные данные";
                                view = View("PenetrationDepthCreate", db);
                                return view;
                        }
                    }
                }

                else if (menuitem.Equals("PenetrationDepth.Create.Cancel")) view = View("PenetrationDepth", db);
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
        public ActionResult PenetrationDepthDelete(int code)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.PenetrationDepth";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();

                if (menuitem.Equals("PenetrationDepth.Delete.Delete"))
                {
                    if (EGH01DB.Types.PenetrationDepth.DeleteByCode(db, code)) view = View("PenetrationDepth", db);
                }
                else if (menuitem.Equals("PenetrationDepth.Delete.Cancel")) view = View("PenetrationDepth", db);

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
        public ActionResult PenetrationDepthUpdate(PenetrationDepthView sp)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.PenetrationDepth";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                view = View("PenetrationDepth", db);
                if (menuitem.Equals("PenetrationDepth.Update.Update"))
                {
                    int code = sp.code;
                    String name = sp.name;
                    string strmindepth = this.HttpContext.Request.Params["mindepth"] ?? "Empty";
                    string strmaxdepth = this.HttpContext.Request.Params["maxdepth"] ?? "Empty";

                    float mindepth = 0.0f;
                    float maxdepth = 0.0f;


                    if (!Helper.FloatTryParse(strmindepth, out mindepth))
                    {
                        mindepth = 0.0f;
                    }

                    if (!Helper.FloatTryParse(strmaxdepth, out maxdepth))
                    {
                        maxdepth = 0.0f;
                    }

                    EGH01DB.Types.PenetrationDepth penetration = new EGH01DB.Types.PenetrationDepth(code, name, mindepth, maxdepth);
                    if (EGH01DB.Types.PenetrationDepth.Update(db, penetration))
                    {
                        view = View("PenetrationDepth", db);
                    }


                }
                else if (menuitem.Equals("PenetrationDepth.Update.Cancel"))
                    view = View("PenetrationDepth", db);



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