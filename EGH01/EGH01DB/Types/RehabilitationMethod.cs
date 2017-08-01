using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;
using EGH01DB.Types;

// Классификатор методов реабилитации - RehabilitationMethod
namespace EGH01DB.Types
{
    public class RehabilitationMethod
    {
        public int type_code { get; private set; }   // код 
        public RiskObjectType riskobjecttype { get; private set; } // ---- Тип Техногенного Объекта	
        public CadastreType cadastretype  { get; private set; } // ---- Назначение Земель	
        public PetrochemicalCategories petrochemicalcategory  { get; private set; } // ---- Категория Нефтепродукта	
        public EmergencyClass emergencyclass { get; private set; } //  ---- Классификация Аварий
        public PenetrationDepth penetrationdepth  { get; private set; } // ---- Категория Проникновения Нефтепродукта
        public SoilPollutionCategories soilpollutioncategories { get; private set; } // ---- Категория Загрязнения Грунта	
        public bool waterachieved { get; private set; } // ---- Достижение Горизонта Грунтовых Вод
        public WaterPollutionCategories waterpollutioncategories { get; private set; } // ---- Категория Загрязнения Грунтовых Вод
        public WaterProtectionArea waterprotectionarea { get; private set; }   // ---- Категория Водоохранной Территории
        public SoilCleaningMethod soilcleaningmethod  { get; private set; }   // ---- Категория Методов ликвидации Загрязнения грунта
        public WaterCleaningMethod watercleaningmethod { get; private set; }   //---- Категория Методов ликвидации Загрязнения грунтовых вод

        static public RehabilitationMethod defaulttype { get { return new RehabilitationMethod(0, null, null, null,null, null, null, false, null, null, null, null); } }  // выдавать при ошибке 


        public RehabilitationMethod()
        {
            this.type_code = -1;
            this.riskobjecttype = new RiskObjectType();
            this.cadastretype = new CadastreType ();
            this.petrochemicalcategory = new PetrochemicalCategories();
            this.emergencyclass = new EmergencyClass();
            this.penetrationdepth = new PenetrationDepth();
            this.soilpollutioncategories = new SoilPollutionCategories();
            this.waterachieved = false;
            this.waterpollutioncategories = new WaterPollutionCategories();
            this.waterprotectionarea = new WaterProtectionArea();
            this.soilcleaningmethod = new SoilCleaningMethod();
            this.watercleaningmethod = new WaterCleaningMethod();
        }
        public RehabilitationMethod(int type_code)
        {
            this.type_code = type_code;
            this.riskobjecttype = new RiskObjectType();
            this.cadastretype = new CadastreType();
            this.petrochemicalcategory = new PetrochemicalCategories();
            this.emergencyclass = new EmergencyClass();
            this.penetrationdepth = new PenetrationDepth();
            this.soilpollutioncategories = new SoilPollutionCategories();
            this.waterachieved = false;
            this.waterpollutioncategories = new WaterPollutionCategories();
            this.waterprotectionarea = new WaterProtectionArea();
            this.soilcleaningmethod = new SoilCleaningMethod();
            this.watercleaningmethod = new WaterCleaningMethod();
        }
        public RehabilitationMethod(int type_code, RiskObjectType riskobjecttype, 
                                    CadastreType cadastretype, PetrochemicalCategories petrochemicalcategory,
                                    EmergencyClass emergencyclass, PenetrationDepth penetrationdepth,
                                    SoilPollutionCategories soilpollutioncategories, bool waterachieved,
                                    WaterPollutionCategories waterpollutioncategories, WaterProtectionArea waterprotectionarea,
                                    SoilCleaningMethod soilcleaningmethod, WaterCleaningMethod watercleaningmethod)
        {
            this.type_code = type_code;
            this.riskobjecttype = riskobjecttype;
            this.cadastretype = cadastretype;
            this.petrochemicalcategory = petrochemicalcategory;
            this.emergencyclass = emergencyclass;
            this.penetrationdepth = penetrationdepth;
            this.soilpollutioncategories = soilpollutioncategories;
            this.waterachieved = waterachieved;
            this.waterpollutioncategories = waterpollutioncategories;
            this.waterprotectionarea = waterprotectionarea;
            this.soilcleaningmethod = soilcleaningmethod;
            this.watercleaningmethod = watercleaningmethod;
        }
       
        public RehabilitationMethod(XmlNode node)
        {
            this.type_code = Helper.GetIntAttribute(node, "type_code", -1);
            
            XmlNode riskobjecttype = node.SelectSingleNode(".//RiskObjectType");
            if (riskobjecttype != null) this.riskobjecttype = new RiskObjectType(riskobjecttype);
            else this.riskobjecttype = null;

            XmlNode cadastretype = node.SelectSingleNode(".//CadastreType");
            if (cadastretype != null) this.cadastretype = new CadastreType(cadastretype);
            else this.cadastretype = null;

            XmlNode petrochemicalcategory = node.SelectSingleNode(".//PetrochemicalCategories");
            if (petrochemicalcategory != null) this.petrochemicalcategory = new PetrochemicalCategories(petrochemicalcategory);
            else this.petrochemicalcategory = null;

            XmlNode emergencyclass = node.SelectSingleNode(".//EmergencyClass");
            if (emergencyclass != null) this.emergencyclass = new EmergencyClass(emergencyclass);
            else this.emergencyclass = null;

            XmlNode penetrationdepth = node.SelectSingleNode(".//PenetrationDepth");
            if (penetrationdepth != null) this.penetrationdepth = new PenetrationDepth(penetrationdepth);
            else this.penetrationdepth = null;

            XmlNode soilpollutioncategories = node.SelectSingleNode(".//SoilPollutionCategories");
            if (soilpollutioncategories != null) this.soilpollutioncategories = new SoilPollutionCategories(soilpollutioncategories);
            else this.soilpollutioncategories = null;

            this.waterachieved = Helper.GetBoolAttribute(node, "waterachieved", false);

            XmlNode waterpollutioncategories = node.SelectSingleNode(".//WaterPollutionCategories");
            if (waterpollutioncategories != null) this.waterpollutioncategories = new WaterPollutionCategories(waterpollutioncategories);
            else this.waterpollutioncategories = null;

            XmlNode waterprotectionarea = node.SelectSingleNode(".//WaterProtectionArea");
            if (waterprotectionarea != null) this.waterprotectionarea = new WaterProtectionArea(waterprotectionarea);
            else this.waterprotectionarea = null;

            XmlNode soilcleaningmethod = node.SelectSingleNode(".//SoilCleaningMethod");
            if (soilcleaningmethod != null) this.soilcleaningmethod = new SoilCleaningMethod(soilcleaningmethod);
            else this.soilcleaningmethod = null;

            XmlNode watercleaningmethod = node.SelectSingleNode(".//WaterCleaningMethod");
            if (watercleaningmethod != null) this.watercleaningmethod = new WaterCleaningMethod(watercleaningmethod);
            else this.watercleaningmethod = null;
        }
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int type_code, out RehabilitationMethod rehabilitation_method)
        {
            bool rc = false;
            rehabilitation_method = new RehabilitationMethod(type_code);
            using (SqlCommand cmd = new SqlCommand("EGH.GetRehabilitationMethodByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКлассификатора", SqlDbType.Int);
                    parm.Value = type_code;
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
                    if (reader.Read())
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

                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) 
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
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return rc;
        }
        static public bool GetByParm(EGH01DB.IDBContext dbcontext, 
                                     int riskobjecttypecode,  
									 int cadastretypecode,
									 int petrochemicalcategorytypecode,
									 int emergencyclasstypecode,
									 int penetrationdepthtypecode,
									 int soilpollutioncategoriestypecode,
									 bool waterachieved,
									 int waterpollutioncategoriestypecode,
									 int waterprotectionareatypecode,
									 out RehabilitationMethod rehabilitation_method) 
        {
            bool rc = false;
            rehabilitation_method = new RehabilitationMethod();
            using (SqlCommand cmd = new SqlCommand("EGH.GetListRehabilitationMethodOnParam", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                {
                    SqlParameter parm = new SqlParameter("@ТипТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = riskobjecttypecode;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НазначениеЗемель", SqlDbType.Int);
                    parm.Value = cadastretypecode;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияНефтепродукта", SqlDbType.Int);
                    parm.Value = petrochemicalcategorytypecode;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КлассификацияАварий", SqlDbType.Int);
                    parm.Value = emergencyclasstypecode;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияПроникновенияНефтепродукта", SqlDbType.Int);
                    parm.Value = penetrationdepthtypecode;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияЗагрязненияГрунта", SqlDbType.Int);
                    parm.Value = soilpollutioncategoriestypecode;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДостижениеГоризонтаГрунтовыхВод", SqlDbType.Bit);
                    parm.Value = waterachieved;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияЗагрязненияГрунтовыхВод", SqlDbType.Int);
                    parm.Value = waterpollutioncategoriestypecode;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияВодоохраннойТерритории", SqlDbType.Int);
                    parm.Value = waterprotectionareatypecode;
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
                    if (reader.Read())
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
                        PetrochemicalCategories petrochemical_category = new PetrochemicalCategories(petrochemical_category_type_code, petrochemical_category_name);

                        int emergency_class_type_code = (int)reader["КлассификацияАварий"];
                        string emergency_class_name = (string)reader["KA_НаименованиеТипаАварии"];
                        float emergency_class_min = (float)reader["KA_МинМасса"];
                        float emergency_class_max = (float)reader["KA_МаксМасса"];
                        EmergencyClass emergency_class = new EmergencyClass(emergency_class_type_code, emergency_class_name, emergency_class_min, emergency_class_max);

                        int penetration_depth_type_code = (int)reader["КатегорияПроникновенияНефтепродукта"];
                        string penetration_depth_name = (string)reader["PN_НаименованиеТипаКатегории"];
                        float penetration_depth_min = (float)reader["PN_МинДиапазон"];
                        float penetration_depth_max = (float)reader["PN_МаксДиапазон"];
                        PenetrationDepth penetration_depth = new PenetrationDepth(penetration_depth_type_code, penetration_depth_name, penetration_depth_min, penetration_depth_max);

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
                        bool water_achieved = (bool)reader["ДостижениеГоризонтаГрунтовыхВод"];
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
                        WaterProtectionArea water_protection_area = new WaterProtectionArea(water_protection_area_type_code, water_protection_area_name);

                        int soil_cleaning_method_type_code = (int)reader["КатегорияМЛЗагрязненияПГ"];
                        string soil_cleaning_method_name = (string)reader["PG_ОписаниеМетода"];
                        SoilCleaningMethod soil_cleaning_method = new SoilCleaningMethod(soil_cleaning_method_type_code, soil_cleaning_method_name);

                        int water_cleaning_method_type_code = (int)reader["КатегорияМЛЗагрязненияГВ"];
                        string water_cleaning_method_name = (string)reader["PW_ОписаниеМетода"];
                        WaterCleaningMethod water_cleaning_method = new WaterCleaningMethod(water_cleaning_method_type_code, water_cleaning_method_name);

                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0)
                            rehabilitation_method = new RehabilitationMethod(code,
                                                                            risk_object_type,
                                                                            cadastre_type,
                                                                            petrochemical_category,
                                                                            emergency_class,
                                                                            penetration_depth,
                                                                            soilpollution_categories,
                                                                            water_achieved,
                                                                            water_pollution_categories,
                                                                            water_protection_area,
                                                                            soil_cleaning_method,
                                                                            water_cleaning_method);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }
            return true;
        }
        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextRehabilitationMethodCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКлассификатора", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
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
                    code = (int)cmd.Parameters["@КодКлассификатора"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }

        static public bool Create(EGH01DB.IDBContext dbcontext, RehabilitationMethod rehabilitation_method)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateRehabilitationMethod", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКлассификатора", SqlDbType.Int);
                    int new_rehabilitation_method_type_code = 0;
                    if (GetNextCode(dbcontext, out new_rehabilitation_method_type_code)) rehabilitation_method.type_code = new_rehabilitation_method_type_code;
                    parm.Value = rehabilitation_method.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.riskobjecttype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НазначениеЗемель", SqlDbType.Int);
                    parm.Value = rehabilitation_method.cadastretype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияНефтепродукта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.petrochemicalcategory.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КлассификацияАварий", SqlDbType.Int);
                    parm.Value = rehabilitation_method.emergencyclass.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияПроникновенияНефтепродукта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.penetrationdepth.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияЗагрязненияГрунта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.soilpollutioncategories.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДостижениеГоризонтаГрунтовыхВод", SqlDbType.Bit);
                    parm.Value = rehabilitation_method.waterachieved;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияЗагрязненияГрунтовыхВод", SqlDbType.Int);
                    parm.Value = rehabilitation_method.waterpollutioncategories.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияВодоохраннойТерритории", SqlDbType.Int);
                    parm.Value = rehabilitation_method.waterprotectionarea.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияМЛЗагрязненияПГ", SqlDbType.Int);
                    parm.Value = rehabilitation_method.soilcleaningmethod.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияМЛЗагрязненияГВ", SqlDbType.Int);
                    parm.Value = rehabilitation_method.watercleaningmethod.type_code;
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
                    rc = (int)cmd.Parameters["@exitrc"].Value == rehabilitation_method.type_code;
              }
                catch (Exception e)
                {
                    rc = false;
                };

            }

            return rc;
        }
        static public bool Update(EGH01DB.IDBContext dbcontext, RehabilitationMethod rehabilitation_method)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateRehabilitationMethod", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКлассификатора", SqlDbType.Int);
                    parm.Value = rehabilitation_method.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.riskobjecttype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НазначениеЗемель", SqlDbType.Int);
                    parm.Value = rehabilitation_method.cadastretype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияНефтепродукта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.petrochemicalcategory.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КлассификацияАварий", SqlDbType.Int);
                    parm.Value = rehabilitation_method.emergencyclass.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияПроникновенияНефтепродукта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.penetrationdepth.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияЗагрязненияГрунта", SqlDbType.Int);
                    parm.Value = rehabilitation_method.soilpollutioncategories.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДостижениеГоризонтаГрунтовыхВод", SqlDbType.Bit);
                    parm.Value = rehabilitation_method.waterachieved;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияЗагрязненияГрунтовыхВод", SqlDbType.Int);
                    parm.Value = rehabilitation_method.waterpollutioncategories.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияВодоохраннойТерритории", SqlDbType.Int);
                    parm.Value = rehabilitation_method.waterprotectionarea.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияМЛЗагрязненияПГ", SqlDbType.Int);
                    parm.Value = rehabilitation_method.soilcleaningmethod.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияМЛЗагрязненияГВ", SqlDbType.Int);
                    parm.Value = rehabilitation_method.watercleaningmethod.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Real);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }

        static public bool Delete(EGH01DB.IDBContext dbcontext, RehabilitationMethod rehabilitation_method)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteRehabilitationMethod", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКлассификатора", SqlDbType.Int);
                    parm.Value = rehabilitation_method.type_code;
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
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }
        static public bool DeleteByCode(EGH01DB.IDBContext dbcontext, int code)
        {
            return Delete(dbcontext, new RehabilitationMethod(code));
        }
        
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("RehabilitationMethod");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);

            rc.SetAttribute("type_code", this.type_code.ToString());
            rc.AppendChild(doc.ImportNode(this.riskobjecttype.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.petrochemicalcategory.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.emergencyclass.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.penetrationdepth.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.soilpollutioncategories.toXmlNode(), true));
            rc.SetAttribute("waterachieved", this.waterachieved.ToString());
            rc.AppendChild(doc.ImportNode(this.waterpollutioncategories.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.waterprotectionarea.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.soilcleaningmethod.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.watercleaningmethod.toXmlNode(), true));

            return (XmlNode)rc;

        }

    }
    public class RehabilitationMethodList : List<RehabilitationMethod>
    {
        List<EGH01DB.Types.RehabilitationMethod> list_rehabilitation_method = new List<EGH01DB.Types.RehabilitationMethod>();
        public RehabilitationMethodList()
        {

        }
        public RehabilitationMethodList(List<RehabilitationMethod> list)
            : base(list)
        {

        }
        public RehabilitationMethodList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListRehabilitationMethod(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("RehabilitationMethodList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }

    }
}
