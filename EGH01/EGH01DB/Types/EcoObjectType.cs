using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;


namespace EGH01DB.Types
{
    public class EcoObjectType
    {
        public int type_code { get; private set; }   // код типа природоохрнного объекта объекта 
        public string name { get; private set; }     // наименование типа природоохранного  объекта
        public WaterProtectionArea waterprotectionarea { get; private set; }   // Категории водоохранной территории - WaterProtectionAreas
        static public EcoObjectType defaulttype { get { return new EcoObjectType(0, "Не определен", null); } }  // выдавать при ошибке  
      
        public EcoObjectType()
        {
            this.type_code = -1;
            this.name = string.Empty;
            this.waterprotectionarea = null;
        }

        public EcoObjectType(int type_code, String name, WaterProtectionArea waterprotectionarea)
        {
            this.type_code = type_code;
            this.name = name;
            this.waterprotectionarea = waterprotectionarea;

        }
        public EcoObjectType(int type_code)
        {
            this.type_code = type_code;
            this.name = "";
            this.waterprotectionarea = null;
        }

        public EcoObjectType(String name)
        {
            this.type_code = -1;
            this.name = name;
            this.waterprotectionarea = null;
        }
        public EcoObjectType(XmlNode node)
        {
            this.type_code = Helper.GetIntAttribute(node, "type_code", -1);
            this.name = Helper.GetStringAttribute(node, "name", "");

            XmlNode waterprotectionarea = node.SelectSingleNode(".//WaterProtectionArea");
            if (waterprotectionarea != null) this.waterprotectionarea = new WaterProtectionArea(waterprotectionarea);
            else this.waterprotectionarea = null;
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, EcoObjectType ecoobject_type)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateEcoObjectType", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаПриродоохранногоОбъекта", SqlDbType.Int);
                        int new_ecoobject_type_code = 0;
                        if (GetNextCode(dbcontext, out new_ecoobject_type_code)) ecoobject_type.type_code = new_ecoobject_type_code;
                    parm.Value = ecoobject_type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаПриродоохранногоОбъекта", SqlDbType.VarChar);
                    parm.Value = ecoobject_type.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияВодоохрТер", SqlDbType.Int);
                    parm.IsNullable = true;
                    parm.Value = ecoobject_type.waterprotectionarea.type_code;
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
                    rc = (int)cmd.Parameters["@exitrc"].Value == ecoobject_type.type_code;
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }

            return rc;
        }

        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextEcoObjectTypeCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаПриродоохранногоОбъекта", SqlDbType.Int);
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
                    code = (int)cmd.Parameters["@КодТипаПриродоохранногоОбъекта"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }

        static public bool Update(EGH01DB.IDBContext dbcontext, EcoObjectType ecoobject_type)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateEcoObjectType", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаПриродоохранногоОбъекта", SqlDbType.Int);
                    parm.Value = ecoobject_type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаПриродоохранногоОбъекта", SqlDbType.VarChar);
                    parm.Value = ecoobject_type.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КатегорияВодоохрТер", SqlDbType.Int);
                    parm.IsNullable = true;
                    parm.Value = ecoobject_type.waterprotectionarea.type_code;
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

        static public bool Delete(EGH01DB.IDBContext dbcontext, EcoObjectType ecoobject_type)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteEcoObjectType", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаПриродоохранногоОбъекта", SqlDbType.Int);
                    parm.Value = ecoobject_type.type_code;
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
            return Delete(dbcontext, new EcoObjectType(code));
        }
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int type_code, out EcoObjectType eco_object_type)
        {
            bool rc = false;
            eco_object_type = new EcoObjectType();
            using (SqlCommand cmd = new SqlCommand("EGH.GetEcoObjectTypeByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаПриродоохранногоОбъекта", SqlDbType.Int);
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
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int code = (int)reader["КодТипаПриродоохранногоОбъекта"];
                        string name = (string)reader["НаименованиеТипаПриродоохранногоОбъекта"];

                        int cat_water_name = (int)reader["КатегорияВодоохрТер"];
                        int category_code = (int)reader["КодТипаКатегории"];
                        string category_name = (string)reader["НаименованиеКатегории"];
                        WaterProtectionArea waterprotectionarea = new WaterProtectionArea(category_code, category_name);
                        eco_object_type = new EcoObjectType(code, name, waterprotectionarea);
                     }
                    
                    reader.Close();
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
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
            XmlElement rc = doc.CreateElement("EcoObjectType");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("type_code", this.type_code.ToString());
            rc.SetAttribute("name", this.name.ToString());
            rc.AppendChild(doc.ImportNode(this.waterprotectionarea.toXmlNode(), true));
            return (XmlNode)rc;
        }
    }
    public class EcoObjectTypeList : List<EcoObjectType>
    {
        public EcoObjectTypeList()
        {

        }

        public EcoObjectTypeList(List<EcoObjectType> list)
              : base(list)
        {

        }

        public EcoObjectTypeList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListEcoObjectType(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {

            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("EcoObjectTypeList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);

            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));

            return (XmlNode)rc;
        }
    }
    }
