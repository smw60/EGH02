using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using EGH01DB.Types;
using EGH01DB.Objects;
using EGH01DB.Points;
using EGH01DB.Primitives;
using EGH01DB.Blurs;

namespace EGH01DB.Primitives
{
    public partial class Helper
    {

        static public bool GetListIncidentType(EGH01DB.IDBContext dbcontext, ref List<IncidentType> list_type)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetIncidentTypeList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_type = new List<IncidentType>();
                    while (reader.Read())
                    {
                        list_type.Add(new IncidentType((int)reader["КодТипа"], (string)reader["Наименование"]));
                    }
                    rc = list_type.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public IncidentTypeList GetListIncidentType(EGH01DB.IDBContext dbcontext)
        {
            List<IncidentType> list = new List<IncidentType>();
            IncidentTypeList rc = new IncidentTypeList(list);
            if (Helper.GetListIncidentType(dbcontext, ref list)) 
            {
                rc = new IncidentTypeList(list);
            }
            return rc;
        }
        static public EGH01DB.Objects.RiskObjectsList GetListRiskObject(EGH01DB.IDBContext dbcontext)
        {
            List<RiskObject> list = new List<RiskObject>();
            EGH01DB.Objects.RiskObjectsList rc = new EGH01DB.Objects.RiskObjectsList(list);
            if (Helper.GetListRiskObject(dbcontext, ref list))
            {
                rc = new EGH01DB.Objects.RiskObjectsList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.PetrochemicalTypeList GetListPetrochemicalType(EGH01DB.IDBContext dbcontext)
        {
            List<PetrochemicalType> list = new List<PetrochemicalType>();
            EGH01DB.Types.PetrochemicalTypeList rc = new EGH01DB.Types.PetrochemicalTypeList(list);
            if (Helper.GetListPetrochemicalType(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.PetrochemicalTypeList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.CadastreTypeList GetListCadastreType(EGH01DB.IDBContext dbcontext)
        {
            List<CadastreType> list = new List<CadastreType>();
            EGH01DB.Types.CadastreTypeList rc = new EGH01DB.Types.CadastreTypeList(list);
            if (Helper.GetListCadastreType(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.CadastreTypeList(list);
            }
            return rc;
        }
        //static public EGH01DB.Blurs.GroundPollutionList GetListGroundPollution(EGH01DB.IDBContext dbcontext)
        //{
        //    List<GroundPollution> list = new List<GroundPollution>();
        //    EGH01DB.Blurs.GroundPollutionList rc = new EGH01DB.Blurs.GroundPollutionList(list);
        //    if (Helper.GetListGroundPollution(dbcontext, ref list))
        //    {
        //        rc = new EGH01DB.Blurs.GroundPollutionList(list);
        //    }
        //    return rc;
        //}
        static public EGH01DB.Objects.EcoObjectsList GetListEcoObject(EGH01DB.IDBContext dbcontext)
        {
            List<EcoObject> list = new List<EcoObject>();
            EGH01DB.Objects.EcoObjectsList rc = new EGH01DB.Objects.EcoObjectsList(list);
            if (Helper.GetListEcoObject(dbcontext, ref list))
            {
                rc = new EGH01DB.Objects.EcoObjectsList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.EcoObjectTypeList GetListEcoObjectType(EGH01DB.IDBContext dbcontext)
        {
            List<EcoObjectType> list = new List<EcoObjectType>();
            EGH01DB.Types.EcoObjectTypeList rc = new EGH01DB.Types.EcoObjectTypeList(list);
            if (Helper.GetListEcoObjectType(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.EcoObjectTypeList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.GroundTypeList GetListGroundType(EGH01DB.IDBContext dbcontext)
        {
            List<GroundType> list = new List<GroundType>();
            EGH01DB.Types.GroundTypeList rc = new EGH01DB.Types.GroundTypeList(list);
            if (Helper.GetListGroundType(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.GroundTypeList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.SoilPollutionCategoriesList GetListSoilPollutionCategories(EGH01DB.IDBContext dbcontext)
        {
            List<SoilPollutionCategories> list = new List<SoilPollutionCategories>();
            EGH01DB.Types.SoilPollutionCategoriesList rc = new EGH01DB.Types.SoilPollutionCategoriesList(list);
            if (Helper.GetListSoilPollutionCategories(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.SoilPollutionCategoriesList(list);
            }
            return rc;
        }
        static public EGH01DB.Primitives.WaterPropertiesList GetListWaterProperties(EGH01DB.IDBContext dbcontext)
        {
            List<WaterProperties> list = new List<WaterProperties>();
            EGH01DB.Primitives.WaterPropertiesList rc = new EGH01DB.Primitives.WaterPropertiesList(list);
            if (Helper.GetListWaterProperties(dbcontext, ref list))
            {
                rc = new EGH01DB.Primitives.WaterPropertiesList(list);
            }
            return rc;
        }
        static public EGH01DB.Primitives.SpreadingCoefficientList GetListSpreadingCoefficient(EGH01DB.IDBContext dbcontext)
        {
            List<SpreadingCoefficient> list = new List<SpreadingCoefficient>();
            EGH01DB.Primitives.SpreadingCoefficientList rc = new EGH01DB.Primitives.SpreadingCoefficientList(list);
            if (Helper.GetListSpreadingCoefficient(dbcontext, ref list))
            {
                rc = new EGH01DB.Primitives.SpreadingCoefficientList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.RehabilitationMethodList GetListRehabilitationMethod(EGH01DB.IDBContext dbcontext)
        {
            List<RehabilitationMethod> list = new List<RehabilitationMethod>();
            EGH01DB.Types.RehabilitationMethodList rc = new EGH01DB.Types.RehabilitationMethodList(list);
            if (Helper.GetListRehabilitationMethod(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.RehabilitationMethodList(list);
            }
            return rc;
        }
        static public EGH01DB.Points.AnchorPointList GetListAnchorPoint(EGH01DB.IDBContext dbcontext)
        {
            List<AnchorPoint> list = new List<AnchorPoint>();
            EGH01DB.Points.AnchorPointList rc = new EGH01DB.Points.AnchorPointList(list);
            if (Helper.GetListAnchorPoint(dbcontext, ref list))
            {
                rc = new EGH01DB.Points.AnchorPointList(list);
            }
            return rc;
        }
   static public EGH01DB.Types.WaterProtectionAreaList GetListWaterProtectionArea(EGH01DB.IDBContext dbcontext)
        {
            List<WaterProtectionArea> list = new List<WaterProtectionArea>();
        EGH01DB.Types.WaterProtectionAreaList rc = new EGH01DB.Types.WaterProtectionAreaList(list);
            if (Helper.GetListWaterProtectionArea(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.WaterProtectionAreaList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.PenetrationDepthList GetListPenetrationDepth(EGH01DB.IDBContext dbcontext)
        {
            List<PenetrationDepth> list = new List<PenetrationDepth>();
            EGH01DB.Types.PenetrationDepthList rc = new EGH01DB.Types.PenetrationDepthList(list);
            if (Helper.GetListPenetrationDepth(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.PenetrationDepthList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.WaterPollutionCategoriesList GetListWaterPollutionCategories(EGH01DB.IDBContext dbcontext)
        {
            List<WaterPollutionCategories> list = new List<WaterPollutionCategories>();
            EGH01DB.Types.WaterPollutionCategoriesList rc = new EGH01DB.Types.WaterPollutionCategoriesList(list);
            if (Helper.GetListWaterPollutionCategories(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.WaterPollutionCategoriesList(list);
            }
            return rc;
        }

        static public EGH01DB.Types.SoilCleaningMethodList GetListSoilCleaningMethod(EGH01DB.IDBContext dbcontext)
        {
            List<SoilCleaningMethod> list = new List<SoilCleaningMethod>();
            EGH01DB.Types.SoilCleaningMethodList rc = new EGH01DB.Types.SoilCleaningMethodList(list);
            if (Helper.GetListSoilCleaningMethods(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.SoilCleaningMethodList(list);
            }
            return rc;
        }

        static public EGH01DB.Types.WaterCleaningMethodList GetListWaterCleaningMethod(EGH01DB.IDBContext dbcontext)
        {
            List<WaterCleaningMethod> list = new List<WaterCleaningMethod>();
            EGH01DB.Types.WaterCleaningMethodList rc = new EGH01DB.Types.WaterCleaningMethodList(list);
            if (Helper.GetListWaterCleaningMethods(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.WaterCleaningMethodList(list);
            }
            return rc;
        }
        static public EGH01DB.Types.EmergencyClassList GetListEmergencyClass(EGH01DB.IDBContext dbcontext)
        {
            List<EmergencyClass> list = new List<EmergencyClass>();
            EGH01DB.Types.EmergencyClassList rc = new EGH01DB.Types.EmergencyClassList(list);
            if (Helper.GetListEmergencyClass(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.EmergencyClassList(list);
            }
            return rc;
        }
        
        static public EGH01DB.Types.PetrochemicalCategoriesList GetListPetrochemicalCategories(EGH01DB.IDBContext dbcontext)
        {
            List<PetrochemicalCategories> list = new List<PetrochemicalCategories>();
            EGH01DB.Types.PetrochemicalCategoriesList rc = new EGH01DB.Types.PetrochemicalCategoriesList(list);
            if (Helper.GetListPetrochemicalCategories(dbcontext, ref list))
            {
                rc = new EGH01DB.Types.PetrochemicalCategoriesList(list);
            }
            return rc;
        }
        static public bool GetListGroundType(EGH01DB.IDBContext dbcontext, ref List<GroundType> list_type)
        { 
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetGroundTypeList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_type = new List<GroundType>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодТипаГрунта"];
                        string name = (string)reader["НаименованиеТипаГрунта"];
                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];

                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];

                        list_type.Add(new GroundType((int)code, 
                                                    (string)name, 
                                                    (float)porosity, 
                                                    (float)holdmigration, 
                                                    (float)waterfilter, 
                                                    (float)diffusion, 
                                                    (float)distribution, 
                                                    (float)sorption,
                                                    (float)watercapacity,
                                                    (float)soilmoisture,
                                                    (float)аveryanovfactor,
                                                    (float)permeability,
                                                    (float)density));
                    }
                    rc = list_type.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        
        }
        static public bool GetListCadastreType(EGH01DB.IDBContext dbcontext, ref List<CadastreType> list_type) 
        { 
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetLandRegistryTypeList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_type = new List<CadastreType>();
                    while (reader.Read())
                    {
                        list_type.Add(new CadastreType((int)reader["КодНазначенияЗемель"], 
                                                        (string)reader["НаименованиеНазначенияЗемель"], 
                                                        (float)reader["ПДК"],
                                                        (float)reader["ПДКВоды"],
                                                        (string)reader["НормДокументЗемля"],
                                                        (string)reader["НормДокументВода"]));
                    }
                    rc = list_type.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public bool GetListWaterProperties(EGH01DB.IDBContext dbcontext, ref List<WaterProperties> list)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetWaterPropertiesList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list = new List<WaterProperties>();
                    while (reader.Read())
                    {
                        list.Add(new WaterProperties((int)reader["КодПоказателяВоды"],
                                                        (float)reader["Температура"],
                                                        (float)reader["Вязкость"],
                                                        (float)reader["Плотность"],
                                                       (float)reader["КоэфПовНат"]));
                    }
                    rc = list.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public bool GetListEcoObjectType(EGH01DB.IDBContext dbcontext, ref List<EcoObjectType> list_type)
        { 
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetEcoObjectTypeList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    EcoObjectType eco_object_type = new EcoObjectType();
                    list_type = new List<EcoObjectType>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодТипаПриродоохранногоОбъекта"];
                        string name = (string)reader["НаименованиеТипаПриродоохранногоОбъекта"];
                        int? cat_water_name;
                        cat_water_name = (reader["КатегорияВодоохрТер"] == DBNull.Value) ? null : (int?)reader["КатегорияВодоохрТер"];
                        if (cat_water_name != null)
                        {
                            int category_code = (int)reader["КодТипаКатегории"];
                            string category_name = (string)reader["НаименованиеКатегории"];
                            WaterProtectionArea waterprotectionarea = new WaterProtectionArea(category_code, category_name);
                            eco_object_type = new EcoObjectType(code, name, waterprotectionarea);
                        }
                        else eco_object_type = new EcoObjectType(code, name, null);
                        list_type.Add(eco_object_type);
                    }
                    rc = list_type.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListPetrochemicalType(EGH01DB.IDBContext dbcontext, ref List<PetrochemicalType> list_type) 
        { 
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetPetrochemicalTypeList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_type = new List<PetrochemicalType>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодТипаНефтепродукта"];
                        string name = (string)reader["НаименованиеТипаНефтепродукта"];
                        float boilingtemp = (float)reader["ТемператураКипения"];
                        float density = (float)reader["Плотность"];
                        float viscosity = (float)reader["КинематическаяВязкость"];
                        float solubility = (float)reader["Растворимость"];
                        float tension = (float)reader["КоэфНатяжения"];
                        float dynamicviscosity = (float)reader["ДинамическаяВязкость"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        int petrochemicalcategories = (int)reader["КодКатегорииНефтепродукта"];
                        string petrochemicalcategoriesname = (string)reader["НаименованиеКатегорииНефтепродукта"];
                        PetrochemicalCategories petro_cat = new PetrochemicalCategories(petrochemicalcategories, petrochemicalcategoriesname);
                        list_type.Add(new PetrochemicalType((int)code, 
                                                            (string)name, 
                                                            (float)boilingtemp, 
                                                            (float)density, 
                                                            (float)viscosity, 
                                                            (float)solubility, 
                                                            (float)tension,
                                                            (float)dynamicviscosity,
                                                            (float)diffusion, petro_cat));
                    }
                    rc = list_type.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListRegion(EGH01DB.IDBContext dbcontext, ref List<Region> list_region)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetRegionList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_region = new List<Region>();
                    while (reader.Read())
                    {
                        list_region.Add(new Region((int)reader["КодОбласти"], (string)reader["Область"]));
                    }
                    rc = list_region.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public bool GetListDistrict(EGH01DB.IDBContext dbcontext, int region_code, ref List<District> list_district)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetDistrictList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@Область", SqlDbType.Int);
                    parm.Value = region_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_district = new List<District>();
                    while (reader.Read())
                    {
                        Region region = new Region((int)reader["КодОбласти"], (string)reader["Область"]);
                        District district = new District((int)reader["КодРайона"], region, (string)reader["Район"]);
                        list_district.Add(district);
                    }
                    rc = list_district.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public bool GetListDistrict(EGH01DB.IDBContext dbcontext, ref List<District> list_district)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetDistrictListFull", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_district = new List<District>();
                    while (reader.Read())
                    {
                        Region region = new Region((int)reader["КодОбласти"], (string)reader["Область"]);
                        District district = new District((int)reader["КодРайона"], region, (string)reader["Район"]);
                        list_district.Add(district);
                    }
                    rc = list_district.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public bool GetListRiskObjectType(EGH01DB.IDBContext dbcontext, ref List<RiskObjectType> list_type)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetRiskObjectTypeList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_type = new List<RiskObjectType>();
                    while (reader.Read())
                    {
                        list_type.Add(new RiskObjectType((int)reader["КодТипаТехногенногоОбъекта"],
                                                         (string)reader["НаименованиеТипаТехногенногоОбъекта"]));
                    }
                    rc = list_type.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListRiskObject(EGH01DB.IDBContext dbcontext, ref List<RiskObject> risk_objects)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetRiskObjectList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    risk_objects = new List<RiskObject>();
                    while (reader.Read())
                    {
                        int id = (int)reader["IdТехногенногоОбъекта"];
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        Coordinates coordinates = new Coordinates((float)x, (float)y);
                        string ground_type_name = (string)reader["НаименованиеТипаГрунта"];
                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];
                        GroundType ground_type = new GroundType((int)reader["ТипГрунта"],
                                                                    (string)ground_type_name,
                                                                    (float)porosity,
                                                                    (float)holdmigration,
                                                                    (float)waterfilter,
                                                                    (float)diffusion,
                                                                    (float)distribution,
                                                                    (float)sorption,
                                                                    (float)watercapacity,
                                                                    (float)soilmoisture,
                                                                    (float)аveryanovfactor,
                                                                    (float)permeability,
                                                                    (float)density);
                        float waterdeep = (float)reader["ГлубинаГрунтовыхВод"];
                        float height = (float)reader["ВысотаУровнемМоря"];

                        int district_code = (int)reader["РайонТехногенногоОбъекта"];
                        string district_name = (string)reader["Район"];

                        int region_code = (int)reader["ОбластьТехногенногоОбъекта"];
                        string region_name = (string)reader["Область"];
                        Region region = new Region(region_code, region_name);
                        District district = new District(district_code, region, district_name);

                        string ownership = (string)reader["Принадлежность"];
                        string phone = (string)reader["Телефон"];
                        string fax = (string)reader["Факс"];
                        string email = (string)reader["EMail"];

                        DateTime foundationdate = (DateTime)reader["ДатаВводаЭкспл"];
                        DateTime reconstractiondate = (DateTime)reader["ДатаПоследнейРеконструкции"];
                        
                        int numberofrefuel = (int)reader["КолВоЗаправокСут"];
                        int volume = (int)reader["ОбъемХранения"];

                        float groundtank = (float)reader["ЕмкостьНаземногоРезервуара"];
                        float undergroundtank = (float)reader["ЕмкостьПодземногоРезервуара"];

                        bool watertreatment = (bool)reader["ОчистнДождСток"];
                        bool watertreatmentcollect = (bool)reader["ОчистнСборПроливов"];

                        string fueltype = (string)reader["ТипТоплива"];
                        int numberofthreads = (int)reader["КоличествоНиток"];
                        float productivity = (float)reader["Производительность"];
                        float tubediameter = (float)reader["ДиаметрТрубы"];
                        string geodescription = (string)reader["ГеографическоеОписание"];
					
                        byte[] map = (Byte[])reader["Карта"]; // даша

                        Point point = new Point(coordinates, ground_type, (float)waterdeep, (float)height);
                        string risk_object_type_name = (string)reader["НаименованиеТипаТехногенногоОбъекта"];
                        RiskObjectType risk_object_type = new RiskObjectType((int)reader["КодТипаТехногенногоОбъекта"], (string)risk_object_type_name);
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];

                        string name = (string)reader["НаименованиеТехногенногоОбъекта"];
                        string address = (string)reader["АдресТехногенногоОбъекта"];
                        CadastreType cadastre_type = new CadastreType((int)reader["КодТипаНазначенияЗемель"], (string)cadastre_type_name,
                                                                        (float)pdk, (float)water_pdk_coef,
                                                                        (string)ground_doc_name, (string)water_doc_name);
                        RiskObject risk_object = new RiskObject(id, point, risk_object_type, cadastre_type,
                                                                name, district, region, address, ownership, phone, fax, email, 
                                                                foundationdate, reconstractiondate,
                                                                numberofrefuel, volume,
                                                                watertreatment, watertreatmentcollect, map,
                                                                groundtank, undergroundtank, fueltype, numberofthreads, (float)tubediameter, (float)productivity, geodescription);
                        
                        risk_objects.Add(risk_object);
                    }
                    rc = risk_objects.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListSpreadingCoefficient(EGH01DB.IDBContext dbcontext, ref List<SpreadingCoefficient> spreading_coefficient)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetSpreadingCoefficientList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    spreading_coefficient = new List<SpreadingCoefficient>();
                    while (reader.Read())
                    {
                        int ground_type_code = (int)reader["ТипГрунта"];
                        string ground_type_name = (string)reader["НаименованиеТипаГрунта"];

                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузииГрунта"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];
                        GroundType ground_type = new GroundType ((int)ground_type_code,
                                                    (string)ground_type_name, 
                                                    (float)porosity, 
                                                    (float)holdmigration, 
                                                    (float)waterfilter, 
                                                    (float)diffusion, 
                                                    (float)distribution, 
                                                    (float)sorption,
                                                    (float)watercapacity,
                                                    (float)soilmoisture,
                                                    (float)аveryanovfactor,
                                                    (float)permeability,
                                                    (float)density);
                        int code = (int)reader["КодКоэффициентаРазлива"];
                        float min_volume = (float)reader["МинПролива"];
                        float max_volume = (float)reader["МаксПролива"];
                        float min_angle = (float)reader["МинУклона"];
                        float max_angle = (float)reader["МаксУклона"];
                        double koef = (double)reader["КоэффициентРазлива"];

                        int petrochemical_type_code = (int)reader["КодТипаНефтепродукта"];
                        string name = (string)reader["НаименованиеТипаНефтепродукта"];
                        float boilingtemp = (float)reader["ТемператураКипения"];
                        float petrochemical_density = (float)reader["Плотность"];
                        float viscosity = (float)reader["КинематическаяВязкость"];
                        float solubility = (float)reader["Растворимость"];
                        float tension = (float)reader["КоэфНатяжения"];
                        float dynamicviscosity = (float)reader["ДинамическаяВязкость"];
                        float petrochemical_diffusion = (float)reader["КоэфДиффузииНП"];
                        int petrochemicalcategories = (int)reader["КодКатегорииНефтепродукта"];
                        string petrochemicalcategoriesname = (string)reader["НаименованиеКатегорииНефтепродукта"];
                        PetrochemicalCategories petro_cat = new PetrochemicalCategories(petrochemicalcategories, petrochemicalcategoriesname);
                        PetrochemicalType petrochemical_type = new PetrochemicalType(petrochemical_type_code,
                                                                                     name,
                                                                                     (float)boilingtemp,
                                                                                     (float)petrochemical_density,
                                                                                     (float)viscosity,
                                                                                     (float)solubility,
                                                                                     (float)tension,
                                                                                     (float)dynamicviscosity,
                                                                                     (float)petrochemical_diffusion, petro_cat);

                        spreading_coefficient.Add(new SpreadingCoefficient(code, ground_type, petrochemical_type,
                                                        (float)min_volume,
                                                        (float)max_volume,
                                                        (float)min_angle,
                                                        (float)max_angle,
                                                        (float)koef));
                    }
                    rc = spreading_coefficient.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public bool GetListAnchorPoint(EGH01DB.IDBContext dbcontext, ref List<AnchorPoint> anchor_points)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetAnchorPointList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    anchor_points = new List<AnchorPoint>();
                    while (reader.Read())
                    {
                        int id = (int)reader["IdОпорнойГеологическойТочки"];
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        Coordinates coordinates = new Coordinates((float)x, (float)y);

                        string ground_type_name = (string)reader["НаименованиеТипаГрунта"];
                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];
                        GroundType ground_type = new GroundType((int)reader["ТипГрунта"],
                                                                    (string)ground_type_name,
                                                                    (float)porosity,
                                                                    (float)holdmigration,
                                                                    (float)waterfilter,
                                                                    (float)diffusion,
                                                                    (float)distribution,
                                                                    (float)sorption,
                                                                    (float)watercapacity,
                                                                    (float)soilmoisture,
                                                                    (float)аveryanovfactor,
                                                                    (float)permeability,
                                                                    (float)density);
                        float waterdeep = (float)reader["ГлубинаГрунтовыхВод"];
                        float height = (float)reader["ВысотаУровнемМоря"];
                        Point point = new Point(coordinates, ground_type, (float)waterdeep, (float)height);

                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДК"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];

                        CadastreType cadastre_type = new CadastreType((int)reader["КодНазначенияЗемель"], (string)cadastre_type_name,
                                                                        (float)pdk, (float)water_pdk_coef,
                                                                        (string)ground_doc_name, (string) water_doc_name);
                        AnchorPoint anchor_point = new AnchorPoint(id, point, cadastre_type);

                        anchor_points.Add(anchor_point);
                    }
                    rc = anchor_points.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListEcoObject(EGH01DB.IDBContext dbcontext, ref List<EcoObject> ecoobjects)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetEcoObjectList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    ecoobjects = new List<EcoObject>();
                    while (reader.Read())
                    {
                        int id = (int)reader["IdПриродоохранногоОбъекта"];
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        Coordinates coordinates = new Coordinates((float)x, (float)y);

                        string ground_type_name = (string)reader["НаименованиеТипаГрунта"];
                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];
                        GroundType ground_type = new GroundType((int)reader["ТипГрунта"],
                                                                    (string)ground_type_name,
                                                                    (float)porosity,
                                                                    (float)holdmigration,
                                                                    (float)waterfilter,
                                                                    (float)diffusion,
                                                                    (float)distribution,
                                                                    (float)sorption,
                                                                    (float)watercapacity,
                                                                    (float)soilmoisture,
                                                                    (float)аveryanovfactor,
                                                                    (float)permeability,
                                                                    (float)density);
                        float waterdeep = (float)reader["ГлубинаГрунтовыхВод"];
                        float height = (float)reader["ВысотаУровнемМоря"];
                        Point point = new Point(coordinates, ground_type, (float)waterdeep, (float)height);
                        int cadastre_type_code = (int)reader["КодТипаНазначенияЗемель"];
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code,cadastre_type_name,
                                                                        (float)pdk, (float)water_pdk_coef,
                                                                        ground_doc_name, water_doc_name);
                       
                        bool iswaterobject;
                        int cat_water_name = (int)reader["КатегорияВодоохрТер"];
                        string category_name = (string)reader["НаименованиеКатегории"];

                        if (cat_water_name!=0 ) iswaterobject = true; else iswaterobject= false;
                        WaterProtectionArea waterprotectionarea = new WaterProtectionArea(cat_water_name, category_name);
                        int ecoobject_type_code = (int)reader["КодТипаПриродоохранногоОбъекта"];
                        string ecoobject_type_name = (string)reader["НаименованиеТипаПриродоохранногоОбъекта"];

                        EcoObjectType eco_object_type = new EcoObjectType(ecoobject_type_code, ecoobject_type_name, waterprotectionarea);
                          
                        string ecoobject_name = (string)reader["НаименованиеПриродоохранногоОбъекта"];

                        EcoObject ecoobject = new EcoObject(id, point, eco_object_type, cadastre_type, ecoobject_name, iswaterobject);

                        ecoobjects.Add(ecoobject);
                    }
                    rc = ecoobjects.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListECOForecast(EGH01DB.IDBContext dbcontext, ref List<RGEContext.ECOForecast> list_eco_forecast)
        {
            bool rc = false;
            list_eco_forecast =   new List<RGEContext.ECOForecast>();   //   new RGEContext.ECOForecastlist();
            using (SqlCommand cmd = new SqlCommand("EGH.GetEcoForecastList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        string stage = (string)reader["Стадия"];
                         int report_id = (int)reader ["IdОтчета"];
                         DateTime date = (DateTime)reader["ДатаОтчета"];   
                         int predator = (int)reader["Родитель"];
                        
                         string xmlContent = (string)reader["ТекстОтчета"];
                         if (!xmlContent.Trim().Equals(""))
                         {
                          XmlDocument doc = new XmlDocument();
                          doc.LoadXml(xmlContent);
                          XmlNode newNode = doc.DocumentElement;
                          list_eco_forecast.Add(new RGEContext.ECOForecast (newNode));
                         //comment = (string)reader["Комментарий"];
                         }
                    }
                    rc = ((int)cmd.Parameters["@exitrc"].Value >0);
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return rc;
        }
        static public bool GetListECOEvalution(EGH01DB.IDBContext dbcontext, ref List<CEQContext.ECOEvalution> list_ecoevalution)
        {
            bool rc = false;
            list_ecoevalution =   new List<CEQContext.ECOEvalution>();   //   new RGEContext.ECOForecastlist();
            using (SqlCommand cmd = new SqlCommand("EGH.GetECOEvalutionList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    // cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        string stage = (string)reader["Стадия"];
                         int report_id = (int)reader ["IdОтчета"];
                         DateTime date = (DateTime)reader["ДатаОтчета"];   
                         int predator = (int)reader["Родитель"];
                        
                         string xmlContent = (string)reader["ТекстОтчета"];
                         if (!xmlContent.Trim().Equals(""))
                         {
                          XmlDocument doc = new XmlDocument();
                          doc.LoadXml(xmlContent);
                          XmlNode newNode = doc.DocumentElement;
                          list_ecoevalution.Add(new CEQContext.ECOEvalution(newNode));
                         }
                    }
                    rc = true;   //   ((int)cmd.Parameters["@exitrc"].Value >0);
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return rc;
        } 
        static public bool GetListECOClassification(EGH01DB.IDBContext dbcontext, ref List<GEAContext.ECOClassification> list_classification)
        {
            bool rc = false;
            list_classification =   new  List<GEAContext.ECOClassification>();  
            using (SqlCommand cmd = new SqlCommand("EGH.GetECOClassificationList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                     SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        string stage = (string)reader["Стадия"];
                         int report_id = (int)reader ["IdОтчета"];
                         DateTime date = (DateTime)reader["ДатаОтчета"];   
                         int predator = (int)reader["Родитель"];
                        
                         string xmlContent = (string)reader["ТекстОтчета"];
                         if (!xmlContent.Trim().Equals(""))
                         {
                          XmlDocument doc = new XmlDocument();
                          doc.LoadXml(xmlContent);
                          XmlNode newNode = doc.DocumentElement;
                          list_classification.Add(new GEAContext.ECOClassification(newNode));
                         }
                    }
                    rc = true;   
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return rc;
        } 


        static public bool GetListReport(EGH01DB.IDBContext dbcontext, ref List<Report> list)
        {
            bool rc = false;
            list = new List<Report>();  
            using (SqlCommand cmd = new SqlCommand("EGH.GetReportList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                            string stage = (string)reader["Стадия"];
                            int report_id = (int)reader["IdОтчета"];
                            DateTime date = (DateTime)reader["ДатаОтчета"];
                            int predator = (int)reader["Родитель"];
                            Report parent_Report = new Report(predator);
                            string comment = (string)reader["Комментарий"];

                            string xmlContent = (string)reader["ТекстОтчета"];
                            xmlContent = xmlContent.TrimEnd();
                            xmlContent = xmlContent.TrimStart();
                   
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xmlContent);
                            XmlNode xml_Content_Node = doc.DocumentElement;
                         
                            string xlst_Content = (string)reader["СтильОтчета"];
                            xlst_Content = xmlContent.TrimEnd();
                            xlst_Content = xmlContent.TrimStart();
                            //if (!xlst_Content.Trim().Equals(""))
                           // {
                                XmlDocument doc_xlst = new XmlDocument();
                                doc_xlst.LoadXml(xlst_Content);
                                XmlNode xlst_Content_Node = doc_xlst.DocumentElement;
                           // }
                                list.Add(new Report(report_id, parent_Report, stage, date, xml_Content_Node, xlst_Content_Node, comment));
 
                    }
                    rc = ((int)cmd.Parameters["@exitrc"].Value > 0);
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return rc;
        }
        static public bool GetListReportByStage(EGH01DB.IDBContext dbcontext, string stage, ref List<Report> list)
        {
            bool rc = false;
            list = new ReportsList();
            using (SqlCommand cmd = new SqlCommand("EGH.GetStageReportList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@Стадия", SqlDbType.NChar);
                    parm.Value = stage;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int report_id = (int)reader["IdОтчета"];
                        DateTime date = (DateTime)reader["ДатаОтчета"];
                        // string stage = (string)reader["Стадия"];
                        int predator = (int)reader["Родитель"];
                        Report parent = new Report(predator);
                        string comment = (string)reader["Комментарий"];

                        string xmlContent = (string)reader["ТекстОтчета"];
                        XmlDocument doc_xml = new XmlDocument();
                        doc_xml.LoadXml(xmlContent);
                        XmlNode xml_Node = doc_xml.DocumentElement;

                        string xslContent = (string)reader["ТекстОтчета"];
                        XmlDocument doc_xsl = new XmlDocument();
                        doc_xsl.LoadXml(xslContent);
                        XmlNode xsl_Node = doc_xsl.DocumentElement;
                        Report report = new Report(report_id, parent, stage, date, xml_Node, xsl_Node, comment);

                        list.Add(report);

                    }
                    rc = ((int)cmd.Parameters["@exitrc"].Value > 0);
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return rc;
        }
        static public bool GetListSoilCleaningMethods(EGH01DB.IDBContext dbcontext, ref List<SoilCleaningMethod> list_soil_clean_method)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetSoilCleaningMethodsList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_soil_clean_method = new List<SoilCleaningMethod>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодТипаКатегории"];
                        string method = (string)reader["ОписаниеМетода"];

                        list_soil_clean_method.Add(new SoilCleaningMethod(code, method));
                    }
                    rc = list_soil_clean_method.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListWaterCleaningMethods(EGH01DB.IDBContext dbcontext, ref List<WaterCleaningMethod> list_water_clean_method)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetWaterCleaningMethodsList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_water_clean_method = new List<WaterCleaningMethod>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодТипаКатегории"];
                        string method = (string)reader["ОписаниеМетода"];

                        list_water_clean_method.Add(new WaterCleaningMethod(code, method));
                    }
                    rc = list_water_clean_method.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListWaterPollutionCategories(EGH01DB.IDBContext dbcontext, ref List<WaterPollutionCategories> list_water_pollution_categories)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetWaterPollutionCategoriesList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_water_pollution_categories = new List<WaterPollutionCategories>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодКатегорииЗагрязненияГВ"];
                        string name = (string)reader["НаименованиеКатегорииЗагрязненияГВ"];
                        float min = (float)reader["МинДиапазон"];
                        float max = (float)reader["МаксДиапазон"];

                        int cadastre_type_code = (int)reader["КодНазначенияЗемель"];
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk_coef = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code, cadastre_type_name,
                                                                        pdk_coef, water_pdk_coef,
                                                                        ground_doc_name, water_doc_name);

                        list_water_pollution_categories.Add(new WaterPollutionCategories(code, name, min, max, cadastre_type));
                    }
                    rc = list_water_pollution_categories.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListSoilPollutionCategories(EGH01DB.IDBContext dbcontext, ref List<SoilPollutionCategories> list_soil_pollution_categories)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetSoilPollutionCategoriesList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list_soil_pollution_categories = new List<SoilPollutionCategories>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодКатегорииЗагрязненияГрунта"];
                        string name = (string)reader["НаименованиеКатегорииЗагрязненияГрунта"];
                        float min = (float)reader["МинДиапазон"];
                        float max = (float)reader["МаксДиапазон"];

                        int cadastre_type_code = (int)reader["КодНазначенияЗемель"];
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk_coef = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code, cadastre_type_name,
                                                                        pdk_coef, water_pdk_coef,
                                                                        ground_doc_name, water_doc_name);


                        list_soil_pollution_categories.Add(new SoilPollutionCategories(code, name, min, max, cadastre_type));
                    }
                    rc = list_soil_pollution_categories.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListEmergencyClass(EGH01DB.IDBContext dbcontext, ref List<EmergencyClass> emergency_class)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetEmergencyClassList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    emergency_class = new List<EmergencyClass>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодТипаАварии"];
                        string name = (string)reader["НаименованиеТипаАварии"];
                        float min = (float)reader["МинМасса"];
                        float max = (float)reader["МаксМасса"];

                        emergency_class.Add(new EmergencyClass(code, name, min, max));
                    }
                    rc = emergency_class.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListPenetrationDepth(EGH01DB.IDBContext dbcontext, ref List<PenetrationDepth> penetration_depth)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetPenetrationDepthList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    penetration_depth = new List<PenetrationDepth>();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодТипаКатегории"];
                        string name = (string)reader["НаименованиеТипаКатегории"];
                        float min = (float)reader["МинДиапазон"];
                        float max = (float)reader["МаксДиапазон"];

                        penetration_depth.Add(new PenetrationDepth(code, name, min, max));
                    }
                    rc = penetration_depth.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListPetrochemicalCategories(EGH01DB.IDBContext dbcontext, ref List<PetrochemicalCategories> list)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetPetrochemicalCategoriesList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list = new List<PetrochemicalCategories>();
                    while (reader.Read())
                    {
                        list.Add(new PetrochemicalCategories((int)reader["КодКатегорииНефтепродукта"], (string)reader["НаименованиеКатегорииНефтепродукта"]));
                    }
                    rc = list.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetListWaterProtectionArea(EGH01DB.IDBContext dbcontext, ref List<WaterProtectionArea> list)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetWaterProtectionAreaList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    list = new List<WaterProtectionArea>();
                    while (reader.Read())
                    {
                        list.Add(new WaterProtectionArea((int)reader["КодТипаКатегории"], (string)reader["НаименованиеКатегории"]));
                    }
                    rc = list.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;

            }
        }
        static public bool GetListRehabilitationMethod(EGH01DB.IDBContext dbcontext, ref List<RehabilitationMethod> list)
        {
            bool rc = false;
            RehabilitationMethod rehabilitation_method = new RehabilitationMethod();
            list = new List<RehabilitationMethod>();
            using (SqlCommand cmd = new SqlCommand("EGH.GetRehabilitationMethodList", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                

                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int code = (int)reader["КодКлассификатора"];

                        int risk_object_type_code = (int)reader["ТипТехногенногоОбъекта"];
                        string risk_object_type_name = (string)reader["НаименованиеТипаТехногенногоОбъекта"];
                        RiskObjectType risk_object_type = new RiskObjectType(risk_object_type_code, risk_object_type_name);

                        int cadastre_type_code = (int)reader["НазначениеЗемель"];
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float cadastre_type_pdk_coef = (float)reader["ПДК"];
                        float cadastre_type_water_pdk_coef = (float)reader["ПДКводы"];
                        string cadastre_type_ground_doc_name = (string)reader["НормДокументЗемля"];
                        string cadastre_type_water_doc_name = (string)reader["НормДокументВода"];
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code, cadastre_type_name, cadastre_type_pdk_coef,
                                            cadastre_type_water_pdk_coef, cadastre_type_ground_doc_name, cadastre_type_water_doc_name);

                        int petrochemical_category_type_code = (int)reader["КатегорияНефтепродукта"];
                        string petrochemical_category_name = (string)reader["KN_НаименованиеКатегорииНефтепродукта"];
                        PetrochemicalCategories petrochemical_category = new PetrochemicalCategories (petrochemical_category_type_code, petrochemical_category_name);

                        int emergency_class_type_code = (int)reader["КлассификацияАварий"];
                        string emergency_class_name = (string)reader["KA_НаименованиеТипаАварии"];
                        float emergency_class_min = (float)reader["KA_МинМасса"];
                        float emergency_class_max = (float)reader["KA_МаксМасса"];
                        EmergencyClass emergency_class = new EmergencyClass (emergency_class_type_code, emergency_class_name, emergency_class_min, emergency_class_max);
                        
                        int penetration_depth_type_code = (int)reader["КатегорияПроникновенияНефтепродукта"];
                        string penetration_depth_name = (string)reader["PN_НаименованиеТипаКатегории"];
                        float penetration_depth_min = (float)reader["PN_МинДиапазон"];
                        float penetration_depth_max = (float)reader["PN_МаксДиапазон"];
                        PenetrationDepth penetration_depth = new PenetrationDepth (penetration_depth_type_code, penetration_depth_name, penetration_depth_min, penetration_depth_max);

                        int soil_pollution_categories_type_code = (int)reader["КатегорияЗагрязненияГрунта"];
                        string soil_pollution_categories_name = (string)reader["GP_НаименованиеКатегорииЗагрязненияГрунта"];
                        float soil_pollution_categories_min = (float)reader["GP_МинДиапазон"];
                        float soil_pollution_categories_max = (float)reader["GP_МаксДиапазон"];
                        int soil_pollution_categories_cadastre_type_code = (int)reader["GP_КодНазначенияЗемель"];
                        CadastreType soil_pollution_categories_cadastre_type = new CadastreType(soil_pollution_categories_cadastre_type_code);
                        SoilPollutionCategories soilpollution_categories = new SoilPollutionCategories(soil_pollution_categories_type_code,
                                                                                                       soil_pollution_categories_name,
                                                                                                       soil_pollution_categories_min,
                                                                                                       soil_pollution_categories_max,
                                                                                                       soil_pollution_categories_cadastre_type);
                        bool waterachieved = (bool)reader["ДостижениеГоризонтаГрунтовыхВод"];
                        int water_pollution_categories_type_code = (int)reader["КатегорияЗагрязненияГрунтовыхВод"];
                        string water_pollution_categories_name = (string)reader["WG_НаименованиеКатегорииЗагрязненияГВ"];
                        float water_pollution_categories_min = (float)reader["WG_МинДиапазон"];
                        float water_pollution_categories_max = (float)reader["WG_МаксДиапазон"];
                        int water_pollution_categories_cadastre_type_code = (int)reader["WG_КодНазначенияЗемель"];
                        CadastreType water_pollution_categories_cadastre_type = new CadastreType(water_pollution_categories_cadastre_type_code);
                        WaterPollutionCategories water_pollution_categories = new WaterPollutionCategories(water_pollution_categories_type_code,
                                                                                                       water_pollution_categories_name,
                                                                                                       water_pollution_categories_min,
                                                                                                       water_pollution_categories_max,
                                                                                                       water_pollution_categories_cadastre_type);
                        int water_protection_area_type_code = (int)reader["КатегорияВодоохраннойТерритории"];
                        string water_protection_area_name = (string)reader["WT_НаименованиеКатегории"];
                        WaterProtectionArea water_protection_area = new WaterProtectionArea (water_protection_area_type_code, water_protection_area_name);

                        int soil_cleaning_method_type_code = (int)reader["КатегорияМЛЗагрязненияПГ"];
                        string soil_cleaning_method_name = (string)reader["PG_ОписаниеМетода"];
                        SoilCleaningMethod soil_cleaning_method = new SoilCleaningMethod (soil_cleaning_method_type_code, soil_cleaning_method_name);

                        int water_cleaning_method_type_code = (int)reader["КатегорияМЛЗагрязненияГВ"];
                        string water_cleaning_method_name = (string)reader["PW_ОписаниеМетода"];
                        WaterCleaningMethod water_cleaning_method = new WaterCleaningMethod (water_cleaning_method_type_code, water_cleaning_method_name);

                        rehabilitation_method = new RehabilitationMethod(code,
                                                                            risk_object_type,
                                                                            cadastre_type,
                                                                            petrochemical_category,
                                                                            emergency_class,
                                                                            penetration_depth,
                                                                            soilpollution_categories,
                                                                            waterachieved,
                                                                            water_pollution_categories,
                                                                            water_protection_area,
                                                                            soil_cleaning_method, 
                                                                            water_cleaning_method);
                        list.Add(rehabilitation_method);
                    }
                    rc = (list.Count > 0); 
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return rc;
         }
        
        
        static public float     GetFloatAttribute(XmlNode n, string name, float errorvalue = 0.0f)
        {
            float rc = errorvalue;
            if (n.Attributes[name] != null)  if (!FloatTryParse(n.Attributes[name].Value, out rc)) rc = errorvalue;   
            return rc;
        }
        static public int       GetIntAttribute(XmlNode n, string name, int errorvalue = -1)
        {
            int rc = errorvalue;
            if (n.Attributes[name] != null) if (!int.TryParse(n.Attributes[name].Value, out rc)) rc = errorvalue;
            return rc;
        }
        static public string    GetStringAttribute(XmlNode n, string name, string  errorvalue = "")
        {
            string rc = errorvalue;
            if (n.Attributes[name] != null) rc = n.Attributes[name].Value;
            return rc;
        }
        static public bool      GetBoolAttribute(XmlNode n, string name, bool errorvalue = false)
        {
            bool rc = errorvalue;
            if (n.Attributes[name] != null) if (!bool.TryParse(n.Attributes[name].Value, out rc)) rc = errorvalue;
            return rc;
        }
        static public DateTime  GetDateTimeAttribute(XmlNode n, string name, DateTime errorvalue)
        {
            DateTime rc = errorvalue;
            if (n.Attributes[name] != null) if (!DateTime.TryParse(n.Attributes[name].Value, out rc)) rc = errorvalue;
            return rc;
        }
        
    }
}
