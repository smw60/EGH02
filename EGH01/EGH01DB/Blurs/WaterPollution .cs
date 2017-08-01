using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Types;
using EGH01DB.Objects;
using EGH01DB.Primitives;
using EGH01DB.Points;
using System.Xml;

namespace EGH01DB.Blurs
{
    public partial  class WaterPollution : Point   //загрязнение в точке
    {

        public PetrochemicalType petrochemicatype { get; private set; }          // нефтепродукт
        public CadastreType cadastretype          { get; private set; }          // кадастровый тип земли
        public WaterPollutionCategories   waterpollutioncategories    { get; set; }          // уровень загрязнения 
        public float distance                     { get; private set; }          // расстояние до центра разлива 
        public float maxconcentration             { get; set; }                  // максимальная концентрация нефтепродукта
        public float timemaxconcentration         { get; private set; }          // время достижения  максимальной концентрация нефтепродукта
        public float daymaxconcentration          { get { return  (float)Math.Round(timemaxconcentration/Const.SEC_PER_DAY,1);}} // время в сутках  
        public DateTime datemaxconcentration      { get; set; }                  // дата достижения  максимальной концентрация нефтепродукта
        public float speedhorizontal              { get; private set; }          // горизонтальная скорость 
        public float angle                        { get; private set; }          // гидравлический угол наклона  
        public string comment                     { get; private set; }          // комментарий 
        public string name                        { get; private set; }          // наименование 
       
 
        public bool iswaterobject                 { get; private set; }   // является ли водным объектом 
        public float excessconcentration          {
                                                    get
                                                      { 
                                                       float rc = 0.0f;
                                                       if (iswaterobject && cadastretype.water_pdk_coef > 0) rc =  maxconcentration/cadastretype.water_pdk_coef; 
                                                       else if (!iswaterobject && cadastretype.pdk_coef > 0) rc =  maxconcentration/cadastretype.pdk_coef; 
                                                       return rc;
                                                      }
                                                 
                                                     
                                                  }
        public POINTTYPE pointtype { get; private set; }          // тип точки 
        private readonly string comment_format = "{0}-{1}:";         // тип-id:


        public WaterPollution()
        {
            this.petrochemicatype = new PetrochemicalType();
            this.cadastretype = new CadastreType();
            this.waterpollutioncategories = new WaterPollutionCategories();
            this.distance = 0.0f;
            this.maxconcentration = 0.0f;
            this.timemaxconcentration = 0.0f;
            this.datemaxconcentration = DateTime.Parse("1900-01-01 01:01:01"); 
            this.speedhorizontal = 0.0f;
            this.angle = 0.0f;
            this.name = String.Empty;
            this.comment = String.Empty;
            this.pointtype = POINTTYPE.UNDEF;
            this.iswaterobject = false;
        }
        public WaterPollution(XmlNode node)
            : base(node.SelectSingleNode(".//Point"))
        {
            XmlNode petrochemical_type = node.SelectSingleNode(".//PetrochemicalType");
            if (petrochemical_type != null) this.petrochemicatype = new PetrochemicalType(petrochemical_type);
            else this.petrochemicatype = null;
            XmlNode cadastre_type = node.SelectSingleNode(".//CadastreType");
            if (cadastre_type != null) this.cadastretype = new CadastreType(cadastre_type);
            else this.cadastretype = null;
           
            this.distance = Helper.GetFloatAttribute(node, "distance");
            this.maxconcentration = Helper.GetFloatAttribute(node, "maxconcentration");
            this.timemaxconcentration = Helper.GetFloatAttribute(node, "timemaxconcentration");
            this.datemaxconcentration = Helper.GetDateTimeAttribute(node, "datemaxconcentration", DateTime.Parse("1900-01-01 01:01:01"));
            this.speedhorizontal = Helper.GetFloatAttribute(node, "speedhorizontal");
            this.angle = Helper.GetFloatAttribute(node, "angle");
            this.comment = Helper.GetStringAttribute(node, "comment");
            this.iswaterobject =  Helper.GetStringAttribute(node, "iswaterobject","нет").Equals("да");
            { 
              XmlNode x = node.SelectSingleNode(".//WaterPollutionCategories");
              if (x != null) this.waterpollutioncategories = new WaterPollutionCategories(x);
            }   
                      
        }

        public WaterPollution(EcoObject ecojbject, float distance, float angle, PetrochemicalType petrochemicatype, float speedhorizontal, float maxconcentration = 0.0f, float timemaxconcentration = 0.0f,
                              WaterPollutionCategories   waterpollutioncategories  = null)
            : base(ecojbject)
        {
            this.pointtype = POINTTYPE.ECO;
            this.comment = string.Format(comment_format, EcoObject.PREFIX, ecojbject.id);
            this.petrochemicatype = petrochemicatype;
            this.cadastretype = ecojbject.cadastretype;
            this.distance = distance;
            this.angle = angle;
            this.name = ecojbject.name;
            this.speedhorizontal = speedhorizontal;
            this.maxconcentration = maxconcentration;
            this.timemaxconcentration = timemaxconcentration;
            this.datemaxconcentration = Const.DATE_INFINITY;
            this.iswaterobject = ecojbject.iswaterobject;
            this.waterpollutioncategories = waterpollutioncategories; 
        }
        
        
        public new  XmlNode   toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("WaterPollution");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.AppendChild(doc.ImportNode(this.petrochemicatype.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));
            rc.SetAttribute("distance", this.distance.ToString());
            rc.SetAttribute("maxconcentration", this.maxconcentration.ToString());
            rc.SetAttribute("timemaxconcentration", this.timemaxconcentration.ToString());
            rc.SetAttribute("daymaxconcentration", this.daymaxconcentration.ToString());
            rc.SetAttribute("datemaxconcentration", this.datemaxconcentration.ToShortDateString());
            rc.SetAttribute("speedhorizontal", this.speedhorizontal.ToString());
            rc.SetAttribute("iswaterobject", this.iswaterobject ? "да" : "нет");
            rc.SetAttribute("angle", this.angle.ToString());
            rc.SetAttribute("name", this.name);
            rc.SetAttribute("comment", this.comment);
            rc.SetAttribute("pointtype", this.pointtype.ToString());
            rc.SetAttribute("excessconcentration", this.excessconcentration.ToString());
   
           if (this.waterpollutioncategories != null)
           {
              XmlNode x = this.waterpollutioncategories.toXmlNode("");   
              rc.AppendChild(doc.ImportNode(x, true));
           }
           {
              XmlNode x = base.toXmlNode("");
              rc.AppendChild(doc.ImportNode(x, true));
           }
           
            
            return (XmlNode)rc;
        }

    }
    public class WaterPollutionList : List<WaterPollution>    //  загрязнение во всех точках  в  водном радиусе 
    {
          public bool Add(WaterPollution waterpollution)
          {
               base.Add(waterpollution);
               return true;
           }
          public static WaterPollutionList CreateWaterPollutionList(XmlNode node)
          {
              WaterPollutionList water_pollution_list = new WaterPollutionList();
              foreach (XmlElement x in node)
              {
                  if (x.Name.Equals("WaterPollution")) water_pollution_list.Add(new WaterPollution(x));    
              }
              return water_pollution_list;
          }
          public XmlNode toXmlNode(string comment = "")
          {
              XmlDocument doc = new XmlDocument();
              XmlElement rc = doc.CreateElement("WaterPollutionList");
              if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
              this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
              return (XmlNode)rc;
          }

      }
    
}