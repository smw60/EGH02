using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EGH01.Models.EGHCAI;
using EGH01DB;
using EGH01DB.Points;
using EGH01DB.Primitives;
using EGH01DB.Types;

namespace EGH01.Controllers
{
    public partial class EGHCAIController : Controller
    {
        public ActionResult RiskObject()
        {
            CAIContext db = null;
            ViewBag.EGHLayout = "CAI";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CAIContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("RiskObject", db);

                if (menuitem.Equals("RiskObject.Create"))
                {

                    view = View("RiskObjectCreate");

                }
                else if (menuitem.Equals("RiskObject.Delete"))
                {
                    string type_code_item = this.HttpContext.Request.Params["id"];
                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Objects.RiskObject it = new EGH01DB.Objects.RiskObject();
                            if (EGH01DB.Objects.RiskObject.GetById(db, c, ref it))
                            {
                                view = View("RiskObjectDelete", it);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("RiskObject.Update"))
                {
                    string type_code_item = this.HttpContext.Request.Params["id"];

                    if (type_code_item != null)
                    {
                        int c = 0;
                        if (int.TryParse(type_code_item, out c))
                        {
                            EGH01DB.Objects.RiskObject it = new EGH01DB.Objects.RiskObject();
                            if (EGH01DB.Objects.RiskObject.GetById(db, c, ref it))
                            {
                                view = View("RiskObjectUpdate", it);
                            }
                        }
                    }
                }
                else if (menuitem.Equals("RiskObject.Excel"))
                {

                    EGH01DB.Objects.RiskObjectsList list = new EGH01DB.Objects.RiskObjectsList(db);
                    XmlNode node = list.toXmlNode();
                    XmlDocument doc = new XmlDocument();
                    XmlNode nnode = doc.ImportNode(node, true);
                    doc.AppendChild(nnode);
                    doc.Save(Server.MapPath("~/App_Data/RiskObject.xml"));
                    view = View("Index");

                    view = File(Server.MapPath("~/App_Data/RiskObject.xml"), "text/plain", "Техногенный объект.xml");


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
        public ActionResult RiskObjectCreate(EGH01.Models.EGHCAI.RiskObject rs)
        {
            CAIContext db = null;
            ViewBag.EGHLayout = "CAI";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CAIContext();
                view = View("RiskObject", db);
                if (menuitem.Equals("RiskObject.Create.Create"))
                {

                    int id = -1;
                    if (EGH01DB.Objects.RiskObject.GetNextId(db, out id))
                    {
                        String ownership = "f";
                        int numberofrefuel = 1;
                        int volume = 1;
                        Boolean watertreatment = rs.watertreatment;
                        Boolean watertreatmentcollect = rs.watertreatmentcollect;
                        string base64 = rs.base64;
                        Byte[] map;
                        if (base64 == null)
                        {
                            map = new byte[0];
                        }
                        else { map = ASCIIEncoding.ASCII.GetBytes(base64); }


                        int groundtank = rs.groundtank;
                        int undergroundtank = rs.undergroundtank;
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
                        Coordinates coordinates = new Coordinates(rs.latitude, rs.lat_m, lat_s, rs.lngitude, rs.lng_m, lng_s);
                        float latitude = EGH01DB.Primitives.Coordinates.dms_to_d(rs.latitude, rs.lat_m, rs.lat_s);
                        EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                        if (EGH01DB.Types.GroundType.GetByCode(db, rs.list_groundType, out ground_type))
                        {
                            Point point = new Point(coordinates, ground_type, waterdeep, height);
                            EGH01DB.Types.RiskObjectType risk_object_type = new EGH01DB.Types.RiskObjectType();
                            if (EGH01DB.Types.RiskObjectType.GetByCode(db, rs.selectlist, out risk_object_type))
                            {
                                EGH01DB.Types.CadastreType cadastre_type = new EGH01DB.Types.CadastreType();
                                if (EGH01DB.Types.CadastreType.GetByCode(db, rs.list_cadastre, out cadastre_type))
                                {
                                    EGH01DB.Types.District district = new EGH01DB.Types.District();
                                    if (EGH01DB.Types.District.GetByCode(db, rs.list_district, out district))
                                    {
                                        EGH01DB.Types.Region region = new EGH01DB.Types.Region();
                                        if (EGH01DB.Types.Region.GetByCode(db, rs.list_region, out region))
                                        {
                                            DateTime foundationdate = rs.foundationdate;
                                            if (foundationdate.Year == 0001) {
                                                foundationdate = new DateTime(1900,01,01); 

                                            }

                                            DateTime reconstractiondate = rs.reconstractiondate;
                                            if (reconstractiondate.Year == 0001)
                                            {
                                                reconstractiondate = new DateTime(1900, 01, 01);

                                            }
                                            string name = rs.name;
                                            String phone = rs.phone;
                                            String fax = rs.fax;
                                            string address = rs.adress;
                                            String email = rs.email;



                                            String fueltype = rs.fax;
                                            String geodescription = rs.fax;
                                            EGH01DB.Objects.RiskObject risk_object = new EGH01DB.Objects.RiskObject(id, point, risk_object_type, cadastre_type, name,
                                                district, region, address, ownership, phone, fax, email, foundationdate, reconstractiondate, numberofrefuel, volume, watertreatment,
                                                watertreatmentcollect, map, groundtank, undergroundtank, fueltype, 0, 0.0f, 0.0f, geodescription);

                                            if (EGH01DB.Objects.RiskObject.Create(db, risk_object))
                                            {
                                                view = View("RiskObject", db);
                                            }
                                        }
                                    }

                                }
                            }
                            else if (menuitem.Equals("RiskObject.Create.Cancel")) view = View("RiskObject", db);
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
        public ActionResult RiskObjectDelete(int type_code)
        {
            CAIContext db = null;
            ViewBag.EGHLayout = "CAI";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CAIContext();

                if (menuitem.Equals("RiskObject.Delete.Delete"))
                {
                    if (EGH01DB.Objects.RiskObject.DeleteById(db, type_code)) view = View("RiskObject", db);
                }
                else if (menuitem.Equals("RiskObject.Delete.Cancel")) view = View("RiskObject", db);

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
        public ActionResult RiskObjectUpdate(EGH01.Models.EGHCAI.RiskObject itv)
        {
            CAIContext db = null;
            ViewBag.EGHLayout = "CAI";
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new CAIContext();
                view = View("RiskObject", db);
                if (menuitem.Equals("RiskObject.Update.Update"))
                {

                    String ownership = "f";
                    int numberofrefuel = 1;
                    int volume = 1;
                    Boolean watertreatment = itv.watertreatment;
                    Boolean watertreatmentcollect = itv.watertreatmentcollect;
                    string base64 = itv.base64;
                    Byte[] map;
                    if (base64 == null)
                    {
                        map = new byte[0];
                    }
                    else { map = ASCIIEncoding.ASCII.GetBytes(base64); }
                   
                    int groundtank = itv.groundtank;
                    int undergroundtank = itv.undergroundtank;
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
                    Coordinates coordinates = new Coordinates(itv.latitude, itv.lat_m, lat_s, itv.lngitude, itv.lng_m, lng_s);
                    EGH01DB.Types.GroundType ground_type = new EGH01DB.Types.GroundType();
                    if (EGH01DB.Types.GroundType.GetByCode(db, itv.list_groundType, out ground_type))
                    {

                        Point point = new Point(coordinates, ground_type, waterdeep, height);
                        EGH01DB.Types.RiskObjectType risk_object_type = new EGH01DB.Types.RiskObjectType();
                        if (EGH01DB.Types.RiskObjectType.GetByCode(db, itv.selectlist, out risk_object_type))
                        {
                            EGH01DB.Types.CadastreType cadastre_type = new EGH01DB.Types.CadastreType();
                            if (EGH01DB.Types.CadastreType.GetByCode(db, itv.list_cadastre, out cadastre_type))
                            {
                                EGH01DB.Types.District district = new EGH01DB.Types.District();
                                if (EGH01DB.Types.District.GetByCode(db, itv.list_district, out district))
                                {
                                    EGH01DB.Types.Region region = new EGH01DB.Types.Region();
                                    if (EGH01DB.Types.Region.GetByCode(db, itv.list_region, out region))
                                    {
                                        DateTime foundationdate = itv.foundationdate;
                                        if (foundationdate.Year == 0001)
                                        {
                                            foundationdate = new DateTime(1900, 01, 01);

                                        }

                                        DateTime reconstractiondate = itv.reconstractiondate;
                                        if (reconstractiondate.Year == 0001)
                                        {
                                            reconstractiondate = new DateTime(1900, 01, 01);

                                        }
                                        string name = itv.name;
                                        String phone = itv.phone;
                                        String fax = itv.fax;
                                        string address = itv.adress;
                                        String email = itv.email;

                                        String fueltype = itv.fax;
                                        String geodescription = itv.fax;

                                        EGH01DB.Objects.RiskObject risk_object = new EGH01DB.Objects.RiskObject(itv.type_code, point, risk_object_type, cadastre_type, name,
                                            district, region, address, ownership, phone, fax, email, foundationdate, reconstractiondate, numberofrefuel, volume, watertreatment,
                                            watertreatmentcollect, map, groundtank, undergroundtank, fueltype, 0, 0.0f, 0.0f, geodescription);

                                        if (EGH01DB.Objects.RiskObject.Update(db, risk_object))
                                            view = View("RiskObject", db);
                                    }
                                    else if (menuitem.Equals("RiskObject.Update.Cancel")) view = View("RiskObject", db);
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
