using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;

namespace EGH01DB.Primitives
{
    public class WaterProperties  // физико-химические свойства воды
    {
        public int water_code { get; private set; }    // код показателя воды
        public float temperature { get; private set; }    // температура , градусы Цельсия
        public float viscocity {get; private set;}    // вязкость , кг/м с 
        public float density   {get; private set;}    // плотность, кг/м3
        public float tension   {get; private set;}    // коэф. поверхностного натяжения , кг/с2 

        public WaterProperties()
        {
            this.water_code = -1;
            this.temperature = 0.0f;
            this.viscocity = 0.0f;
            this.density = 0.0f;
            this.tension = 0.0f;
        }
        public WaterProperties(int water_code)
        {
            this.water_code = water_code;
            this.temperature = 0.0f;
            this.viscocity = 0.0f;
            this.density = 0.0f;
            this.tension = 0.0f;
        }
        public WaterProperties(int water_code, float temperature, float viscocity, float density, float tension)
        {
            this.water_code = water_code;
            this.temperature = temperature;
            this.viscocity = viscocity;
            this.density = density;
            this.tension = tension;
        }
        public WaterProperties(XmlNode node)
        {
            this.water_code = Helper.GetIntAttribute(node, "water_code", -1);
            this.temperature = Helper.GetFloatAttribute(node, "temperature", 0.0f);
            this.viscocity = Helper.GetFloatAttribute(node, "viscocity", 0.0f);
            this.density = Helper.GetFloatAttribute(node, "density", 0.0f);
            this.tension = Helper.GetFloatAttribute(node, "tension", 0.0f);
        }
        public static bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextWaterPropertiesCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодПоказателяВоды", SqlDbType.Int);
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
                    code = (int)cmd.Parameters["@КодПоказателяВоды"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        public static bool Create(EGH01DB.IDBContext dbcontext, WaterProperties waterproperties)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateWaterProperties", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодПоказателяВоды", SqlDbType.Int);
                    int new_water_property_code = 0;
                    if (GetNextCode(dbcontext, out new_water_property_code)) waterproperties.water_code = new_water_property_code;
                    parm.Value = waterproperties.water_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Температура", SqlDbType.Float);
                    parm.Value = waterproperties.temperature;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Вязкость", SqlDbType.Float);
                    parm.Value = waterproperties.viscocity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Плотность", SqlDbType.Float);
                    parm.Value = waterproperties.density;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфПовНат", SqlDbType.Float);
                    parm.Value = waterproperties.tension;
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
                    rc = ((int)cmd.Parameters["@exitrc"].Value > 0);
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }

            return rc;
        }
        public static bool Update(EGH01DB.IDBContext dbcontext, WaterProperties waterproperties)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateWaterProperties", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодПоказателяВоды", SqlDbType.Int);
                    parm.Value = waterproperties.water_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Температура", SqlDbType.Float);
                    parm.Value = waterproperties.temperature;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Вязкость", SqlDbType.Float);
                    parm.Value = waterproperties.viscocity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Плотность", SqlDbType.Float);
                    parm.Value = waterproperties.density;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфПовНат", SqlDbType.Float);
                    parm.Value = waterproperties.tension;
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
        static public bool Delete(EGH01DB.IDBContext dbcontext, WaterProperties waterproperties)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteWaterProperties", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодПоказателяВоды", SqlDbType.Int);
                    parm.Value = waterproperties.water_code;
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
            return Delete(dbcontext, new WaterProperties(code));
        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("WaterProperties");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("water_code", this.water_code.ToString());
            rc.SetAttribute("temperature", this.temperature.ToString());
            rc.SetAttribute("viscocity", this.viscocity.ToString());
            rc.SetAttribute("density", this.density.ToString());
            rc.SetAttribute("tension", this.tension.ToString());
            return (XmlNode)rc;
        }
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int code, out WaterProperties waterproperties)
        {
            bool rc = false;
            waterproperties = new WaterProperties();
            using (SqlCommand cmd = new SqlCommand("EGH.GetWaterPropertiesByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодПоказателяВоды", SqlDbType.Int);
                    parm.Value = code;
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
                        float temperature = (float)reader["Температура"];
                        float viscocity = (float)reader["Вязкость"];
                        float density = (float)reader["Плотность"];
                        float tension = (float)reader["КоэфПовНат"];
                        
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) waterproperties = new WaterProperties(code, (float)temperature, (float)viscocity, (float)density, (float)tension);
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
        public static bool Get(EGH01DB.IDBContext dbcontext, float temp, out  WaterProperties waterproperties, out float delta)
        {
            bool rc = false;
            delta = -1.0f;
            waterproperties = new WaterProperties();
            using (SqlCommand cmd = new SqlCommand("EGH.GetWaterNearTemp", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@Температура", SqlDbType.Float);
                    parm.Value = temp;
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
                        int code = (int)reader["КодПоказателяВоды"];
                        float temperature = (float)reader["Температура"];
                        float viscocity = (float)reader["Вязкость"];
                        float density = (float)reader["Плотность"];
                        float tension = (float) reader["КоэфПовНат"];

                        delta = (float)((double)reader["delta"]);

                        waterproperties = new WaterProperties(code, (float)temperature, (float)viscocity, (float)density, (float)tension);
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

    public class WaterPropertiesList : List<WaterProperties>
    {
        List<EGH01DB.Primitives.WaterProperties> list_water_properties = new List<EGH01DB.Primitives.WaterProperties>();
        public WaterPropertiesList()
        {

        }
        public WaterPropertiesList(List<WaterProperties> list)
            : base(list)
        {

        }
        public WaterPropertiesList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListWaterProperties(dbcontext))
            {

            }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("WaterPropertiesList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
    }
}
