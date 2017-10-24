using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using EGH01DB.Types;
using EGH01DB.Primitives;

namespace EGH01DB.Points
{
    public class Incident: SpreadPoint 
    {

        public int      id           { get; private set; }   // идентификатор 
        public DateTime date         { get; private set; }   // дата и время происшествия 
        public DateTime date_message { get; private set; }   // дата и время  получения сообщения 
        public IncidentType type     { get; private set; }   // тип инцидента 

        public Incident():base()
        { 
            this.id = -1;
            this.date = DateTime.MinValue;
            this.date_message = DateTime.MinValue;
            this.type = null;
        }

        public  Incident(DateTime date, DateTime date_message, IncidentType type, SpreadPoint spreadpoint):base(spreadpoint)
        {
            this.id = -1;
            this.date = date;
            this.date_message = date_message;
            this.type = type;
 
        }

        public Incident(XmlNode node): base(node.SelectSingleNode(".//SpreadPoint"))
        {
            this.id = Helper.GetIntAttribute(node, "id", -1);
            this.date = Helper.GetDateTimeAttribute(node, "date", DateTime.MinValue);
            this.date_message = Helper.GetDateTimeAttribute(node, "date_message", DateTime.MinValue);
            XmlNode incident_type = node.SelectSingleNode(".//IncidentType");
            if (incident_type != null) this.type = new IncidentType(incident_type);
            else this.type = null;
        }
        public new XmlNode  toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("Incident");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("date", this.date.ToShortDateString());
            rc.SetAttribute("date_message", this.date_message.ToShortDateString());
            rc.AppendChild(doc.ImportNode(this.type.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(base.toXmlNode(), true));
            return rc;

        }

     }
}
