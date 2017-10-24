using System;
using System.Web.Mvc;
using System.Xml;
using EGH01DB;
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01.Models.EGHRGE;


namespace EGH01.Controllers
{
    public partial class EGHRGEController : Controller
    {

        public ActionResult SpreadingCoefficient()
        {

            RGEContext db = null;
            ActionResult view = View("Index");
            ViewBag.EGHLayout = "RGE.SpreadingCoefficient";
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("SpreadingCoefficient", db);


                if (menuitem.Equals("SpreadingCoefficient.Create"))
                {

                    view = View("SpreadingCoefficientCreate");

                }

                else if (menuitem.Equals("SpreadingCoefficient.Delete"))
                {

                    string code = this.HttpContext.Request.Params["code"];
                    if (code != null)
                    {
                        int c = 0;
                        if (int.TryParse(code, out c))
                        {
                            EGH01DB.Primitives.SpreadingCoefficient sc = new EGH01DB.Primitives.SpreadingCoefficient();
                            if (EGH01DB.Primitives.SpreadingCoefficient.GetByCode(db, c, out sc))
                            {
                                view = View("SpreadingCoefficientDelete", sc);
                            }
                        }
                    }


                }


                else if (menuitem.Equals("SpreadingCoefficient.Update"))
                {
                    string code = this.HttpContext.Request.Params["code"];

                    if (code != null)
                    {
                        int c = 0;
                        if (int.TryParse(code, out c))
                        {
                            SpreadingCoefficient sc = new SpreadingCoefficient();
                            if (EGH01DB.Primitives.SpreadingCoefficient.GetByCode(db, c, out sc))
                            {
                                view = View("SpreadingCoefficientUpdate", sc);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("SpreadingCoefficient.Excel"))
                {
                    EGH01DB.Primitives.SpreadingCoefficientList list = new SpreadingCoefficientList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/SpreadingCoefficient.xml"));
                    view = View("Index");
                    view = File(Server.MapPath("~/App_Data/SpreadingCoefficient.xml"), "text/plain", "Коэффициент растекания.xml");
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
        public ActionResult SpreadingCoefficientCreate(SpreadingCoefficientView scv)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.SpreadingCoefficient";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                view = View("SpreadingCoefficient", db);
                if (menuitem.Equals("SpreadingCoefficient.Create.Create"))
                {
                   
                    EGH01DB.Types.GroundType type_groud = new EGH01DB.Types.GroundType();
                    if (EGH01DB.Types.GroundType.GetByCode(db, scv.list_groundType, out type_groud))
                    {
                        EGH01DB.Types.PetrochemicalType petrochemical_type = new PetrochemicalType();
                        if (EGH01DB.Types.PetrochemicalType.GetByCode(db,scv.list_petrochemicalType, ref petrochemical_type))
                        { 
                        int code = scv.code;

                        string strmin_angle = this.HttpContext.Request.Params["min_angle"] ?? "Empty";
                        float min_angle;
                        Helper.FloatTryParse(strmin_angle, out min_angle);

                        string strmax_angle = this.HttpContext.Request.Params["max_angle"] ?? "Empty";
                        float max_angle;
                        Helper.FloatTryParse(strmax_angle, out max_angle);

                        string strmin_volume = this.HttpContext.Request.Params["min_volume"] ?? "Empty";
                        float min_volume;
                        Helper.FloatTryParse(strmin_volume, out min_volume);

                        string strmax_volume = this.HttpContext.Request.Params["max_volume"] ?? "Empty";
                        float max_volume;
                        Helper.FloatTryParse(strmax_volume, out max_volume);

                        string strkoef = this.HttpContext.Request.Params["koef"] ?? "Empty";
                        float koef;
                        Helper.FloatTryParse(strkoef, out koef);

                        SpreadingCoefficient sc = new EGH01DB.Primitives.SpreadingCoefficient((int)code, type_groud, petrochemical_type, (float)min_volume, (float)max_volume, (float)min_angle, (float)max_angle, (float)koef);
                        // исправить тип нефтепродукта
                        //koef = EGH01DB.Primitives.SpreadingCoefficient.Get(db, sc);
                        //sc = new EGH01DB.Primitives.SpreadingCoefficient((int)code, type_groud, (float)min_volume, (float)max_volume, (float)min_angle, (float)max_angle, (float)koef);
                        if (EGH01DB.Primitives.SpreadingCoefficient.Create(db, sc))
                        {
                            view = View("SpreadingCoefficient", db);
                        }
                        else if (menuitem.Equals("SpreadingCoefficient.Create.Cancel")) 
                            view = View("SpreadingCoefficient", db);
                    }

                    else if (menuitem.Equals("SpreadingCoefficient.Create.Cancel")) 
                        view = View("SpreadingCoefficient", db);
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
        public ActionResult SpreadingCoefficientDelete(int code)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.SpreadingCoefficient";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("SpreadingCoefficient.Delete.Delete"))
                {
                    if (EGH01DB.Primitives.SpreadingCoefficient.DeleteByCode(db, code))
                        view = View("SpreadingCoefficient", db);
                }
                else if (menuitem.Equals("SpreadingCoefficient.Delete.Cancel"))
                    view = View("SpreadingCoefficient", db);

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
        public ActionResult SpreadingCoefficientUpdate(SpreadingCoefficientView scv)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.SpreadingCoefficient";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                if (menuitem.Equals("SpreadingCoefficient.Update.Update"))
                {
                     EGH01DB.Types.GroundType type_groud = new EGH01DB.Types.GroundType();
                     if (EGH01DB.Types.GroundType.GetByCode(db, scv.list_groundType, out type_groud))
                     {
                         EGH01DB.Types.PetrochemicalType petrochemical_type = new PetrochemicalType();
                         if (EGH01DB.Types.PetrochemicalType.GetByCode(db,scv.list_petrochemicalType, ref petrochemical_type))
                         {
                         int code = scv.code;

                         string strmin_volume = this.HttpContext.Request.Params["min_volume"] ?? "Empty";
                         float min_volume;
                         Helper.FloatTryParse(strmin_volume, out min_volume);

                         string strmax_volume = this.HttpContext.Request.Params["max_volume"] ?? "Empty";
                         float max_volume;
                         Helper.FloatTryParse(strmax_volume, out max_volume);

                         string strmin_angle = this.HttpContext.Request.Params["min_angle"] ?? "Empty";
                         float min_angle;
                         Helper.FloatTryParse(strmin_angle, out min_angle);

                         string strmax_angle = this.HttpContext.Request.Params["max_angle"] ?? "Empty";
                         float max_angle;
                         Helper.FloatTryParse(strmax_angle, out max_angle);

                         string strkoef = this.HttpContext.Request.Params["koef"] ?? "Empty";
                         float koef;
                         Helper.FloatTryParse(strkoef, out koef);

                         SpreadingCoefficient sc = new EGH01DB.Primitives.SpreadingCoefficient((int)code, type_groud, petrochemical_type, (float)min_volume, (float)max_volume, (float)min_angle, (float)max_angle, (float)koef);
                         // исправить тип нефтепродукта
                         //koef = EGH01DB.Primitives.SpreadingCoefficient.Get(db, sc);

                         //sc = new SpreadingCoefficient((int)code, type_groud, (float)min_volume, (float)max_volume, (float)min_angle, (float)max_angle, (float)koef);
                         if (EGH01DB.Primitives.SpreadingCoefficient.Update(db, sc)) 
                         { 
                             view = View("SpreadingCoefficient", db);
                         }
                        
                     }
                     }
                }
                else if (menuitem.Equals("SpreadingCoefficient.Update.Cancel"))
                    view = View("SpreadingCoefficient", db);
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