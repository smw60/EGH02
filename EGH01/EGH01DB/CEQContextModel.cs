using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using EGH01DB.Objects;
using EGH01DB.Types;
using EGH01DB.Blurs;
using EGH01DB.Primitives;


namespace EGH01DB
{
    public  partial class CEQContext : IDBContext
    {
        public class Report
        {
            RGEContext.Report report;
            public float excessgroundconcentration = 0.0f;     // превышение концентрации в грунте (наземное пятно)
            public float exesswaterconcentration = 0.0f;     // превышение концентрации в воде   (наземное пятно)
            public float[] wb_exesswaterconcentration;           // превышение концентрации в воде   (водное   пятно)
            public Report(CEQContext db, RGEContext.Report report)
            {
                this.report = report;
                if (report.riskobject_id > 0)
                {
                    RiskObject robj = new RiskObject();
                    if (RiskObject.GetById(db, report.riskobject_id, ref robj))
                    {
                        if (robj.cadastretype.pdk_coef > 0 && this.report.C3 > 0)
                        {
                            this.excessgroundconcentration = this.report.C3 / robj.cadastretype.pdk_coef;
                            this.exesswaterconcentration = this.report.C4 / robj.cadastretype.water_pdk_coef;
                        }
                    }
                }
            }
        }
  

    #region ECOEvalution [старая версия]
    public partial class ECOEvalution : RGEContext.ECOForecast
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public float excessgroundconcentration { get; set; }      // отношение значения средней конентрации в грунте к ПДК  
        public float exesswaterconcentration { get; set; }      // отношение значения vмакс конентрации в грунте к ПДК    
        private string errormssageformat = "ECOEvalution: Ошибка в данных. {0}";
        public GroundPollutionList groundpollutionlist { get; set; }
        public WaterPollutionList waterpolutionlist { get; set; }


        public string line
        {
            get
            {
                return string.Format("{0}-Р-{1:yyyy-MM-dd}/{2}-П-{3:yyyy-MM-dd}", this.id, this.date, base.id, base.date)
                      + string.Format(": {0}, {1}, {2}", this.incident.volume, this.incident.petrochemicaltype.name, this.incident.riskobject.name);
            }
        }

        public ECOEvalution(RGEContext.ECOForecast forecast)
            : base(forecast)
        {
            CEQContext db = new CEQContext();    // заглушка, выставить правильный контекст //blinova
            //if (this.groundblur.spreadpoint.cadastretype.pdk_coef <= 0)
            //throw new EGHDBException(string.Format(errormssageformat, "Значение предельно-дупустимой концентрации не может быть  меньше или равно нулю"));

            this.date = DateTime.Now;
            if (this.groundblur.spreadpoint.cadastretype.pdk_coef > 0) this.excessgroundconcentration = this.groundblur.concentrationinsoil / this.groundblur.spreadpoint.cadastretype.pdk_coef;
            else this.excessgroundconcentration = 0.0f;



            if (this.groundblur.spreadpoint.cadastretype.water_pdk_coef > 0) this.exesswaterconcentration = this.groundblur.maxconcentrationwater / this.groundblur.spreadpoint.cadastretype.water_pdk_coef;
            else this.exesswaterconcentration = 0.0f;


            this.groundpollutionlist = new GroundPollutionList(this.groundblur.groundpolutionlist.Where(p => p.pointtype == Points.Point.POINTTYPE.ECO).ToList());

            this.waterpolutionlist = new WaterPollutionList();
            foreach (WaterPollution p in this.waterblur.watepollutionlist)
            {
                if (p.distance > this.groundblur.radius && p.distance < this.waterblur.radius)
                {
                    this.waterpolutionlist.Add(p);
                }
            }

        }


        public ECOEvalution(XmlNode node)
            : base(node.SelectSingleNode(".//ECOForecast"))
        {
            this.id = Helper.GetIntAttribute(node, "id");
            this.date = Helper.GetDateTimeAttribute(node, "date", DateTime.Now);
            this.excessgroundconcentration = Helper.GetFloatAttribute(node, "excessgroundconcentration", 0.0f);
            this.exesswaterconcentration = Helper.GetFloatAttribute(node, "exesswaterconcentration", 0.0f);

            {
                XmlNode x = node.SelectSingleNode(".//GroundPollutionList");
                if (x != null) this.groundpollutionlist = GroundPollutionList.Create(x);
                else this.groundpollutionlist = null;
            }
            {
                XmlNode x = node.SelectSingleNode(".//WaterPollutionList");
                if (x != null) this.waterpolutionlist = WaterPollutionList.CreateWaterPollutionList(x);
                else this.waterpolutionlist = null;
            }
        }

        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("ECOEvalution");
            if (!string.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("id", this.id.ToString());
            rc.SetAttribute("date", this.date.ToString());
            rc.SetAttribute("excessgroundconcentration", this.excessgroundconcentration.ToString());
            rc.SetAttribute("exesswaterconcentration", this.exesswaterconcentration.ToString());

            {
                XmlNode n = this.groundpollutionlist.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
            }
            {
                XmlNode n = this.waterpolutionlist.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
            }
            {
                XmlNode n = base.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
            }

            return (XmlNode)rc;
        }

        public static ECOEvalution GetById(IDBContext db, int id)
        {
            ECOEvalution rc = null;
            using (SqlCommand cmd = new SqlCommand("EGH.GetECOEvalutionById", db.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                    parm.Value = id;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string xmlContent = (string)reader["ТекстОтчета"];
                        if (!xmlContent.Trim().Equals(""))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xmlContent);
                            rc = new CEQContext.ECOEvalution(doc.DocumentElement);
                        }
                    }
                    reader.Close();
                }
                catch (Exception e) { rc = null; };
            }
            return rc;
        }

    }
    #endregion 
    }
}        

     

 