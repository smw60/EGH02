using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;

// 1.1.1.1.21	 Категории проникновения нефтепродукта (PenetrationDepth)

namespace EGH01DB.Types
{
   public class PenetrationDepth
    {
        public int type_code { get; private set; }      // код категории
        public string name { get; private set; }        // наименование категории
        public float mindepth { get; private set; }     // минимальное значение диапазона
        public float maxdepth { get; private set; }     // максимальное значение диапазона

        static public PenetrationDepth defaulttype { get { return new PenetrationDepth (0, "Не определен", 0.0f, 0.0f); } }  // выдавать при ошибке  
      
        public PenetrationDepth()
        {
            this.type_code = -1;
            this.name = string.Empty;
            this.mindepth = 0.0f;
            this.maxdepth = 0.0f;
        }
        public PenetrationDepth(int code)
        {
            this.type_code = code;
            this.name = string.Empty;
            this.mindepth = 0.0f;
            this.maxdepth = 0.0f;
        }
    
        public PenetrationDepth(int code, String name, float min, float max)
        {
            this.type_code = code;
            this.name = name;
            this.mindepth = min;
            this.maxdepth = max;
        }
        public PenetrationDepth(XmlNode node)
        {
            this.type_code = Helper.GetIntAttribute(node, "code", -1);
            this.name = Helper.GetStringAttribute(node, "name", "");
            this.mindepth = Helper.GetFloatAttribute(node, "mindepth", 0.0f);
            this.maxdepth = Helper.GetFloatAttribute(node, "maxdepth", 0.0f);
        }
        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextPenetrationDepthCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
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
                    code = (int)cmd.Parameters["@КодТипаКатегории"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, PenetrationDepth penetration_depth)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreatePenetrationDepth", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    int new_type_code = 0;
                    if (GetNextCode(dbcontext, out new_type_code)) penetration_depth.type_code = new_type_code;
                    SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
                    parm.Value = penetration_depth.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаКатегории", SqlDbType.NVarChar);
                    parm.Value = penetration_depth.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинДиапазон", SqlDbType.Real);
                    parm.Value = penetration_depth.mindepth;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксДиапазон", SqlDbType.Real);
                    parm.Value = penetration_depth.maxdepth;
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
        static public bool Update(EGH01DB.IDBContext dbcontext, PenetrationDepth penetration_depth)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdatePenetrationDepth", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
                    parm.Value = penetration_depth.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаКатегории", SqlDbType.VarChar);
                    parm.Value = penetration_depth.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинДиапазон", SqlDbType.Real);
                    parm.Value = penetration_depth.mindepth;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксДиапазон", SqlDbType.Real);
                    parm.Value = penetration_depth.maxdepth;
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
            return Delete(dbcontext, new PenetrationDepth(code));
        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, PenetrationDepth penetration_depth)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeletePenetrationDepth", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
                    parm.Value = penetration_depth.type_code;
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
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int code, out PenetrationDepth penetration_depth)
        {
            bool rc = false;
            penetration_depth = new PenetrationDepth();
            using (SqlCommand cmd = new SqlCommand("EGH.GetPenetrationDepthByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
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
                        int re_code = (int)reader["КодТипаКатегории"];
                        string name = (string)reader["НаименованиеТипаКатегории"];
                        float min = (float)reader["МинДиапазон"];
                        float max = (float)reader["МаксДиапазон"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) penetration_depth = new PenetrationDepth(code, name, min, max);

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
        static public bool GetByDepth(EGH01DB.IDBContext dbcontext, float depth, out PenetrationDepth penetration_depth)
        {
            bool rc = false;
            penetration_depth = PenetrationDepth.defaulttype; 
            using (SqlCommand cmd = new SqlCommand("EGH.GetPenetrationDepthByDepth", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@Глубина", SqlDbType.Float);
                    parm.Value = depth;
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
                        int code = (int)reader["КодТипаКатегории"];
                        string name = (string)reader["НаименованиеТипаКатегории"];
                        float min = (float)reader["МинДиапазон"];
                        float max = (float)reader["МаксДиапазон"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) penetration_depth = new PenetrationDepth(code, name, min, max);

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
            XmlElement rc = doc.CreateElement("PenetrationDepth");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("code", this.type_code.ToString());
            rc.SetAttribute("name", this.name.ToString());
            rc.SetAttribute("mindepth", this.mindepth.ToString());
            rc.SetAttribute("maxdepth", this.maxdepth.ToString());
            return (XmlNode)rc;
        }
    }
    public class PenetrationDepthList : List<PenetrationDepth>
    {
        List<EGH01DB.Types.PenetrationDepth> list_penetrationDepth = new List<EGH01DB.Types.PenetrationDepth>();
        public PenetrationDepthList()
        {

        }
        public PenetrationDepthList(List<PenetrationDepth> list) : base(list)
        {

        }
        public PenetrationDepthList(EGH01DB.IDBContext dbcontext) : base(Helper.GetListPenetrationDepth(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("PenetrationDepthList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
    }
}
