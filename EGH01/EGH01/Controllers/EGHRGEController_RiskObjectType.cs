using EGH01.Models.EGHRGE;
using EGH01DB;
using EGH01DB.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EGH01.Controllers
{
    public  partial class EGHRGEController: Controller
    {
        // GET: EGHRGEController_RiskObjectType
        public ActionResult RiskObjectType()
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.RiskObjectType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext(this);
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View(db);
                if (menuitem.Equals("RiskObjectType.Create"))
                {

                    view = View("RiskObjectTypeCreate");

                }
                else if (menuitem.Equals("RiskObjectType.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.RiskObjectType rt = new EGH01DB.Types.RiskObjectType();
                            if (EGH01DB.Types.RiskObjectType.GetByCode(db, c, out rt))
                            {
                                view = View("RiskObjectTypeDelete", rt);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("RiskObjectType.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.RiskObjectType rt = new EGH01DB.Types.RiskObjectType();
                            if (EGH01DB.Types.RiskObjectType.GetByCode(db, c, out rt))
                            {
                                view = View("RiskObjectTypeUpdate", rt);
                            }
                        }
                    }
                }
                //else if (menuitem.Equals("RiskObjectType.Excel"))
                //{
                //    EGH01DB.Types.RiskObjectTypeList list = new EGH01DB.Types.GroundTypeList(db);
                //    XmlNode node = list.toXmlNode();
                //    XmlDocument doc = new XmlDocument();
                //    XmlNode nnode = doc.ImportNode(node, true);
                //    doc.AppendChild(nnode);
                //    doc.Save(Server.MapPath("~/App_Data/GroundType.xml"));
                //    view = View("Index");

                //    view = File(Server.MapPath("~/App_Data/GroundType.xml"), "text/plain", "Типы грунтов.xml");


                //}

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
        public ActionResult RiskObjectTypeCreate(RiskObjectTypeView rt)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.RiskObjectType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                view = View("RiskObjectType", db);
                if (menuitem.Equals("RiskObjectType.Create.Create"))
                {
                    if (EGH01DB.Types.RiskObjectType.Create(db, new RiskObjectType(0, rt.name)))
                    {
                        view = View("RiskObjectType", db);
                    }
                    else if (menuitem.Equals("RiskObjectType.Create.Cancel")) view = View("RiskObjectType", db);
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
        public ActionResult RiskObjectTypeDelete(int type_code)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.RiskObjectType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("RiskObjectType.Delete.Delete"))
                {
                    if (EGH01DB.Types.RiskObjectType.DeleteByCode(db, type_code)) view = View("RiskObjectType", db);
                }
                else if (menuitem.Equals("RiskObjectType.Delete.Cancel")) view = View("RiskObjectType", db);

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
        public ActionResult RiskObjectTypeUpdate(RiskObjectTypeView rt)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.RiskObjectType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("RiskObjectType.Update.Update"))
                {
                    if (EGH01DB.Types.RiskObjectType.Update(db, new EGH01DB.Types.RiskObjectType(rt.type_code, rt.name))) view = View("RiskObjectType", db);
                }
                else if (menuitem.Equals("RiskObjectType.Update.Cancel")) view = View("RiskObjectType", db);

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