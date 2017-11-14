using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Objects;
using EGH01DB.Types;
using EGH01DB.Primitives;


namespace EGH01DB.Types
{
    public class MapType
    {
        public string x { get; private set; }     // долгота
        public string y { get; private set; }     // широта

        public MapType()
        {
            this.x = this.y = "";
        }
        public MapType(string x, string y)
        {
            this.x = x;
            this.y = y;
        }

        static public bool GetGroundType(MapType point, EGH01DB.IDBContext dbcontext, out GroundType ground_type) // коэффициенты: пористость, фильтрации и влагоемкость - требуется замена карты!!!!
        {
            bool rc = false;
            ground_type = new GroundType();
            using (SqlCommand cmd = new SqlCommand("EGH.InKoefMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
                        float watercapacity = (float)reader["vlagoemkos"];
                        float waterfilter = (float)reader["k_filtraci"];
                        float porosity = (float)reader["poristost"];
                        ground_type = new GroundType(-1, "Получено из карты коэффициентов грунта", porosity, 85.2f, waterfilter, 0.0f, 0.0f, 0.0f, watercapacity, 0.4f, 6.0f, 0.0f, 1750.0f);
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
        static public bool GetDistrict(MapType point, EGH01DB.IDBContext dbcontext, out District district) // район и область
        {
            bool rc = false;
            district = new District();
            using (SqlCommand cmd = new SqlCommand("EGH.InRegionMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
                        string district_name = (string)reader["district"];
                        string region_name = (string)reader["region"];

                        Region region = new Region(region_name);
                        district = new District(-1, region, district_name);

                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
        static public bool GetSoilType(MapType point, EGH01DB.IDBContext dbcontext, out SoilType soil_type)  // тип почвы, высота почвенного слоя и коэффициенты
        {
            bool rc = false;
            soil_type = new SoilType();
            using (SqlCommand cmd = new SqlCommand("EGH.InSoilMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
                        string name = (string)reader["name"];
                        float filter = (float)reader["kf"];
                        float porosity = (float)reader["p"];
                        float watercapacity = (float)reader["kv"];
                        float gumus_height = (float)reader["gumus"];
                        string klass = (string)reader["klass"];
                        string soil_type_name = (string)reader["type"];
                        soil_type = new SoilType(name, filter, porosity, watercapacity, gumus_height, klass, soil_type_name);
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
        static public bool GetHeight(MapType point, EGH01DB.IDBContext dbcontext, out float height)  // высота над уровнем моря - требуется замена карты!!!!
        {
            bool rc = false;
            height = 0.0f;
            using (SqlCommand cmd = new SqlCommand("EGH.InTopographyMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
                        height = (float)reader["height"];
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
        static public bool GetWaterdeep(MapType point, EGH01DB.IDBContext dbcontext, out float waterdeep) // глубина грунтовых вод
        {
            bool rc = false;
            waterdeep = 0.0f;
            using (SqlCommand cmd = new SqlCommand("EGH.InGroundWaterMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
                        waterdeep = (float)reader["h"];
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
        static public bool GetSelfCleaningZone(MapType point, EGH01DB.IDBContext dbcontext, out string self_cleaning_zone) // способность к самоочищению почв
        {
            bool rc = false;
            self_cleaning_zone = "";
            using (SqlCommand cmd = new SqlCommand("EGH.InGroundSelfCleaningMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    int eee = cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int region_name_code = (int)reader["Obj_Id"];
                        self_cleaning_zone = (string)reader["type"];
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
        static public bool GetTimeMigration(MapType point, EGH01DB.IDBContext dbcontext, out float time_migration)  // время миграции до грунтовых вод
        {
            bool rc = false;
            time_migration = 0.0f;
            using (SqlCommand cmd = new SqlCommand("EGH.InTimeMigrationMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
        static public bool GetEcoObject(MapType point, EGH01DB.IDBContext dbcontext, out EcoObject eco_object) // охраняемые объекты - заповедники и нацпарки
        {
            bool rc = false;
            eco_object = new EcoObject();
            using (SqlCommand cmd = new SqlCommand("EGH.InEcoObjectMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
                        int id = (int)reader["Obj_Id"];
                        string name = (string)reader["name"];
                        string type = (string)reader["type"];
                        EcoObjectType eco_object_type = new EcoObjectType(type);
                        CadastreType cadastre_type = new CadastreType("не определено на карте");

                        eco_object = new EcoObject(name, eco_object_type, cadastre_type);

                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
        static public bool GetWaterObject(MapType point, EGH01DB.IDBContext dbcontext, out EcoObject water_object) // водные объекты - реки, озера, каналы, водохранилища
        {
            bool rc = false;
            water_object = new EcoObject();
            using (SqlCommand cmd = new SqlCommand("EGH.InWaterObjectMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
                        int id = (int)reader["Obj_Id"];
                        string name = (string)reader["name"];
                        string type = (string)reader["type"];
                        EcoObjectType eco_object_type = new EcoObjectType(type);
                        CadastreType cadastre_type = new CadastreType("Водный объект");

                        water_object = new EcoObject(name, eco_object_type, cadastre_type, true);

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
        static public bool GetCity(MapType point, EGH01DB.IDBContext dbcontext, out string city)  // Город
        {
            bool rc = false;
            city = "";
            string map_point = "Point("+point.x+" "+point.y+")";
            using (SqlCommand cmd = new SqlCommand("EGH.InCityMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar, 45);// сделала костыль - заранее собрала строку
                    parm.Value = map_point;
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
                        int city_name_code = (int)reader["Obj_Id"];
                        city = (string)reader["name"];
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
        static public bool GetWaterProtection(MapType point, EGH01DB.IDBContext dbcontext, out WaterProtectionArea water_protection) // водоохраняемая зона
        {
            bool rc = false;
            water_protection = new WaterProtectionArea(-1, "Не является водоохраняемой зоной");
            using (SqlCommand cmd = new SqlCommand("EGH.InWaterProtectionMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
        static public bool GetWaterIntake(MapType point, EGH01DB.IDBContext dbcontext, out WaterProtectionArea water_intake) // зона водозабора
        {
            bool rc = false;
            water_intake = new WaterProtectionArea(-1, "Не является зоной водозабора");
            using (SqlCommand cmd = new SqlCommand("EGH.InWaterIntakeZoneMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
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
