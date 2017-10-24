using System;
using System.Web.Mvc;
using EGH01DB.Types;
using EGH01.Models.EGHCCO;
using EGH01DB;
using System.Xml;
using System.Globalization;
using EGH01DB.Primitives;



namespace EGH01.Controllers
{
    public partial class EGHCCOController : Controller
    {
        public ActionResult PetrochemicalType()
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("PetrochemicalType", db);


                if (menuitem.Equals("PetrochemicalType.Create"))
                {

                    view = View("PetrochemicalTypeCreate");

                }
                else if (menuitem.Equals("PetrochemicalType.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            PetrochemicalType pt = new PetrochemicalType();
                            if (EGH01DB.Types.PetrochemicalType.GetByCode(db, c, ref pt))
                            {
                                view = View("PetrochemicalTypeDelete", pt);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("PetrochemicalType.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.PetrochemicalType pt = new EGH01DB.Types.PetrochemicalType();
                            if (EGH01DB.Types.PetrochemicalType.GetByCode(db, c, ref pt))
                            {
                                view = View("PetrochemicalTypeUpdate", pt);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("PetrochemicalType.Excel"))
                {
                    EGH01DB.Types.PetrochemicalTypeList list = new PetrochemicalTypeList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/PetrochemicalType.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/PetrochemicalType.xml"), "text/plain", "Химический состав нефтей.xml");

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
        public ActionResult PetrochemicalTypeCreate(PetrochemicalTypeView ptv)
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                view = View("PetrochemicalType", db);
                if (menuitem.Equals("PetrochemicalType.Create.Create"))
                {

                    int id = -1;
                    if (EGH01DB.Types.PetrochemicalType.GetNextCode(db, out id))
                    {
                        int    type_code = ptv.code_type;
                        string name = ptv.name;

                        string strboilingtemp = this.HttpContext.Request.Params["boilingtemp"] ?? "Empty";
                        float boilingtemp;
                        Helper.FloatTryParse(strboilingtemp, out boilingtemp);

                        string strdensity = this.HttpContext.Request.Params["density"] ?? "Empty";
                        float density;
                        Helper.FloatTryParse(strdensity, out density);

                        string strviscosity = this.HttpContext.Request.Params["viscosity"] ?? "Empty";
                        float viscosity;
                        Helper.FloatTryParse(strviscosity, out viscosity);

                        string strsolubility = this.HttpContext.Request.Params["solubility"] ?? "Empty";
                        float solubility;
                        Helper.FloatTryParse(strsolubility, out solubility);

                        string strtension = this.HttpContext.Request.Params["tension"] ?? "Empty";
                        float tension;
                        Helper.FloatTryParse(strtension, out tension);

                        string strdynamicviscosity = this.HttpContext.Request.Params["dynamicviscosity"] ?? "Empty";
                        float dynamicviscosity;
                        Helper.FloatTryParse(strdynamicviscosity, out dynamicviscosity);

                        string strdiffusion = this.HttpContext.Request.Params["diffusion"] ?? "Empty";
                        float diffusion;
                        Helper.FloatTryParse(strdiffusion, out diffusion);

                        int petrochemicalcategories_type_code = ptv.list_PetrochemicalCategories;

                        PetrochemicalCategories petrochemicalcategories = new PetrochemicalCategories((int)petrochemicalcategories_type_code);
                        if (EGH01DB.Types.PetrochemicalCategories.GetByCode(db, petrochemicalcategories_type_code, out petrochemicalcategories))
                        {
                            PetrochemicalType pt = new PetrochemicalType((int)type_code, (string)name, (float)boilingtemp, (float)density,
                               (float)viscosity, (float)solubility, (float)tension, (float)dynamicviscosity, (float)diffusion, petrochemicalcategories);
                            if (EGH01DB.Types.PetrochemicalType.Create(db, pt))
                            {
                                view = View("PetrochemicalType", db);
                            }
                        }
                           
                        
                        else if (menuitem.Equals("PetrochemicalType.Create.Cancel")) view = View("PetrochemicalType", db);
                    }
                }
                else if (menuitem.Equals("PetrochemicalType.Create.Cancel")) view = View("PetrochemicalType", db);
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
        public ActionResult PetrochemicalTypeDelete(int type_code)
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                if (menuitem.Equals("PetrochemicalType.Delete.Delete"))
                {
                    if (EGH01DB.Types.PetrochemicalType.DeleteByCode(db, type_code)) view = View("PetrochemicalType", db);
                }
                else if (menuitem.Equals("PetrochemicalType.Delete.Cancel")) view = View("PetrochemicalType", db);

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
        public ActionResult PetrochemicalTypeUpdate(PetrochemicalTypeView ptv)
        {
            CCOContext db = null;
            ViewBag.EGHLayout = "CCO";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CCOContext();
                if (menuitem.Equals("PetrochemicalType.Update.Update"))
                {

                    int type_code = ptv.code_type;
                    string name = ptv.name;

                    string strboilingtemp = this.HttpContext.Request.Params["boilingtemp"] ?? "Empty";
                    float boilingtemp;
                    Helper.FloatTryParse(strboilingtemp, out boilingtemp);

                    string strdensity = this.HttpContext.Request.Params["density"] ?? "Empty";
                    float density;
                    Helper.FloatTryParse(strdensity, out density);

                    string strviscosity = this.HttpContext.Request.Params["viscosity"] ?? "Empty";
                    float viscosity;
                    Helper.FloatTryParse(strviscosity, out viscosity);

                    string strsolubility = this.HttpContext.Request.Params["solubility"] ?? "Empty";
                    float solubility;
                    Helper.FloatTryParse(strsolubility, out solubility);

                    string strtension = this.HttpContext.Request.Params["tension"] ?? "Empty";
                    float tension;
                    Helper.FloatTryParse(strtension, out tension);

                    string strdynamicviscosity = this.HttpContext.Request.Params["dynamicviscosity"] ?? "Empty";
                    float dynamicviscosity;
                    Helper.FloatTryParse(strdynamicviscosity, out dynamicviscosity);

                    string strdiffusion = this.HttpContext.Request.Params["diffusion"] ?? "Empty";
                    float diffusion;
                    Helper.FloatTryParse(strdiffusion, out diffusion);

                    int petrochemicalcategories_type_code = ptv.list_PetrochemicalCategories;
                  

                    PetrochemicalCategories petrochemicalcategories = new PetrochemicalCategories(); 
                    if (EGH01DB.Types.PetrochemicalCategories.GetByCode(db, (int)petrochemicalcategories_type_code, out petrochemicalcategories))
                    {
                        PetrochemicalType pt = new PetrochemicalType((int)type_code, (string)name, (float)boilingtemp, (float)density, (float)viscosity,
                                               (float)solubility, (float)tension, (float)dynamicviscosity, (float)diffusion, (PetrochemicalCategories)petrochemicalcategories); 
                        if (EGH01DB.Types.PetrochemicalType.Update(db, pt))
                            view = View("PetrochemicalType", db);
                    }



                    else if (menuitem.Equals("PetrochemicalType.Update.Cancel"))
                        view = View("PetrochemicalType", db);

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


    }
}