using System;
using System.Web.Mvc;
using EGH01.Models.EGHCCO;
using EGH01DB;
using System.Xml;
using EGH01DB.Types;


namespace EGH01.Controllers
{
    public partial class EGHCCOController: Controller
    {
        public ActionResult PetrochemicalCategories()
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO.PetrochemicalCategories";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("PetrochemicalCategories", db);


                if (menuitem.Equals("PetrochemicalCategories.Create"))
                {

                    view = View("PetrochemicalCategoriesCreate");

                }
                else if (menuitem.Equals("PetrochemicalCategories.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            PetrochemicalCategories pt = new PetrochemicalCategories();
                            if (EGH01DB.Types.PetrochemicalCategories.GetByCode(db, c, out pt))
                            {
                                view = View("PetrochemicalCategoriesDelete", pt);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("PetrochemicalCategories.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            PetrochemicalCategories pt = new PetrochemicalCategories();
                            if (EGH01DB.Types.PetrochemicalCategories.GetByCode(db, c, out pt))
                            {
                                view = View("PetrochemicalCategoriesUpdate", pt);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("PetrochemicalCategories.Excel"))
                {
                    EGH01DB.Types.PetrochemicalCategoriesList list = new PetrochemicalCategoriesList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/PetrochemicalCategories.xml"));
                    view = View("Index");
                    view = File(Server.MapPath("~/App_Data/PetrochemicalCategories.xml"), "text/plain", "Категория нефтепродукта.xml");
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
        public ActionResult PetrochemicalCategoriesCreate(PetrochemicalCategoriesView pcv)
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO.PetrochemicalCategories";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                view = View("PetrochemicalCategories", db);
                if (menuitem.Equals("PetrochemicalCategories.Create.Create"))
                {
                    int id = -1;
                    if (EGH01DB.Types.PetrochemicalCategories.GetNextCode(db, out id))
                    {
                        int type_code = pcv.type_code;
                        string name = pcv.name;

                        PetrochemicalCategories pc = new PetrochemicalCategories(type_code, name);
                        if (EGH01DB.Types.PetrochemicalCategories.Create(db, pc))
                        {
                            view = View("PetrochemicalCategories", db);
                        }
                        else if (menuitem.Equals("PetrochemicalCategories.Create.Cancel")) view = View("PetrochemicalCategories", db);
                    }
                }
                else if (menuitem.Equals("PetrochemicalCategories.Create.Cancel")) view = View("PetrochemicalCategories", db);
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
        public ActionResult PetrochemicalCategoriesDelete(int type_code)
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO.PetrochemicalCategories";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                if (menuitem.Equals("PetrochemicalCategories.Delete.Delete"))
                {
                    if (EGH01DB.Types.PetrochemicalCategories.DeleteByCode(db, type_code)) view = View("PetrochemicalCategories", db);
                }
                else if (menuitem.Equals("PetrochemicalCategories.Delete.Cancel")) view = View("PetrochemicalCategories", db);

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
        public ActionResult PetrochemicalCategoriesUpdate(PetrochemicalCategoriesView pcv)
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO.PetrochemicalCategories";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                if (menuitem.Equals("PetrochemicalCategories.Update.Update"))
                {

                    int type_code = pcv.type_code;
                    string name = pcv.name;

                    PetrochemicalCategories pc = new PetrochemicalCategories(type_code, name);
                    if (EGH01DB.Types.PetrochemicalCategories.Update(db, pc))
                        view = View("PetrochemicalCategories", db);
                }
                else if (menuitem.Equals("PetrochemicalCategories.Update.Cancel")) view = View("PetrochemicalCategories", db);
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