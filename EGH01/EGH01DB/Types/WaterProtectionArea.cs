using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;

// Категории водоохранной территории - WaterProtectionAreas

namespace EGH01DB.Types
{
   public class WaterProtectionArea
    {
        public int                 type_code   {get; private set; }   // код водоохранной категории 
        public string              name        {get; private set; }   // наименование водоохранной категории
        static public WaterProtectionArea defaulttype { get { return new WaterProtectionArea(0, "Не определен"); } }  // выдавать при ошибке  
      
        public WaterProtectionArea()
        {
            this.type_code = -1;
            this.name = string.Empty;
        }
        public WaterProtectionArea(int code)
        {
            this.type_code = code;
            this.name = "";
        }
        public WaterProtectionArea(int code, String name)
        {
            this.type_code = code;
            this.name = name;
        }
        public WaterProtectionArea(XmlNode node)
        {
            this.type_code = Helper.GetIntAttribute(node, "type_code", -1);
            this.name = Helper.GetStringAttribute(node, "name", "");
        }
        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextWaterProtectionAreaCode", dbcontext.connection))
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
        static public bool Create(EGH01DB.IDBContext dbcontext, WaterProtectionArea water_protection_area)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateWaterProtectionArea", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

               {
                   SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
                    int new_cat_code = 0;
                    if (GetNextCode(dbcontext, out new_cat_code)) water_protection_area.type_code = new_cat_code;
                    parm.Value = water_protection_area.type_code;
                    cmd.Parameters.Add(parm);
               }
               {
                   SqlParameter parm = new SqlParameter("@НаименованиеКатегории", SqlDbType.NVarChar);
                   parm.Value = water_protection_area.name;
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
        static public bool Update(EGH01DB.IDBContext dbcontext, WaterProtectionArea water_protection_area)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateWaterProtectionArea", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
                    parm.Value = water_protection_area.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеКатегории", SqlDbType.NVarChar);
                    parm.Value = water_protection_area.name;
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
            return Delete(dbcontext, new WaterProtectionArea(code));
        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, WaterProtectionArea water_protection_area)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteWaterProtectionArea", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаКатегории", SqlDbType.Int);
                    parm.Value = water_protection_area.type_code;
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
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int code, out WaterProtectionArea water_protection_area)
        {
            bool rc = false;
            water_protection_area = new WaterProtectionArea();
            using (SqlCommand cmd = new SqlCommand("EGH.GetWaterProtectionAreaByCode", dbcontext.connection))
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
                        string name = (string)reader["НаименованиеКатегории"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) water_protection_area = new WaterProtectionArea(code, name);

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
            XmlElement rc = doc.CreateElement("WaterProtectionArea");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("type_code", this.type_code.ToString());
            rc.SetAttribute("name", this.name.ToString());
            return (XmlNode)rc;
        }
    }
    public class WaterProtectionAreaList : List<WaterProtectionArea>
    {
        List<EGH01DB.Types.WaterProtectionArea> list_waterpropertionArea = new List<EGH01DB.Types.WaterProtectionArea>();
        public WaterProtectionAreaList()
        {

        }
        public WaterProtectionAreaList(List<WaterProtectionArea> list)
            : base(list)
        {

        }
        public WaterProtectionAreaList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListWaterProtectionArea(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("WaterProtectionArea");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
    }
}
