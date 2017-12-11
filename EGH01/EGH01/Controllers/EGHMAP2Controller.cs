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
        public ActionResult EGHMAP2()
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            ActionResult view = View("EGHMAP2");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("EGHMAP2", db);
            return view;
        }
        [HttpPost]
        public JsonResult CreateFrom2(EGH01.Models.EGHMAP.MapPointView mp)
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            ActionResult view = View("EGHMAPsoil");

            db = new RGEContext();
            view = View("EGHMAPsoil", db);
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
            EGH01DB.Primitives.Coordinates mapPoint = new Coordinates(coords,coordm);
            EGH01DB.Types.District district = new District();
            EGH01DB.Primitives.MapHelper.GetRegion(db, mapPoint, out district);
            ViewData["district"] = district.name;
            ViewData["region"] = district.region.name;

            string protection_level = "";
            EGH01DB.Primitives.MapHelper.GetGroundWaterMapProtectLevel(db,mapPoint,out protection_level);

            string self_cleaning_zone = "";
            EGH01DB.Primitives.MapHelper.GetSelfCleaningZone(db,mapPoint, out self_cleaning_zone);

            string protect_zone = "";
            int protect_grade = 0;
            EGH01DB.Primitives.MapHelper.GetGroundProtectZone(db,mapPoint,out protect_zone, out protect_grade);
         
            var heights = new
            {
             
                District = district.name,
                Region = district.region.name,
                Protectionlevel= protection_level,
                Selfcleaningzone = self_cleaning_zone,
                Protectzone = protect_zone,
                Protectgrade = protect_grade



            };

            return Json(heights);
        }



    }
}
