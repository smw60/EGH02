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
    public partial class MapHelper 
        // предназначен для показа данных из карт в приложении 
        // по одному методу на карту

    {
        // #1 Получение района и области из карты административного деления
        static public bool GetRegion(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out District district) 
        {
                    
            bool rc = false;
            district = new District();
            string point = coordinates.GetMapPoint();
            using (SqlCommand cmd = new SqlCommand("MAP.InRegionMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = point;
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
                    if (reader.Read())
                    {
                        string district_name = (string)reader["district"];
                        string region_name = (string)reader["region"];

                        Region region = new Region(region_name);
                        district = new District(-1, region, district_name);

                        rc = true;
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

        // #2 Получение карты водоемов
        static public bool GetWaterPond(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out EcoObject water_object) // водные объекты - озера, водохранилища
        {
            bool rc = false;
            water_object = new EcoObject();
            using (SqlCommand cmd = new SqlCommand("MAP.InPondMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        int id = (int)reader["Obj_Id"];
                        string type = (string)reader["type"];
                        string name = (string)reader["name"];
                        
                        EcoObjectType eco_object_type = new EcoObjectType(type);
                        CadastreType cadastre_type = new CadastreType("Водный объект");

                        water_object = new EcoObject(name, eco_object_type, cadastre_type, true);
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
       
        // #3 Получение карты водозаборов и санитарных зон
        static public bool GetWaterIntake(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out WaterProtectionArea water_intake) // зона водозабора
        {
            bool rc = false;
            water_intake = new WaterProtectionArea(-1, "Не является зоной водозабора");
            using (SqlCommand cmd = new SqlCommand("MAP.InWaterIntakeZoneMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        int id = (int)reader["Obj_Id"];
                        string name = (string)reader["name"];
                        int buffer = (int)reader["buff_distance"];
                        water_intake = new WaterProtectionArea(-1, name, buffer);

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

        // #4 Получение карты водоохранных зон поверх вод
        static public bool GetWaterProtection(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out WaterProtectionArea water_protection) // водоохраняемая зона
        {
            bool rc = false;
            water_protection = new WaterProtectionArea(-1, "Не является водоохраняемой зоной");
            using (SqlCommand cmd = new SqlCommand("MAP.InWaterProtectionMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        int id = (int)reader["Obj_Id"];
                        string name = (string)reader["name"];
                        int buffer = (int)reader["buff_distance"];
                        water_protection = new WaterProtectionArea(-1, name, buffer);
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
        
        // #5 Получение карты времени миграции нефтепродуктов
        static public bool GetTimeMigration(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out float time_migration)  // время миграции до грунтовых вод
        {
            bool rc = false;
            time_migration = 0.0f;
            using (SqlCommand cmd = new SqlCommand("MAP.InTimeMigrationMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        time_migration = (float)reader["migration_time"];

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

        // #6 Получение карты уровней грунтовых вод
        
        // #7 Получение карты густоты речной сети
        static public bool GetRiverDensity(EGH01DB.IDBContext dbcontext, Coordinates coordinates,
                                    out string district, out string region, out string type,
                                    out float network_density, out float length, out float district_area)  // карта густоты речной сети
        {
            bool rc = false;
            district = "";
            region = "";
            type = "";

            network_density = 0.0f;
            length = 0.0f;
            district_area = 0.0f;

            using (SqlCommand cmd = new SqlCommand("MAP.InRiverDensityMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (rc = reader.Read())
                    {
                        int city_name_code = (int)reader["Obj_Id"];

                        district = (string)reader["district"];
                        region = (string)reader["region"];
                        type = (string)reader["type"];

                        network_density = (float)reader["network_density"];
                        length = (float)reader["length"];
                        district_area = (float)reader["district_area"];
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
        
        // #8 Получение карты защищенности подземных вод
        static public bool GetGroundWaterMapProtectLevel(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string protection_level)  // карта защищенности подземных вод
        {
            bool rc = false;
            protection_level = "";

            using (SqlCommand cmd = new SqlCommand("MAP.InProtectGroundWaterMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (rc = reader.Read())
                    {
                        int city_name_code = (int)reader["Obj_Id"];
                        protection_level = (string)reader["protect"];
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

        // #9 Получение карты зон аэрации
        static public bool GetAerationZone(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string aeration_power,
                                       out float average_aeration_power, out float max_aeration_power, out string litology) // Карта зоны аэрации
        {
            bool rc = false;
            aeration_power = "";
            average_aeration_power = 0.0f;
            max_aeration_power = 0.0f;
            litology = "";

            using (SqlCommand cmd = new SqlCommand("MAP.InAerationrMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        aeration_power = (string)reader["aeration_power"];
                        average_aeration_power = (float)reader["average_aeration_power"];
                        max_aeration_power = (float)reader["max_aeration_power"];
                        litology = (string)reader["litology"];
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
        
        // #10 Получение карты населенных пунктов
        static public bool GetCity(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string city)  // Город
        {
            bool rc = false;
            city = "";

            using (SqlCommand cmd = new SqlCommand("MAP.InCityMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (rc = reader.Read())
                    {
                        int city_name_code = (int)reader["Obj_Id"];
                        city = (string)reader["name"];
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

        // #11 Получение карты осадков
        public static bool GetDownfall(IDBContext dbcontext, Coordinates coordinates, out int[] downfall)
        {
            bool rc = false;
            downfall = new int[4];

            using (SqlCommand cmd = new SqlCommand("MAP.InDownfallMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    while (reader.Read())
                    {
                        int id = (int)reader["Obj_Id"];
                        int period = (int)reader["period"];
                        downfall[period - 1] = (int)reader["height"];
                    }
                    reader.Close();
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }
        
        // #12 Получение карты почв
        static public bool GetSoil(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out SoilType soiltype)  // Тип почвы, высота почвенного слоя и коэффициенты
        {
            bool rc = false;
            soiltype = new SoilType();
            using (SqlCommand cmd = new SqlCommand("MAP.InSoilMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        string name = (string)reader["name"];
                        float filter = (float)reader["filter_coef_ave"];
                        float porosity = (float)reader["porosity_ave"];
                        float watercapacity = (float)reader["cap_moisture_capacity"];
                        float gumus_depth = (float)reader["gumus_depth"];

                        string mehsost = (string)reader["mehsost"];
                        string soil_type = (string)reader["soil_type"];
                        string soil_subtype = (string)reader["soil_subtype"];

                        float humidity = (float)reader["humidity"];
                        float petrol_filtration_coef = (float)reader["petrol_filtration_coef"];
                        float fuel_oil_filtration_coef = (float)reader["fuel_oil_filtration_coef"];
                        float diesel_filtration_coef = (float)reader["diesel_filtration_coef"];

                        float hydrolytic_acidity = (float)reader["hydrolytic_acidity"];
                        float oil_capacity = (float)reader["oil_capacity"];
                        float density = (float)reader["density"];

                        string carbon_content = (string)reader["carbon_content"];
                        string filter_coef_interval = (string)reader["filter_coef_interval"];
                        string porosity_interval = (string)reader["porosity_interval"];
                        string glina_interval = (string)reader["glina_interval"];
                        string mg_interval = (string)reader["mg_interval"];
                        string ph_interval = (string)reader["ph_interval"];
                        string density_interval = (string)reader["density_interval"];
                        string max_moisture_capacity_interval = (string)reader["max_moisture_capacity_interval"];
                        string gumus_percentage_interval = (string)reader["gumus_percentage_interval"];

                        string klass = (string)reader["klass"];
                        string soil_class_code = (string)reader["soil_class_code"];
                        string wrb_code = (string)reader["wrb_code"];
                        string wrb_new_code = (string)reader["wrb_new_code"];
                        string rgb_code = (string)reader["rgb_code"];
                        string shtrihovka = (string)reader["shtrihovka"];

                        soiltype = new SoilType(name, filter, porosity, watercapacity, gumus_depth, mehsost, soil_type, soil_subtype,
                                                humidity, petrol_filtration_coef, fuel_oil_filtration_coef, diesel_filtration_coef, hydrolytic_acidity,
                                                oil_capacity, density, carbon_content, filter_coef_interval, porosity_interval, glina_interval,
                                                mg_interval, ph_interval, density_interval, max_moisture_capacity_interval, gumus_percentage_interval,
                                                klass, soil_class_code, wrb_code, wrb_new_code, rgb_code, shtrihovka);
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

        // #13 Получение карты растительности
        static public bool GetVegetation(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string assoc_gr_1,
                                        out string shtrih_cod, out string code_legen, out string class_form, 
                                        out string associatio, out string code_rgb, out string type_rasty, out string krap_code, out string dop_code)
        {
                     
            bool rc = false;
            assoc_gr_1 = "";
            shtrih_cod = "";
            code_legen = "";
            class_form = "";
            associatio = "";
            code_rgb = "";
            type_rasty = "";
            krap_code = "";
            dop_code = "";

            using (SqlCommand cmd = new SqlCommand("MAP.InVegetationMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        assoc_gr_1 = (string)reader["assoc_gr_1"];
                        shtrih_cod = (string)reader["shtrih_cod"];
                        code_legen = (string)reader["code_legen"];
                        class_form = (string)reader["class_form"];
                        associatio = (string)reader["associatio"];
                        code_rgb = (string)reader["code_rgb"];
                        type_rasty = (string)reader["type_rasty"];
                        krap_code = (string)reader["krap_code"];
                        dop_code = (string)reader["dop_code"];
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

        // #14 Получение карты рельефа
        static public bool GetHeight(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out float height)  // Высота над уровнем моря
        {
            bool rc = false;
            height = 0.0f;
            using (SqlCommand cmd = new SqlCommand("MAP.InTopographyMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        height = (float)reader["height"];
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

        // #15 Получение карты геологической
        static public bool GetGeology(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string aeration_power,
                                       out float average_aeration_power, out float max_aeration_power, out string litology) // Карта зоны аэрации
        {
            bool rc = false;
            aeration_power = "";
            average_aeration_power = 0.0f;
            max_aeration_power = 0.0f;
            litology = "";

            using (SqlCommand cmd = new SqlCommand("MAP.InAerationrMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        aeration_power = (string)reader["aeration_power"];
                        average_aeration_power = (float)reader["average_aeration_power"];
                        max_aeration_power = (float)reader["max_aeration_power"];
                        litology = (string)reader["litology"];
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
        

        // #16 Получение карты самоочищения почв
        static public bool GetSelfCleaningZone(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string self_cleaning_zone) // Способность к самоочищению почв
        {
            bool rc = false;
            self_cleaning_zone = "";
            using (SqlCommand cmd = new SqlCommand("MAP.InGroundSelfCleaningMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        int region_name_code = (int)reader["Obj_Id"];
                        self_cleaning_zone = (string)reader["type"];
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
        
        // #17 Получение карты солнечной радиации
        public static bool GetSunRadiation(IDBContext dbcontext, Coordinates coordinates, out SunRadiation[] sunradiation)
        {
            bool rc = false;
            sunradiation = new SunRadiation[2];

            using (SqlCommand cmd = new SqlCommand("MAP.InSunRadiationMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    while (reader.Read())
                    {
                        int id = (int)reader["Obj_Id"];
                        int period = (int)reader["period"];
                        string layer = (string)reader["layer"];
                        int from_rad = (int)reader["from_rad"];
                        int to_rad = (int)reader["to_rad"];
                        float average_rad = (float)reader["average_rad"];
                        sunradiation[period - 1] = new SunRadiation(layer, from_rad, to_rad, average_rad);
                    }
                    reader.Close();
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }

        // #18 Получение карты среднемесячных температур
        public static bool GetMonthTemperature(IDBContext dbcontext, Coordinates coordinates, out Climat climat)
        {
            bool rc = false;
            climat = new Climat(dbcontext, coordinates);
            using (SqlCommand cmd = new SqlCommand("MAP.InMonthTempMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    float[] temperature = new float[12];
                    while (reader.Read())
                    {
                        int id = (int)reader["Obj_Id"];
                        string period = (string)reader["period"];
                        int p = Convert.ToInt32(period);
                        temperature[p - 1] = (float)reader["temperature"];
                    }
                    reader.Close();
                    climat = new Climat(dbcontext, coordinates, temperature);
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }

        // #19 Получение карты четвертичных отложений
        static public bool GetQuatSediments(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string gorizont_power,
                                       out string gorizont_name, out string litology, out string genеtic_type, out float genesys_code,
                                       out string otdel, out string podotdel, out string podgorizon, out float litology_code,
                                       out string geology_index, out string rgb, out string sistema)
        {
            
            bool rc = false;
            gorizont_power = "";
            gorizont_name = "";
            litology = "";
            genеtic_type = "";
            genesys_code = 0.0f;
            otdel = "";
            podotdel = "";
            podgorizon = "";
            litology_code = 0.0f;
            geology_index = "";
            rgb = "";
            sistema = "";
            
            using (SqlCommand cmd = new SqlCommand("MAP.QuatSedimentsMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        gorizont_power = (string)reader["gorizont_power"];
                        gorizont_name = (string)reader["gorizont_name"];
                        litology = (string)reader["litology"];
                        genеtic_type = (string)reader["genеtic_type"];

                        genesys_code = (float)reader["genesys_code"];

                        otdel = (string)reader["otdel"];
                        podotdel = (string)reader["podotdel"];
                        podgorizon = (string)reader["podgorizon"];
                        litology_code = (float)reader["litology_code"];
                        geology_index = (string)reader["geology_index"];

                        rgb = (string)reader["rgb"];
                        sistema = (string)reader["sistema"];
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


        // #20 Получение карты особо охраняемых природных территорий локального значения (point)
        static public bool GetEcoLocalPoint(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string localpoint)  
        {
            bool rc = false;
            localpoint = "Не является особо охраняемой природной территорией локального значения";

            using (SqlCommand cmd = new SqlCommand("MAP.EcoObjectLocalPoint", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (reader.Read())
                    {
                        int code = (int)reader["Obj_Id"];
                        localpoint = (string)reader["name"];
                    }
                    reader.Close();
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }

        // #21 Получение карты особо охраняемых природных территорий локального значения (polygon)
        static public bool GetEcoLocalPoly(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string localpoly)
        {
            bool rc = false;
            localpoly = "Не является особо охраняемой природной территорией локального значения";

            using (SqlCommand cmd = new SqlCommand("MAP.EcoObjectLocalPoly", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (reader.Read())
                    {
                        int code = (int)reader["Obj_Id"];
                        localpoly = (string)reader["name"];
                    }
                    reader.Close();
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }

        // #22 Получение карты особо охраняемых природных территорий национального значения (polygon)
        static public bool GetEcoNational(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string nationalpark)
        {
            bool rc = false;
            nationalpark = "Не является особо охраняемой природной территорией национального значения";

            using (SqlCommand cmd = new SqlCommand("MAP.EcoObjectNational", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (reader.Read())
                    {
                        int code = (int)reader["Obj_Id"];
                        nationalpark = (string)reader["name"];
                    }
                    reader.Close();
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }


        // #23 Получение карты особо охраняемых природных территорий республиканского значения (point)
        static public bool GetEcoRepublicPoint(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string republicpoint)
        {
            bool rc = false;
            republicpoint = "Не является особо охраняемой природной территорией республиканского значения";

            using (SqlCommand cmd = new SqlCommand("MAP.EcoObjectRepublicPoint", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (reader.Read())
                    {
                        int code = (int)reader["Obj_Id"];
                        republicpoint = (string)reader["name"];
                        
                    }
                    reader.Close();
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }


        // #24 Получение карты особо охраняемых природных территорий республиканского значения (polygon)
        static public bool GetEcoRepublicPoly(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string republicpoly)
        {
            bool rc = false;
            republicpoly = "Не является особо охраняемой природной территорией республиканского значения";

            using (SqlCommand cmd = new SqlCommand("MAP.EcoObjectRepublicPoly", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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

                    if (reader.Read())
                    {
                        int code = (int)reader["Obj_Id"];
                        republicpoly = (string)reader["name"];

                    }
                    reader.Close();
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }


        // #25 Получение карты районированной защищенности геологической среды
        static public bool GetGroundProtectZone(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string protect_zone, out int protect_grade) 
        {
            bool rc = false;
            protect_zone = "";
            protect_grade = 0;

            using (SqlCommand cmd = new SqlCommand("MAP.InGroundProtectMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        int code = (int)reader["Obj_Id"];
                        protect_zone = (string)reader["protection_name"];
                        protect_grade = (int)reader["protection_grade"];
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

        // #26 Получение карты коэффициентов грунта
        static public bool GetGroundCoef(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out GroundType ground_type)
        {
            bool rc = false;
            ground_type = new GroundType();
            using (SqlCommand cmd = new SqlCommand("MAP.InGroundCoefMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
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
                    if (rc = reader.Read())
                    {
                        float watercapacity = (float)reader["watercapacity"];
                        float waterfilter = (float)reader["waterfilter"];
                        float porosity = (float)reader["porosity"];
                        ground_type = new GroundType(-1, "Получено из карты коэффициентов грунта", porosity, 85.2f, waterfilter, 0.0f, 0.0f, 0.0f, watercapacity, 0.4f, 6.0f, 0.0f, 1750.0f);
                    }
                    reader.Close();
                    // porosity          пористость     >0    <1, безразмерная , доля застрявшего  в грунте нефтепродукта       
                    // holdmigration    коэфф. задержки миграции нефтепродуктов 
                    // waterfilter      коэфф. фильтрации воды
                    // diffusion       коэфф. диффузии
                    // distribution   коэфф. распределения
                    // sorption       коэфф. сорбции
                    // watercapacity   капиллярная влагоемкость (от 0 до 1)
                    // soilmoisture     влажность грунта (от 0 до 1)
                    // аveryanovfactor  коэффициент Аверьянова (от 4 до 9)
                    // permeability    водопроницаемость м/с
                    // density   плотность м/с
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }
    }
}
