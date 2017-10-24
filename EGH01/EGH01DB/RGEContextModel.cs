using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using EGH01DB.Objects;
using EGH01DB.Blurs;
using EGH01DB.Types;
using EGH01DB.Primitives;
using EGH01DB.Points;
using System.Xml;
namespace EGH01DB
{
   
    public partial class RGEContext
    {
        public partial class ECOForecast         //  модель прогнозирования 
        {
            public int           id                      {get; set;}                  // идентификатор прогноза 
            public DateTime      date                    {get; private set;}          // дата формирования отчета 
            public Incident      incident                {get; private set;}          // описание ицидента 
            public GroundBlur    groundblur              {get; private set;}          // наземное пятно 
            public WaterBlur     waterblur               {get; private set;}          // пятно  загрязнения грунтвых вод 
            public DateTime      dateconcentrationinsoil {get; private set;}          // дата достижения загрянения грунтовых вод    
            public DateTime      datewatercompletion     {get; private set;}          // дата достижения загрянения грунтовых вод 
            public DateTime      datemaxwaterconc        {get; private set;}          // дата достижения  иаксимального загрянения г на уровне рунтовых вод 
            public string        errormessage            {get; private set;}          // сообщение об ошибке 
            public string        line                    {
                                                          get
                                                             {
                                                              return  string.Format("{0}-П-{1:yyy-MM-dd}", this.id, this.date) 
                                                                    + string.Format(": {0}, {1}, {2}", this.incident.volume, this.incident.petrochemicaltype.name, this.incident.riskobject.name) ;
                                                             } 
                                                          }
        

           public ECOForecast(ECOForecast forecast)
            {
                this.id = forecast.id;
                this.date = forecast.date;
                this.incident = forecast.incident;
                this.groundblur = forecast.groundblur;
                this.waterblur = forecast.waterblur;
                this.dateconcentrationinsoil = forecast.dateconcentrationinsoil;
                this.datewatercompletion = forecast.datewatercompletion;
                this.datemaxwaterconc = forecast.datemaxwaterconc;
                this.errormessage = this.errormessage;
          }
            
            public ECOForecast()
            {
                RGEContext db = new RGEContext();
                Init(db, new Incident()); 
            }
            public ECOForecast(int id)
            {
                //RGEContext db = new RGEContext();
                this.id = id;
                this.date = DateTime.Parse("1900-01-01 01:01:01");
                this.dateconcentrationinsoil = DateTime.Parse("1900-01-01 01:01:01");
                this.datewatercompletion = DateTime.Parse("1900-01-01 01:01:01");
                this.datemaxwaterconc = DateTime.Parse("1900-01-01 01:01:01");
                this.errormessage = "errormessage";
            }
            public ECOForecast(Incident incident)
            {
                RGEContext db = new RGEContext();
                Init(db, incident);   
            }
            public ECOForecast(IDBContext db, Incident incident)
            {
                  Init( db, incident);      
            }
            private bool Init(IDBContext db, Incident incident)
            {
                this.errormessage = string.Empty;
                try
                {

                    this.incident = incident;
                    this.groundblur = new GroundBlur(this.incident);
                    this.waterblur =  new WaterBlur(db, this.groundblur);
                   
                    this.date = DateTime.Now;
                    
                    if (!Const.isINFINITY(this.groundblur.timewatercomletion) && !Const.isINFINITY(this.groundblur.timemaxwaterconc))
                    {

                        this.datewatercompletion = (new DateTime(incident.date.Ticks)).AddSeconds(this.groundblur.timewatercomletion);
                        this.datemaxwaterconc = (new DateTime(incident.date.Ticks)).AddSeconds(this.groundblur.timemaxwaterconc);
                    }
                    else
                    {
                        this.datewatercompletion = Const.DATE_INFINITY;
                        this.datemaxwaterconc = Const.DATE_INFINITY;

                    }
                    if (!Const.isINFINITY(this.groundblur.timeconcentrationinsoil))
                    {
                        this.dateconcentrationinsoil = (new DateTime(incident.date.Ticks)).AddSeconds(this.groundblur.timeconcentrationinsoil);
                    }
                    else
                    {
                        this.dateconcentrationinsoil = Const.DATE_INFINITY;
                    }

                    foreach (WaterPollution p in this.waterblur.watepollutionlist)
                    {
                        if (!Const.isINFINITY(p.timemaxconcentration)) p.datemaxconcentration = this.incident.date.AddSeconds(p.timemaxconcentration);
                    }

                }
                catch (EGHDBException e)
                {
                    this.errormessage = e.ehgmessage;

                }




                return true;
            }
            public ECOForecast(XmlNode node)
            {
                this.id = Helper.GetIntAttribute(node, "id", -1);
                this.date = Helper.GetDateTimeAttribute(node, "date", DateTime.MinValue);

                XmlNode incident = node.SelectSingleNode(".//Incident");
                if (incident != null) this.incident = new Incident(incident);
                else this.incident = null;

                XmlNode ground_blur = node.SelectSingleNode(".//GroundBlur");
                if (ground_blur != null) this.groundblur = new GroundBlur(ground_blur);
                else this.groundblur = null;

                XmlNode water_blur = node.SelectSingleNode(".//WaterBlur");
                if (water_blur != null) this.waterblur = new WaterBlur(water_blur);
                else this.waterblur = null;


                this.dateconcentrationinsoil = Helper.GetDateTimeAttribute(node, "dateconcentrationinsoil", DateTime.MinValue);
                this.datewatercompletion = Helper.GetDateTimeAttribute(node, "datewatercompletion", DateTime.MinValue);
                this.datemaxwaterconc = Helper.GetDateTimeAttribute(node, "datemaxwaterconc", DateTime.MinValue);
                this.errormessage = Helper.GetStringAttribute(node, "errormessage", "");
            }

            public XmlNode toXmlNode(string comment = "")
            {
                XmlDocument doc = new XmlDocument();
                XmlElement rc = doc.CreateElement("ECOForecast");
                if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
                rc.SetAttribute("id", this.id.ToString());
                rc.SetAttribute("date", this.date.ToString());
                rc.SetAttribute("dateconcentrationinsoil", this.dateconcentrationinsoil.ToShortDateString());
                rc.SetAttribute("datewatercompletion", this.datewatercompletion.ToShortDateString());
                rc.SetAttribute("datemaxwaterconc", this.datemaxwaterconc.ToShortDateString());
               // rc.SetAttribute("errormessage", this.errormessage);
                rc.AppendChild(doc.ImportNode(this.incident.toXmlNode(), true));
                rc.AppendChild(doc.ImportNode(this.groundblur.toXmlNode(), true));
                rc.AppendChild(doc.ImportNode(this.waterblur.toXmlNode(), true));
                return (XmlNode)rc;
            }
       }    
    }
    
 }

