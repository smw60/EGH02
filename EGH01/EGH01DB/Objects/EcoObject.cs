using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Points;
using EGH01DB.Types;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;


namespace EGH01DB.Objects
{

    public class EcoObject : Point  // природоохранные объекты 
    {
        public int           id             {get; private set;}    // идентификатор 
        public EcoObjectType ecoobjecttype  {get; private set;}    // тип природохранного объекта 
        public CadastreType  cadastretype   {get; private set; }   // кадастровый тип земли
        public string name                  {get; private set; }   // наименование природоохранного объекта 
        public bool iswaterobject { get; private set; }    // является водным объектом, если значение EcoObjectType задано, иначе - нет
        public float angle { get; private set; }   // Уклон грунтовых вод 
        public float pollutionecoobject { get; private set; }  // Расстояние от центра загрязнения до точки
        public static  readonly string PREFIX = "ПО";       
        public EcoObject()
        {
            this.id = -1;
            this.ecoobjecttype = new EcoObjectType();
            this.cadastretype = new CadastreType();
            this.name = string.Empty;
            this.angle = 0.0f;
            this.pollutionecoobject = 0.0f;
        }
        public EcoObject(int id)
        {
            this.id = id;
            this.ecoobjecttype = new EcoObjectType();
            this.cadastretype = new CadastreType();
            this.name = string.Empty;
            this.pollutionecoobject = 0.0f;
            this.angle = 0.0f;

        }
        public EcoObject(int id, Point point, EcoObjectType ecoobjecttype, CadastreType cadastretype, string name, bool iswaterobject)
            : base(point)
        {
            this.id = id;
            this.ecoobjecttype = ecoobjecttype;
            this.cadastretype = cadastretype;
            this.name = name;
            this.iswaterobject = iswaterobject;
            this.angle = 0.0f;
            this.pollutionecoobject = 0.0f;
        }
        public EcoObject(int id, Point point, EcoObjectType ecoobjecttype, CadastreType cadastretype, string name, bool iswaterobject, float angle, float pollutionecoobject)
            : base(point)
        {
            this.id = id;
            this.ecoobjecttype = ecoobjecttype;
            this.cadastretype = cadastretype;
            this.name = name;
            this.iswaterobject = iswaterobject;
            this.angle = angle;
            this.pollutionecoobject = pollutionecoobject;
        }
        public EcoObject(XmlNode node)
            : base(new Point(node.SelectSingleNode(".//Point")))
        {
            this.id = Helper.GetIntAttribute(node, "id", -1);
           
            XmlNode cad = node.SelectSingleNode(".//CadastreType");
            if (cad != null) this.cadastretype = new CadastreType(cad);
            else this.cadastretype = null;

            XmlNode eco_object_type = node.SelectSingleNode(".//EcoObjectType");
            if (eco_object_type != null) this.ecoobjecttype = new EcoObjectType(eco_object_type);
            else this.ecoobjecttype = null;

            this.name = Helper.GetStringAttribute(node, "name", "");
            this.angle = Helper.GetFloatAttribute(node, "angle",0.0f);
            this.pollutionecoobject = Helper.GetFloatAttribute(node, "pollutionecoobject", 0.0f);
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, EcoObject ecoobject)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateEcoObject", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdПриродоохранногоОбъекта", SqlDbType.Int);
                    int new_ecoobject_id = 0;
                    if (GetNextId(dbcontext, out new_ecoobject_id)) ecoobject.id = new_ecoobject_id;
                    parm.Value = ecoobject.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеПриродоохранногоОбъекта", SqlDbType.NVarChar);
                    parm.Value = ecoobject.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = ecoobject.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = ecoobject.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = ecoobject.groundtype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГлубинаГрунтовыхВод", SqlDbType.Real);
                    parm.Value = ecoobject.waterdeep;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВысотаУровнемМоря", SqlDbType.Real);
                    parm.Value = ecoobject.height;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНазначенияЗемель", SqlDbType.Int);
                    parm.Value = ecoobject.cadastretype.type_code;
                    cmd.Parameters.Add(parm);
                }
                  {
                    SqlParameter parm = new SqlParameter("@КодТипаПриродоохранногоОбъекта", SqlDbType.Int);
                    parm.Value = ecoobject.ecoobjecttype.type_code;
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
                    rc = ((int)cmd.Parameters["@exitrc"].Value == ecoobject.id);
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetNextId(EGH01DB.IDBContext dbcontext, out int next_id)
        {
            bool rc = false;
            next_id = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextEcoObjectId", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdПриродоохранногоОбъекта", SqlDbType.Int);
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
                    next_id = (int)cmd.Parameters["@IdПриродоохранногоОбъекта"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, EcoObject ecoobject)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteEcoObject", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdПриродоохранногоОбъекта", SqlDbType.Int);
                    parm.Value = ecoobject.id;
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
        static public bool DeleteById(EGH01DB.IDBContext dbcontext, int id)
        {
            return Delete(dbcontext, new EcoObject(id));
        }
        static public bool Update(EGH01DB.IDBContext dbcontext, EcoObject ecoobject)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateEcoObject", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdПриродоохранногоОбъекта", SqlDbType.Int);
                    parm.Value = ecoobject.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаПриродоохранногоОбъекта", SqlDbType.Int);
                    parm.Value = ecoobject.ecoobjecttype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНазначенияЗемель", SqlDbType.Int);
                    parm.Value = ecoobject.cadastretype.type_code;
                    cmd.Parameters.Add(parm);
                }

                {
                    SqlParameter parm = new SqlParameter("@НаименованиеПриродоохранногоОбъекта", SqlDbType.NVarChar);
                    parm.Value = ecoobject.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = ecoobject.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = ecoobject.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = ecoobject.groundtype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГлубинаГрунтовыхВод", SqlDbType.Real);
                    parm.Value = ecoobject.waterdeep;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВысотаУровнемМоря", SqlDbType.Real);
                    parm.Value = ecoobject.height;
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
        static public bool GetById(EGH01DB.IDBContext dbcontext, int id, ref EcoObject ecoobject)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetEcoObjectByID", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdПриродоохранногоОбъекта", SqlDbType.Int);
                    parm.Value = id;
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
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        Coordinates coordinates = new Coordinates((float)x, (float)y);

                        string ground_type_name = (string)reader["НаименованиеТипаГрунта"];
                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];

                        GroundType ground_type = new GroundType((int)reader["ТипГрунта"],
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

                        float waterdeep = (float)reader["ГлубинаГрунтовыхВод"];
                        float height = (float)reader["ВысотаУровнемМоря"];
                        Point point = new Point(coordinates, ground_type, (float)waterdeep, (float)height);
                        int ecoobject_type_code = (int)reader["КодТипаПриродоохранногоОбъекта"];
                        string ecoobject_type_name = (string)reader["НаименованиеТипаПриродоохранногоОбъекта"];

                        int cat_water_name = (int)reader["КатегорияВодоохрТер"];
                        bool iswaterobject;
                        if (cat_water_name!=0 ) iswaterobject = true; else iswaterobject= false ;
                        WaterProtectionArea water_protection_area = new WaterProtectionArea(cat_water_name);
                        EcoObjectType ecoobjecttype = new EcoObjectType(ecoobject_type_code, ecoobject_type_name, water_protection_area);
                    
                        int cadastre_type_code = (int)reader["КодТипаНазначенияЗемель"];
                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];
                        
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code, (string)cadastre_type_name,
                                                                        pdk, water_pdk_coef,
                                                                        ground_doc_name, water_doc_name);
                        string ecoobject_name = (string)reader["НаименованиеПриродоохранногоОбъекта"];


                        ecoobject = new EcoObject(id, point, ecoobjecttype, cadastre_type, ecoobject_name, iswaterobject);
                    }
                    reader.Close();
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };

                return rc;
            }

        }
        static public bool CreateNear(EGH01DB.IDBContext dbcontext, EcoObject ecoobject, float angle, float distance, out EcoObject eco_object)
        {
            bool rc = false;
            int id = -1;
            eco_object = new EcoObject();
            using (SqlCommand cmd = new SqlCommand("EGH.GetCoordinatesByAngle", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@lat1", SqlDbType.Real);
                    parm.Value = ecoobject.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@lng1", SqlDbType.Real);
                    parm.Value = ecoobject.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@angle", SqlDbType.Real);
                    parm.Value = angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@distance", SqlDbType.Real);
                    parm.Value = distance;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@lat2", SqlDbType.Real);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@lng2", SqlDbType.Real);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();

                    float x = (float)cmd.Parameters["@lat2"].Value;
                    float y = (float)cmd.Parameters["@lng2"].Value;
                    Coordinates coordinates = new Coordinates((float)x, (float)y);

                    int new_ground_type_code = ecoobject.groundtype.type_code;
                    float new_waterdeep = ecoobject.waterdeep;
                    float new_height = ecoobject.height;
                    GroundType new_ground_type = new GroundType(new_ground_type_code);
                    Point point = new Point(coordinates, new_ground_type, new_waterdeep, new_height);

                    int new_cadastre_type_code = ecoobject.cadastretype.type_code;
                    bool iswaterobject = ecoobject.iswaterobject;
                    string name = ecoobject.name;
                    int ecoobjecttype_code = ecoobject.ecoobjecttype.type_code;
                    EcoObjectType ecoobjecttype = new EcoObjectType(ecoobjecttype_code);
                    CadastreType cadastretype = new CadastreType(new_cadastre_type_code);

                    eco_object = new EcoObject(id, point, ecoobjecttype, cadastretype, name, iswaterobject);
                    if (EcoObject.Create(dbcontext, eco_object)) rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("EcoObject");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("id", this.id.ToString());
            XmlNode n = base.toXmlNode("");
            rc.AppendChild(doc.ImportNode(n, true));
            rc.AppendChild(doc.ImportNode(this.ecoobjecttype.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));
            rc.SetAttribute("name", this.name.ToString());
            rc.SetAttribute("iswaterobject", this.ecoobjecttype.waterprotectionarea.ToString());
            rc.SetAttribute("angle", this.angle.ToString());
            rc.SetAttribute("pollutionecoobject", this.pollutionecoobject.ToString());
           
            return (XmlNode)rc;
        }
    }
     

    public class EcoObjectsList : List<EcoObject>      // список объектов  с координами 
    {
        public EcoObjectsList()
        {
                
        }

        public EcoObjectsList(List<EcoObject> list)
        {

            list.ForEach(o => this.Add(o));
        }

        public static EcoObjectsList CreateEcoObjectsList(XmlNode node)
        {
            EcoObjectsList eco_objects_list = new EcoObjectsList();
            foreach (XmlElement x in node)
            {
                if (x.Name.Equals("EcoObject")) eco_objects_list.Add(new EcoObject(x));    
            }
            return eco_objects_list;
        }
        public EcoObjectsList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListEcoObject(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("EcoObjectsList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
        public static EcoObjectsList CreateEcoObjectsList(EGH01DB.IDBContext dbcontext,  Point center, float distance = float.MaxValue)
        {
           EcoObjectsList rc = new EcoObjectsList();
           List<EcoObject> ecolist = new List<EcoObject>();
           if (Helper.GetListEcoObject(dbcontext, ref ecolist))
           {
                 var selx = ecolist.Where(o => o.coordinates.Distance(center.coordinates) <= distance).AsQueryable();
                 foreach (EcoObject o in selx)
                 {
                    // float x = o.coordinates.Distance(center.coordinates);
                     rc.Add(o);
                 }
            }       
            return rc;
        }
        public static EcoObjectsList CreateEcoObjectsList(EGH01DB.IDBContext dbcontext, Point center, float distance1 = 0.0f, float distance2 = float.MaxValue)
        {

            EcoObjectsList rc = new EcoObjectsList();
            List<EcoObject> ecolist = new List<EcoObject>();
            if (Helper.GetListEcoObject(dbcontext, ref ecolist))
            {
                var selx = ecolist.Where(o =>o.coordinates.Distance(center.coordinates) >= distance1 && 
                                             o.coordinates.Distance(center.coordinates) <= distance2).AsQueryable();
                foreach (EcoObject o in selx)
                {
                    float x = o.coordinates.Distance(center.coordinates);
                    rc.Add(o);
                }
            }
            return rc;
        }
    }

}
