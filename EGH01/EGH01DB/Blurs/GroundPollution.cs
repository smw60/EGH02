using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Types;
using EGH01DB.Points;
using EGH01DB.Objects;
using EGH01DB.Primitives;
using System.Xml;
using System.Data.SqlClient;
using System.Data;

namespace EGH01DB.Blurs
{
    public  partial class GroundPollution : Point   //загрязнение  в точке 
    {
       
       public float watertime                    { get; private set; }          // время достижения грунтовых вод (с)  
       public float concentration                { get; private set; }          // концентрация нефтепродуктов в грунте    (кг/кг)
       public PetrochemicalType petrochemicatype { get; private set; }          // нефтепродукт
       public CadastreType cadastretype          { get; private set; }          // кадастровый тип земли
       public float distance                     { get; private set; }          // расстояние до центра разлива 
       public float angle                        { get; private set; }          // гидравлический угол наклона  
       public string comment                     { get; private set; }          // комментарий 
       public string name                        { get; private set; }          // наименование 
      // public bool is
       public POINTTYPE  pointtype               { get; private set; }          // тип точки 

       private readonly string comment_format = "{0}-{1}:";         // тип-id:              
       public GroundPollution()
       {
           this.watertime = 0.0f;
           this.concentration = 0.0f;
           this.petrochemicatype = new PetrochemicalType();
           this.cadastretype = new CadastreType();
           this.distance = 0.0f;
           this.angle = 0.0f;
           this.name = String.Empty;
           this.comment = String.Empty;
           this.pointtype = POINTTYPE.UNDEF;
       }
       public GroundPollution(AnchorPoint anchorpoint,   float distance, float angle, PetrochemicalType petrochemicatype, float concentration = 0.0f, float watertime = 0.0f)
           : base(anchorpoint) 
       {
           this.pointtype = POINTTYPE.ANCHOR;  
           this.comment = string.Format(comment_format,AnchorPoint.PREFIX, anchorpoint.id);
           this.watertime = watertime;
           this.concentration = concentration;
           this.petrochemicatype = petrochemicatype;
           this.cadastretype = anchorpoint.cadastretype;
           this.distance = distance;
           this.angle = angle;
           this.name = this.comment;

       }
       public GroundPollution(EcoObject ecojbject, float distance, float angle, PetrochemicalType petrochemicatype, float concentration = 0.0f, float watertime = 0.0f)
           : base(ecojbject)
       {
           this.pointtype = POINTTYPE.ECO; 
           this.comment = string.Format(comment_format, EcoObject.PREFIX,  ecojbject.id);
           this.watertime = watertime;
           this.concentration = concentration;
           this.petrochemicatype = petrochemicatype;
           this.cadastretype = ecojbject.cadastretype;
           this.distance = distance;
           this.angle = angle;
           this.name = ecojbject.name;
       }
       public GroundPollution(SpreadPoint  spreadpoint, float concentration = 0.0f, float watertime = 0.0f)
           : base(spreadpoint)
       {
           this.pointtype = POINTTYPE.RISK; 
           this.comment = string.Format(comment_format, RiskObject.PREFIX, (spreadpoint.isriskobject? spreadpoint.riskobject.id: 0));
           this.watertime = watertime;
           this.concentration = concentration;
           this.petrochemicatype = spreadpoint.petrochemicaltype;
           this.cadastretype = spreadpoint.cadastretype;
           this.distance = 0.0f;
           this.angle = 0.0f;
           this.name = spreadpoint.isriskobject? spreadpoint.riskobject.name: this.comment;
       }
       public GroundPollution(XmlNode node)
            : base(new Point(node.SelectSingleNode(".//Point")))
        {
            this.watertime = Helper.GetFloatAttribute(node, "watertime", 0.0f);
            this.concentration = Helper.GetFloatAttribute(node, "concentration", 0.0f);

            XmlNode petro = node.SelectSingleNode(".//PetrochemicalType");
            if (petro != null) this.petrochemicatype = new PetrochemicalType(petro);
            else this.petrochemicatype = null;

            XmlNode cad = node.SelectSingleNode(".//CadastreType");
            if (cad != null) this.cadastretype = new CadastreType(cad);
            else this.cadastretype = null;

            this.distance = Helper.GetFloatAttribute(node, "distance", 0.0f);
            this.angle = Helper.GetFloatAttribute(node, "angle", 0.0f);

            this.name = Helper.GetStringAttribute(node, "name", "");
            this.comment = Helper.GetStringAttribute(node, "comment", "");
            string string_pointtype = Helper.GetStringAttribute(node, "pointtype", "");
            {
                POINTTYPE p = POINTTYPE.UNDEF;
                if (Enum.TryParse(string_pointtype, out p))
                {
                    this.pointtype  = p;
                }
                else this.pointtype = POINTTYPE.UNDEF;
                         
            } 
        }
       public XmlNode toXmlNode(string comment = "")
       {
           XmlDocument doc = new XmlDocument();
           XmlElement rc = doc.CreateElement("GroundPollution");
           if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
           rc.SetAttribute("watertime", this.watertime.ToString());
           rc.SetAttribute("concentration", this.concentration.ToString());
           XmlNode n = base.toXmlNode("");
           rc.AppendChild(doc.ImportNode(n, true));
           rc.AppendChild(doc.ImportNode(this.petrochemicatype.toXmlNode(), true));
           rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));

           rc.SetAttribute("distance", this.distance.ToString());
           rc.SetAttribute("angle", this.angle.ToString());

           rc.SetAttribute("name", this.name.ToString());
           rc.SetAttribute("comment", this.comment.ToString());
           rc.SetAttribute("pointtype", this.pointtype.ToString());
           return (XmlNode)rc;
       }
    }

    public class GroundPollutionList : List<GroundPollution>    //  загрязнение во всех точках в наземном радиусе
    {
       public GroundPollutionList(SpreadPoint center, float concentration = 0.0f, float watertime = 0.0f)
       {
            this.Add(new GroundPollution(center, concentration, watertime));
       }
       public GroundPollutionList(Point center, AnchorPointList list, PetrochemicalType petrochemicaltype, float concentration = 0.0f, float watertime = 0.0f)
       {
            this.AddRange(center, list, petrochemicaltype, concentration, watertime);
       }
       public GroundPollutionList(Point center, EcoObjectsList list, PetrochemicalType petrochemicaltype, float concentration = 0.0f, float watertime = 0.0f)
       {
            this.AddRange(center, list, petrochemicaltype, concentration, watertime);
       }
       public GroundPollutionList(List<GroundPollution> list): base(list)
       {
       
       }
       public GroundPollutionList(): base(new List<GroundPollution>())
       {
       
       }
       public static  GroundPollutionList Create(XmlNode node)
       {
           GroundPollutionList rc = new GroundPollutionList();      
             
           foreach(XmlElement x in node)
           { 
              if(x.Name.Equals("GroundPollution")) rc.Add(new GroundPollution(x));
           }     
           return rc; 
       }
     


       public XmlNode toXmlNode(string comment = "")
       {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("GroundPollutionList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
       }

       public bool AddRange(Point center, AnchorPointList list, PetrochemicalType petrochemicaltype, float concentration = 0.0f, float watertime = 0.0f)
       {

            list.ForEach(p => this.Add(new GroundPollution(p,
                                                         p.coordinates.Distance(center.coordinates),
                                                         Helper.GeoAngle(center, p),
                                                         petrochemicaltype,
                                                         concentration,
                                                         watertime
                                                         )));

            return true;
        }
       public bool AddRange(Point center, EcoObjectsList list, PetrochemicalType petrochemicaltype, float concentration = 0.0f, float watertime = 0.0f)
        {

            list.ForEach(p => this.Add(new GroundPollution(p,
                                                         p.coordinates.Distance(center.coordinates),
                                                         Helper.GeoAngle(center, p),
                                                         petrochemicaltype,
                                                         concentration,
                                                         watertime
                                                         )));
                                    
            return true;
        }
       public bool Add(SpreadPoint center, float concentration = 0.0f, float watertime = 0.0f)
        {
           this.Add(new GroundPollution(center, concentration, watertime));
            return true;
        }

    }

}
