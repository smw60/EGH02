using EGH01DB.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EGH01DB.Primitives;
using System.Data.SqlClient;
using System.Data;


// коэффициент растекания м<sup>2</sup>/м3
// --- грунт 
//       объем >= 500      объем <= 60      
// угол  <= 1       5         20  
// угол  >  1      12         20
// --- асфальт 
//                         объем <= 60 
//                           150  


namespace EGH01DB.Primitives
{
    public class SpreadingCoefficient
    {
        public int code { get; private set; } 
        public GroundType ground_type { get; private set; }     // тип грунта 
        public float min_volume { get; private set; }           // нижняя граница диапазона пролива   
        public float max_volume { get; private set; }           // верхняя граница диапазона пролива 
        public float min_angle { get; private set; }            // книжняя граница диапазона углов наклона 
        public float max_angle { get; private set; }            // верхняя граница диапазона углов наклона 
        public float koef { get; private set; }                 // коэффициент разлива в диапазоне 
        public PetrochemicalType petrochemical_type { get; private set; }     // тип нефтепродукта
        public bool Create()        { return true; }
        public bool Update()        { return true; }

        public static explicit operator bool(SpreadingCoefficient v)
        {
            throw new NotImplementedException();
        }

        public bool Delete()        { return true; }
        public float Get()          { return -1; }
        public float GetByData()    { return -1; }
        
        public SpreadingCoefficient()
        {
            this.code = -1;
            this.ground_type = new GroundType();
            this.petrochemical_type = new PetrochemicalType();
            this.min_volume = 0.0f;
            this.max_volume = 0.0f;
            this.min_angle = 0.0f;
            this.max_angle = 0.0f;
            this.koef = -1.0f;
        }
        public SpreadingCoefficient(int code)
        {
            this.code = code;
            this.ground_type = new GroundType();
            this.petrochemical_type = new PetrochemicalType();
            this.min_volume = 0.0f;
            this.max_volume = 0.0f;
            this.min_angle = 0.0f;
            this.max_angle = 0.0f;
            this.koef = -1.0f;
        }
        public SpreadingCoefficient(int code, GroundType groundtype, PetrochemicalType petrochemical_type, float min_volume, float max_volume, float min_angle, float max_angle, float koef)
        {
            this.code = code;
            this.ground_type = groundtype;
            this.petrochemical_type = petrochemical_type;
            this.min_volume = min_volume;
            this.max_volume = max_volume;
            this.min_angle = min_angle;
            this.max_angle = max_angle;
            this.koef = koef;
        }
        public SpreadingCoefficient(XmlNode node)
        {
            this.code = Helper.GetIntAttribute(node, "code", -1);

            XmlNode g = node.SelectSingleNode(".//GroundType");
            if (g != null) this.ground_type = new GroundType(g);
            else this.ground_type = null;

            XmlNode petr = node.SelectSingleNode(".//PetrochemicalType");
            if (petr != null) this.petrochemical_type = new PetrochemicalType(petr);
            else this.petrochemical_type = null;

            this.min_volume = Helper.GetIntAttribute(node, "min_volume", -1);
            this.max_volume = Helper.GetIntAttribute(node, "max_volume", -1);
            this.min_angle = Helper.GetIntAttribute(node, "min_angle", -1);
            this.max_angle = Helper.GetIntAttribute(node, "max_angle", -1);
            this.koef = Helper.GetIntAttribute(node, "koef", -1);
        }

        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("SpreadingCoefficient");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("code", this.code.ToString());
            rc.AppendChild(doc.ImportNode(this.ground_type.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.petrochemical_type.toXmlNode(), true));
            rc.SetAttribute("min_volume", this.min_volume.ToString());
            rc.SetAttribute("max_volume", this.max_volume.ToString());
            rc.SetAttribute("min_angle", this.min_angle.ToString());
            rc.SetAttribute("max_angle", this.max_angle.ToString());
            rc.SetAttribute("koef", this.koef.ToString());
            return (XmlNode)rc;
        }

        // заглушка 
        public static bool GetByParms(GroundType groundtype, PetrochemicalType petrochemicaltype, float volume, float angle, out SpreadingCoefficient spreadingcoefficient)  
        {
            spreadingcoefficient = new SpreadingCoefficient(0, groundtype, petrochemicaltype, 0.0f, 80.0f, 0.0f, 0.02f, 4.0f);

            return true;
        }


        // круглое пятно: грунт, объем, угол наклона 
        public static float Get(EGH01DB.IDBContext dbcontext, SpreadingCoefficient spreading_coefficient) // получить коэффициент растекания, -1 - такого нет
        {
            int rc = -1;
            float koefficient = -1.0f;
            using (SqlCommand cmd = new SqlCommand("EGH.GetSpreadingCoefficientByDelta", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = spreading_coefficient.ground_type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинПролива", SqlDbType.Real);
                    parm.Value = spreading_coefficient.min_volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксПролива", SqlDbType.Real);
                    parm.Value = spreading_coefficient.max_volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинУклона", SqlDbType.Real);
                    parm.Value = spreading_coefficient.min_angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксУклона", SqlDbType.Real);
                    parm.Value = spreading_coefficient.max_angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНефтепродукта", SqlDbType.Int);
                    parm.Value = spreading_coefficient.petrochemical_type.code_type;
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
                        double kof = (double)reader["КоэффициентРазлива"];
                        if ((int)cmd.Parameters["@exitrc"].Value > 0)
                        {
                            rc = (int)cmd.Parameters["@exitrc"].Value;
                            koefficient = (float)kof;
                        }
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = -1;
                };
            }
            return koefficient;
        }
        public static float GetByData(EGH01DB.IDBContext dbcontext, GroundType type, PetrochemicalType petrochemical_type, float volume, float angle)
        {
            float rc = -1.0f;
            float koefficient = -1.0f;
            using (SqlCommand cmd = new SqlCommand("EGH.GetSpreadingCoefficientByData", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНефтепродукта", SqlDbType.Int);
                    parm.Value = petrochemical_type.code_type;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Объем", SqlDbType.Real);
                    parm.Value = volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@УголНаклона", SqlDbType.Real);
                    parm.Value = angle;
                    cmd.Parameters.Add(parm);
                }
              
                {
                    SqlParameter parm = new SqlParameter("@КоэффициентРазлива", SqlDbType.Float);
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
                    double k = (double)cmd.Parameters["@КоэффициентРазлива"].Value;
                    koefficient = (float)k;
                }
                catch (Exception e)
                {
                    rc = -1;
                };

            }
            return koefficient;
        }
        public static bool Create(EGH01DB.IDBContext dbcontext, SpreadingCoefficient spreading_coefficient)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateSpreadingCoefficient", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = spreading_coefficient.ground_type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНефтепродукта", SqlDbType.Int);
                    parm.Value = spreading_coefficient.petrochemical_type.code_type;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинПролива", SqlDbType.Real);
                    parm.Value = spreading_coefficient.min_volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксПролива", SqlDbType.Real);
                    parm.Value = spreading_coefficient.max_volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинУклона", SqlDbType.Real);
                    parm.Value = spreading_coefficient.min_angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксУклона", SqlDbType.Real);
                    parm.Value = spreading_coefficient.max_angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэффициентРазлива", SqlDbType.Float);
                    parm.Value = spreading_coefficient.koef;
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
                    rc = ((int)cmd.Parameters["@exitrc"].Value == 1);
                }
                catch (Exception e)
                {
                    rc = false;
                };

            }

            return rc;


        }
        public static bool Update(EGH01DB.IDBContext dbcontext, SpreadingCoefficient spreading_coefficient)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateSpreadingCoefficient", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = spreading_coefficient.ground_type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНефтепродукта", SqlDbType.Int);
                    parm.Value = spreading_coefficient.petrochemical_type.code_type;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодКоэффициентаРазлива", SqlDbType.Int);
                    parm.Value = spreading_coefficient.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинПролива", SqlDbType.Real);
                    parm.Value = spreading_coefficient.min_volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксПролива", SqlDbType.Real);
                    parm.Value = spreading_coefficient.max_volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МинУклона", SqlDbType.Real);
                    parm.Value = spreading_coefficient.min_angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@МаксУклона", SqlDbType.Real);
                    parm.Value = spreading_coefficient.max_angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоэффициентРазлива", SqlDbType.Float);
                    parm.Value = spreading_coefficient.koef;
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
        public static bool Delete(EGH01DB.IDBContext dbcontext, SpreadingCoefficient spreading_coefficient)
        {

            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteSpreadingCoefficient", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКоэффициентаРазлива", SqlDbType.Int);
                    parm.Value = spreading_coefficient.code;
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
        public static bool DeleteByCode(EGH01DB.IDBContext dbcontext, int code)
        {
            return Delete(dbcontext, new SpreadingCoefficient(code));
        }
        public static bool GetByCode(EGH01DB.IDBContext dbcontext, int code, out SpreadingCoefficient spreading_coefficient)
        {
            bool rc = false;
            spreading_coefficient = new SpreadingCoefficient();
            using (SqlCommand cmd = new SqlCommand("EGH.GetSpreadingCoefficientByCode", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@КодКоэффициентаРазлива", SqlDbType.Int);
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
                        int ground_type_code = (int)reader["ТипГрунта"];
                        string ground_type_name = (string)reader["НаименованиеТипаГрунта"];

                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузииГрунта"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];
                        GroundType ground_type = new GroundType((int)ground_type_code,
                                                    (string)ground_type_name,
                                                    (float)porosity,
                                                    (float)holdmigration,
                                                    (float)waterfilter,
                                                    (float)diffusion,
                                                    (float)distribution,
                                                    (float)sorption,
                                                    (float)watercapacity,
                                                    (float)soilmoisture,
                                                    (float)аveryanovfactor,
                                                    (float)permeability,
                                                    (float)density);
                        int petrochemical_type_code = (int)reader["КодТипаНефтепродукта"];
                        string name = (string)reader["НаименованиеТипаНефтепродукта"];
                        float boilingtemp = (float)reader["ТемператураКипения"];
                        float petrochemical_density = (float)reader["Плотность"];
                        float viscosity = (float)reader["КинематическаяВязкость"];
                        float solubility = (float)reader["Растворимость"];
                        float tension = (float)reader["КоэфНатяжения"];
                        float dynamicviscosity = (float)reader["ДинамическаяВязкость"];
                        float petrochemical_diffusion = (float)reader["КоэфДиффузииНП"];
                        int petrochemicalcategories = (int)reader["КодКатегорииНефтепродукта"];
                        string petrochemicalcategoriesname = (string)reader["НаименованиеКатегорииНефтепродукта"];
                        PetrochemicalCategories petro_cat = new PetrochemicalCategories(petrochemicalcategories, petrochemicalcategoriesname);
                        PetrochemicalType petrochemical_type = new PetrochemicalType(petrochemical_type_code, 
                                                                                     name,
                                                                                     (float)boilingtemp,
                                                                                     (float)petrochemical_density,
                                                                                     (float)viscosity,
                                                                                     (float)solubility,
                                                                                     (float)tension,
                                                                                     (float)dynamicviscosity,
                                                                                     (float)petrochemical_diffusion, petro_cat);
                      
                        float min_volume = (float)reader["МинПролива"];
                        float max_volume = (float)reader["МаксПролива"];
                        float min_angle = (float)reader["МинУклона"];
                        float max_angle = (float)reader["МаксУклона"];
                        double koef = (double)reader["КоэффициентРазлива"];
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0)
                                spreading_coefficient =new SpreadingCoefficient(code, ground_type,
                                                                                 petrochemical_type,
                                                                                (float)min_volume,
                                                                                (float)max_volume,
                                                                                (float)min_angle,
                                                                                (float)max_angle,
                                                                                (float)koef);
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
        // другие методы определения коэффициента  
        // static float get
        // static float get
    }

    public class SpreadingCoefficientList : List<SpreadingCoefficient>
    {
        List<EGH01DB.Primitives.SpreadingCoefficient> list_spreading_coefficients = new List<EGH01DB.Primitives.SpreadingCoefficient>();
        public SpreadingCoefficientList()
        {

        }
        public SpreadingCoefficientList(List<SpreadingCoefficient> list) : base(list)
        {

        }
        public SpreadingCoefficientList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListSpreadingCoefficient(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("SpreadingCoefficientList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
    }
}
