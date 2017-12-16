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
        public ActionResult EGHMAPWATER()
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            ActionResult view = View("EGHMAPWATER");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("EGHMAPWATER", db);
            return view;
        }
        [HttpPost]
        public JsonResult CreateFromWATER(EGH01.Models.EGHMAP.MapPointView mp)
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            ActionResult view = View("EGHMAPWATER");

            db = new RGEContext();
            view = View("EGHMAPWATER", db);
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
            EGH01DB.Primitives.Coordinates mapPoint = new Coordinates(coords, coordm);
            EGH01DB.Types.District district = new District();
            EGH01DB.Primitives.MapHelper.GetRegion(db, mapPoint, out district);
            ViewData["district"] = district.name;
            ViewData["region"] = district.region.name;

            EGH01DB.Objects.EcoObject water_Object = new EGH01DB.Objects.EcoObject();
            EGH01DB.Primitives.MapHelper.GetWaterPond(db, mapPoint, out water_Object);

            string watername = "";
            string watertype = "";

            EGH01DB.Types.WaterProtectionArea water_intake = new WaterProtectionArea();
            EGH01DB.Primitives.MapHelper.GetWaterIntake(db, mapPoint, out water_intake);

            string regionW = "";
            string type = "";
            string districtW = "";
            float network_density = 0.0f;
            float lenght = 0.0f;
            float district_area = 0.0f;

            EGH01DB.Primitives.MapHelper.GetRiverDensity(db,mapPoint, out districtW, out regionW, out type, out network_density, out lenght, out district_area);

            if (type == string.Empty) {
                type = "Не является водным объектом";

            }
            int[] downfall;
            EGH01DB.Primitives.MapHelper.GetDownfall(db,mapPoint, out downfall);
           
            if (water_Object.iswaterobject) {

                watername = water_Object.name;
                watertype = water_Object.ecoobjecttype.name;
             
            }
            else
            {
                watername = "Не является водным объектом";
                watertype = "Не является водным объектом";
            }
            EGH01DB.Types.WaterProtectionArea water_area = new WaterProtectionArea();
            EGH01DB.Primitives.MapHelper.GetWaterProtectionZone(db, mapPoint, out water_area);

            var heights = new
            {

                District = district.name,
                Region = district.region.name,
                Watername = watername,
                Watertype = watertype,
                WaterintakeName = water_intake.name,
                Waterintakebuffer = water_intake.buffer,
                RegionW = regionW,
                Type = type,
                DistrictW = districtW,
                Network_density = network_density,
                Lenght = lenght,
                District_area = district_area,
                Downfall = downfall,
                WaterAreaName = water_area.name,
                WaterAreaBuff = water_area.buffer





            };

            return Json(heights);
        }



    }
}
