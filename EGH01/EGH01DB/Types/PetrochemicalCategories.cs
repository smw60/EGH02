using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;

// категории нефтепродукта

namespace EGH01DB.Types
{
    public class PetrochemicalCategories
    {
        public int                 type_code   {get; private set; }   // код категории нефтепродукта
        public string              name        {get; private set; }   // наименование категории нефтепроукта
        static public PetrochemicalCategories defaulttype { get { return new PetrochemicalCategories (0, "Не определен"); } }  // выдавать при ошибке  
      
        public PetrochemicalCategories()
        {
            this.type_code = -1;
            this.name = string.Empty;
        }
        public PetrochemicalCategories(int code)
        {
            this.type_code = code;
            this.name = "";
        }
        public PetrochemicalCategories(int code, String name)
        {
            this.type_code = code;
            this.name = name;
        }
        public PetrochemicalCategories(XmlNode node)
        {
            this.type_code = Helper.GetIntAttribute(node, "type_code", -1);
            this.name = Helper.GetStringAttribute(node, "name", "");
        }
        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextPetrochemicalCategoriesCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииНефтепродукта", SqlDbType.Int);
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
                    code = (int)cmd.Parameters["@КодКатегорииНефтепродукта"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, PetrochemicalCategories petrochemical_categories)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreatePetrochemicalCategories", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

               {
                   SqlParameter parm = new SqlParameter("@КодКатегорииНефтепродукта", SqlDbType.Int);
                    int new_petrochemical_cat_code = 0;
                    if (GetNextCode(dbcontext, out new_petrochemical_cat_code)) petrochemical_categories.type_code = new_petrochemical_cat_code;
                    parm.Value = petrochemical_categories.type_code;
                    cmd.Parameters.Add(parm);
               }
               {
                   SqlParameter parm = new SqlParameter("@НаименованиеКатегорииНефтепродукта", SqlDbType.NVarChar);
                   parm.Value = petrochemical_categories.name;
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
        static public bool Update(EGH01DB.IDBContext dbcontext, PetrochemicalCategories petrochemical_categories)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdatePetrochemicalCategories", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииНефтепродукта", SqlDbType.Int);
                    parm.Value = petrochemical_categories.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеКатегорииНефтепродукта", SqlDbType.NVarChar);
                    parm.Value = petrochemical_categories.name;
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
            return Delete(dbcontext, new PetrochemicalCategories(code));
        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, PetrochemicalCategories petrochemical_categories)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeletePetrochemicalCategories", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииНефтепродукта", SqlDbType.Int);
                    parm.Value = petrochemical_categories.type_code;
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
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int code, out PetrochemicalCategories petrochemical_categories)
        {
            bool rc = false;
            petrochemical_categories = new PetrochemicalCategories();
            using (SqlCommand cmd = new SqlCommand("EGH.GetPetrochemicalCategoriesByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииНефтепродукта", SqlDbType.Int);
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
                        string name = (string)reader["НаименованиеКатегорииНефтепродукта"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) petrochemical_categories = new PetrochemicalCategories(code, name);

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
            XmlElement rc = doc.CreateElement("PetrochemicalCategories");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("type_code", this.type_code.ToString());
            rc.SetAttribute("name", this.name.ToString());
            return (XmlNode)rc;
        }

    }
    public class PetrochemicalCategoriesList : List<PetrochemicalCategories>
    {
        List<EGH01DB.Types.SoilCleaningMethod> list_soil_cleaning_method = new List<EGH01DB.Types.SoilCleaningMethod>();
        public PetrochemicalCategoriesList()
        {

        }
        public PetrochemicalCategoriesList(List<PetrochemicalCategories> list) : base(list)
        {

        }
        public PetrochemicalCategoriesList(EGH01DB.IDBContext dbcontext) : base(Helper.GetListPetrochemicalCategories(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("PetrochemicalCategoriesList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
    }
}
