using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EGH01DB.Primitives;
using System.Data.SqlClient;
using System.Data;

namespace EGH01DB.Types
{
    public class GroundType   // тип грунта 
    {
        public int     type_code        {get; private set;}
        public string  name             {get; private set;}
        public float   porosity         {get; private set;}          // пористость     >0    <1, безразмерная , доля застрявшего  в грунте нефтепродукта       
        public float   holdmigration    {get; private set;}          // коэфф. задержки миграции нефтепродуктов 
        public float   waterfilter      {get; private set;}          // коэфф. фильтрации воды
        public float   diffusion        {get; private set;}          // коэфф. диффузии
        public float   distribution     {get; private set;}          // коэфф. распределения
        public float   sorption         {get; private set;}          // коэфф. сорбции
        public float   watercapacity    {get; private set;}          // капиллярная влагоемкость (от 0 до 1)
        public float   soilmoisture     {get; private set;}          // влажность грунта (от 0 до 1)
        public float   аveryanovfactor  {get; private set;}          // коэффициент Аверьянова (от 4 до 9)
        public float   permeability     {get; private set;}          // водопроницаемость м/с
        public float   density          {get; private set;}          // водопроницаемость м/с

        public bool    Create()      {return true;}
        public bool    Delete()      {return true;}
        public bool    GetByCode(int code)  {return true; }

        public GroundType()
        {
            this.type_code = -1;
            this.name = "";
            this.porosity = 0.0f;
            this.holdmigration = 0.0f;
            this.waterfilter = 0.0f;
            this.diffusion = 0.0f;
            this.distribution = 0.0f;
            this.sorption = 0.0f;
            this.watercapacity = 0.0f;
            this.soilmoisture = 0.0f;
            this.аveryanovfactor = 0.0f;
            this.permeability = 0.0f;
            this.density = 0.0f;
        }
        public GroundType(int type_code)
        {
            this.type_code = type_code;
            this.name = "";
            this.porosity = 0.0f;
            this.holdmigration = 0.0f;
            this.waterfilter = 0.0f;
            this.diffusion = 0.0f;
            this.distribution = 0.0f;
            this.sorption = 0.0f;
            this.watercapacity = 0.0f;
            this.soilmoisture = 0.0f;
            this.аveryanovfactor = 0.0f;
            this.permeability = 0.0f;
            this.density = 0.0f;
        }
        public GroundType(int type_code, string name, float porosity, float holdmigration, float waterfilter, float diffusion, float distribution, float sorption)
        {
            this.type_code = type_code;
            this.name = name;
            this.porosity = porosity;
            this.holdmigration = holdmigration;
            this.waterfilter = waterfilter;
            this.diffusion = diffusion;
            this.distribution = distribution;
            this.sorption = sorption;
            this.watercapacity = -1.0f;
            this.soilmoisture = -1.0f;
            this.аveryanovfactor = -1.0f;
            this.permeability = -1.0f;
            this.density = 0.0f;
        }
        public GroundType(int type_code, 
                            string name, 
                            float porosity, 
                            float holdmigration, 
                            float waterfilter, 
                            float diffusion, 
                            float distribution, 
                            float sorption,
                            float watercapacity,
                            float soilmoisture,
                            float аveryanovfactor,
                            float permeability,
                            float density)
        {
            this.type_code = type_code;
            this.name = name;
            this.porosity = porosity;
            this.holdmigration = holdmigration;
            this.waterfilter = waterfilter;
            this.diffusion = diffusion;
            this.distribution = distribution;
            this.sorption = sorption;
            this.watercapacity = watercapacity;
            this.soilmoisture = soilmoisture;
            this.аveryanovfactor = аveryanovfactor;
            this.permeability = permeability;
            this.density = density;
        }
        public GroundType(XmlNode node)
        {
            this.type_code =        Helper.GetIntAttribute(node, "type_code");
            this.name =             Helper.GetStringAttribute(node, "name");
            this.porosity =         Helper.GetFloatAttribute(node, "porosity");
            this.holdmigration =    Helper.GetFloatAttribute(node, "holdmigration");
            this.waterfilter =      Helper.GetFloatAttribute(node, "waterfilter");
            this.diffusion =        Helper.GetFloatAttribute(node, "diffusion");
            this.distribution =     Helper.GetFloatAttribute(node, "distribution");
            this.sorption =         Helper.GetFloatAttribute(node, "sorption");
            this.watercapacity =    Helper.GetFloatAttribute(node, "watercapacity");
            this.soilmoisture =     Helper.GetFloatAttribute(node, "soilmoisture");
            this.аveryanovfactor =  Helper.GetFloatAttribute(node, "аveryanovfactor");
            this.permeability =     Helper.GetFloatAttribute(node, "permeability");
            this.density   =        Helper.GetFloatAttribute(node, "density");
        }

        static public bool GetByCode(EGH01DB.IDBContext dbcontext, int type_code, out GroundType ground_type)
        {
            bool rc = false;
            ground_type = new GroundType();
            using (SqlCommand cmd = new SqlCommand("EGH.GetGroundTypeByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаГрунта", SqlDbType.Int);
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
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string name = (string)reader["НаименованиеТипаГрунта"];
                        float porosity = (float)reader["КоэфПористости"];
                        float holmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0)
                            ground_type = new GroundType((int)type_code,
                                                         (string)name,
                                                         (float)porosity,
                                                         (float)holmigration,
                                                         (float)waterfilter,
                                                         (float)diffusion,
                                                         (float)distribution,
                                                         (float)sorption,
                                                         (float)watercapacity,
                                                         (float)soilmoisture,
                                                         (float)аveryanovfactor,
                                                         (float)permeability,
                                                         (float)density);
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
        static public bool GetNextCode(EGH01DB.IDBContext dbcontext, out int code)
        {
            bool rc = false;
            code = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextGroundTypeCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаГрунта", SqlDbType.Int);
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
                    code = (int)cmd.Parameters["@КодТипаГрунта"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, GroundType ground_type)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateGroundType", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаГрунта", SqlDbType.Int);
                    if (ground_type.type_code <= 0)
                    {
                        int new_ground_type_code = 0;
                        if (GetNextCode(dbcontext, out new_ground_type_code)) ground_type.type_code = new_ground_type_code;
                    }
                    parm.Value = ground_type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаГрунта", SqlDbType.NVarChar);
                    parm.Value = ground_type.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфПористости", SqlDbType.Real);
                    parm.Value = ground_type.porosity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфЗадержкиМиграции", SqlDbType.Real);
                    parm.Value = ground_type.holdmigration;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфФильтрацииВоды", SqlDbType.Real);
                    parm.Value = ground_type.waterfilter;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфДиффузии", SqlDbType.Real);
                    parm.Value = ground_type.diffusion;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфРаспределения", SqlDbType.Real);
                    parm.Value = ground_type.distribution;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфСорбции", SqlDbType.Real);
                    parm.Value = ground_type.sorption;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфКапВлагоемкости", SqlDbType.Real);
                    parm.Value = ground_type.watercapacity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВлажностьГрунта", SqlDbType.Real);
                    parm.Value = ground_type.soilmoisture;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфАверьянова", SqlDbType.Real);
                    parm.Value = ground_type.аveryanovfactor;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Водопроницаемость", SqlDbType.Real);
                    parm.Value = ground_type.permeability;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@СредняяПлотностьГрунта", SqlDbType.Real);
                    parm.Value = ground_type.density;
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
                    rc = (int)cmd.Parameters["@exitrc"].Value == ground_type.type_code;
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }

            return rc;
        }
        static public bool Update(EGH01DB.IDBContext dbcontext, GroundType ground_type)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateGroundType", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаГрунта", SqlDbType.Int);
                    parm.Value = ground_type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТипаГрунта", SqlDbType.VarChar);
                    parm.Value = ground_type.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфПористости", SqlDbType.Real);
                    parm.Value = ground_type.porosity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфЗадержкиМиграции", SqlDbType.Real);
                    parm.Value = ground_type.holdmigration;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфФильтрацииВоды", SqlDbType.Real);
                    parm.Value = ground_type.waterfilter;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфДиффузии", SqlDbType.Real);
                    parm.Value = ground_type.diffusion;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфРаспределения", SqlDbType.Real);
                    parm.Value = ground_type.distribution;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфСорбции", SqlDbType.Real);
                    parm.Value = ground_type.sorption;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфКапВлагоемкости", SqlDbType.Real);
                    parm.Value = ground_type.watercapacity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВлажностьГрунта", SqlDbType.Real);
                    parm.Value = ground_type.soilmoisture;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэфАверьянова", SqlDbType.Real);
                    parm.Value = ground_type.аveryanovfactor;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Водопроницаемость", SqlDbType.Real);
                    parm.Value = ground_type.permeability;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@СредняяПлотностьГрунта", SqlDbType.Real);
                    parm.Value = ground_type.density;
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
        static public bool Delete(EGH01DB.IDBContext dbcontext, GroundType ground_type)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteGroundType", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодТипаГрунта", SqlDbType.Int);
                    parm.Value = ground_type.type_code;
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
            return Delete(dbcontext, new GroundType(code));
        }
        public XmlNode toXmlNode(string comment = "")
        {
             XmlDocument doc = new XmlDocument();
             XmlElement rc = doc.CreateElement("GroundType");
             if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
             rc.SetAttribute("type_code",       this.type_code.ToString());
             rc.SetAttribute("name",            this.name);
             rc.SetAttribute("porosity",        this.porosity.ToString());
             rc.SetAttribute("holdmigration",   this.holdmigration.ToString());
             rc.SetAttribute("waterfilter",     this.waterfilter.ToString());
             rc.SetAttribute("diffusion",       this.diffusion.ToString());
             rc.SetAttribute("distribution",    this.distribution.ToString());
             rc.SetAttribute("sorption",        this.sorption.ToString());
             rc.SetAttribute("watercapacity",   this.watercapacity.ToString());
             rc.SetAttribute("soilmoisture",    this.soilmoisture.ToString());
             rc.SetAttribute("аveryanovfactor", this.аveryanovfactor.ToString());
             rc.SetAttribute("permeability",    this.permeability.ToString());
             rc.SetAttribute("density",         this.density.ToString());
             return (XmlNode)rc;
        }
    }
    public class GroundTypeList : List<GroundType>   // список  опорных точек 
    {
        public GroundTypeList() : base()
        {

        }
        public GroundTypeList(List<GroundType> list)
            : base(list)
        {

        }
        public GroundTypeList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListGroundType(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {

            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("GroundTypeList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
    }
    }
