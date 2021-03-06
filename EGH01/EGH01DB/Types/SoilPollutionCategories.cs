﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;


// Категории загрязнения грунтов

namespace EGH01DB.Types
{
    public class SoilPollutionCategories
    {
        public int code { get; private set; }   // код категории
        public string name { get; private set; }   // наименование категории
        public float min { get; private set; }   // минимальное значение диапазона
        public float max { get; private set; }   // максимальное значение диапазона
        public CadastreType cadastretype { get; private set; }   //  значение земельного кадастра
        static public SoilPollutionCategories defaulttype { get { return new SoilPollutionCategories(0, "Не определен", 0.0f, 0.0f, CadastreType.defaulttype); } }  // выдавать при ошибке  

        public SoilPollutionCategories()
        {
            this.code = -1;
            this.name = string.Empty;
            this.min = 0.0f;
            this.max = 0.0f;
            this.cadastretype = null;
        }
        public SoilPollutionCategories(int code)
        {
            this.code = code;
            this.name = string.Empty;
            this.min = 0.0f;
            this.max = 0.0f;
            this.cadastretype = null;
        }

        public SoilPollutionCategories(int code, String name, float min, float max, CadastreType cadastre_type)
        {
            this.code = code;
            this.name = name;
            this.min = min;
            this.max = max;
            this.cadastretype = cadastre_type;
        }
        public SoilPollutionCategories(XmlNode node)
        {
            this.code = Helper.GetIntAttribute(node, "code", -1);
            this.name = Helper.GetStringAttribute(node, "name", "");
            this.min = Helper.GetFloatAttribute(node, "min", 0.0f);
            this.max = Helper.GetFloatAttribute(node, "max", 0.0f);

            XmlNode cad = node.SelectSingleNode(".//CadastreType");
            if (cad != null) this.cadastretype = new CadastreType(cad);
            else this.cadastretype = null;
        }
        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextSoilPollutionCategoriesCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииЗагрязненияГрунта", SqlDbType.Int);
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
                    code = (int)cmd.Parameters["@КодКатегорииЗагрязненияГрунта"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, SoilPollutionCategories soil_pollution_categories)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateSoilPollutionCategories", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    int new_type_code = 0;
                    if (GetNextCode(dbcontext, out new_type_code)) soil_pollution_categories.code = new_type_code;
                    SqlParameter parm = new SqlParameter("@КодКатегорииЗагрязненияГрунта", SqlDbType.Int);
                    parm.Value = soil_pollution_categories.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеКатегорииЗагрязненияГрунта", SqlDbType.NVarChar);
                    parm.Value = soil_pollution_categories.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинДиапазон", SqlDbType.Real);
                    parm.Value = soil_pollution_categories.min;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксДиапазон", SqlDbType.Real);
                    parm.Value = soil_pollution_categories.max;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодНазначенияЗемель", SqlDbType.Int);
                    parm.Value = soil_pollution_categories.cadastretype.type_code;
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
        static public bool Update(EGH01DB.IDBContext dbcontext, SoilPollutionCategories soil_pollution_categories)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateSoilPollutionCategories", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииЗагрязненияГрунта", SqlDbType.Int);
                    parm.Value = soil_pollution_categories.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеКатегорииЗагрязненияГрунта", SqlDbType.VarChar);
                    parm.Value = soil_pollution_categories.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинДиапазон", SqlDbType.Real);
                    parm.Value = soil_pollution_categories.min;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксДиапазон", SqlDbType.Real);
                    parm.Value = soil_pollution_categories.max;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодНазначенияЗемель", SqlDbType.Int);
                    parm.Value = soil_pollution_categories.cadastretype.type_code;
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
            return Delete(dbcontext, new SoilPollutionCategories(code));
        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, SoilPollutionCategories soil_pollution_categories)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteSoilPollutionCategories", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииЗагрязненияГрунта", SqlDbType.Int);
                    parm.Value = soil_pollution_categories.code;
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
        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int code, out SoilPollutionCategories soil_pollution_categories)
        {
            bool rc = false;
            soil_pollution_categories = SoilPollutionCategories.defaulttype;
            using (SqlCommand cmd = new SqlCommand("EGH.GetSoilPollutionCategoriesByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКатегорииЗагрязненияГрунта", SqlDbType.Int);
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
                        int re_code = (int)reader["КодКатегорииЗагрязненияГрунта"];
                        string name = (string)reader["НаименованиеКатегорииЗагрязненияГрунта"];
                        float min = (float)reader["МинДиапазон"];
                        float max = (float)reader["МаксДиапазон"];

                        int cadastre_type_code = (int)reader["КодНазначенияЗемель"];
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk_coef = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code, cadastre_type_name,
                                                                        pdk_coef, water_pdk_coef, 
                                                                        ground_doc_name, water_doc_name);

                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) 
                                soil_pollution_categories = new SoilPollutionCategories(code, name, min, max, cadastre_type);

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
        static public bool GetByVolume_Cadastre(EGH01DB.IDBContext dbcontext, float volume, int cadatsre_type_code, out SoilPollutionCategories soil_pollution_categories)
        {
            bool rc = false;
            soil_pollution_categories = SoilPollutionCategories.defaulttype;
            using (SqlCommand cmd = new SqlCommand("EGH.GetSoilPollutionCategoriesByVolume_Cadastre", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@Объем", SqlDbType.Float);
                    parm.Value = volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодНазначенияЗемель", SqlDbType.Int);
                    parm.Value = cadatsre_type_code;
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
                        int re_code = (int)reader["КодКатегорииЗагрязненияГрунта"];
                        string name = (string)reader["НаименованиеКатегорииЗагрязненияГрунта"];
                        float min = (float)reader["МинДиапазон"];
                        float max = (float)reader["МаксДиапазон"];

                        int cadastre_type_code = (int)reader["КодНазначенияЗемель"];
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk_coef = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code, cadastre_type_name,
                                                                        pdk_coef, water_pdk_coef,
                                                                        ground_doc_name, water_doc_name);

                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0)
                            soil_pollution_categories = new SoilPollutionCategories(re_code, name, min, max, cadastre_type);

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
            XmlElement rc = doc.CreateElement("SoilPollutionCategories");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("code", this.code.ToString());
            rc.SetAttribute("name", this.name.ToString());
            rc.SetAttribute("min", this.min.ToString());
            rc.SetAttribute("max", this.max.ToString());
            rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));

            return (XmlNode)rc;
        }
    }
        public class SoilPollutionCategoriesList : List<SoilPollutionCategories>
        {
            List<EGH01DB.Types.SoilPollutionCategories> list_SoilPollutionCategories = new List<EGH01DB.Types.SoilPollutionCategories>();
            public SoilPollutionCategoriesList()
            {

            }
            public SoilPollutionCategoriesList(List<SoilPollutionCategories> list)
                : base(list)
            {

            }
            public SoilPollutionCategoriesList(EGH01DB.IDBContext dbcontext)
                : base(Helper.GetListSoilPollutionCategories(dbcontext))
            {

            }
            public XmlNode toXmlNode(string comment = "")
            {
                XmlDocument doc = new XmlDocument();
                XmlElement rc = doc.CreateElement("SoilPollutionCategories");
                if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
                this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
                return (XmlNode)rc;
            }
        }
    
}
