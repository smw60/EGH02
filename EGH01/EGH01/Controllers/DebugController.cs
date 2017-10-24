using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01DB.Points;
using EGH01DB.Objects;
using EGH01DB;

namespace EGH01.Controllers
{
    public partial  class DebugController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XML()
        {
            {
                Coordinates coord = new Coordinates(53.891779f, 27.557892f);
                XmlNode xmlcoord = coord.toXmlNode("Координаты точки разлива");
                Coordinates coord1 = new Coordinates(xmlcoord);
            }
            {
                CoordinatesList clist = new CoordinatesList()
                {
                    new Coordinates(53.891779f, 27.557892f),
                    new Coordinates(53.881780f, 27.537890f),
                    new Coordinates(53.871781f, 27.547893f),
                };
                XmlNode xmllist = clist.toXmlNode();
                CoordinatesList clist1 = CoordinatesList.CreateCoordinatesList(xmllist);

            }
            {
                GroundType gt = new GroundType(1, "песок", 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);
                XmlNode xml = gt.toXmlNode("Тип грунта");
                GroundType gt1 = new GroundType(xml);
            }
            {
                Point p = new Point(new Coordinates(53.1000f, 27.2345f), new GroundType(2, "ground", 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f), 4, 200.0f);
                XmlNode xml = p.toXmlNode("test");
                Point p1 = new Point(xml);
            }

            {
                EGH01DB.RGEContext db = new EGH01DB.RGEContext();
                IncidentTypeList list = new IncidentTypeList(db);
                XmlNode n = list.toXmlNode();
                int k = 1;
            }

            return View();
        }

        // проверка процедур RehabilitationMethod
        public ActionResult RehabilitationMethod_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //int code_test = 1;
                //RiskObjectType r = new RiskObjectType(code_test);
                //CadastreType ct = new CadastreType(code_test);
                //PetrochemicalCategories pc = new PetrochemicalCategories(1);
                //EmergencyClass ec = new EmergencyClass(code_test);
                //PenetrationDepth pd = new PenetrationDepth(code_test);
                //SoilPollutionCategories sp = new SoilPollutionCategories(2);
                //bool waterachieved = true;
                //WaterPollutionCategories wp = new WaterPollutionCategories(code_test);
                //WaterProtectionArea wpa = new WaterProtectionArea(code_test);

                //SoilCleaningMethod sm = new SoilCleaningMethod(code_test);
                //WaterCleaningMethod wm = new WaterCleaningMethod(code_test);

                //RehabilitationMethod t = new RehabilitationMethod(code, r, ct, pc, ec, pd, sp, waterachieved, wp, wpa, sm, wm);
                //if (RehabilitationMethod.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult RehabilitationMethod_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //RehabilitationMethod r = new RehabilitationMethod();
                //if (RehabilitationMethod.DeleteByCode(db, 1))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult RehabilitationMethod_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //RehabilitationMethod r = new RehabilitationMethod();
                
                //if (RehabilitationMethod.GetByCode(db, 1, ref r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult RehabilitationMethod_Update()// y
        {
            RGEContext db = new RGEContext();
            {

                //int code = 1;
                //int code_test = 1;
                //RiskObjectType r = new RiskObjectType(code_test);
                //CadastreType ct = new CadastreType(code_test);
                //PetrochemicalCategories pc = new PetrochemicalCategories(1);
                //EmergencyClass ec = new EmergencyClass(code_test);
                //PenetrationDepth pd = new PenetrationDepth(code_test);
                //SoilPollutionCategories sp = new SoilPollutionCategories(3);
                //bool waterachieved = true;
                //WaterPollutionCategories wp = new WaterPollutionCategories(code_test);
                //WaterProtectionArea wpa = new WaterProtectionArea(code_test);

                //SoilCleaningMethod sm = new SoilCleaningMethod(code_test);
                //WaterCleaningMethod wm = new WaterCleaningMethod(code_test);

                //RehabilitationMethod t = new RehabilitationMethod(code, r, ct, pc, ec, pd, sp, waterachieved, wp, wpa, sm, wm);
                //if (RehabilitationMethod.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult RehabilitationMethod_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<RehabilitationMethod> list = new List<RehabilitationMethod>();
                //if (Helper.GetListRehabilitationMethod(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult RehabilitationMethod_GetByParm()// n
        {
            RGEContext db = new RGEContext();
            {
                
                //int code_test = 1;
                //RiskObjectType r = new RiskObjectType(code_test);
                //CadastreType ct = new CadastreType(code_test);
                //PetrochemicalCategories pc = new PetrochemicalCategories(1);
                //EmergencyClass ec = new EmergencyClass(code_test);
                //PenetrationDepth pd = new PenetrationDepth(code_test);
                //SoilPollutionCategories sp = new SoilPollutionCategories(2);
                
                //WaterPollutionCategories wp = new WaterPollutionCategories(code_test);
                //WaterProtectionArea wpa = new WaterProtectionArea(code_test);

              
                //RehabilitationMethod t = new RehabilitationMethod();
                //if (RehabilitationMethod.GetByParm(db, 1, 1, 1, 1, 1, 2, true, 1, 1, out t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        
        // проверка процедур  Report
        public ActionResult Report_GetById()// 
        {
            RGEContext db = new RGEContext();
            string comment = "Comment";
            Report f = new Report();
            if (Report.GetById(db, 9, out f, out comment))
            {
                int k = 1;

            };
            string gtm = f.ToHTML();
            return View();
        }
        public ActionResult Report_GetList()// 
        {
            //RGEContext db = new RGEContext();
            //List<Report> flist = new List<Report>();
            //if (Helper.GetListReport(db, ref flist))
            //{
            //    int k = 1;
            //};
            return View();
        }
        

        // проверка процедур PetrochemicalCategories
        public ActionResult PetrochemicalCategories_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //string name = "Test111";
                //float min = -1.1f;
                //float max = 1111.1f;
                //PetrochemicalCategories t = new PetrochemicalCategories(code, name, min, max);
                //if (PetrochemicalCategories.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PetrochemicalCategories_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //PetrochemicalCategories r = new PetrochemicalCategories();
                //if (PetrochemicalCategories.DeleteByCode(db, 1))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PetrochemicalCategories_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //PetrochemicalCategories r = new PetrochemicalCategories(1);
                //if (PetrochemicalCategories.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PetrochemicalCategories_Update()// y
        {
            RGEContext db = new RGEContext();
            {

                //int code = 1;
                //string name = "Test111222";
                //float min = 11.1f;
                //float max = 1111111.1f;

                //PetrochemicalCategories t = new PetrochemicalCategories((int)code, (string)name, min, max);

                //if (PetrochemicalCategories.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PetrochemicalCategories_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<PetrochemicalCategories> list = new List<PetrochemicalCategories>();
                //if (Helper.GetListPetrochemicalCategories(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }


        // проверка процедур  PenetrationDepth
        public ActionResult PenetrationDepth_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //string name = "Test111";
                //float min = -1.1f;
                //float max = 1111.1f;
                //PenetrationDepth t = new PenetrationDepth(code, name, min, max);
                //if (PenetrationDepth.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PenetrationDepth_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //PenetrationDepth r = new PenetrationDepth();
                //if (PenetrationDepth.DeleteByCode(db, 1))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PenetrationDepth_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //PenetrationDepth r = new PenetrationDepth(1);
                //if (PenetrationDepth.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PenetrationDepth_Update()// y
        {
            RGEContext db = new RGEContext();
            {

                //int code = 1;
                //string name = "Test111222";
                //float min = 11.1f;
                //float max = 1111111.1f;

                //PenetrationDepth t = new PenetrationDepth((int)code, (string)name, min, max);

                //if (PenetrationDepth.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PenetrationDepth_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<PenetrationDepth> list = new List<PenetrationDepth>();
                //if (Helper.GetListPenetrationDepth(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult PenetrationDepth_GetByDepth()// y
        {
            RGEContext db = new RGEContext();
            {
                //PenetrationDepth r = new PenetrationDepth(1);
                //if (PenetrationDepth.GetByDepth(db, 90.0f, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур  EmergencyClass
        public ActionResult EmergencyClass_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //string name = "Test111";
                //float min = -1.1f;
                //float max = 1111.1f;
                //EmergencyClass t = new EmergencyClass(code, name, min, max);
                //if (EmergencyClass.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EmergencyClass_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //EmergencyClass r = new EmergencyClass();
                //if (EmergencyClass.DeleteByCode(db, 1))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EmergencyClass_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //EmergencyClass r = new EmergencyClass(1);
                //if (EmergencyClass.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EmergencyClass_Update()// y
        {
            RGEContext db = new RGEContext();
            {

                //int code = 1;
                //string name = "Test111222";
                //float min = 11.1f;
                //float max = 1111111.1f;

                //EmergencyClass t = new EmergencyClass((int)code, (string)name, min, max);

                //if (EmergencyClass.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EmergencyClass_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<EmergencyClass> list = new List<EmergencyClass>();
                //if (Helper.GetListEmergencyClass(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EmergencyClass_GetByMass()// y
        {
            RGEContext db = new RGEContext();
            {
                //EmergencyClass r = new EmergencyClass(1);
                //if (EmergencyClass.GetByMass(db, 10.0f, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }


        // проверка процедур  WaterPollutionCategories
        public ActionResult WaterPollutionCategories_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //string name = "Test111";
                //float min = -1.1f;
                //float max = 1111.1f;
                //CadastreType cad = new CadastreType(1);
                //WaterPollutionCategories t = new WaterPollutionCategories(code, name, min, max, cad);
                //if (WaterPollutionCategories.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterPollutionCategories_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //WaterPollutionCategories r = new WaterPollutionCategories();
                //if (WaterPollutionCategories.DeleteByCode(db, 4))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterPollutionCategories_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //WaterPollutionCategories r = new WaterPollutionCategories(1);
                //if (WaterPollutionCategories.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterPollutionCategories_Update()// y
        {
            RGEContext db = new RGEContext();
            {

                //int code = 1;
                //string name = "Test111222";
                //float min = 11.1f;
                //float max = 1111111.1f;

                //WaterPollutionCategories t = new WaterPollutionCategories((int)code, (string)name, min, max);

                //if (WaterPollutionCategories.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterPollutionCategories_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<WaterPollutionCategories> list = new List<WaterPollutionCategories>();
                
                //if (Helper.GetListWaterPollutionCategories(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterPollutionCategories_GetByMult_Cadastre() // y
        {
            //RGEContext db = new RGEContext();
            //{
            //    WaterPollutionCategories r = new WaterPollutionCategories(1);

            //    if (WaterPollutionCategories.GetByMult_Cadastre(db, 200, 1, out r))
            //    {
            //        int k = 1;
            //    };
            //}
            return View();
        }
        
        // проверка процедур  SoilPollutionCategories
        public ActionResult SoilPollutionCategories_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //string name = "Test111";
                //float min = -1.1f;
                //float max = 1111.1f;
                //CadastreType cad = new CadastreType(1);
                //SoilPollutionCategories t = new SoilPollutionCategories(code, name, min, max, cad);
                //if (SoilPollutionCategories.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilPollutionCategories_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //SoilPollutionCategories r = new SoilPollutionCategories();

                //if (SoilPollutionCategories.DeleteByCode(db, 1))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilPollutionCategories_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //SoilPollutionCategories r = new SoilPollutionCategories(1);
                //if (SoilPollutionCategories.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilPollutionCategories_Update()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = 1;
                //string name = "Test111222";
                //float min = 11.1f;
                //float max = 1111111.1f;

                //SoilPollutionCategories t = new SoilPollutionCategories((int)code, (string)name, min, max);

                //if (SoilPollutionCategories.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilPollutionCategories_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<SoilPollutionCategories> list = new List<SoilPollutionCategories>();
                
                //if (Helper.GetListSoilPollutionCategories(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilPollutionCategories_Volume_Cadastre() // y
        {
            RGEContext db = new RGEContext();
            {
                ////SoilPollutionCategories r = new SoilPollutionCategories();

                ////if (SoilPollutionCategories.GetByVolume_Cadastre(db, 4.0f, 1, out r))
                ////{
                ////    int k = 1;
                ////};
            }
            return View();
        }

        // проверка процедур  SoilCleaningMethod
        public ActionResult SoilCleaningMethod_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //string name = "Test111";
                //string method = "Test method";
                //SoilCleaningMethod t = new SoilCleaningMethod(code, name, method);
                //if (SoilCleaningMethod.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilCleaningMethodn_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //SoilCleaningMethod r = new SoilCleaningMethod();
                //if (SoilCleaningMethod.DeleteByCode(db, 1))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilCleaningMethod_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //SoilCleaningMethod r = new SoilCleaningMethod(1);
                //if (SoilCleaningMethod.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilCleaningMethod_Update()// y
        {
            RGEContext db = new RGEContext();
            {

                //int code = 1;
                //string name = "Test111222";
                //string method = "Test111222";
                //SoilCleaningMethod t = new SoilCleaningMethod((int)code, (string)name, method);
                
                //if (SoilCleaningMethod.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SoilCleaningMethod_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<SoilCleaningMethod> list = new List<SoilCleaningMethod>();
                //if (Helper.GetListSoilCleaningMethods(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур  WaterCleaningMethod
        public ActionResult WaterCleaningMethod_Create()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = -1;
                //string name = "Test111";
                //string method = "Test method";
                //WaterCleaningMethod t = new WaterCleaningMethod(code, name, method);
                //if (WaterCleaningMethod.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterCleaningMethod_DeleteByCode()  // y
        {
            RGEContext db = new RGEContext();
            {
                //WaterCleaningMethod r = new WaterCleaningMethod();
                //if (WaterCleaningMethod.DeleteByCode(db, 1))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterCleaningMethod_GetByCode()// y
        {
            RGEContext db = new RGEContext();
            {
                //WaterCleaningMethod r = new WaterCleaningMethod(1);
                //if (WaterCleaningMethod.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterCleaningMethod_Update()// y
        {
            RGEContext db = new RGEContext();
            {
                //int code = 1;
                //string name = "Test111222";
                //string method = "Test111222";
                //WaterCleaningMethod t = new WaterCleaningMethod((int)code, (string)name, method);

                //if (WaterCleaningMethod.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult WaterCleaningMethod_list() // y
        {
            RGEContext db = new RGEContext();
            {
                //List<WaterCleaningMethod> list = new List<WaterCleaningMethod>();
                //if (Helper.GetListWaterCleaningMethods(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур  XML
        public ActionResult XML_District_Region()
        {
            {
                //Region reg = new Region(2, "Витебская");
                //XmlNode xmlreg = reg.toXmlNode("Область");
                //Region reg1 = new Region(xmlreg);
            
                //District district = new District(1, reg, "Витебский");
                //XmlNode xml = district.toXmlNode("test");
                //District district1 = new District(xml);
            }
            return View();
        }
        public ActionResult XML_AnchorPoint()
        {
            {
                //int id = 1;
                //CadastreType cad = new CadastreType(2, "Населенных пунктов, садоводческих товариществ, дач", 127, 0.3f);
                //Point p = new Point(new Coordinates(53.1000f, 27.2345f), new GroundType(3, "ground", 120f, 2.7f, 3.8f, 4.0f, 5.7f, 6.2f), 4, 200.0f);
  
                //AnchorPoint ap = new AnchorPoint(id, p, cad);
                //XmlNode xml_ap = ap.toXmlNode("Опорная точка");

                //AnchorPoint ap_from_xml = new AnchorPoint(xml_ap);
            }
            return View();
        }
        public ActionResult XML_RiskObject()
        {
            RGEContext db = new RGEContext();
            {
                //RiskObject ro = new RiskObject();
                //RiskObject.GetById(db, 4, ref ro);
                //XmlNode xml_ro = ro.toXmlNode("Техногенный объект");

                //RiskObject ro_from_xml = new RiskObject(xml_ro);
            }
            return View();
        }
        public ActionResult XML_EcoObject()
        {
            RGEContext db = new RGEContext();
            {
                //EcoObject eco = new EcoObject();
                //EcoObject.GetById(db, 9, ref eco);
                //XmlNode xml_eco = eco.toXmlNode("Природоохранный объект");

                //EcoObject eco_from_xml = new EcoObject(xml_eco);
            }
            return View();
        }
        public ActionResult XML_CadastreType()
        {
            {
                //CadastreType cad = new CadastreType(2, "Населенных пунктов, садоводческих товариществ, дач", 127, 0.3f);
                //XmlNode xmlcad = cad.toXmlNode("Кадастр");
                //CadastreType cad1 = new CadastreType(xmlcad);
                
            }
            return View();
        }
        public ActionResult XML_PetrochemicalType()
        {
            {
                //PetrochemicalType pc = new PetrochemicalType(2, "Сырая нефть", 129.4f, 12.3f, 15.6f, 78.2f, 450.2f, 12.3f, 2.4f);
                //XmlNode xmlpc = pc.toXmlNode("Petrochemical Type");
                //PetrochemicalType pc1 = new PetrochemicalType(xmlpc);
            }
            return View();
        }
        public ActionResult XML_RiskObjectType()
        {
            {
                //RiskObjectType ro = new RiskObjectType(2, "Оборудование нефтеперерабатывающих предприятий");
                //XmlNode xmlro = ro.toXmlNode("Risk Object Type");
                //RiskObjectType ro1 = new RiskObjectType(xmlro);
            }
            return View();
        }
        public ActionResult XML_EcoObjectType()
        {
            {
                //EcoObjectType eo = new EcoObjectType(2, "Заповедник");
                //XmlNode xmleo = eo.toXmlNode("Eco Object Type");
                //EcoObjectType eo1 = new EcoObjectType(xmleo);
            }
            return View();
        }
        public ActionResult XML_WaterProperties()
        {
            {
                //WaterProperties wp = new WaterProperties (12, 70.0f, 09.0f, 80.0f, 70.0f);
                //XmlNode xml_wp = wp.toXmlNode("Water Properties");
                //WaterProperties wp1 = new WaterProperties(xml_wp);
            }
            return View();
        }
        public ActionResult XML_RehabilitationMethod()
        {
            RGEContext db = new RGEContext();
            {
                //RehabilitationMethod wp = new RehabilitationMethod();

                //RehabilitationMethod.GetByCode(db, 1, out wp);
                //XmlNode xml_wp = wp.toXmlNode("Rehabilitation Method");
                //RehabilitationMethod wp1 = new RehabilitationMethod(xml_wp);
            }
            return View();
        }

        // проверка процедур  Ecoforecast
        public ActionResult EF_GetById()// 
        {
            //RGEContext db = new RGEContext();
            //string comment = "Comment";
            //RGEContext.ECOForecast f = new RGEContext.ECOForecast();
            //if (RGEContext.ECOForecast.GetById(db, 35, out f, out comment))
            //{
            //    int k = 1;
            //};
            return View();
        }
        public ActionResult EF_GetList()// 
        {
            //RGEContext db = new RGEContext();  
            //List <RGEContext.ECOForecast> flist = new List<RGEContext.ECOForecast>();
            //if (Helper.GetListEcoforecast(db, ref flist))
            //{
            //    int k = 1;
            //};
            return View();
        }
        public ActionResult EF_DeleteById()// 
        {
            //RGEContext db = new RGEContext();
            //RGEContext.ECOForecast f = new RGEContext.ECOForecast();
            //if (RGEContext.ECOForecast.DeleteById(db, 2))
            //{
            //    int k = 1;
            //};
            return View();
        }
        public ActionResult EF_UpdateCommentById()// 
        {
            //RGEContext db = new RGEContext();
            //string comment = "New comment";
            //RGEContext.ECOForecast f = new RGEContext.ECOForecast();
            //if (RGEContext.ECOForecast.UpdateCommentById(db, 35, comment))
            //{
            //    int k = 1;
            //};
            return View();
        }

        // проверка процедур  Water Properties
        public ActionResult Water_Properties_list() // yes
        {
            RGEContext db = new RGEContext();
            {
                //List<WaterProperties> list = new List<WaterProperties>();
                //if (Helper.GetListWaterProperties(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Water_Properties_Obj() // yes
        {
            RGEContext db = new RGEContext();
            {
                //WaterProperties wp = new WaterProperties();
                //if (WaterProperties.GetByCode(db, 4, out wp))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Water_Properties_DeleteByCode() // yes
        {
            RGEContext db = new RGEContext();
            {
                //if (WaterProperties.DeleteByCode(db, 10)) // удалена
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Water_Properties_Create()// yes
        {
            RGEContext db = new RGEContext();
            {
                //int id = 0;
                //WaterProperties ap = new WaterProperties(id, 80.0f, 13.7f, 55.7f, 90.9f);
                //if (WaterProperties.Create(db, ap))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Water_Properties_Update() // yes
        {
            RGEContext db = new RGEContext();
            {

                //int id = 12;
                //WaterProperties ap = new WaterProperties(id, 90.0f, 67.9f, 78.0f, 66.6f);
                //if (WaterProperties.Update(db, ap))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Water_Properties_By_Temp() // yes
        {
            RGEContext db = new RGEContext();
            {
                //float temp = 64.0f;
                //float delta = 0.0f;
                //WaterProperties wp = new WaterProperties();
                //if (WaterProperties.Get(db, temp, out wp, out delta))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур  EcoObject
        public ActionResult EcoObject_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<EcoObject> list = new List<EcoObject>();
                //if (Helper.GetListEcoObject(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObject_Obj() // есть
        {
            RGEContext db = new RGEContext();
            {
                //EcoObject ap = new EcoObject();
                //if (EcoObject.GetById(db, 8, ref ap))
                //{

                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObject_DeleteById() // есть
        {
            RGEContext db = new RGEContext();
            {
                //if (EcoObject.DeleteById(db, 21)) // удалена
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObject_Create()// есть
        {
            RGEContext db = new RGEContext();
            {
                //int id = 0;
                //Point point = new Point(new Coordinates(53.53f, 27.27f), new GroundType(1), 1.0f, 20.0f);
                //CadastreType cadastretype = new CadastreType(1);
                //EcoObjectType ecoobjecttype = new EcoObjectType(1);
                //EcoObject ap = new EcoObject(id, point, ecoobjecttype, cadastretype, "test1", true);
                //if (EcoObject.Create(db, ap))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObject_Update() // есть
        {
            RGEContext db = new RGEContext();
            {

                //int id = 21;
                //Point point = new Point(new Coordinates(53.53f, 27.27f), new GroundType(1), 150.0f, 20.0f);
                //CadastreType cad_type = new CadastreType(1);
                //EcoObjectType etype = new EcoObjectType(1);
                //EcoObject ecoobj = new EcoObject(21, point, etype, cad_type, "new", false);
                //if (EcoObject.Update(db, ecoobj))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObject_CreateNear() // n
        {
            RGEContext db = new RGEContext();
            {
                //Coordinates coord = new Coordinates(553.678f, 27.14f);
                //float distance = 10000.0f;
                //AnchorPointList apl = AnchorPointList.CreateNear(coord, distance);
            }
            return View();
        }
        
        // проверка процедур  Anchor point
        public ActionResult Anchor_Point_list() // yes
        {
            RGEContext db = new RGEContext();
            {
                //List<AnchorPoint> list = new List<AnchorPoint>();
                //if (Helper.GetListAnchorPoint(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Anchor_Point_Obj() // yes
        {
            RGEContext db = new RGEContext();
            {
                //AnchorPoint ap = new AnchorPoint();

                //if (AnchorPoint.GetById(db, 4, ref ap))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Anchor_Point_DeleteById() // yes
        {
            RGEContext db = new RGEContext();
            {
                //if (AnchorPoint.DeleteById(db, 3)) // удалена
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Anchor_Point_Create()// yes
        {
            RGEContext db = new RGEContext();
            {
                //int id = 0;
                //Point point = new Point(new Coordinates(53.53f, 27.27f), new GroundType(1), 1.0f, 20.0f);
                //CadastreType cadastretype = new CadastreType(1);
                //AnchorPoint ap = new AnchorPoint(id, point, cadastretype);
                //if (AnchorPoint.Create(db, ap))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Anchor_Point_Create_At_Distance()// перегрузка
        {
            RGEContext db = new RGEContext();
            {
                
                //AnchorPoint ap = new AnchorPoint();
                //if (AnchorPoint.GetById(db, 23, ref ap))
                //{
                //    int k = 1;
                //};
                //AnchorPoint new_anchor_point = new AnchorPoint();
                //if (AnchorPoint.Create(db, ap, 30, 1000))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Anchor_Point_Update() // yes
        {
            RGEContext db = new RGEContext();
            {

                //int id = 4;
                //Point point = new Point(new Coordinates(53.53f, 27.27f), new GroundType(1), 150.0f, 20.0f);
                //CadastreType cad = new CadastreType(1);

                //AnchorPoint ap = new AnchorPoint(id, point, cad);

                //if (AnchorPoint.Update(db, ap))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Anchor_Point_CreateNear() // yes
        {
            RGEContext db = new RGEContext();
            {
                //Coordinates coord = new Coordinates(553.678f, 27.14f);
                //float distance = 10000000.0f;
                //AnchorPointList apl = AnchorPointList.CreateNear(coord, distance);
            }
            return View();
        }
        
        // проверка процедур Create Near - по углу и расстоянию строить опорные точки для RiskObject и EcoObject
        public ActionResult Risk_Create_Near()// перегрузка
        {
            RGEContext db = new RGEContext();
            {

                //RiskObject risk_object = new RiskObject();
                //if (RiskObject.GetById(db, 16, ref risk_object))
                //{
                //    int k = 1;
                //};
                //AnchorPoint new_anchor_point = new AnchorPoint();
                //if (RiskObject.CreateNear(db, risk_object, 30, 1000, out new_anchor_point))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Eco_Create_Near()// перегрузка
        {
            RGEContext db = new RGEContext();
            {

                //EcoObject eco_object = new EcoObject();
                //if (EcoObject.GetById(db, 7, ref eco_object))
                //{
                //    int k = 1;
                //};
                //EcoObject new_eo = new EcoObject();
                //if (EcoObject.CreateNear(db, eco_object, 30, 1000, out new_eo))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур Spreading Coefficient
        public ActionResult SC_Create()// есть
        {
            RGEContext db = new RGEContext();
            {
                int t = 2;
                int code = 2;
                GroundType g = new GroundType(t);
                float v1 = 0.01f;
                float v2 = 50.0f;
                float a1 = 97.0f;
                float a2 = 100.0f;
                float k = 20.0f;
                PetrochemicalType p = new PetrochemicalType(2);
                SpreadingCoefficient sc = new SpreadingCoefficient(code, g, p,(float)v1, (float)v2, (float)a1, (float)a2, (float)k);

                if (SpreadingCoefficient.Create(db, sc))
                {
                    int k1 = 1;
                };
            }
            return View();
        }
        public ActionResult SC_Get()// есть!
        {
            RGEContext db = new RGEContext();
            {
                //GroundType gr = new GroundType(1);
                //SpreadingCoefficient sc = new SpreadingCoefficient(5, gr, (float)20f, (float)40f, (float)5f, (float)10f, (float)0.0f);
                //float koeff = SpreadingCoefficient.Get(db, sc);
                
            }
            return View();
        }
        public ActionResult SC_GetByData()// есть
        {
            RGEContext db = new RGEContext();
            {
                //GroundType gr = new GroundType(7);
                //PetrochemicalType pt = new PetrochemicalType(1);
                
                //float koeff = SpreadingCoefficient.GetByData(db, gr, pt, 100.0f, 20.0f);

            }
            return View();
        }
        public ActionResult SC_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<SpreadingCoefficient> list = new List<SpreadingCoefficient>();
                //if (Helper.GetListSpreadingCoefficient(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        } 
        public ActionResult SC_Update()// есть
        {
           RGEContext db = new RGEContext();
            {
                //GroundType gr = new GroundType(2);
                //SpreadingCoefficient sc = new SpreadingCoefficient(gr, (float)10.0f, (float)50.0f, (float)0.0f, (float)50.0f, (float)34.0f);
                //if (SpreadingCoefficient.Update(db, sc))
                //{
                //    int k1 = 1;
                //};
            }
            return View();
        }
        public ActionResult SC_Delete()  // есть
        {
            RGEContext db = new RGEContext();
            {
                //GroundType gr = new GroundType(2);
                //SpreadingCoefficient sc = new SpreadingCoefficient(gr, (float)10.0f, (float)50.0f, (float)0.0f, (float)50.0f, (float)0.0f);
                //if (SpreadingCoefficient.Delete(db, sc))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SC_DeleteByCode()  // есть
        {
            RGEContext db = new RGEContext();
            {
                //SpreadingCoefficient ct = new SpreadingCoefficient();
                //if (SpreadingCoefficient.DeleteByCode(db, 14))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult SC_GetByCode()// есть
        {
            //RGEContext db = new RGEContext();
            //{
            //    SpreadingCoefficient r = new SpreadingCoefficient(1);
            //    if (SpreadingCoefficient.GetByCode(db, 1, out r))
            //    {
            //        int k = 1;
            //    };
            //}

            return View();
        }

        // проверка процедур Region
        public ActionResult Region_Create()// есть
        {
            RGEContext db = new RGEContext();
            {


                //string name = "Test111";

                //Region t = new Region(name);
                //if (Region.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Region_DeleteByCode()  // есть
        {
            RGEContext db = new RGEContext();
            {
                //Region r = new Region();
                //if (Region.DeleteByCode(db, 8))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Region_GetByCode()// есть
        {
            RGEContext db = new RGEContext();
            {
                //Region r = new Region(1);
                //if (Region.GetByCode(db, 1, out r))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Region_Update()// есть
        {
            RGEContext db = new RGEContext();
            {

                //int region_code = 5;
                //string name = "Test111";

                //Region t = new Region((int)region_code, (string)name);
                //if (Region.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Region_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<Region> list = new List<Region>();
                //if (Helper.GetListRegion(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
       

        // проверка процедур District
        public ActionResult District_Create()// есть
        {
            RGEContext db = new RGEContext();
            {

                //int region_code = 5;
                //string name = "Test111";

                //District t = new District((int)region_code, (string)name);
                //if (District.Create(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult District_GetByCode()// есть
        {
            RGEContext db = new RGEContext();
            {
                //District ct = new District(1);
                //if (District.GetByCode(db, 5, out ct))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult District_DeleteByCode()  // есть
        {
            RGEContext db = new RGEContext();
            {
                //District ct = new District();
                //if (District.DeleteByCode(db, 128))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult District_Update()// 
        {
            RGEContext db = new RGEContext();
            {

                //int code = 104;
                //Region region = new Region(6);
                //string name = "Могилев";

                //District t = new District((int)code, region, (string)name);
                //if (District.Update(db, t))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult District_list() // 
        {
            RGEContext db = new RGEContext();
            {
                //List<District> list = new List<District>();
                //if (Helper.GetListDistrict(db, 2, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult District_listFull() // 
        {
            RGEContext db = new RGEContext();
            {
                List<District> list = new List<District>();
                if (Helper.GetListDistrict(db, ref list))
                {
                    int k = 1;
                };
            }
            return View();
        }


        // проверка процедур Risk Object, раскомментить нужные области,  перепроверить после внесения обновлений в процедуры
        public ActionResult Risk_Obj_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<RiskObject> list = new List<RiskObject>();
                //if (Helper.GetListRiskObject(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Risk_Obj() // есть
        {
            RGEContext db = new RGEContext();
            {
                //RiskObject rs = new RiskObject();
                //if (RiskObject.GetById(db, 4, ref rs))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Risk_Obj_DeleteById() // есть
        {
            RGEContext db = new RGEContext();
            {
                //RiskObject rs = new RiskObject();
                //if (RiskObject.DeleteById(db, 2)) // удалена
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Risk_Obj_Create()// есть
        {
            RGEContext db = new RGEContext();
            {
                //int id = 7;
                //Point point = new Point(new Coordinates(53.53f, 27.27f), new GroundType(1), 0.0f, 0.0f);
                //RiskObjectType type = new RiskObjectType(1);
                //CadastreType cad = new CadastreType(1);
                //Region region = new Region(2);
                //District district = new District(2);
                //DateTime date1 = DateTime.Now;
                //DateTime date2 = DateTime.Now;
                //byte[] map = new byte[0];

                //RiskObject rs = new RiskObject(id, point, type, cad, "new", district, region, "address1", "OOO nexttime", "375290000000", "375290000000", "r@33.com",date1, date2, 100, 40000, false, false, map, 200, 300, "fuel", 0,0.0f, 0.0f, "geo" );
                //if (RiskObject.Create(db, rs))
                //{
                    
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Risk_Obj_Update() // есть
        {
            RGEContext db = new RGEContext();
            {
                 //int id = 7;
                //Point point = new Point(new Coordinates(53.53f, 27.27f), new GroundType(1), 0.0f, 0.0f);
                //Region region = new Region(2);
                //District district = new District(2);
                //RiskObjectType type = new RiskObjectType(1);
                //CadastreType cad = new CadastreType(1);
                //DateTime date1 = DateTime.Now;
                //DateTime date2 = DateTime.Now;
                //byte[] map = new byte[0];
                //RiskObject rs = new RiskObject(id, point, type, cad, "new up", district, region, "address1", "OOO nexttime", "375290000000", "375290000000", "r@33.com",date1, date2, 100, 40000, false, false, map, 200, 300, "fuelfuel", 0,1.0f, 0.0f, "geo" );
                //if (RiskObject.Update(db, rs))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Risk_Obj_D() // есть
        {
            RGEContext db = new RGEContext();
            {
                //RiskObject.RiskObjectsList list = new RiskObject.RiskObjectsList();
                //Point point = new Point(new Coordinates(53.53f, 27.27f), new GroundType(1), 0.0f, 0.0f);
                //float distance = 54.0f;
                //if (RiskObject.RiskObjectsList.CreateRiskObjectsList(db, point, distance, ref list))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult Risk_Obj_D1() // есть, перегрузка, с двумя аргументами, > distance1 и <  distance2
        {
            RGEContext db = new RGEContext();
            {
                //RiskObject.RiskObjectsList list = new RiskObject.RiskObjectsList();
                //Point point = new Point(new Coordinates(3.7f, 5.6f), new GroundType(1), 0.0f, 0.0f);
                //float distance1 = 53.0f;
                //float distance2 = 55.0f;
                //if (RiskObject.RiskObjectsList.CreateRiskObjectsList(db, point, distance1, distance2, ref list))
                {
                    int k = 1;
                };
            }
            return View();
        }

        // проверка процедур Petrochemical Type
        public ActionResult Petr_GetByCode()// есть
        {
            RGEContext db = new RGEContext();
            {
                //PetrochemicalType pt = new PetrochemicalType();
                //if (PetrochemicalType.GetByCode(db, 7, ref pt))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Petr_GetNextCode() // есть
        {
            RGEContext db = new RGEContext();
            {
                //PetrochemicalType pt = new PetrochemicalType();
                //int k1 = 0;
                //if (PetrochemicalType.GetNextCode(db, out k1))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Petr_DeleteByCode()  // есть
        {
            RGEContext db = new RGEContext();
            {
                //PetrochemicalType pt = new PetrochemicalType();
                //if (PetrochemicalType.DeleteByCode(db, 10))  // удален
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Petr_Create()// есть
        {
            RGEContext db = new RGEContext();
            {

                //int code_type = 7;
                //string name = "test";
                //float boilingtemp = 100.0f;
                //float density = 30.0f;
                //float viscosity = 50.0f;
                //float solubility = 3.0f;
                //float tension = 12.4f;
                //float dynamicviscosity = 0.8f;
                //float diffusion = 16.2f;
                //PetrochemicalType pt = new PetrochemicalType((int)code_type, (string)name,
                //    (float)boilingtemp, (float)density, (float)viscosity,
                //    (float)solubility, (float)tension, (float)dynamicviscosity, (float)diffusion);
                //if (PetrochemicalType.Create(db, pt))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Petr_Update()// есть
        {
            RGEContext db = new RGEContext();
            {
                //int code_type = 10;
                //string name = "test1";
                //float boilingtemp = 110.0f;
                //float density = 35.0f;
                //float viscosity = 50.0f;
                //float solubility = 3.000001f;
                //float tension = 12.4034f;
                //float dynamicviscosity = 0.87868453f;
                //float diffusion = 16.23332221f;

                //PetrochemicalType pt = new PetrochemicalType((int)code_type, (string)name,
                //                                (float)boilingtemp, (float)density, (float)viscosity, (float)solubility,
                //                                (float)tension, (float)dynamicviscosity, (float)diffusion);
                //if (PetrochemicalType.Update(db, pt))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Petr_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<PetrochemicalType> list = new List<PetrochemicalType>();
                //if (Helper.GetListPetrochemicalType(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур Ground Type
        public ActionResult Ground_GetByCode()// есть
        {
            RGEContext db = new RGEContext();
            {
                //int type_code = 2;
                //string name = "test";
                //float porosity = 0.1f;//  от нуля до единицы, не включая, в базе есть check!
                //float holdmigration = 0.2f;
                //float waterfilter = 23.0f;
                //float diffusion = 1.0f;
                //float distribution = 0.1f;
                //float sorption = 0.2f;
                //GroundType gt = new GroundType(type_code, name, porosity, holdmigration, waterfilter, diffusion, distribution, sorption);
                //if (GroundType.GetByCode(db, 1, out gt))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Ground_GetNextCode() // есть
        {
            RGEContext db = new RGEContext();
            {
                //GroundType gt = new GroundType();
                //int k1 = 0;
                //if (GroundType.GetNextCode(db, out k1))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult Ground_DeleteByCode()  // 
        {
            RGEContext db = new RGEContext();
            {
                //GroundType gt = new GroundType();
                //if (GroundType.DeleteByCode(db, 2))  // 
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult Ground_Create()// есть
        {
            RGEContext db = new RGEContext();
            {

                //int type_code = 2;
                //string name = "Глинистый";
                //float porosity = 0.8f;
                //float holdmigration = 30.0f;
                //float waterfilter = 50.0f;
                //float diffusion = 3.0f;
                //float distribution = 3.0f;
                //float sorption = 30.0f;

                //float watercapacity = 50.0f;
                //float soilmoisture = 3.0f;
                //float аveryanovfactor = 3.0f;
                //float permeability = 30.0f;

                //GroundType gt = new GroundType((int)type_code,
                //                                (string)name,
                //                                (float)porosity,
                //                                (float)holdmigration,
                //                                (float)waterfilter,
                //                                (float)diffusion,
                //                                (float)distribution,
                //                                (float)sorption,
                //                                (float)watercapacity,
                //                                (float)soilmoisture,
                //                                (float)аveryanovfactor,
                //                                (float)permeability);
                //if (GroundType.Create(db, gt))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult Ground_Update() // есть
        {
            RGEContext db = new RGEContext();
            {
                //int type_code = 2;
                //string name = "Глинистый";
                //double porosity = 0.6f;
                //double holdmigration = 30.0f;
                //double waterfilter = 50.0f;
                //double diffusion = 3.1f;
                //double distribution = 3.0f;
                //double sorption = 30.6f;

                //float watercapacity = 20.0f;
                //float soilmoisture = 173.0f;
                //float аveryanovfactor = 23.0f;
                //float permeability = 300.0f;

                //GroundType gt = new GroundType((int)type_code,
                //                                (string)name,
                //                                (float)porosity,
                //                                (float)holdmigration,
                //                                (float)waterfilter,
                //                                (float)diffusion,
                //                                (float)distribution,
                //                                (float)sorption,
                //                                (float)watercapacity,
                //                                (float)soilmoisture,
                //                                (float)аveryanovfactor,
                //                                (float)permeability);
                //if (GroundType.Update(db, gt))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Ground_Type_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<GroundType> list = new List<GroundType>();
                
                //if (Helper.GetListGroundType(db, ref list))    
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур Cadastre Type        
        public ActionResult Cadastre_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<CadastreType> list = new List<CadastreType>();
                //if (Helper.GetListCadastreType(db, ref list))
                //{

                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Cadastre_GetByCode()// есть
        {
            RGEContext db = new RGEContext();
            {
                CadastreType ct = new CadastreType(1);

                //if (CadastreType.GetByCode(db, 1, out ct))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Cadastre_GetNextCode() // есть
        {
            RGEContext db = new RGEContext();
            {
                //CadastreType ct = new CadastreType();
                //int k1 = 0;
                //if (CadastreType.GetNextCode(db, out k1))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult Cadastre_DeleteByCode()  // 
        {
            RGEContext db = new RGEContext();
            {
                //CadastreType ct = new CadastreType();
                //if (CadastreType.DeleteByCode(db, 13))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Cadastre_Create()// есть
        {
            RGEContext db = new RGEContext();
            {

                //int type_code = 6;
                //string name = "Воздушных путей сообщения";
                //int pdk = 30;
                //CadastreType ct = new CadastreType((int)type_code, (string)name, (int)pdk, 0.3f);
                //if (CadastreType.Create(db, ct))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult Cadastre_Update() // есть
        {
            RGEContext db = new RGEContext();
            {
                //int type_code = 8;
                //string name = "Железнодорожных путей сообщения";
                //float pdk = 140.0f;
                //float pdk_w = 0.03f;
                //CadastreType ct = new CadastreType((int)type_code, (string)name, pdk, pdk_w, "999", "999");
                //if (CadastreType.Update(db, ct))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }

        // проверка процедур Risk Object Type
        public ActionResult RiskObjectType_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<RiskObjectType> list = new List<RiskObjectType>();
                //if (Helper.GetListRiskObjectType(db, ref list))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult RiskObjectType_GetByCode()// есть
        {
            RGEContext db = new RGEContext();
            {
                //RiskObjectType ct = new RiskObjectType(1);
                //if (RiskObjectType.GetByCode(db, 1, out ct))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult RiskObjectType_GetNextCode() // есть
        {
            RGEContext db = new RGEContext();
            {
                //RiskObjectType ct = new RiskObjectType();
                //int k1 = 0;
                //if (RiskObjectType.GetNextCode(db, out k1))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult RiskObjectType_DeleteByCode()  // 
        {
            RGEContext db = new RGEContext();
            {
                //RiskObjectType ct = new RiskObjectType();
                //if (RiskObjectType.DeleteByCode(db, 4))  // 
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult RiskObjectType_Create()// есть
        {
            RGEContext db = new RGEContext();
            {

                //int type_code = 1;
                //string name = "Газопровод";
                //RiskObjectType ct = new RiskObjectType((int)type_code, (string)name);
                //if (RiskObjectType.Create(db, ct))
                {
                    int k = 1;
                };
            }
            return View();
        }
        public ActionResult RiskObjectType_Update() // есть
        {
            RGEContext db = new RGEContext();
            {
                //int type_code = 1;
                //string name = "Газопровод";
                //RiskObjectType ct = new RiskObjectType((int)type_code, (string)name);
                //if (RiskObjectType.Update(db, ct))
                {
                    int k = 1;
                };
            }
            return View();
        }

        // проверка процедур EcoObject Type
        public ActionResult EcoObjectType_list() // есть
        {
            RGEContext db = new RGEContext();
            {
                //List<EcoObjectType> list = new List<EcoObjectType>();
                //if (Helper.GetListEcoObjectType(db, ref list))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObjectType_GetByCode()//есть
        {
            RGEContext db = new RGEContext();
            {
                //EcoObjectType ct = new EcoObjectType();
                //if (EcoObjectType.GetByCode(db, 3, out ct))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObjectType_DeleteByCode()  // есть
        {
            RGEContext db = new RGEContext();
            {
                //EcoObjectType ct = new EcoObjectType();
                //if (EcoObjectType.DeleteByCode(db, 4))  // 
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObjectType_Create()// есть
        {
            RGEContext db = new RGEContext();
            {

                //int type_code = 1;
                //string name = "Заповедник1";
                //WaterProtectionArea wpa = new WaterProtectionArea(1);
                //EcoObjectType ct = new EcoObjectType((int)type_code, (string)name, wpa);
                //if (EcoObjectType.Create(db, ct))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
        public ActionResult EcoObjectType_Update() // есть
        {
            RGEContext db = new RGEContext();
            {
                //int type_code = 1;
                //string name = "Озеро";
                //EcoObjectType ct = new EcoObjectType((int)type_code, (string)name);
                //if (EcoObjectType.Update(db, ct))
                //{
                //    int k = 1;
                //};
            }
            return View();
        }
       
        
        // проверка процедур Incident Type
        public ActionResult IncidentType_list() // 
        {
            RGEContext db = new RGEContext();
            {
                //List<IncidentType> list = new List<IncidentType>();
                //if (Helper.GetListIncidentType(db, ref list))
                {
                    int k = 1;
                }
            }
            return View();
        }
        public ActionResult IncidentType_GetByCode()// 
        {
            RGEContext db = new RGEContext();
            {

                //    IncidentType t = new IncidentType();
                //    if (IncidentType.GetByCode(db, 2,  out t))
                //    {
                //        int r = 1;
                //    }
            }
            return View();
        }
        public ActionResult IncidentType_GetNextCode() // 
        {
            RGEContext db = new RGEContext();
            {
                //    int k1;
                //    if (IncidentType.GetNextCode(db, out k1))
                //    {
                //        int r = 1;
                //    }
            }
            return View();
        }
        public ActionResult IncidentType_DeleteByCode()  // 
        {
            RGEContext db = new RGEContext();
            {
                //    if (IncidentType.DeleteByCode(db, 5))
                //    {
                //        int r = 1;
                //    }
            }
            return View();
        }
        public ActionResult IncidentType_Create() // 
        {
            RGEContext db = new RGEContext();
            {
                //IncidentType inc_type = new IncidentType(7, "Отладка");
                //if (IncidentType.Create(db, inc_type))
                //{
                //    int k = 1;
                //}
            }
            return View();
        }
        public ActionResult IncidentType_Update() // 
        {
            RGEContext db = new RGEContext();
            {
                //    if (IncidentType.Update(db, new IncidentType(7, "yyy")))
                //    {
                //        int r = 1;
                //    }
            }
            return View();
        }
    }
}