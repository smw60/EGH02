using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Types;
using EGH01DB.Objects;
using EGH01DB.Points;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Primitives;

namespace EGH01DB.Points
{
    public class SpreadPoint: Point          // разлив 
    {
        public PetrochemicalType petrochemicaltype {get;  private set;}                   // нефтепродукт 
        public float             volume            {get;  private set;}                   // объем разлива м3
        public RiskObject        riskobject        {get;  private set;}                   // техногенный объект
        public CadastreType      cadastretype      {get; private set; }                   // кадастровый тип земли
        public bool              isriskobject      {get {return riskobject != null;} }    // разлив на техногенном объекте?


        public SpreadPoint(): base()
        {
            this.petrochemicaltype = null;
            this.volume = 0;
            this.riskobject = null;         //  разлив не связан с техногенным объектом 
            this.cadastretype = null;
        }
        // разлив в произвольной точке
        public SpreadPoint(Point point, CadastreType  cadastretype, PetrochemicalType petrochemicaltype, float volume): base(point) 
        {
            this.petrochemicaltype = petrochemicaltype;
            this.volume = volume;
            this.riskobject = null;         //  разлив не связан с техногенным объектом 
            this.cadastretype = cadastretype;
        }
        // разлив на техногенном объекте 
        public SpreadPoint(RiskObject riskobject, PetrochemicalType petrochemicaltype, float volume): base(riskobject)
        {
            this.petrochemicaltype = petrochemicaltype;
            this.volume = volume;
            this.riskobject = riskobject;   //  разлив на техногенном объекте 
            this.cadastretype = riskobject.cadastretype;

        }
       
        public SpreadPoint(SpreadPoint spreadpoint): base(spreadpoint)
        {
            this.petrochemicaltype = spreadpoint.petrochemicaltype;
            this.volume = spreadpoint.volume;
            this.riskobject = spreadpoint.riskobject;
            this.cadastretype = spreadpoint.cadastretype;
            
        }
        public SpreadPoint(XmlNode node)
            : base(node.SelectSingleNode(".//Point"))
        {
            XmlNode petro = node.SelectSingleNode(".//PetrochemicalType");
            if (petro != null) this.petrochemicaltype = new PetrochemicalType(petro);
            else this.petrochemicaltype = null;

            this.volume = Helper.GetFloatAttribute(node, "volume", 0.0f);

            XmlNode risk_object = node.SelectSingleNode(".//RiskObject");
            if (risk_object != null) this.riskobject = new RiskObject(risk_object);
            else this.riskobject = null;

            XmlNode cad = node.SelectSingleNode(".//CadastreType");
            if (cad != null) this.cadastretype = new CadastreType(cad);
            else this.cadastretype = null;

            //this.isriskobject = Helper.GetBoolAttribute(node, "isriskobject", false);
         }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("SpreadPoint");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.AppendChild(doc.ImportNode(this.petrochemicaltype.toXmlNode(), true));
            rc.SetAttribute("volume", this.volume.ToString());
            rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.riskobject.toXmlNode(), true));
            XmlNode n = base.toXmlNode("");
            rc.AppendChild(doc.ImportNode(n, true));
           // is riskobject
            return (XmlNode)rc;
        }

    }
}
