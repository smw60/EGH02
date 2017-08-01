using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01DB.Points;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace EGH01DB.Points
{
    public class AnchorPoint:Point   // опорная геологическая точка   
    {
        public static readonly string PREFIX = "ОT"; 
        public int          id           {get; private set;}  
        public CadastreType cadastretype {get; private set;}   // кадастровый тип земли

        public AnchorPoint()
        {
            this.id = -1;
            this.cadastretype = new CadastreType ();
        }
        public AnchorPoint(int id, Point point, CadastreType cadastretype):base (point)
        {
            this.id = id;
            this.cadastretype = cadastretype;
        }
        public AnchorPoint(int id)
        {
            this.id = id;
            this.cadastretype = new CadastreType ();
        }
        public AnchorPoint(XmlNode node)
            : base(new Point(node.SelectSingleNode(".//Point")))
        {
            this.id = Helper.GetIntAttribute(node, "id", -1);
            XmlNode cad = node.SelectSingleNode(".//CadastreType");
            if (cad != null) this.cadastretype = new CadastreType(cad);
            else this.cadastretype = null;
        }

        static public bool Create(EGH01DB.IDBContext dbcontext, AnchorPoint anchor_point)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateAnchorPoint", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОпорнойГеологическойТочки", SqlDbType.Int);
                    int new_anchor_point_id = 0;
                    if (GetNextId(dbcontext, out new_anchor_point_id)) anchor_point.id = new_anchor_point_id;
                    parm.Value = anchor_point.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = anchor_point.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = anchor_point.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = anchor_point.groundtype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГлубинаГрунтовыхВод", SqlDbType.Real);
                    parm.Value = anchor_point.waterdeep;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВысотаУровнемМоря", SqlDbType.Real);
                    parm.Value = anchor_point.height;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодНазначенияЗемель", SqlDbType.Int);
                    parm.Value = anchor_point.cadastretype.type_code;
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
                    rc = ((int)cmd.Parameters["@exitrc"].Value == anchor_point.id);
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Create(EGH01DB.IDBContext dbcontext, AnchorPoint anchor_point, float angle, float distance)
        {
            bool rc = false;
            int id = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetCoordinatesByAngle", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@lat1", SqlDbType.Real);
                    parm.Value = anchor_point.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@lng1", SqlDbType.Real);
                    parm.Value = anchor_point.coordinates.lngitude;
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
                //{
                //    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                //    parm.Direction = ParameterDirection.ReturnValue;
                //    cmd.Parameters.Add(parm);
                //}
                try
                {
                    cmd.ExecuteNonQuery();
                   // if (rc = ((int)cmd.Parameters["@exitrc"].Value) > 0)
                   // {
                        float x = (float)cmd.Parameters["@lat2"].Value;
                        float y = (float)cmd.Parameters["@lng2"].Value;
                        Coordinates coordinates = new Coordinates ((float)x, (float)y);
                        
                        int new_ground_type_code = anchor_point.groundtype.type_code;
                        float new_waterdeep = anchor_point.waterdeep;
                        float new_height = anchor_point.height;
                        GroundType new_ground_type = new GroundType(new_ground_type_code);
                        Point point = new Point(coordinates, new_ground_type, new_waterdeep, new_height);

                        int new_cadastre_type_code = anchor_point.cadastretype.type_code;
                        CadastreType cadastretype = new CadastreType(new_cadastre_type_code);

                        AnchorPoint new_anchor_point = new AnchorPoint(id, point, cadastretype);
                        if (AnchorPoint.Create(dbcontext, new_anchor_point)) rc = true;
                   // }
                  
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
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextAnchorPointId", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОпорнойГеологическойТочки", SqlDbType.Int);
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
                    next_id = (int)cmd.Parameters["@IdОпорнойГеологическойТочки"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, AnchorPoint anchor_point)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteAnchorPoint", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОпорнойГеологическойТочки", SqlDbType.Int);
                    parm.Value = anchor_point.id;
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
            return Delete(dbcontext, new AnchorPoint(id));
        }
        static public bool Update(EGH01DB.IDBContext dbcontext, AnchorPoint anchor_point)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateAnchorPoint", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОпорнойГеологическойТочки", SqlDbType.Int);
                    parm.Value = anchor_point.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = anchor_point.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = anchor_point.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = anchor_point.groundtype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГлубинаГрунтовыхВод", SqlDbType.Real);
                    parm.Value = anchor_point.waterdeep;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВысотаУровнемМоря", SqlDbType.Real);
                    parm.Value = anchor_point.height;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодНазначенияЗемель", SqlDbType.Int);
                    parm.Value = anchor_point.cadastretype.type_code;
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
        static public bool GetById(EGH01DB.IDBContext dbcontext, int id, ref AnchorPoint anchor_point)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetAnchorPointByID", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОпорнойГеологическойТочки", SqlDbType.Int);
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

                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];

                        CadastreType cadastre_type = new CadastreType((int)reader["КодНазначенияЗемель"], (string)cadastre_type_name,
                                                        (float)pdk, (float)water_pdk_coef,
                                                        ground_doc_name, water_doc_name);

                        anchor_point = new AnchorPoint(id, point, cadastre_type);
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

        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("AnchorPoint");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("id", this.id.ToString());
            XmlNode n = base.toXmlNode("");
            rc.AppendChild(doc.ImportNode(n, true));
            rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));
            return (XmlNode)rc;
        }
    }
    public class AnchorPointList : List<AnchorPoint>   // список  опорных точек 
    {
        public float avgheight // средняя глубина грунтовых вод
        {
          get {return  (this.Count() > 0? this.Average(a => a.height):0); }
        }

        public float sumheight
        {
            get { return (this.Count() > 0 ? this.Sum(a => a.height) : 0); }
        }
        public float avgwaterdeep // средняя глубина грунтовых вод
        {
            get { return (this.Count() > 0 ? this.Average(a => a.waterdeep) : 0); }
        }
        public float sumwaterdeep
        {
            get { return (this.Count() > 0 ? this.Sum(a => a.waterdeep) : 0); }
        }

        public AnchorPointList() : base()
        {
            
        }
        public AnchorPointList(List<AnchorPoint> list)
            : base(list)
        {

        }
        public AnchorPointList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListAnchorPoint(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {

            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("AnchorPointList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);

            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));

            //   rc.AppendChild(doc.ImportNode(this.coordinates.toXmlNode(), true));
            //rc.AppendChild(doc.ImportNode(this.groundtype.toXmlNode(), true));
            return (XmlNode)rc;
        }
        public static AnchorPointList CreateAnchorPointList(XmlNode node)
        {
            AnchorPointList apl = new AnchorPointList();
            foreach (XmlElement x in node)
            {
                if (x.Name.Equals("AnchorPoint")) apl.Add(new AnchorPoint(x));     
            }
            return apl;
        }
        //  найти список точек в заданном радиусе 
        public static AnchorPointList CreateNear(Coordinates center, float distance)
        {
            bool rc = false;
            RGEContext db = new RGEContext();
            AnchorPointList anchor_point_list = new AnchorPointList();
            using (SqlCommand cmd = new SqlCommand("EGH.GetListAnchorPointOnDistanceLessThanD", db.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = center.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = center.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Расстояние", SqlDbType.Real);
                    parm.Value = distance;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                   //cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["IdОпорнойГеологическойТочки"];
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        int ground_type_code = (int)reader["ТипГрунта"];
                        
                        float waterdeep = (float)reader["ГлубинаГрунтовыхВод"];
                        float height = (float)reader["ВысотаУровнемМоря"];

                        GroundType ground_type = new GroundType(ground_type_code);
                        
                        Coordinates coordinates = new Coordinates((float)x, (float)y);

                        int cadastre_type_code = (int)reader["КодНазначенияЗемель"];
                        string ground_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk_koef = (float)reader["ПДК"];
                        float water_pdk_koef = (float)reader["ПДКводы"];
	                    string pdk_korf_doc = (string)reader["НормДокументЗемля"];
		                string water_pdk_korf_doc = (string)reader["НормДокументВода"];

                        CadastreType cadastre_type = new CadastreType(cadastre_type_code, ground_type_name, pdk_koef, water_pdk_koef,
                                                                        pdk_korf_doc, water_pdk_korf_doc);
                        Point point = new Point(coordinates, ground_type, (float)waterdeep, (float)height);
                        //delta = (float)reader["Расстояние"];
                        AnchorPoint anchor_point = new AnchorPoint(id, point, cadastre_type);
                        anchor_point_list.Add(anchor_point);
                    }
                    rc = anchor_point_list.Count > 0;
                    reader.Close();




                }
                catch (Exception e)
                {
                    rc = false;
                };
                return anchor_point_list;
            }
        }

        public static AnchorPointList CreateNear(Coordinates center, float radius1, float radius2)
        {
            bool rc = false;
            RGEContext db = new RGEContext();
            AnchorPointList anchor_point_list = new AnchorPointList();
            using (SqlCommand cmd = new SqlCommand("EGH.GetListAnchorPointOnDistanceLessThanD2MoreThanD1", db.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = center.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = center.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Расстояние1", SqlDbType.Real);
                    parm.Value = radius1;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Расстояние2", SqlDbType.Real);
                    parm.Value = radius2;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["IdТехногенногоОбъекта"];
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        int ground_type_code = (int)reader["ТипГрунта"];
                        int cadastre_type_code = (int)reader["КодНазначенияЗемель"];
                        float waterdeep = (float)reader["ГлубинаГрунтовыхВод"];
                        float height = (float)reader["ВысотаУровнемМоря"];
                        GroundType ground_type = new GroundType(ground_type_code);
                        Coordinates coordinates = new Coordinates((float)x, (float)y);
                        CadastreType cadastre_type = new CadastreType(cadastre_type_code);
                        Point point = new Point(coordinates, ground_type, (float)waterdeep, (float)height);
                        //delta = (float)reader["Расстояние"];
                        AnchorPoint anchor_point = new AnchorPoint(id, point, cadastre_type);
                        anchor_point_list.Add(anchor_point);
                    }
                    rc = anchor_point_list.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return anchor_point_list;
            }

        }

        //  найти  список точек в заданном полигоне 
        public static AnchorPointList CreateNear(Coordinates center, CoordinatesList border)
        {

            // отладка 
            return new AnchorPointList()
            {


            };

        }


    }







}
