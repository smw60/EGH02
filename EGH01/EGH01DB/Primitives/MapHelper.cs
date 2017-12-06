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
    public partial class MapHelper // предназначен для показа данных из карт в приложении -- еще не готовы
    {
        static public bool GetGroundType(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out GroundType ground_type) // коэффициенты: пористость, фильтрации и влагоемкость - требуется замена карты!!!!
        {
            bool rc = false;
            ground_type = new GroundType();
            using (SqlCommand cmd = new SqlCommand("MAP.InKoefMap", dbcontext.connection))
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
                        float watercapacity = (float)reader["vlagoemkos"];
                        float waterfilter = (float)reader["k_filtraci"];
                        float porosity = (float)reader["poristost"];
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

        static public bool GetSoilType(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out SoilType soil_type)  // тип почвы, высота почвенного слоя и коэффициенты
        {
            bool rc = false;
            soil_type = new SoilType();
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
                        float filter = (float)reader["kf"];
                        float porosity = (float)reader["p"];
                        float watercapacity = (float)reader["kv"];
                        float gumus_height = (float)reader["gumus"];
                        string klass = (string)reader["klass"];
                        string soil_type_name = (string)reader["type"];
                        soil_type = new SoilType(name, filter, porosity, watercapacity, gumus_height, klass, soil_type_name);
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
        static public bool GetHeight(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out float height)  // высота над уровнем моря
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
        static public bool GetWaterdeep(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out float waterdeep) // глубина грунтовых вод
        {
            bool rc = false;
            waterdeep = 0.0f;
            using (SqlCommand cmd = new SqlCommand("MAP.InGroundWaterMap", dbcontext.connection))
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
                        waterdeep = (float)reader["height"];
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
        static public bool GetSelfCleaningZone(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out string self_cleaning_zone) // способность к самоочищению почв
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

    }
}
