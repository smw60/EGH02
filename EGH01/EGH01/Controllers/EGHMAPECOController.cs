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
        public ActionResult EGHMAPECO()
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            ActionResult view = View("EGHMAPECO");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("EGHMAPECO", db);
            return view;
        }
        [HttpPost]
        public JsonResult CreateFromECO(EGH01.Models.EGHMAP.MapPointView mp)
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            ActionResult view = View("EGHMAPECO");

            db = new RGEContext();
            view = View("EGHMAPECO", db);
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


            string nationalpark = "";
            string nationalpark_type = "";
            string nationalpark_subtype = "";
            string nationalpark_city = "";
            EGH01DB.Primitives.MapHelper.GetEcoNational(db, mapPoint, out nationalpark, out nationalpark_type, out nationalpark_subtype, out nationalpark_city); // blinova - поправить метод

            string republicpoint = "";
            string republicpoly = "";

            string republic = "";
            EGH01DB.Primitives.MapHelper.GetEcoRepublic(db, mapPoint, out republic);

            string local = "";
            EGH01DB.Primitives.MapHelper.GetEcoLocal(db, mapPoint, out local);
            var heights = new
            {
             
                District = district.name,
                Region = district.region.name,
                Republic = republic,
                Local = local,
                Nationalpark= nationalpark,
                Nationalparktype = nationalpark_type,
                Nationalparksubtype = nationalpark_subtype,
                Nationalparkcity = nationalpark_city,
                Republicpoint = republicpoint,
            Republicpoly = republicpoly



            };

            return Json(heights);
        }



    }
}
