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
        static public bool GetWaterObject(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out EcoObject water_object) // водные объекты - реки, озера, каналы, водохранилища
        {
            bool rc = false;
            water_object = new EcoObject();
            using (SqlCommand cmd = new SqlCommand("MAP.InWaterObjectMap", dbcontext.connection))
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
                        string type = (string)reader["type"];
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
                        string name = (string)reader["vodozabor"];
                        water_intake = new WaterProtectionArea(-1, name);

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
                        water_protection = new WaterProtectionArea(-1, name);
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

        // #8 Получение карты защищенности подземных вод

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

        // #18 Получение карты среднемесячных температур


        // #19 Получение карты четвертичных отложений


        // #20 Получение карты особо охраняемых природных территорий локального значения (point)


        // #21 Получение карты особо охраняемых природных территорий локального значения (polygon)


        // #22 Получение карты особо охраняемых природных территорий национального значения (polygon)


        // #23 Получение карты особо охраняемых природных территорий республиканского значения (point)



        // #24 Получение карты особо охраняемых природных территорий республиканского значения (polygon)



        // #25 Получение карты районированной защищенности геологической среды


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
