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
        public ActionResult EGHMAPOTHER()
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            ActionResult view = View("EGHMAPOTHER");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("EGHMAPOTHER", db);
            return view;
        }
        [HttpPost]
        public JsonResult CreateFromOTHER(EGH01.Models.EGHMAP.MapPointView mp)
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            ActionResult view = View("EGHMAPOTHER");

            db = new RGEContext();
            view = View("EGHMAPOTHER", db);
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

            string city = "";
            EGH01DB.Primitives.MapHelper.GetCity(db,mapPoint, out city);
            if (city == string.Empty) {

                city = "Города нет";
            }

            String assoc_gr_1 = "";
            String shtrih_cod = "";
            String code_legen = "";
            String class_form = "";
            String associastion = "";
            String code_rgb = "";
            String type_resty = "";
            String krap_code = "";
            String dop_code = "";
            EGH01DB.Primitives.MapHelper.GetVegetation(db,mapPoint, out assoc_gr_1, out shtrih_cod, out code_legen, out class_form, out associastion, out code_rgb, out type_resty, out krap_code, out dop_code);




            float aeration_power = 0.0f;
            float average_aeration_power = 0.0f;
            float max_aeration_power= 0.0f;
            float litology = 0.0f;
            EGH01DB.Primitives.MapHelper.GetGeology(db, mapPoint, out aeration_power, out average_aeration_power, out max_aeration_power, out litology);

            string gorizont_power = "";
            string gorizont_name = "";
            string litologyQuat = "";
            float genesys_code = 0.0f;
            string genetic_type = "";
            string otdel = "";
            string podotdel = "";
            string podgorizon = "";
            float litology_code = 0.0f;
            string geology_index = "";
            string rgb = "";
            string sistema = "";

            EGH01DB.Primitives.MapHelper.GetQuatSediments(db, mapPoint, out gorizont_power, out gorizont_name,
                out litologyQuat, out genetic_type, out genesys_code,
                out otdel, out podotdel, out podgorizon, out litology_code, out geology_index, out rgb, out sistema);

            SunRadiation[] sunradiation;
            EGH01DB.Primitives.MapHelper.GetSunRadiation(db, mapPoint, out sunradiation);
            sunradiation.ToString();
            var heights = new
            {

                District = district.name,
                Region = district.region.name,
                City= city,
                Assoc_gr_1 = assoc_gr_1,
                Shtrih_cod = shtrih_cod,
                Code_legen = code_legen,
                Class_form = class_form,
                Associastion = associastion,
                Code_rgb = code_rgb,
                Type_resty = type_resty,
                Krap_code = krap_code,
                Dop_code = dop_code,
                Aeration_power = aeration_power,
                Average_aeration_power = average_aeration_power,
                Max_aeration_power = max_aeration_power,
                Litology = litology,
                Gorizont_power = gorizont_power,
                Gorizont_name = gorizont_name,
                LitologyQuat = litologyQuat,
                Genesys_code = genesys_code,
                Genetic_type = genetic_type,
                Otdel = otdel,
                Podotdel = podotdel,
                Podgorizon = podgorizon,
                Litology_code = litology_code,
                Geology_index = geology_index,
                Rgb = rgb,
                Sistema = sistema,
                Average_rad0 = sunradiation[0].average_rad,
                From_rad0=sunradiation[0].from_rad,
                To_rad0 = sunradiation[0].to_rad,
                Average1 = sunradiation[1].average_rad,
                //From1 = sunradiation[1].from_rad,
                //To1 = sunradiation[1].to_rad






            };

            return Json(heights);
        }



    }
}
