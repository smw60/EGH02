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
        public ActionResult EcoObject()
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObject";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("EcoObject", db);

                if (menuitem.Equals("EcoObject.Create"))
                {

                    view = View("EcoObjectCreate");

                }
                else if (menuitem.Equals("EcoObject.Delete"))
                {
                    string id = this.HttpContext.Request.Params["type_code"];
                    if (id != null)
                    {
                        int c = 0;
                        if (int.TryParse(id, out c))
                        {
                            EGH01DB.Objects.EcoObject eo = new EGH01DB.Objects.EcoObject();
                            if (EGH01DB.Objects.EcoObject.GetById(db, c, ref eo))
                            {
                                view = View("EcoObjectDelete", eo);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("EcoObject.Update"))
                {
                    string id = this.HttpContext.Request.Params["type_code"];

                    if (id != null)
                    {
                        int c = 0;
                        if (int.TryParse(id, out c))
                        {
                            EGH01DB.Objects.EcoObject eo = new EGH01DB.Objects.EcoObject();
                            if (EGH01DB.Objects.EcoObject.GetById(db, c, ref eo))
                            {
                                view = View("EcoObjectUpdate", eo);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("EcoObject.Excel"))
                {
                    EGH01DB.Objects.EcoObjectsList list = new EGH01DB.Objects.EcoObjectsList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/EcoObject.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/EcoObject.xml"), "text/plain", "Природоохранные объекты.xml");


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
        public ActionResult EcoObjectCreate(EGH01.Models.EGHRGE.EcoObjectView eo)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObject";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
               
                db = new RGEContext();
                view = View("EcoObject", db);
                if (menuitem.Equals("EcoObjectCreateEco.Create"))
                {

                    view = View("EcoObjectCreateEco");
                    return view;

                }
                if (menuitem.Equals("EcoObject.Create.Create"))
                {

                    int id = -1;
                    if (EGH01DB.Objects.EcoObject.GetNextId(db, out id))
                    {
                        String name = eo.name;
                        CadastreType type_cadastre = new CadastreType();
                        if (EGH01DB.Types.CadastreType.GetByCode(db, eo.list_cadastre, out type_cadastre))
                        {
                            EcoObjectType eco_type = new EcoObjectType();
                            if (EGH01DB.Types.EcoObjectType.GetByCode(db, eo.list_ecoType, out eco_type))
                            {
                               bool iswaterobject = eo.iswaterobject;
                                string strlat_s = this.HttpContext.Request.Params["lat_s"] ?? "Empty";
                                string strlng_s = this.HttpContext.Request.Params["lng_s"] ?? "Empty";
                                string strwaterdeep = this.HttpContext.Request.Params["waterdeep"] ?? "Empty";
                                string strheight = this.HttpContext.Request.Params["height"] ?? "Empty";
                                float lat_s = 0.0f;
                                float lng_s = 0.0f;
                                float waterdeep = 0.0f;
                                float height = 0.0f;
                                if (!Helper.FloatTryParse(strlat_s, out lat_s))
                                {
                                    lat_s = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strlng_s, out lng_s))
                                {
                                    lng_s = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strwaterdeep, out waterdeep))
                                {
                                    waterdeep = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strheight, out height))
                                {
                                    height = 0.0f;
                                }
                                Coordinates coordinates = new Coordinates(eo.latitude, eo.lat_m, lat_s, eo.lngitude, eo.lng_m, lng_s);
                                EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                                if (EGH01DB.Types.GroundType.GetByCode(db, eo.list_groundType, out ground_type))
                                {
                                    Point point = new Point(coordinates, ground_type, waterdeep, height);
                                    EGH01DB.Objects.EcoObject eco_object = new EGH01DB.Objects.EcoObject(id, point, eco_type, type_cadastre, name, iswaterobject);
                                    if (EGH01DB.Objects.EcoObject.Create(db, eco_object))
                                    {
                                        view = View("EcoObject", db);
                                    }
                                }
                            }
                        }
                        else if (menuitem.Equals("EcoObject.Create.Cancel")) view = View("EcoObject", db);
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
        public ActionResult EcoObjectDelete(int type_code)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObject";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();

                if (menuitem.Equals("EcoObject.Delete.Delete"))
                {
                    if (EGH01DB.Objects.EcoObject.DeleteById(db, type_code))
                        view = View("EcoObject", db);
                }
                else if (menuitem.Equals("EcoObject.Delete.Cancel"))
                    view = View("EcoObject", db);

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
        public ActionResult EcoObjectUpdate(EcoObjectView eov)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObject";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                view = View("EcoObject", db);
                if (menuitem.Equals("EcoObject.Update.Update"))
                {
                    int id = eov.id;
                    String name = eov.name;
                    CadastreType type_cadastre = new CadastreType();
                    if (EGH01DB.Types.CadastreType.GetByCode(db, eov.list_cadastre, out type_cadastre))
                    {
                        EcoObjectType eco_type = new EcoObjectType();
                        if (EGH01DB.Types.EcoObjectType.GetByCode(db, eov.list_ecoType, out eco_type))
                        {
                            bool iswaterobject = eov.iswaterobject;

                            string strlat_s = this.HttpContext.Request.Params["lat_s"] ?? "Empty";
                            string strlng_s = this.HttpContext.Request.Params["lng_s"] ?? "Empty";
                            string strwaterdeep = this.HttpContext.Request.Params["waterdeep"] ?? "Empty";
                            string strheight = this.HttpContext.Request.Params["height"] ?? "Empty";
                            float lat_s = 0.0f;
                            float lng_s = 0.0f;
                            float waterdeep = 0.0f;
                            float height = 0.0f;
                            if (!Helper.FloatTryParse(strlat_s, out lat_s))
                            {
                                lat_s = 0.0f;
                            }
                            if (!Helper.FloatTryParse(strlng_s, out lng_s))
                            {
                                lng_s = 0.0f;
                            }
                            if (!Helper.FloatTryParse(strwaterdeep, out waterdeep))
                            {
                                waterdeep = 0.0f;
                            }
                            if (!Helper.FloatTryParse(strheight, out height))
                            {
                                height = 0.0f;
                            }
                            Coordinates coordinates = new Coordinates(eov.latitude, eov.lat_m, lat_s, eov.lngitude, eov.lng_m, lng_s);
                            EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                            if (EGH01DB.Types.GroundType.GetByCode(db, eov.list_groundType, out ground_type))
                            {
                                Point point = new Point(coordinates, ground_type, waterdeep, height);
                                EGH01DB.Objects.EcoObject eco_object = new EGH01DB.Objects.EcoObject(id, point, eco_type, type_cadastre, name, iswaterobject);
                                if (EGH01DB.Objects.EcoObject.Update(db, eco_object))
                                {
                                    view = View("EcoObject", db);
                                }


                            }
                            else if (menuitem.Equals("EcoObject.Update.Cancel"))
                                view = View("EcoObject", db);

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
        public ActionResult EcoObjectCreateEco(EGH01.Models.EGHRGE.EcoObjectView eo)
        {
            RGEContext db = null;
            ViewBag.EGHLayout = "RGE.EcoObject";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new RGEContext();
                view = View("EcoObject", db);
                if (menuitem.Equals("EcoObject.Create.Create"))
                {

                    int id = -1;
                    if (EGH01DB.Objects.EcoObject.GetNextId(db, out id))
                    {
                        String name = eo.name;
                        CadastreType type_cadastre = new CadastreType();
                        if (EGH01DB.Types.CadastreType.GetByCode(db, eo.list_cadastre, out type_cadastre))
                        {
                            EcoObjectType eco_type = new EcoObjectType();
                            if (EGH01DB.Types.EcoObjectType.GetByCode(db, eo.list_ecoType, out eco_type))
                            {
                                bool iswaterobject = eo.iswaterobject;
                                string strlat_s = this.HttpContext.Request.Params["lat_s"] ?? "Empty";
                                string strlng_s = this.HttpContext.Request.Params["lng_s"] ?? "Empty";
                                string strwaterdeep = this.HttpContext.Request.Params["waterdeep"] ?? "Empty";
                                string strheight = this.HttpContext.Request.Params["height"] ?? "Empty";
                                string strangel = this.HttpContext.Request.Params["angel"] ?? "Empty";
                                string strdistance = this.HttpContext.Request.Params["distance"] ?? "Empty";
                                float lat_s = 0.0f;
                                float angel = 0.0f;
                                float distance = 0.0f;
                                float lng_s = 0.0f;
                                float waterdeep = 0.0f;
                                float height = 0.0f;
                                if (!Helper.FloatTryParse(strlat_s, out lat_s))
                                {
                                    lat_s = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strangel, out angel))
                                {
                                    angel = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strdistance, out distance))
                                {
                                    distance = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strlng_s, out lng_s))
                                {
                                    lng_s = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strwaterdeep, out waterdeep))
                                {
                                    waterdeep = 0.0f;
                                }
                                if (!Helper.FloatTryParse(strheight, out height))
                                {
                                    height = 0.0f;
                                }
                                Coordinates coordinates = new Coordinates(eo.latitude, eo.lat_m, lat_s, eo.lngitude, eo.lng_m, lng_s);
                                EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                                if (EGH01DB.Types.GroundType.GetByCode(db, eo.list_groundType, out ground_type))
                                {
                                    Point point = new Point(coordinates, ground_type, waterdeep, height);                          
                                    EGH01DB.Objects.EcoObject eco_object = new EGH01DB.Objects.EcoObject(id, point, eco_type, type_cadastre, name, iswaterobject); 
                                    EGH01DB.Objects.EcoObject new_eco_object = new EGH01DB.Objects.EcoObject(); 
                                    if (EGH01DB.Objects.EcoObject.CreateNear(db, eco_object, angel, distance, out new_eco_object))
                                    {
                                        view = View("EcoObject", db);
                                    }
                                }
                            }
                        }
                        else if (menuitem.Equals("EcoObject.Create.Cancel")) view = View("EcoObject", db);
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