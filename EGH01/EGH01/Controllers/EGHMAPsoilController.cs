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
        public ActionResult EGHMAPsoil()
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            ActionResult view = View("EGHMAPsoil");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("EGHMAPsoil", db);
            return view;
        }
        [HttpPost]
        public JsonResult CreateFromSoil(EGH01.Models.EGHMAP.MapPointView mp)
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


            float time_migration = 0.0f;
            EGH01DB.Primitives.MapHelper.GetTimeMigration(db,mapPoint, out time_migration);


            string aeration_power = "";
            float average_aeration_power = 0.0f;
            float max_aeration_power = 0.0f;
            string litology = "";
            EGH01DB.Primitives.MapHelper.GetAerationZone(db,mapPoint, out aeration_power, out average_aeration_power, out max_aeration_power, out litology);
            float height = 0.0f;
            EGH01DB.Primitives.MapHelper.GetHeight( db,mapPoint, out height);
            EGH01DB.Types.SoilType soilType = new SoilType();
            EGH01DB.Primitives.MapHelper.GetSoil(db,mapPoint, out soilType);

            EGH01DB.Primitives.Climat climat = new Climat(db,mapPoint);
            EGH01DB.Primitives.MapHelper.GetMonthTemperature(db,mapPoint, out climat);
            float[] period = climat.temperature;

            //string gorizont_power = "";
            //string gorizont_name = "";
            //string litologyQuat = "";
            //float genesys_code = 0.0f;
            //string genetic_type = "";
            //string otdel = "";
            //string podotdel = "";
            //string podgorizon = "";
            //float litology_code = 0.0f;
            //string geology_index = "";
            //string rgb = "";
            //string sistema = "";
            
            //EGH01DB.Primitives.MapHelper.GetQuatSediments(db,mapPoint,out gorizont_power, out gorizont_name,
            //    out litologyQuat, out genetic_type, out  genesys_code,
            //    out otdel,out podotdel,out podgorizon,out litology_code,out geology_index,out rgb, out sistema);

            var heights = new
            {
                Soilname = soilType.name,
                Soilmehsost = soilType.mehsost,
                Soiltype = soilType.soil_type,
                Soilsubtype = soilType.soil_subtype,
                Humidity = soilType.humidity,
                Gumusdepth = soilType.gumus_depth,
                Capmoisturecapacity = soilType.watercapacity,
                Petrolfiltrationcoef = soilType.petrol_filtration_coef,
                Fueloilfiltrationcoef = soilType.fuel_oil_filtration_coef,
                Dieselfiltrationcoef = soilType.diesel_filtration_coef,
                Hydrolyticacidity = soilType.hydrolytic_acidity,
                Oilcapacity = soilType.oil_capacity,
                Density = soilType.density,
                Carboncontent = soilType.carbon_content,
                Filtercoefinterval = soilType.filter_coef_interval,
                Porosityinterval = soilType.porosity_interval,
                Glinainterval = soilType.glina_interval,
                Mginterval = soilType.mg_interval,
                Phinterval = soilType.ph_interval,
                Densityinterval = soilType.density,
                Maxmoisturecapacityinterval = soilType.max_moisture_capacity_interval,
                Gumuspercentageinterval = soilType.gumus_percentage_interval,
                Klass = soilType.klass,
                Soiltypecode = soilType.soil_class_code,
                Wrbcode = soilType.wrb_code,
                Wrbnewcode = soilType.wrb_new_code,
                Rgbcode = soilType.rgb_code,
                Shtrihovka = soilType.shtrihovka,
                Height = height,
                Aeration_power = aeration_power,
                Average_aeration_power = average_aeration_power,
                Max_aeration_power = max_aeration_power,
                Litology = litology,
                Time_migration = time_migration,
                District = district.name,
                Region = district.region.name,
                
                Period = period

                //Gorizont_power = gorizont_power,
                //Gorizont_name = gorizont_name,
                //LitologyQuat = litologyQuat,
                //Genesys_code = genesys_code,
                //Genetic_type = genetic_type,
                //Otdel = otdel,
                //Podotdel = podotdel,
                //Podgorizon = podgorizon,
                //Litology_code = litology_code,
                //Geology_index = geology_index,
                //Rgb = rgb,
                //Sistema = sistema



            };

            return Json(heights);
        }



    }
}
