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


          
     public class  ECOForecastX
     {
         public  ECOForecast0 level0  {get; private set;}
         public  ECOForecast1 level1  {get; private set;}
         public  ECOForecast2 level2  {get; private set;}
         public  ECOForecast3 level3  {get; private set;}
         public ECOForecastX(IDBContext db, Incident incident)
         {

             this.level0 = new ECOForecast0(db, 
                                            incident.date, 
                                            incident.date_message,
                                            incident.type, 
                                            incident.petrochemicaltype,
                                            incident.volume,
                                            incident.temperature,
                                            incident.riskobject);

             this.level1 = new ECOForecast1(this.level0);
             this.level2 = new ECOForecast2(this.level1);
             this.level3 = new ECOForecast3(this.level2); 


         }

     }

    public class ECOForecast0     // поверхность 
     {
         public IDBContext        db                { get; set; }
         public DateTime          date              { get; set; }
         public DateTime          date_message      { get; set; }
         public IncidentType      incidenttype      { get; set; }
         public PetrochemicalType petrochemicaltype { get; set; }   // тип НП
         public float             V0                { get; set; }
         public float             temperature       { get; set; }
         public RiskObject        riskobject        { get; set; }
         public float             delta0            { get; set; }
         public float             ro0               { get; set; }   
         public float             M0                { get; set; }  // масса пролитого НП
                    

         public  ECOForecast0(IDBContext          db,
                              DateTime            date,
                              DateTime            date_message,
                              IncidentType        incidenttype,
                              PetrochemicalType   petrochemicaltype,              // тип НП
                              float               volume,
                              float               temperature,
                              RiskObject          riskobject)
 
         {
         this.db                 = db;
         this.date               = date;
         this.date_message       = date_message;
         this.incidenttype       = incidenttype;
         this.petrochemicaltype  = petrochemicaltype;  // тип НП
         this.V0                 = volume;
         this.temperature        = temperature;
         this.riskobject         = riskobject;
         this.delta0             = this.petrochemicaltype.tension;
         this.ro0                = this.petrochemicaltype.density;
         this.M0                 = this.V0 * this.ro0;                 // масса пролитого НП 
         }

         public ECOForecast0(ECOForecast0 f0)
         {
             this.db = f0.db;
             this.date = f0.date;
             this.date_message = f0.date_message;
             this.incidenttype = f0.incidenttype;
             this.petrochemicaltype = f0.petrochemicaltype;  // тип НП
             this.V0 = f0.V0;
             this.temperature = f0.temperature;
             this.riskobject = f0.riskobject;
             this.M0 = f0.M0;                  
         }

     }
    public class ECOForecast1      // почва
     {
         public ECOForecast0 f0     { get; private set; } 
         public float        d1     { get; private set; }       // коэффициент разлива (1/м) 
         public float        q1     { get; private set; }       // удельная коэффициент выбросов  
         public float        S1     { get; private set; }       // площадь пятна  
         public float        H1     { get; private set; }       // толщина пятна  
         public float        M1     { get; private set; }       // масса испарившегося нп   
         public float        R1     { get; private set; }       // радиус  пятна при равномерном растекании   
         public BlurBorder   bb     { get; private set; }       // границы пятна 

        
         public ECOForecast1(ECOForecast0 f0)  
         {
             this.f0 = f0;
             this.d1 = SpreadingCoefficient.GetByData(f0.db, f0.riskobject.groundtype, f0.petrochemicaltype, f0.V0, 0.0f);
             this.q1 = EvaporationCoefficient.GetByData(f0.temperature);
             this.S1 = this.d1 * this.f0.V0;
             this.R1 =  (float)Math.Round((float)Math.Sqrt(this.S1 / Math.PI),1);
             this.H1 = f0.V0 / this.S1;
             this.M1 = this.S1 * this.q1;
             this.bb =  new BlurBorder(this.R1, new BlurBorder.XY[]   // отладка 
                                                    {
                                                        new BlurBorder.XY(7,25),
                                                        new BlurBorder.XY(8,7), 
                                                        new BlurBorder.XY(10,5),
                                                        new BlurBorder.XY(20,8), 
                                                        new BlurBorder.XY(37,23),
                                                        new BlurBorder.XY(22,36), 
                                                        new BlurBorder.XY(7,25)
                                                    }); 


         }

     }
     public  class ECOForecast2     // грунт   
     {

         public ECOForecast0 f0 { get; private set; }
         public ECOForecast1 f1 { get; private set; }
         public float        u2 { get; private set; }    // нефтеемкость 
         public float        h2 { get; private set; }    // средняя толщина почвенного слоя
         public float        H2 { get; private set; }    // глубина проникновения  
         public float        M2 { get; private set; }    // адсорбированная почвой масса

         public ECOForecast2(ECOForecast1 f1)
         {
             this.f0 = f1.f0;
             this.f1 = f1;
             this.u2 = OilCapacity.defaultvalue.ocapacity; 
             this.h2 = f0.riskobject.soiltype.gumus_depth;
             this.M2 = f1.S1 * this.h2 * this.u2 * this.f0.delta0;
             this.H2 = (this.h2 * (f0.M0 - f1.M1) / this.M2) > this.h2 ? this.h2 : (this.h2 * (f0.M0 - f1.M1) / this.M2);    
           
         }
     }
     public class ECOForecast3     // грунтовые воды 
     {
         public ECOForecast0 f0  { get; private set; }
         public ECOForecast1 f1  { get; private set; }
         public ECOForecast2 f2  { get; private set; }
         public string       groundtypename { get; private set; }   // название грунта
         public float        h3  { get; private set; }   // толщина грунта
         public float        rov { get; private set; }   // плотность воды 
         public float        deltav  { get; private set; }   // коэффициент поверностного натяжения  воды 
         public float        k3  { get; private set; }   // коэффициент фильтрации воды 
         public float        r3  { get; private set; }   // коэффициент задержки НП
         public float        m3  { get; private set; }   // пористость грунта
         public float        w3  { get; private set; }   // капилярная влагоемкость грунта
         public float        ro3 { get; private set; }   // плотность грунта 
         public float        H3  { get; private set; }   // грубина проникновения НП в грунт 
         public float        M3  { get; private set; }   // масса абсорбированного в грунте НП  
         public float        C3  { get; private set; }   // максимальная концентрация нп в грунте 

        

         public ECOForecast3(ECOForecast2 f2)
         {
            this.f0 = f2.f0;
            this.f1 = f2.f1;
            {
                this.ro3 = 1000; 
                WaterProperties wp = null;
                float delta = 0;
                if (WaterProperties.Get(f0.db, f0.temperature, out wp, out delta))
                {
                    this.ro3 = wp.density;
                    this.deltav = wp.tension; 
                }
             }

             this.groundtypename = this.f0.riskobject.groundtype.name; 
             this.h3 =  this.f0.riskobject.waterdeep;
             this.k3  = this.f0.riskobject.groundtype.waterfilter;
             this.r3  = this.f0.riskobject.groundtype.holdmigration;
             this.m3  = this.f0.riskobject.groundtype.porosity;
             this.w3  = this.f0.riskobject.groundtype.watercapacity;
             this.ro3 = this.f0.riskobject.groundtype.density;
             this.M3  = this.h3 * this.f1.S1 * this.rov * this.m3 * this.w3 * this.f0.delta0 / this.deltav;
             {
                 if (this.f0.M0 - this.f1.M1 - this.f2.M2 <= this.M3) this.H3 = this.h3 * (this.f0.M0 - this.f1.M1 - this.f2.M2) / this.M3;
                 else this.H3 = this.h3;
             }
             
             this.C3 = this.M3 / (this.f1.S1 * this.ro3);
             

            

         }

     }

     public class BlurBorder
     {
         public class XY
         {
             public int x;
             public int y;
             public XY(int x, int y) { this.x = x; this.y = y; }
         }
         public int R;
         public int LimitR = 7;
         public int nr;  
         public int   CanvaX = 400;
         public int   CanvaY = 400;
         public XY center = null;
         public float KS = 1.0f;
         public XY[] xy;
        
         public BlurBorder(float r, XY[] xy)
         {
             this.xy = xy;
             this.KS = getKS();
             this.nr = (int)Math.Ceiling(r);
             this.R =  (int)Math.Ceiling(r*KS);
            
             this.center = new XY(this.CanvaX / 2, this.CanvaY / 2);
             for (int k = 0; k < this.xy.Length; k++)
             {
                 this.xy[k].x = (int)(this.KS * (float)this.xy[k].x);
                 this.xy[k].y = (int)(this.KS * (float)this.xy[k].y); 
             }
             
         }
         private float getKS()
         {
             float rc = 1.0f;
             int   mxy = 1;
             for (int k = 0; k < this.xy.Length; k++)
             {
                 mxy = xy[k].x > mxy ? xy[k].x : mxy;
                 mxy = xy[k].y > mxy ? xy[k].y : mxy;
             }
             rc = ((float)Math.Min(this.CanvaX, this.CanvaY)) / ((float)mxy); 

             return rc;
         }


     } 



     #region ECOForecast [старая версия]
     public partial class ECOForecast         //  модель прогнозирования 
     {
         public int id { get; set; }                  // идентификатор прогноза 
         public DateTime date { get; private set; }          // дата формирования отчета 
         public Incident incident { get; private set; }          // описание ицидента 
         public GroundBlur groundblur { get; private set; }          // наземное пятно 
         public WaterBlur waterblur { get; private set; }          // пятно  загрязнения грунтвых вод 
         public DateTime dateconcentrationinsoil { get; private set; }          // дата достижения загрянения грунтовых вод    
         public DateTime datewatercompletion { get; private set; }          // дата достижения загрянения грунтовых вод 
         public DateTime datemaxwaterconc { get; private set; }          // дата достижения  иаксимального загрянения г на уровне рунтовых вод 
         public string errormessage { get; private set; }          // сообщение об ошибке 
         public string line
         {
             get
             {
                 return string.Format("{0}-П-{1:yyy-MM-dd}", this.id, this.date)
                       + string.Format(": {0}, {1}, {2}", this.incident.volume, this.incident.petrochemicaltype.name, this.incident.riskobject.name);
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
             Init(db, incident);
         }
         private bool Init(IDBContext db, Incident incident)
         {
             this.errormessage = string.Empty;
             try
             {

                 this.incident = incident;
                 this.groundblur = new GroundBlur(this.incident);
                 this.waterblur = new WaterBlur(db, this.groundblur);

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
     #endregion 







    }
    
 }

