using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EGH01.Models.EGHRGE;
using EGH01DB;
using EGH01DB.Primitives;
using EGH01DB.Types;
using System.Globalization;
using EGH01DB.Points;

namespace EGH01.Controllers
{
    public partial class EGHCEQController : Controller
    {
        public ActionResult CadastreType()
        {
            CEQContext db = null;
            ViewBag.EGHLayout = "CEQ.CadastreType";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CEQContext(this);
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("CadastreType", db);

                if (menuitem.Equals("CadastreType.Create"))
                {

                    view = View("CadastreTypeCreate");

                }
                else if (menuitem.Equals("CadastreType.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.CadastreType cd = new EGH01DB.Types.CadastreType();
                            if (EGH01DB.Types.CadastreType.GetByCode(db, c, out cd))
                            {
                                view = View("CadastreTypeDelete", cd);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("CadastreType.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Types.CadastreType cd = new EGH01DB.Types.CadastreType();
                            if (EGH01DB.Types.CadastreType.GetByCode(db, c, out cd))
                            {
                                view = View("CadastreTypeUpdate", cd);
                            }
                        }
                    }
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
        public ActionResult CadastreTypeUpdate(CadastreTypeView cd)
        {
           CEQContext db = null;
            ViewBag.EGHLayout = "CEQ";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CEQContext(this);
                view = View("CadastreType", db);
                if (menuitem.Equals("CadastreType.Update.Update"))
                {

                    int id = cd.type_code;
                    String name = cd.name;
                    float pdk_coef = cd.pdk_coef;
                    String strpdk_coef = this.HttpContext.Request.Params["pdk_coef"] ?? "Empty"; ;
                    if (!Helper.FloatTryParse(strpdk_coef, out pdk_coef))
                    {
                        pdk_coef = 0.0f;
                    }
                    float water_pdk_coef = cd.water_pdk_coef;
                    String strwater_pdk_coef = this.HttpContext.Request.Params["water_pdk_coef"] ?? "Empty"; ;
                    if (!Helper.FloatTryParse(strwater_pdk_coef, out water_pdk_coef))
                    {
                        water_pdk_coef = 0.0f;
                    }
                    String water_doc_coef = cd.water_doc_name;
                    String pdk_doc_coef = cd.ground_doc_name;
                    EGH01DB.Types.CadastreType cadastre_type = new EGH01DB.Types.CadastreType(id, name, pdk_coef, water_pdk_coef,pdk_doc_coef,water_doc_coef); 
                    if (EGH01DB.Types.CadastreType.Update(db, cadastre_type))
                    {
                        view = View("CadastreType", db);
                    }
                }


                else if (menuitem.Equals("CadastreType.Update.Cancel")) view = View("CadastreType", db);
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