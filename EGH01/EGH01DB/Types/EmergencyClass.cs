using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;

// Классификация аварий 

namespace EGH01DB.Types
{
   public class EmergencyClass
    {
        public int type_code { get; private set; }   // код категории
        public string name { get; private set; }   // наименование категории
        public float minmass { get; private set; }   // минимальное значение диапазона
        public float maxmass { get; private set; }   // максимальное значение диапазона

        static public EmergencyClass defaulttype { get { return new EmergencyClass (0, "Не определен", 0.0f, 0.0f); } }  // выдавать при ошибке  
      
        public EmergencyClass()
        {
            this.type_code = -1;
            this.name = string.Empty;
            this.minmass = 0.0f;
            this.maxmass = 0.0f;
        }
        public EmergencyClass(int code)
        {
            this.type_code = code;
            this.name = string.Empty;
            this.minmass = 0.0f;
            this.maxmass = 0.0f;
        }
    
        public EmergencyClass(int code, String name, float min, float max)
        {
            this.type_code = code;
            this.name = name;
            this.minmass = min;
            this.maxmass = max;
        }
        public EmergencyClass(XmlNode node)
        {
            this.type_code = Helper.GetIntAttribute(node, "code", -1);
            this.name = Helper.GetStringAttribute(node, "name", "");
            this.minmass = Helper.GetFloatAttribute(node, "minmass", 0.0f);
            this.maxmass = Helper.GetFloatAttribute(node, "maxmass", 0.0f);
        }
        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextEmergencyClassCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаАварии", SqlDbType.Int);
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
                    code = (int)cmd.Parameters["@КодТипаАварии"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, EmergencyClass emergency_class)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateEmergencyClass", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    int new_type_code = 0;
                    if (GetNextCode(dbcontext, out new_type_code)) emergency_class.type_code = new_type_code;
                    SqlParameter parm = new SqlParameter("@КодТипаАварии", SqlDbType.Int);
                    parm.Value = emergency_class.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаАварии", SqlDbType.NVarChar);
                    parm.Value = emergency_class.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинМасса", SqlDbType.Real);
                    parm.Value = emergency_class.minmass;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксМасса", SqlDbType.Real);
                    parm.Value = emergency_class.maxmass;
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
        static public bool Update(EGH01DB.IDBContext dbcontext, EmergencyClass emergency_class)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateEmergencyClass", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаАварии", SqlDbType.Int);
                    parm.Value = emergency_class.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаАварии", SqlDbType.VarChar);
                    parm.Value = emergency_class.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинМасса", SqlDbType.Real);
                    parm.Value = emergency_class.minmass;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксМасса", SqlDbType.Real);
                    parm.Value = emergency_class.maxmass;
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
            return Delete(dbcontext, new EmergencyClass(code));
        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, EmergencyClass emergency_class)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteEmergencyClass", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаАварии", SqlDbType.Int);
                    parm.Value = emergency_class.type_code;
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
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int code, out EmergencyClass emergency_class)
        {
            bool rc = false;
            emergency_class = new EmergencyClass();
            using (SqlCommand cmd = new SqlCommand("EGH.GetEmergencyClassByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаАварии", SqlDbType.Int);
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
                        int re_code = (int)reader["КодТипаАварии"];
                        string name = (string)reader["НаименованиеТипаАварии"];
                        float min = (float)reader["МинМасса"];
                        float max = (float)reader["МаксМасса"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) emergency_class = new EmergencyClass(code, name, min, max);

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
        static public bool GetByMass(EGH01DB.IDBContext dbcontext, float mass,  out EmergencyClass emergency_class)
        {
            bool rc = false;
            emergency_class = new EmergencyClass();
            using (SqlCommand cmd = new SqlCommand("EGH.GetEmergencyClassByMass", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@Масса", SqlDbType.Float);
                    parm.Value = mass;
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
                        int code = (int)reader["КодТипаАварии"];
                        string name = (string)reader["НаименованиеТипаАварии"];
                        float min = (float)reader["МинМасса"];
                        float max = (float)reader["МаксМасса"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) emergency_class = new EmergencyClass(code, name, min, max);

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
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("EmergencyClass");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("code", this.type_code.ToString());
            rc.SetAttribute("name", this.name.ToString());
            rc.SetAttribute("minmass", this.minmass.ToString());
            rc.SetAttribute("maxmass", this.maxmass.ToString());
            return (XmlNode)rc;
        }
    }
    public class EmergencyClassList : List<EmergencyClass>
    {
        List<EGH01DB.Types.SoilCleaningMethod> list_soil_cleaning_method = new List<EGH01DB.Types.SoilCleaningMethod>();
        public EmergencyClassList()
        {

        }
        public EmergencyClassList(List<EmergencyClass> list) : base(list)
        {

        }
        public EmergencyClassList(EGH01DB.IDBContext dbcontext) : base(Helper.GetListEmergencyClass(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("EmergencyClassList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
    }
}
