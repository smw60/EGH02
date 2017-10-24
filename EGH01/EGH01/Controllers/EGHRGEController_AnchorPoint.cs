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
    public partial class EGHRGEController : Controller
    {
        public ActionResult AnchorPoint()
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.AnchorPoint";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("AnchorPoint", db);

                if (menuitem.Equals("AnchorPoint.Create"))
                {

                    view = View("AnchorPointCreate");

                }
              
                else if (menuitem.Equals("AnchorPoint.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Points.AnchorPoint ah = new EGH01DB.Points.AnchorPoint();
                            if (EGH01DB.Points.AnchorPoint.GetById(db, c, ref ah))
                            {
                                view = View("AnchorPointDelete", ah);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("AnchorPoint.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["type_code"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Points.AnchorPoint ah = new EGH01DB.Points.AnchorPoint();
                            if (EGH01DB.Points.AnchorPoint.GetById(db, c, ref ah))
                            {
                                view = View("AnchorPointUpdate", ah);
                            }
                        }
                    }
                }
               else if (menuitem.Equals("AnchorPointCrearePoint.Create"))
                {

                    view = View("AnchorPointCreatePoint");

                }
                else if (menuitem.Equals("AnchorPoint.Excel"))
                {
                    EGH01DB.Points.AnchorPointList list = new AnchorPointList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/AnchorPoint.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/AnchorPoint.xml"), "text/plain", "Опорные точки.xml");


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
        public ActionResult AnchorPointCreate(EGH01.Models.EGHRGE.AnchorPointView ah)
        {
           
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.AnchorPoint";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            
            try
            {
                db = new RGEContext();
                view = View("AnchorPoint", db);
                if (menuitem.Equals("AnchorPointCreatePoint.Create"))
                {
             
                    view = View("AnchorPointCreatePoint");
                    return view;

                }
                if (menuitem.Equals("AnchorPoint.Create.Create"))
                {

                    int id = -1;
                    if (EGH01DB.Points.AnchorPoint.GetNextId(db, out id))
                    {
                        float lat_s = 0.0f;
                        string strlat_s = this.HttpContext.Request.Params["lat_s"] ?? "Empty";
                        if (!Helper.FloatTryParse(strlat_s, out lat_s))
                        {
                            lat_s = 0.0f;
                        }

                        float lng_s = 0.0f;
                        string strlng_s = this.HttpContext.Request.Params["lng_s"] ?? "Empty";
                        if (!Helper.FloatTryParse(strlng_s, out lng_s))
                        {
                            lng_s = 0.0f;
                        }


                        Coordinates coordinates = new Coordinates(ah.Lat_d, ah.lat_m, lat_s, ah.lngitude, ah.lng_m, lng_s);
                        float waterdeep = 0.0f;
                        int list_cadastre = ah.list_cadastre;

                        float height = 0.0f;
                        string strheight = this.HttpContext.Request.Params["height"] ?? "Empty";
                        if (!Helper.FloatTryParse(strheight, out height))
                        {
                            height = 0.0f;
                        }

                        string strwaterdeep = this.HttpContext.Request.Params["waterdeep"] ?? "Empty";
                        if (!Helper.FloatTryParse(strwaterdeep, out waterdeep))
                        {
                            waterdeep = 0.0f;
                        }
                        EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                        if (EGH01DB.Types.GroundType.GetByCode(db, ah.list_groundType, out ground_type))
                        {
                            Point point = new Point(coordinates, ground_type, waterdeep, height);
                            CadastreType type_cadastre = new CadastreType();
                            if (EGH01DB.Types.CadastreType.GetByCode(db, ah.list_cadastre, out type_cadastre))
                            {
                                EGH01DB.Points.AnchorPoint anchor_point = new EGH01DB.Points.AnchorPoint(id, point, type_cadastre);


                                if (EGH01DB.Points.AnchorPoint.Create(db, anchor_point))
                                {
                                    view = View("AnchorPoint", db);
                                }
                            }

                        }
                        else if (menuitem.Equals("AnchorPoint.Create.Cancel")) view = View("AnchorPoint", db);
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
        public ActionResult AnchorPointDelete(int id)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.AnchorPoint";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();

                if (menuitem.Equals("AnchorPoint.Delete.Delete"))
                {
                    if (EGH01DB.Points.AnchorPoint.DeleteById(db, id)) view = View("AnchorPoint", db);
                }
                else if (menuitem.Equals("AnchorPoint.Delete.Cancel")) view = View("AnchorPoint", db);

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
        public ActionResult AnchorPointUpdate(AnchorPointView ah)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.AnchorPoint";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                view = View("AnchorPoint", db);
                if (menuitem.Equals("AnchorPoint.Update.Update"))
                {

                    int id = ah.id;

                    float lat_s = 0.0f;
                    string strlat_s = this.HttpContext.Request.Params["lat_s"] ?? "Empty";
                    if (!Helper.FloatTryParse(strlat_s, out lat_s))
                    {
                        lat_s = 0.0f;
                    }

                    float lng_s = 0.0f;
                    string strlng_s = this.HttpContext.Request.Params["lng_s"] ?? "Empty";
                    if (!Helper.FloatTryParse(strlng_s, out lng_s))
                    {
                        lng_s = 0.0f;
                    }
                    Coordinates coordinates = new Coordinates(ah.Lat_d, ah.lat_m, lat_s, ah.lngitude, ah.lng_m, lng_s);
                    float waterdeep = 0.0f;
                    int list_cadastre = ah.list_cadastre;
                    float height = 0.0f;
                    string strheight = this.HttpContext.Request.Params["height"] ?? "Empty";
                    if (!Helper.FloatTryParse(strheight, out height))
                    {
                        height = 0.0f;
                    }
                    string strwaterdeep = this.HttpContext.Request.Params["waterdeep"] ?? "Empty";
                    if (!Helper.FloatTryParse(strwaterdeep, out waterdeep))
                    {
                        waterdeep = 0.0f;
                    }

                    EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                    if (EGH01DB.Types.GroundType.GetByCode(db, ah.list_groundType, out ground_type))
                    {

                        Point point = new Point(coordinates, ground_type, waterdeep, height);

                        CadastreType type_cadastre = new CadastreType();
                        if (EGH01DB.Types.CadastreType.GetByCode(db, ah.list_cadastre, out type_cadastre))
                        {
                            EGH01DB.Points.AnchorPoint anchor_point = new EGH01DB.Points.AnchorPoint(id, point, type_cadastre);


                            if (EGH01DB.Points.AnchorPoint.Update(db, anchor_point))
                            {
                                view = View("AnchorPoint", db);
                            }
                        }

                    }
                    else if (menuitem.Equals("AnchorPoint.Update.Cancel")) view = View("AnchorPoint", db);
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
        public ActionResult AnchorPointCreatePoint(AnchorPointView ah)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.AnchorPoint";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";

            try
            {
                db = new RGEContext();
                view = View("AnchorPoint", db);

                if (menuitem.Equals("AnchorPointCreate.Create.Create"))
                {

                    int id = -1;
                    if (EGH01DB.Points.AnchorPoint.GetNextId(db, out id))
                    {
                        float lat_s = 0.0f;
                        string strlat_s = this.HttpContext.Request.Params["lat_s"] ?? "Empty";
                        if (!Helper.FloatTryParse(strlat_s, out lat_s))
                        {
                            lat_s = 0.0f;
                        }

                        float angel = 0.0f;
                        string strangel = this.HttpContext.Request.Params["angel"] ?? "Empty";
                        if (!Helper.FloatTryParse(strangel, out angel))
                        {
                            angel = 0.0f;
                        }

                        float distance = 0.0f;
                        string strdistance = this.HttpContext.Request.Params["distance"] ?? "Empty";
                        if (!Helper.FloatTryParse(strdistance, out distance))
                        {
                            distance = 0.0f;
                        }


                        float lng_s = 0.0f;
                        string strlng_s = this.HttpContext.Request.Params["lng_s"] ?? "Empty";
                        if (!Helper.FloatTryParse(strlng_s, out lng_s))
                        {
                            lng_s = 0.0f;
                        }


                        Coordinates coordinates = new Coordinates(ah.Lat_d, ah.lat_m, lat_s, ah.lngitude, ah.lng_m, lng_s);
                        float waterdeep = 0.0f;
                        int list_cadastre = ah.list_cadastre;

                        float height = 0.0f;
                        string strheight = this.HttpContext.Request.Params["height"] ?? "Empty";
                        if (!Helper.FloatTryParse(strheight, out height))
                        {
                            height = 0.0f;
                        }

                        string strwaterdeep = this.HttpContext.Request.Params["waterdeep"] ?? "Empty";
                        if (!Helper.FloatTryParse(strwaterdeep, out waterdeep))
                        {
                            waterdeep = 0.0f;
                        }
                        EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                        if (EGH01DB.Types.GroundType.GetByCode(db, ah.list_groundType, out ground_type))
                        {
                            Point point = new Point(coordinates, ground_type, waterdeep, height);
                            CadastreType type_cadastre = new CadastreType();
                            if (EGH01DB.Types.CadastreType.GetByCode(db, ah.list_cadastre, out type_cadastre))
                            {
                                EGH01DB.Points.AnchorPoint anchor_point = new EGH01DB.Points.AnchorPoint(id, point, type_cadastre);


                                if (EGH01DB.Points.AnchorPoint.Create(db, anchor_point, angel, distance))
                                {
                                    view = View("AnchorPoint", db);
                                }
                                else if (menuitem.Equals("AnchorPoint.Create.Cancel")) view = View("AnchorPoint", db);
                                {
                                }
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




    }
}