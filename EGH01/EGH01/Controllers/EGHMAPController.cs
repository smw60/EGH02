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

//        [HttpPost]
//        public ActionResult IndexCreate(EGH01.Models.EGHMAP.MapPointView mp)
//        {
//            ViewBag.EGHLayout = "MAP";
//            RGEContext db = null;
//            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
//            ActionResult view = View("Index");
//            try
//            {
//                db = new RGEContext();
//                view = View("Index", db);
//                String Latitude = mp.Latitude;
//                ViewData["Latitude"] = Latitude;
//                String Lat_m = mp.Lat_m;
//                ViewData["Lat_m"] = Lat_m;
//                String Lat_s = mp.Lat_s;
//                ViewData["Lat_s"] = Lat_s;
//                String Lngitude = mp.Lngitude;
//                ViewData["Lngitude"] = Lngitude;
//                String Lng_m = mp.Lng_m;
//                ViewData["Lng_m"] = Lng_m;
//                String Lng_s = mp.Lng_s;
//                ViewData["Lng_s"] = Lng_s;

//                float coords = EGH01DB.Primitives.Coordinates.dms_to_d(int.Parse(Latitude), int.Parse(Lat_m), float.Parse(Lat_s));
//                float coordm = EGH01DB.Primitives.Coordinates.dms_to_d(int.Parse(Lngitude), int.Parse(Lng_m), float.Parse(Lng_s));
//                EGH01DB.Types.MapType mapPoint = new MapType(coords.ToString("F", CultureInfo.InvariantCulture), coordm.ToString("F", CultureInfo.InvariantCulture));
//                EGH01DB.Types.GroundType ground = new GroundType();
//                EGH01DB.Types.MapType.GetGroundType(mapPoint, db, out ground);
//                String grounds = "Получено из карты коэффициентов грунта";
//                if (String.Compare(ground.name, grounds) == 1)
//                {

//                    ViewData["ground"] = ground.name;

//                }


////
//                EGH01DB.Types.WaterProtectionArea waterProtectionArea = new WaterProtectionArea();
//                EGH01DB.Types.MapType.GetWaterProtection(mapPoint, db, out waterProtectionArea);

//                String water = "Не является зоной водозабора";
//                if (String.Compare(waterProtectionArea.name, water) == 1)
//                {

//                    ViewData["waterProtectionArea"] = waterProtectionArea.name;

//                }
              


//                EGH01DB.Types.WaterProtectionArea water_intake = new WaterProtectionArea();
//                EGH01DB.Types.MapType.GetWaterIntake(mapPoint, db, out water_intake);




//                float waterdeep = 0.0f;
//                EGH01DB.Types.MapType.GetWaterdeep(mapPoint, db, out waterdeep);



//                float height = 0.0f;
//                EGH01DB.Types.MapType.GetHeight(mapPoint, db, out  height);   
                


                
//                EGH01DB.Types.SoilType soilType = new SoilType();
//                EGH01DB.Types.MapType.GetSoilType(mapPoint, db, out soilType);

              
          

//                float time_migration = 0.0f;
//                EGH01DB.Types.MapType.GetTimeMigration(mapPoint,db,out time_migration);




//                EGH01DB.Objects.EcoObject ecoW = new EGH01DB.Objects.EcoObject();
//                EGH01DB.Types.MapType.GetWaterObject(mapPoint,db,out ecoW);


                        




//                EGH01DB.Types.District district = new District();
//                EGH01DB.Types.MapType.GetDistrict(mapPoint, db, out district);


//                string city = "";
              
//                EGH01DB.Types.MapType.GetCity(mapPoint, db, out  city);
              
//                if ((String.Compare(city, "") ==1) ){
//                    ViewData["city"] = city;
//                }




//                if ((String.Compare(ecoW.name, "")) == 1) {

//                    ViewData["ecoW"] = ecoW.name;
//                }




//                if (String.Compare(soilType.name, "") == 1)
//                {

//                    ViewData["soilType"] = soilType.name;

//                }

////

//                string self_cleaning_zone = "";
//                EGH01DB.Types.MapType.GetSelfCleaningZone(mapPoint, db, out self_cleaning_zone);

               



//                EGH01DB.Objects.EcoObject eco = new EGH01DB.Objects.EcoObject();
//                EGH01DB.Types.MapType.GetEcoObject(mapPoint, db, out eco);
//                if ((String.Compare(eco.name, "")) == 1)
//                {

//                    ViewData["eco"] = eco.name;
//                }


//                ViewData["height"] = height;
//                ViewData["district"] = district.name;
//                ViewData["region"] = district.region.name;
//                ViewData["self_cleaning_zone"] = self_cleaning_zone;
//                ViewData["time_migration"] = time_migration;
//                ViewData["water_intake"] = water_intake.name;
//                ViewData["waterdeep"] = waterdeep;
//                ViewData["waterfilter"] = ground.waterfilter; //
//                ViewData["porosity"] = ground.porosity; //
//                ViewData["watercapacity"] = ground.watercapacity; //




//            }
//            catch (RGEContext.Exception e)
//            {
//                ViewBag.msg = e.message;
//            }
//            catch (Exception e)
//            {
//                ViewBag.msg = e.Message;
//            }

//          //  ActionResult view = View("Index");
//                view = View("Index", db);
            
//            return view;
//            }
        //ajax
        [HttpPost]
        public JsonResult CreateFromAjax(EGH01.Models.EGHMAP.MapPointView mp)
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            ActionResult view = View("Index");

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
            //EGH01DB.Types.MapType mapPoint = new MapType(coords.ToString("F", CultureInfo.InvariantCulture), coordm.ToString("F", CultureInfo.InvariantCulture));
            EGH01DB.Types.GroundType ground = new GroundType();
            //EGH01DB.Types.MapType.GetGroundType(mapPoint, db, out ground);
            String grounds = "Получено из карты коэффициентов грунта";
            if (String.Compare(ground.name, grounds) == 1)
            {

                ViewData["ground"] = ground.name;

            }


            //
            EGH01DB.Types.WaterProtectionArea waterProtectionArea = new WaterProtectionArea();
            //EGH01DB.Types.MapType.GetWaterProtection(mapPoint, db, out waterProtectionArea);

            String water = "Не является зоной водозабора";
            if (String.Compare(waterProtectionArea.name, water) == 1)
            {

                ViewData["waterProtectionArea"] = waterProtectionArea.name;

            }



            EGH01DB.Types.WaterProtectionArea water_intake = new WaterProtectionArea();
            //EGH01DB.Types.MapType.GetWaterIntake(mapPoint, db, out water_intake);




            float waterdeep = 0.0f;
            //EGH01DB.Types.MapType.GetWaterdeep(mapPoint, db, out waterdeep);



            float height = 0.0f;
            //EGH01DB.Types.MapType.GetHeight(mapPoint, db, out height);




            EGH01DB.Types.SoilType soilType = new SoilType();
            //EGH01DB.Types.MapType.GetSoilType(mapPoint, db, out soilType);




            float time_migration = 0.0f;
            //EGH01DB.Types.MapType.GetTimeMigration(mapPoint, db, out time_migration);




            EGH01DB.Objects.EcoObject ecoW = new EGH01DB.Objects.EcoObject();
            //EGH01DB.Types.MapType.GetWaterObject(mapPoint, db, out ecoW);







            EGH01DB.Types.District district = new District();
            //EGH01DB.Types.MapType.GetDistrict(mapPoint, db, out district);


            string city = "";

            //EGH01DB.Types.MapType.GetCity(mapPoint, db, out city);

            if (!String.IsNullOrEmpty(city))
            {
                ViewData["city"] = city;
            }




            if (!String.IsNullOrEmpty(ecoW.name))
            {

                ViewData["ecoW"] = ecoW.name;
        }

            
            if (!String.IsNullOrEmpty(soilType.name))
            {

                ViewData["soilType"] = soilType.name;

            }

            //

            string self_cleaning_zone = "";
            //EGH01DB.Types.MapType.GetSelfCleaningZone(mapPoint, db, out self_cleaning_zone);





            EGH01DB.Objects.EcoObject eco = new EGH01DB.Objects.EcoObject();
           // EGH01DB.Types.MapType.GetEcoObject(mapPoint, db, out eco);
            if (!String.IsNullOrEmpty(eco.name))
            {

                ViewData["eco"] = eco.name;
            }


            ViewData["height"] = height;
            ViewData["district"] = district.name;
            ViewData["region"] = district.region.name;
            ViewData["self_cleaning_zone"] = self_cleaning_zone;
            ViewData["time_migration"] = time_migration;
            ViewData["water_intake"] = water_intake.name;
            ViewData["waterdeep"] = waterdeep;
            ViewData["waterfilter"] = ground.waterfilter; //
            ViewData["porosity"] = ground.porosity; //
            ViewData["watercapacity"] = ground.watercapacity; //

            var heights = new
            {
                Высота = height,
                Soil = soilType.name,
                Waterdeep = waterdeep,
                District = district.name,
                Region = district.region.name,
                Ground = ground.name,
                Self_cleaning_zone = self_cleaning_zone,
                Time_migration = time_migration,
                WaterProtectionArea = waterProtectionArea.name,
                Eco = eco.name,
                EcoW = ecoW.name,
                City = city,
                Water_intake = water_intake.name,
                Waterfilter = ground.waterfilter,
                Porosity = ground.porosity,
                Watercapacity = ground.watercapacity,


            };

            return Json(heights);
        }



    }
}
