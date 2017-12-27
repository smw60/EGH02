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
         public  ECOForecast4 level4  {get; private set;}
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
             this.level4 = new ECOForecast4(this.level3); 

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
    public class ECOForecast1      //  поверхность
     {
        
         public ECOForecast0      f0               { get; private set; } 
         public float             d1               { get; private set; }       // коэффициент разлива (1/м) 
         public float             q1               { get; private set; }       // удельная коэффициент выбросов  
         public float             S1               { get; private set; }       // площадь пятна  
         public float             H1               { get; private set; }       // толщина пятна  
         public float             M1               { get; private set; }       // масса испарившегося нп   
         public float             R1               { get; private set; }       // радиус  пятна при равномерном растекании   
         public BlurBorder        bb               { get; private set; }       // границы пятна 
         public float             dM1              { get; private set; }       // остаток НП достигший поверхности
         public F1EcoObjectsList  f1ecoobjectslist  { get; private set; }       // перечень экологических объектов в пятне загрязнения
         public F1AnchorPointList f1anchorpointlist { get; private set; }       // перечень  объектов в пятне загрязнения
         

         public ECOForecast1(ECOForecast0 f0)  
         {
             this.f0 = f0;
             this.dM1 = f0.M0 > 0 ? f0.M0 : 0.0f;
             this.d1 = SpreadingCoefficient.GetByData(f0.db, f0.riskobject.groundtype, f0.petrochemicaltype, f0.V0, 0.0f);
             this.d1 = this.d1 <= 0 ? 5.0f : this.d1;   // заглушка
             this.q1 = EvaporationCoefficient.GetByData(f0.temperature);
             this.S1 = this.d1 * this.f0.V0;
             this.R1 =  (float)Math.Round((float)Math.Sqrt(this.S1 / Math.PI),1);
             this.H1 = f0.V0 / this.S1;
             this.M1 = this.S1 * this.q1;
             this.f1ecoobjectslist = new F1EcoObjectsList("BASE", f0.riskobject, EcoObjectsList.CreateEcoObjectsList(this.f0.db, f0.riskobject, this.R1));
           
             {
                 EcoObjectsList ecoobjectslist = null;
                if (EcoObjectsList.FindAtDistance(f0.db, f0.riskobject.coordinates, (int)this.R1, out ecoobjectslist))
                {
                   this.f1ecoobjectslist.AddRange("MAPE", f0.riskobject, ecoobjectslist);
                }
             }

             {
                 AnchorPointList alist = AnchorPointList.CreateNear(f0.db, f0.riskobject.coordinates, 0.0f, this.R1);  
                                  
                 if (alist != null )
                 {
                       this.f1anchorpointlist = new F1AnchorPointList("BASE", f0.riskobject, alist);
                 }
             }
 






                //this.bb = new BlurBorder(this.R1, new BlurBorder.XY[]   // отладка 
                //                                       {
                //                                           new BlurBorder.XY(7,25),
                //                                           new BlurBorder.XY(8,7), 
                //                                           new BlurBorder.XY(10,5),
                //                                           new BlurBorder.XY(20,8), 
                //                                           new BlurBorder.XY(37,23),
                //                                           new BlurBorder.XY(22,36), 
                //                                           new BlurBorder.XY(7,25)
                //                                       }); 
                // public static EcoObjectsList CreateEcoObjectsList(EGH01DB.IDBContext dbcontext, Point center, float distance1 = 0.0f, float distance2 = float.MaxValue)

                this.bb = new BlurBorder(this.f0.db, this.f0.riskobject.coordinates, this.R1);


            }

     }
     public  class ECOForecast2     // почва  
     {

         public ECOForecast0 f0 { get; private set; }
         public ECOForecast1 f1 { get; private set; }
         public float        u2 { get; private set; }    // нефтеемкость 
         public float        h2 { get; private set; }    // средняя толщина почвенного слоя
         public float        H2 { get; private set; }    // глубина проникновения  
         public float        M2 { get; private set; }    // адсорбированная почвой масса
         public float       dM2 { get; private set; }    // остаток НП достигший почвы 
        
         public ECOForecast2(ECOForecast1 f1)
         {
             this.f0 = f1.f0;
             this.f1 = f1;
             this.dM2 = this.f1.dM1 - this.f1.M1 > 0? this.f1.dM1 - this.f1.M1: 0.0f;
             this.u2 = OilCapacity.defaultvalue.ocapacity; 
             this.h2 = f0.riskobject.soiltype.gumus_depth;
             this.M2 = f1.S1 * this.h2 * this.u2 * this.f0.ro0;
             this.H2 = (this.h2 * this.dM2/this.M2) > this.h2 ? this.h2 : (this.h2 * this.dM2 / this.M2);    
             
         }
     }
     public class ECOForecast3     // грунт  
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
         public float        v3  { get; private set; }   // горизонтальная скорость проникновения нп в грунте 
         public float        dM3 { get; private set; }    // остаток НП достигший грунта

         public ECOForecast3(ECOForecast2 f2)
         {
             this.f0 = f2.f0;
             this.f1 = f2.f1;
             this.f2 = f2;
             this.dM3 = this.f2.dM2 - this.f2.M2 > 0 ? this.f2.dM2 - this.f2.M2 : 0;
             {
                 this.ro3 = 1000;
                 WaterProperties wp = null;
                 float delta = 0;
                 if (WaterProperties.Get(f0.db, 20.0f, out wp, out delta))   //f0.temperature
                 {
                     this.rov = wp.density;
                     this.deltav = wp.tension;
                 }
             }

             this.groundtypename = this.f0.riskobject.groundtype.name;
             this.h3 = this.f0.riskobject.waterdeep;
             this.k3 = this.f0.riskobject.groundtype.waterfilter;
             this.r3 = this.f0.riskobject.groundtype.holdmigration;
             this.m3 = this.f0.riskobject.groundtype.porosity;
             this.w3 = this.f0.riskobject.groundtype.watercapacity;
             this.ro3 = this.f0.riskobject.groundtype.density;

             this.v3 = this.k3 / this.r3;

             this.M3 = this.h3 * this.f1.S1 * this.rov * this.m3 * this.w3 * this.f0.delta0 / this.deltav;
             
             if (this.dM3 <= this.M3) this.H3 = this.h3 * this.dM3 / this.M3;
                 else this.H3 = this.h3;
            
             if (this.H3 > 0) this.C3 = this.M3 / (this.f1.S1 * this.H3 * this.ro3);
             else this.C3 = 0;

         }

     }
     public class ECOForecast4     // грунтовые воды  
     {
         public ECOForecast0 f0 { get; private set; }
         public ECOForecast1 f1 { get; private set; }
         public ECOForecast2 f2 { get; private set; }
         public ECOForecast3 f3 { get; private set; }
        
         public float t4        { get; private set; }   //  интервал достижения грунтовых вод
         public float l4        { get; private set; }   //  максимальный радиус распространения загрязнения 
         public float v4        { get; private set; }   //  горизонтальная скорость распространения загрязнения
         public float C4        { get; private set; }   //  концентрация в гр. водах 
         public float h4       { get { return 1.0f; } }   //  толщина слоя грунтовых вод
         public float dM4       { get; private set; }    // остаток НП достигший грунтовых вод 



         public ECOForecast4(ECOForecast3 f3)
         { 
          this.f0 =  f3.f0;
          this.f1 =  f3.f1;  
          this.f2 =  f3.f2;
          this.f3 =  f3;
          this.dM4 =  this.f3.dM3 - this.f3.M3 > 0? this.f3.dM3 - this.f3.M3: 0.0f;
          this.t4  = (this.f2.h2+this.f3.h3)/this.f3.v3;                  // у насти ошибка !!!            
          this.l4  = this.dM4/(2*this.f1.R1 * this.h4 * this.f3.rov* this.f3.m3* this.f3.w3* this.f0.delta0/this.f3.deltav);
          this.C4  = this.dM4/(2*this.f1.R1*this.l4);                     // у насти ошибка !!! 

         }
         
     }


     public class F1EcoObjectsList : List<F1EcoObjectsList.F1EcoObject>
     {
         public class F1EcoObject
         {
             public string prefix = "BAS";
             public int    id;
             public float  distance;
             public float  height;
             public float  angle;
             public string line
             {
                 get
                 {
                     return String.Format("{0}-{1,5}: расстояние:{2,6}м,  высота:{3,4}м,  угол:{4, 5}",
                                          this.prefix,
                                          this.id,
                                          Math.Round(this.distance, 1),
                                          this.height,
                                          Math.Round(Math.Atan(this.angle), 3));
                                       }
             }
         }

         public F1EcoObjectsList(string px, Point center,  EcoObjectsList ecojbjectslist)
         {

             AddRange(px, center, ecojbjectslist);
             //if (ecojbjectslist != null)
             //{    
             //    foreach (EcoObject eo in ecojbjectslist)
             //    {
                   
             //        this.Add(new F1EcoObject()
             //                    {
             //                        prefix = px, 
             //                        id = eo.id,
             //                        height = eo.height,
             //                        distance = center.coordinates.Distance(eo.coordinates),
             //                        angle = center.coordinates.Distance(eo.coordinates) != 0?(center.height - eo.height) / center.coordinates.Distance(eo.coordinates): 0
             //                      }
             //                );
             //    }
             //}              
         }
         public bool  AddRange(string px, Point center, EcoObjectsList ecojbjectslist)
         {
             bool rc = false;
             if   (rc  = (ecojbjectslist != null))
             {
                 foreach (EcoObject eo in ecojbjectslist)
                 {
                     this.Add(new F1EcoObject()
                                     {
                                         prefix = px,
                                         id = eo.id,
                                         height = eo.height,
                                         distance = center.coordinates.Distance(eo.coordinates),
                                         angle = center.coordinates.Distance(eo.coordinates) != 0 ? (center.height - eo.height) / center.coordinates.Distance(eo.coordinates) : 0
                                     }
                              );
                 }
             }
             return rc;
          }


     }


     public class F1AnchorPointList : List<F1AnchorPointList.F1AnchorPoint>
     {
         public class F1AnchorPoint
         {
             public string prefix = "ANCH";
             public int id;
             public float distance;
             public float height;
             public float angle;
             public string line
             {
                 get
                 {
                     return String.Format("{0}-{1,5}: расстояние:{2,6}м,  высота:{3,4}м,  угол:{4, 5}",
                                          this.prefix,
                                          this.id,
                                          Math.Round(this.distance, 1),
                                          this.height,
                                          Math.Round(Math.Atan(this.angle), 3));
                 }
             }
         }

         public F1AnchorPointList(string px, Point center,   AnchorPointList anchorslist)
         {

             AddRange(px, center, anchorslist);
            
         }
         public bool AddRange(string px, Point center, AnchorPointList anchorslist)
         {
             bool rc = false;
             if (rc = (anchorslist != null))
             {
                 foreach (AnchorPoint ap in anchorslist)
                 {
                     this.Add(new F1AnchorPoint()
                                     {
                                         prefix = px,
                                         id = ap.id,
                                         height = ap.height,
                                         distance = center.coordinates.Distance(ap.coordinates),
                                         angle = center.coordinates.Distance(ap.coordinates) != 0 ? (center.height - ap.height) / center.coordinates.Distance(ap.coordinates) : 0
                                     }
                              );
                 }
             }
             return rc;
         }

     }



     public class BlurBorder
     {
         public class XY
         {
             public float x;
             public float y;
             public XY(float x, float y) { this.x = x; this.y = y; }
         }
            public int R;
            public int LimitR = 7;
            public int nr;
            public int CanvaX = 450;
            public int CanvaY = 450;
            public XY center;
            public XY center2;
            // public float KS = 1.0f;
            // public XY[] xy;
            public Coordinates[] border;
            public float[] height;
            public float[] resigular;
            public float[] length;
            public Coordinates[] newborder;
            public float[] distance;
            public Coordinates[] newborder2;
            public float[] newlength;
            public XY[] newborder3;

            //   public BlurBorder(float r, XY[] xy)
            //{
            //    this.xy = xy;
            //    this.KS = getKS();
            //    this.nr = (int)Math.Ceiling(r);
            //    this.R =  (int)Math.Ceiling(r*KS);

            //    this.center = new XY(this.CanvaX / 2, this.CanvaY / 2);
            //    for (int k = 0; k < this.xy.Length; k++)
            //    {
            //        this.xy[k].x = (int)(this.KS * (float)this.xy[k].x);
            //        this.xy[k].y = (int)(this.KS * (float)this.xy[k].y); 
            //    }

            //}
            //private float getKS()
            //{
            //    float rc = 1.0f;
            //    int   mxy = 1;
            //    for (int k = 0; k < this.xy.Length; k++)
            //    {
            //        mxy = xy[k].x > mxy ? xy[k].x : mxy;
            //        mxy = xy[k].y > mxy ? xy[k].y : mxy;
            //    }
            //    rc = ((float)Math.Min(this.CanvaX, this.CanvaY)) / ((float)mxy); 

            //    return rc;
            //}
            public BlurBorder(IDBContext db, Coordinates coordinates, float r)
            {


                this.border = new Coordinates[8];
                Coordinates c = new Coordinates(coordinates);
                for (int i = 0; i < 8; i++)
                {
                    border[i] = c.getByAngle(coordinates, 45 * i, r);
                }

                this.height = new float[8] ;

                for (int i = 0; i < 8; i++)
                {
                    height[i] = 0.0f;
                    if (!MapHelper.GetHeight(db, border[i], out height[i])) height[i] = -1.0f;
                }

                this.resigular = new float[8];
                float h0 = 0.0f;
               if (!MapHelper.GetHeight(db, coordinates, out h0)) h0 = -1.0f;
                for (int i = 0; i < 8; i++)
                {
                    resigular[i] = h0 - height[i];
                }

                this.length = new float[8];
                for (int i = 0; i < 8; i++)
                {
                    length[i] = resigular[i] + r;
                }

                this.newborder = new Coordinates[8];
                for (int i = 0; i < 8; i++)
                {
                    newborder[i] = c.getByAngle(coordinates, 45 * i, length[i]);
                }

                this.distance = new float[8];
                for (int i = 0; i < 8; i++)
                {
                    if (i == 7)
                    {
                        this.distance[i] = new Coordinates(newborder[i]).Distance(newborder[0]);

                    }
                    else
                        this.distance[i] = new Coordinates(newborder[i]).Distance(newborder[i + 1]);
                }

                float t = 0.0f;
                for (int i = 0; i < 8; i++)
                {
                    float p;
                    float g;
                    if (i == 7)
                    {
                        p = (length[i] + length[0] + distance[i]) / 2;
                        g = (float)Math.Sqrt(p * (p - length[i]) * (p - length[0]) * (p - distance[i]));

                    }
                    else
                    {
                        p = (length[i] + length[i + 1] + distance[i]) / 2;
                        g = (float)Math.Sqrt(p * (p - length[i]) * (p - length[i + 1]) * (p - distance[i]));
                    }

                    t += g;

                }

                int k = (int)Math.Sqrt(((float)Math.PI * (float)Math.Pow(r, 2)) / t);

                this.newlength = new float[8];
                for (int i = 0; i < newlength.Length; i++)
                {
                    newlength[i] = length[i] * k;
                }

                this.newborder2 = new Coordinates[8];
                Coordinates co = new Coordinates(coordinates);
                for (int i = 0; i < 8; i++)
                {
                    newborder2[i] = co.getByAngle(coordinates, 45 * i, newlength[i]);
                }

                float max_x = 1.0f;
                float max_y = 1.0f;
                float ymax_x = 0.0f;
                float ymax_y = 0.0f;
                float ymin_x = 0.0f;
                float xmin_y = 0.0f;
                for (int i = 0; i < this.newborder2.Length; i++)
                {
                    max_x = newborder2[i].latitude > max_x ? newborder2[i].latitude : max_x;
                    max_y = newborder2[i].lngitude > max_y ? newborder2[i].lngitude : max_y;
                    if (newborder2[i].latitude == max_x)
                    {
                        ymax_x = newborder2[i].lngitude;
                    }
                    if (newborder2[i].lngitude == max_y)
                    {
                        ymax_y = newborder2[i].latitude;
                    }
                }
                float min_y = 999.0f;
                float min_x = 999.0f;
                for (int i = 0; i < this.newborder2.Length; i++)
                {
                    min_x = newborder2[i].latitude < min_x ? newborder2[i].latitude : min_x;
                    min_y = newborder2[i].lngitude < min_y ? newborder2[i].lngitude : min_y;

                    if (newborder2[i].latitude == min_x)
                    {
                        ymin_x = newborder2[i].lngitude;
                    }
                    if (newborder2[i].lngitude == min_y)
                    {
                        xmin_y = newborder2[i].latitude;
                    }

                }

                //float l1 = (float)Math.Abs(coordinates.latitude - max_x);
                //float l2 = (float)Math.Abs(coordinates.lngitude - max_y);
                //float l3 = (float)Math.Abs(coordinates.latitude - min_x);
                //float l4 = (float)Math.Abs(coordinates.lngitude - min_y);

                float l1 = (float)Math.Sqrt(Math.Pow(coordinates.latitude - max_x, 2) + Math.Pow(coordinates.lngitude - ymax_x, 2));
                float l2 = (float)Math.Sqrt(Math.Pow(coordinates.latitude - xmin_y, 2) + Math.Pow(coordinates.lngitude - min_y, 2));
                float l3 = (float)Math.Sqrt(Math.Pow(coordinates.latitude - min_x, 2) + Math.Pow(coordinates.lngitude - ymin_x, 2));
                float l4 = (float)Math.Sqrt(Math.Pow(coordinates.latitude - ymax_y, 2) + Math.Pow(coordinates.lngitude - max_y, 2));

                float l5 = l1 + l3;
                float l6 = l2 + l4;

                float min = 0.0f;
                float max = 0.0f;
                if (l5 < l6)
                {
                    max = l6;
                    min = l5;
                }

                else
                {
                    max = l5;
                    min = l6;
                }


                float k1 = 0.0f;
                k1 = 400 / max;
                float k2 = 400 / min;


                this.newborder3 = new XY[8];
                for (int i = 0; i < 8; i++)
                {
                   
                    float a = (newborder2[i].latitude - coordinates.latitude) * k2 + this.CanvaX / 2;
                    float b = (newborder2[i].lngitude - coordinates.lngitude) * k1 + this.CanvaY / 2;
                    newborder3[i] = new XY(a, b);
                }

                float f = (coordinates.latitude - min_x) * k2;
                float h = (coordinates.lngitude - min_y) * k1;
                this.center = new XY(f, h);


                this.nr = (int)Math.Ceiling(r);
                this.R = (int)Math.Ceiling(nr * 400 / (min * Coordinates.EquatorLat1DegreeLength_m));
                this.center2 = new XY(this.CanvaX / 2, this.CanvaY / 2);



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

