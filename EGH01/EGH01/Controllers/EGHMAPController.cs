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

namespace EGH01.Controllers
{
    public partial class EGHMAPController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("Index", db);
            return view;
        }

        [HttpPost]
        public ActionResult IndexCreate(EGH01.Models.EGHMAP.MapPointView mp)
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            ActionResult view = View("Index");
            try
            {
                db = new RGEContext();
                view = View("Index", db);
                String Latitude = mp.Latitude;
                ViewData["Latitude"] = Latitude;
                String Lat_m = mp.Lat_m;
                ViewData["Lat_m"] = Lat_m;
                String Lat_s = mp.Lat_s;
                ViewData["Lat_s"] = Lat_s;
                String Lngitude = mp.Lngitude;
                ViewData["Lngitude"] = Lngitude;
                String Lng_m = mp.Lng_m;
                ViewData["Lng_m"] = Lng_m;
                String Lng_s = mp.Lng_s;
                ViewData["Lng_s"] = Lng_s;

                float coords = EGH01DB.Primitives.Coordinates.dms_to_d(int.Parse(Latitude), int.Parse(Lat_m), float.Parse(Lat_s));
                float coordm = EGH01DB.Primitives.Coordinates.dms_to_d(int.Parse(Lngitude), int.Parse(Lng_m), float.Parse(Lng_s));

                EGH01DB.Types.MapType mapPoint = new MapType(coordm.ToString("F", CultureInfo.InvariantCulture), coords.ToString("F", CultureInfo.InvariantCulture));
                EGH01DB.Types.GroundType ground = new GroundType();
                EGH01DB.Types.MapType.GetGroundType(mapPoint, db, out ground);
                //EGH01DB.Types.MapType.GetHeight(mapPoint, db, out float height);   //smw60
                //height = 1.2f;                                                     //smw60  
                EGH01DB.Types.SoilType soilType = new SoilType();
                EGH01DB.Types.MapType.GetSoilType(mapPoint, db, out soilType);

                EGH01DB.Types.District district = new District();
                EGH01DB.Types.MapType.GetDistrict(mapPoint, db, out district);

                string city = "";
                EGH01DB.Types.MapType.GetCity(mapPoint, db, out  city);

                string self_cleaning_zone = "";
                EGH01DB.Types.MapType.GetSelfCleaningZone(mapPoint, db, out self_cleaning_zone);

                float waterdeep = 0.0f;
                EGH01DB.Types.MapType.GetWaterdeep(mapPoint, db, out waterdeep);

                EGH01DB.Objects.EcoObject eco = new EGH01DB.Objects.EcoObject();
                EGH01DB.Types.MapType.GetEcoObjectList(mapPoint, db, out eco);

                ViewData["soil"] = soilType.name;
                ViewData["waterdeep"] = waterdeep;
               // ViewData["height"] = height;  // smw60
                ViewData["district"] = district.name;
                ViewData["ground"] = ground.name;
                ViewData["city"] = city;
                ViewData["eco"] = eco.name;
                 ViewData["self_cleaning_zone"] = self_cleaning_zone;

                //ViewData["soil"] = "Почва";
                //ViewData["ground"] = "Грунт";
                //ViewData["district"] = "регион";
                //ViewData["city"] = "город";
                //ViewData["self_cleaning_zone"] = "зона";
                //ViewData["waterdeep"] = "Водная зона";
                //ViewData["eco"] = "Экологический объект";
                //ViewData["height"] = height;


            }
            catch (RGEContext.Exception e)
            {
                ViewBag.msg = e.message;
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }

          //  ActionResult view = View("Index");
                view = View("Index", db);
            
            return view;
            }

        //[HttpPost]
        //public JsonResult CreateFromAjax(EGH01.Models.EGHMAP.MapPointView mp)
        //{
        //    ViewBag.EGHLayout = "MAP";
        //    RGEContext db = null;
        //    string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
        //    ActionResult view = View("Index");
        //        db = new RGEContext();
        //        view = View("Index", db);
        //        String Latitude = mp.Latitude;
        //        ViewData["Latitude"] = Latitude;
        //        String Lat_m = mp.Lat_m;
        //        ViewData["Lat_m"] = Lat_m;
        //        String Lat_s = mp.Lat_s;
        //        ViewData["Lat_s"] = Lat_s;
        //        String Lngitude = mp.Lngitude;
        //        ViewData["Lngitude"] = Lngitude;
        //        String Lng_m = mp.Lng_m;
        //        ViewData["Lng_m"] = Lng_m;
        //        String Lng_s = mp.Lng_s;
        //        ViewData["Lng_s"] = Lng_s;

        //        float coords = EGH01DB.Primitives.Coordinates.dms_to_d(int.Parse(Latitude), int.Parse(Lat_m), float.Parse(Lat_s));
        //        float coordm = EGH01DB.Primitives.Coordinates.dms_to_d(int.Parse(Lngitude), int.Parse(Lng_m), float.Parse(Lng_s));

        //        EGH01DB.Types.MapType mapPoint = new MapType(coordm.ToString("F", CultureInfo.InvariantCulture), "53.9043260781941");
        //        EGH01DB.Types.GroundType ground = new GroundType();
        //        EGH01DB.Types.MapType.GetGroundType(mapPoint, db, out ground);
        //        EGH01DB.Types.MapType.GetHeight(mapPoint, db, out float height);
        //        height = 1.2f;
        //        EGH01DB.Types.SoilType soilType = new SoilType();
        //        EGH01DB.Types.MapType.GetSoilType(mapPoint, db, out soilType);
        //        EGH01DB.Types.District district = new District();
        //        EGH01DB.Types.MapType.GetDistrict(mapPoint, db, out district);

        //        EGH01DB.Types.MapType.GetCity(mapPoint, db, out string city);
        //        string self_cleaning_zone = "";
        //        EGH01DB.Types.MapType.GetSelfCleaningZone(mapPoint, db, out self_cleaning_zone);
        //        EGH01DB.Types.MapType.GetWaterdeep(mapPoint, db, out float waterdeep);

        //        //     ViewData["soil"]= soilType.name;
        //        ViewData["soil"] = "Почва";
        //        // ViewData["ground"] = ground.name;
        //        ViewData["ground"] = "Грунт";
        //        ViewData["height"] = height;
        //        // ViewData["district"] = district.name;
        //        ViewData["district"] = "регион";
        //        //ViewData["city"] = city;
        //        ViewData["city"] = "город";
        //        //   ViewViewData["self_cleaning_zone"] = self_cleaning_zone;
        //        ViewData["self_cleaning_zone"] = "зона";
        //        //   ViewData["waterdeep"] = waterdeep;
        //        ViewData["waterdeep"] = "Водная зона";

        //    var response = new
        //    {
        //        Height = height
        //    };

        //    return Json(response);
        //}



    }
    }
