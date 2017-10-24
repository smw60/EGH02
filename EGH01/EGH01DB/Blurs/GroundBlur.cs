using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Objects;
using EGH01DB.Types;
using EGH01DB.Points;
using EGH01DB.Primitives;
using System.Xml;

// температура 

namespace EGH01DB.Blurs
{
    public partial class GroundBlur              //  пятно  наземное нефтеродукта  
    {
        public SpreadPoint spreadpoint {get;  private set;}   //разлив нефтеродута 
        public CoordinatesList bordercoordinateslist { get; private set; }   // координаты граничных точек  пятна
        
        public SpreadingCoefficient spreadingcoefficient    {get; private set;}       // коэффициент разлива (1/м) 
        public float                square                  {get; private set;}       // площадь (м2)     
        public float                radius                  {get; private set;}       // радиус наземного пятна (м)     
        public float                totalmass               {get; private set;}       // масса пролива (кг)    
        public float                limitadsorbedmass       {get; private set;}       // максиальная маса нефтепродукта, кот. может быть адсорбирована грунтом (т) 
        public float                avgdeep                 {get; private set;}       // средняя глубина грунтовых вод по опорным точкам (м) 
        public float                petrochemicalheight     {get; private set;}       // высота слоя разлитого нефтепродукта (м) 
        public float                adsorbedmass            {get; private set;}       // адсобированная масса нефтепродукта грунтом (т)  
        public float                restmass                {get; private set;}       // масса нефтепродукта достигшая грунтовых вод  (т)  
        public float                depth                   {get; private set;}       // глубина проникновения нефтепродукта в грунт (м)  
        public float                concentrationinsoil     {get; private set;}       // средняя концентрация нефтепродукта в грунте (кг/м3)   
        public float                timeconcentrationinsoil {get; private set;}       // время (с) достижения усредненной концентрации  нефтепродукта в грунте      
        public float                speedvertical           {get; private set;}       // вертикальная скорость проникновения нефтепродукта в грунт (м/с)   
        public float                timewatercomletion      {get; private set;}       // время (с) достижения  нефтепродуктом грунтовых вод   
        public float                daywatercomletion       {get{return this.timewatercomletion/Const.SEC_PER_DAY;} }      // время (сут) достижения  нефтепродуктом грунтовых вод   
        public float                dtimemaxwaterconc       {get; private set;}       // время (с) достижения  максимальной концентрации  нефтепродуктом грунтовых вод  после достиженич границы грунтовых вод 
        public float                timemaxwaterconc        {get; private set;}       // время (с) достижения  максимальной концентрации на уровне грунтовых вод
        public float                daymaxwaterconc         {get{return this.timemaxwaterconc/Const.SEC_PER_DAY;} }    // время (сут) достижения  максимальной концентрации  нефтепродуктом грунтовых вод  после достиженич границы грунтовых вод 
        public float                maxconcentrationwater   {get; private set;}       // максимальной концентрация на уровне грунтовых вод кг/м3
        public float                ozcorrection            {get; private set;}       // OZ-поправка
        
       // public float                watertoobvolume       {get; private set }       // объем  
        public float                ecoobjectsearchradius   {get; private set;}       // радиус поиска  природоохранных объектов 

        public AnchorPointList     anchorpointlist          {get; private set;}       // список опорных точек, попавших в наземное пятно загрязнения    
        public GroundPollutionList groundpolutionlist       {get; private set;}       // список точек загрязнения  
        public WaterProperties      waterproperties         {get; private set;}       // физико-химические свойства воды  
        public EcoObjectsList       ecoobjecstlist          {get; private set;}       // список объектов в т.ч. заглавный которые попали в наземное пятно    
        //public GroundPollutionList pollutionlist          {get; private set;}       // загрязнение в точках  
       
        private string errormssageformat = "GroundBlur: Ошибка в данных. {0}";
        
        public GroundBlur(XmlNode node)
        {
            {
             XmlNode x = node.SelectSingleNode(".//SpreadPoint");
             if (x != null) this.spreadpoint = new SpreadPoint(x);
             else this.spreadpoint = null;
            } 
           

            XmlNode coordinates_list = node.SelectSingleNode(".//CoordinatesList");
            if (coordinates_list != null) this.bordercoordinateslist = CoordinatesList.CreateCoordinatesList(coordinates_list);
            else this.bordercoordinateslist = null;

            XmlNode spreading_coef = node.SelectSingleNode(".//SpreadingCoefficient");
            if (spreading_coef != null) this.spreadingcoefficient = new SpreadingCoefficient(spreading_coef);
            else this.spreadingcoefficient = null;

            this.square = Helper.GetFloatAttribute(node, "square", 0.0f);
            this.radius = Helper.GetFloatAttribute(node, "radius", 0.0f);
            this.totalmass = Helper.GetFloatAttribute(node, "totalmass", 0.0f);
            this.limitadsorbedmass = Helper.GetFloatAttribute(node, "limitadsorbedmass", 0.0f);

            this.avgdeep = Helper.GetFloatAttribute(node, "avgdeep", 0.0f);
            this.petrochemicalheight = Helper.GetFloatAttribute(node, "petrochemicalheight", 0.0f);
            this.adsorbedmass = Helper.GetFloatAttribute(node, "adsorbedmass", 0.0f);
            this.restmass = Helper.GetFloatAttribute(node, "restmass", 0.0f);

            this.depth = Helper.GetFloatAttribute(node, "depth", 0.0f);
            this.concentrationinsoil = Helper.GetFloatAttribute(node, "concentrationinsoil", 0.0f);
            this.timeconcentrationinsoil = Helper.GetFloatAttribute(node, "timeconcentrationinsoil", 0.0f);
            this.speedvertical = Helper.GetFloatAttribute(node, "speedvertical", 0.0f);

            this.timewatercomletion = Helper.GetFloatAttribute(node, "timewatercomletion", 0.0f);
            this.dtimemaxwaterconc = Helper.GetFloatAttribute(node, "dtimemaxwaterconc", 0.0f);
            this.timemaxwaterconc = Helper.GetFloatAttribute(node, "timemaxwaterconc", 0.0f);
            this.maxconcentrationwater = Helper.GetFloatAttribute(node, "maxconcentrationwater", 0.0f);
            this.ozcorrection = Helper.GetFloatAttribute(node, "ozcorrection", 0.0f);
            this.ecoobjectsearchradius = Helper.GetFloatAttribute(node, "ecoobjectsearchradius", 0.0f);

            { 
               XmlNode x = node.SelectSingleNode(".//AnchorPointList");
               if (x != null) this.anchorpointlist = AnchorPointList.CreateAnchorPointList(x);
               else this.anchorpointlist = null;
            }
            {
             XmlNode x = node.SelectSingleNode(".//GroundPollutionList");
             if (x != null) this.groundpolutionlist =  GroundPollutionList.Create(x);
             else this.groundpolutionlist = null;
            }
            {
              XmlNode x = node.SelectSingleNode(".//WaterProperties"      );
              if (x != null) this.waterproperties = new WaterProperties(x);
              else this.waterproperties = null;
            }
            {
              XmlNode x = node.SelectSingleNode(".//EcoObjectsList");
              if (x != null) this.ecoobjecstlist = EcoObjectsList.CreateEcoObjectsList(x);
              else this.ecoobjecstlist = null;
            }
 
           
        }
        public GroundBlur(SpreadPoint spreadpoint)
        {
            this.spreadpoint = spreadpoint;
            RGEContext db = new RGEContext();    // заглушка, выставить правильный контекст //blinova
            this.bordercoordinateslist = new CoordinatesList();

            if (this.spreadpoint.groundtype.watercapacity >=  this.spreadpoint.groundtype.porosity)
                throw new EGHDBException(string.Format(errormssageformat, "Влагоемкость грунта не может быть  больше или равна  пористости"));

            if (this.spreadpoint.groundtype.watercapacity >= this.spreadpoint.groundtype.soilmoisture)
                 throw new EGHDBException(string.Format(errormssageformat, "Влагоемкость грунта не может быть  больше или равна  влажности грунта"));
            
            if (this.spreadpoint.petrochemicaltype.tension  <= 0)
                throw new EGHDBException(string.Format(errormssageformat, "Коэффициент поверностного натяжение нефтеродукта не может быть меньше или равным нулю "));
            

            { // коэф. разлива 

               
                float k = SpreadingCoefficient.GetByData(db, this.spreadpoint.groundtype, this.spreadpoint.petrochemicaltype, this.spreadpoint.volume, 0.0f);
                this.spreadingcoefficient = new SpreadingCoefficient(0, this.spreadpoint.groundtype, this.spreadpoint.petrochemicaltype, 0.0f, this.spreadpoint.volume, 0.0f, 0.02f, k);

            }
            if (this.spreadingcoefficient.koef <= 0.0f)
            {
                                      //this.spreadingcoefficient = new SpreadingCoefficient(0, this.spreadpoint.groundtype, this.spreadpoint.petrochemicaltype, 0.0f, this.spreadpoint.volume, 0.0f, 0.02f, 5.0f);  // заглушка
                throw new EGHDBException(string.Format(errormssageformat, "Коэффициент разлива не может быть меньше или равен нулю"));

            }
               

           
            
            { // свойства воды 
                WaterProperties x = new WaterProperties();
               // RGEContext db = new RGEContext();// заглушка, выставить правильный контекст //blinova
                float delta = 0.0f;
                if (WaterProperties.Get(db, 20.0f, out x, out delta))
                {
                    this.waterproperties = x;
                }
            }
            if (this.waterproperties.viscocity <= 0)
                throw new EGHDBException(string.Format(errormssageformat, "Вязкость воды  не может быть меньше или равным нулю "));


            this.square = this.spreadpoint.volume * this.spreadingcoefficient.koef;                  // площадь  пятна 

            this.radius = (float)Math.Sqrt(square / Math.PI);                                        // радиус  пятна 

            this.petrochemicalheight = this.spreadpoint.volume / this.square;                        // высота слоя разлитого нефтепродукта (м)   

            this.totalmass = this.spreadpoint.volume * this.spreadpoint.petrochemicaltype.density;   // масса пролива 


         
           this.anchorpointlist = AnchorPointList.CreateNear(this.spreadpoint.coordinates, this.radius);

           this.ecoobjectsearchradius = this.radius;
           this.ecoobjecstlist = EcoObjectsList.CreateEcoObjectsList(db, this.spreadpoint, this.ecoobjectsearchradius);

           this.groundpolutionlist = new GroundPollutionList(this.spreadpoint);
           this.groundpolutionlist.AddRange(this.spreadpoint, this.anchorpointlist, this.spreadpoint.petrochemicaltype);
           this.groundpolutionlist.AddRange(this.spreadpoint, this.ecoobjecstlist,  this.spreadpoint.petrochemicaltype);


           //  переделать как метод this.groundpolutionlist
           this.avgdeep =                                                                               // средняя глубина грунтовых вод по опорным точкам  и техногенному  объекту
                         (
                          anchorpointlist.sumwaterdeep +
                          (this.spreadpoint.riskobject != null ? this.spreadpoint.waterdeep : 0.0f)
                          ) /
                         (anchorpointlist.Count + 1);
 
            
                this.limitadsorbedmass =                                                                     // максиальная маса нефтепродукта, кот. может быть адсорбирована грунтом (кг)    
                                     this.avgdeep *                                                           // средняя глубина грутовы вод 
                                     this.square *                                                            // площадь пролива 
                                     this.waterproperties.density *                                           // плотность воды  
                                     this.spreadpoint.groundtype.porosity*      (0.01f)*                      // пористость грунта  * 0.01 (smw 2017.06.18)
                                     this.spreadpoint.groundtype.watercapacity *                              // капилярная влагоемкость грунта                                                     // максиальная маса нефтепродукта, кот. может быть адсорбирована грунтом (кг) 
                                     (float)Math.Pow(this.spreadpoint.petrochemicaltype.dynamicviscosity, 2) *       // динамическая вязкость      
                                     this.waterproperties.tension /                                           // коэфициент поверхностного натяжения воды
                                     (
                                     this.spreadpoint.petrochemicaltype.tension *                             // коэфициент поверхностного натяжения нефтепродукта 
                                     (float)Math.Pow(this.waterproperties.viscocity, 2)                       //  вязкость воды  
                                     );
            

           



            this.adsorbedmass = (limitadsorbedmass >= this.totalmass ? this.totalmass : limitadsorbedmass);             // адсорбированная масса нефтепродукта в грунте т - М1  

            this.restmass = (this.adsorbedmass >= this.totalmass ? 0 : this.totalmass - this.adsorbedmass);             // масса нефтепродукта достигшая грунтовых вод 


           

            this.depth =  (this.restmass > 0 ? this.avgdeep : (float)Math.Round(this.avgdeep * (this.totalmass / this.limitadsorbedmass),3)); // глубина проникновения нефтепродукта в грунт     

            if (this.depth > 0)                                                                                        // если глубина проникновения > 0
            {
                this.concentrationinsoil =                                                                             // средняя концентрация нефтепродуктов в грунте  

                                     this.adsorbedmass /                                                               // адсорбированная масса нефтепродукта в грунте 
                                     (
                                      this.square *                                                                    // площадь  пятна   
                                      this.depth *                                                                     // глубина проникновения нефтепродукта в грунт     
                                      this.spreadpoint.groundtype.density                                              // средняя плотность грунта 
                                      );
            }
            else this.concentrationinsoil = 0.0f;

           

            {   //   вертикальная скорость проникновения нефтепродукта в грунт (м/с) 
                
                float ka =                                                                       // формула аверьянова
                           this.spreadpoint.groundtype.waterfilter *                             // коэф. фильтрации воды          
                           (
                            this.spreadpoint.groundtype.soilmoisture -                           // влажность грунта 
                            this.spreadpoint.groundtype.watercapacity                            // капилярная влагоемкость грунта  
                            ) /
                            (
                            this.spreadpoint.groundtype.porosity -                                //  пористость грунта 
                            this.spreadpoint.groundtype.watercapacity                             // капилярная влагоемкость грунта
                            );
               
                float r =                                                                          // коэффициент задержки 
                            (
                            this.spreadpoint.petrochemicaltype.dynamicviscosity *                  // вязкость нефтепродукта 
                            this.waterproperties.density                                           // плотность воды                               
                            ) /
                            (
                            this.waterproperties.viscocity *                                       // вязкость воды
                            this.spreadpoint.petrochemicaltype.density                             // плотность нефтепродукта 
                            );
                 
                this.speedvertical =  this.petrochemicalheight *                                   //  А.Авхимович: высота слоя разлитого нефтепродукта (м)             // вы 
                                      (ka / r);                                                     // вертикальная скорость проникновения нефтепродукта в грунт (м/с)                               
            }

            this.timeconcentrationinsoil =                                                         // время достижения усреднееной концентации  в грунте  
                                          this.depth /                                             // глубина проникновения нефтепродукта в грунт  
                                          this.speedvertical;                                      // вертикальная скорость проникновения нефтепродукта в грунт (м/с)   


            if (this.restmass > 0)
            {
                this.timewatercomletion =                                                           // время продвижения нефтепродукта до грунтовых вод 
                                     this.avgdeep /                                                 // средняя глубина грунтовых вод по опорным точкам (м) 
                                     this.speedvertical;                                            // вертикальная скорость проникновения нефтепродукта в грунт (м/с)   

                this.dtimemaxwaterconc =                                                                // время (с) достижения  максимальной концентрации  нефтепродуктом грунтовых вод  после достиженич границы грунтовых вод
                                         this.petrochemicalheight /                                     // высота слоя разлитого нефтепродукта (м)  
                                         (
                                         this.speedvertical *                                           // вертикальная скорость проникновения нефтепродукта в грунт (м/с)   
                                         this.spreadpoint.groundtype.porosity                           // пористость грунта 
                                         );

                this.timemaxwaterconc = this.timewatercomletion + this.dtimemaxwaterconc;                // время (с) достижения  максимальной концентрации на уровне грунтовых вод
            }
            else
            {
                this.timewatercomletion = Const.TIME_INFINITY;                                         // никогда 
                this.dtimemaxwaterconc = Const.TIME_INFINITY;                                          // никогда
                this.timemaxwaterconc =   Const.TIME_INFINITY;                                         // никогда         
            }
            

            {
                if (restmass > 0)  // если не все адсорбировалось в грунте 
                {

                    this.ozcorrection =                                                                  // OZ-поправка  
                                           this.petrochemicalheight *                                    // высота слоя разлитого нефтепродукта (м)      
                                           this.restmass /                                               // масса нефтепродукта достигшая грунтовых вод (кг)
                                           this.totalmass;                                               // масса пролива (кг)

                    this.maxconcentrationwater = (float)                                                 // максимальной концентрация на уровне грунтовых вод кг/м3
                        (
                           this.restmass /                                                               // масса нефтепродукта достигшая грунтовых вод (кг)
                           (Math.PI * this.radius * this.radius) *1.0f                                   // объем воды 
                           /
                           (this.ozcorrection * Math.Sqrt(2 * Math.PI))                                   // поправка 0Z   
                        );
                }
                else // если все адсорбировалось в грунте 
                {
                    this.ozcorrection = 0.0f;
                    this.maxconcentrationwater = 0.0f;
                }
            }

                      
            
        }

        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("GroundBlur");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);

            rc.AppendChild(doc.ImportNode(this.spreadpoint.toXmlNode(), true));
            //coordinates list
            rc.AppendChild(doc.ImportNode(this.bordercoordinateslist.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.spreadingcoefficient.toXmlNode(), true));

            rc.SetAttribute("square", this.square.ToString());
            rc.SetAttribute("radius", this.radius.ToString());
            rc.SetAttribute("totalmass", this.totalmass.ToString());
            rc.SetAttribute("limitadsorbedmass", this.limitadsorbedmass.ToString());

            rc.SetAttribute("avgdeep", this.avgdeep.ToString());
            rc.SetAttribute("petrochemicalheight", this.petrochemicalheight.ToString());
            rc.SetAttribute("timeconcentrationinsoil", this.timeconcentrationinsoil.ToString());
            rc.SetAttribute("speedvertical", this.speedvertical.ToString());

            rc.SetAttribute("depth", this.depth.ToString());
            rc.SetAttribute("concentrationinsoil", this.concentrationinsoil.ToString());
            rc.SetAttribute("adsorbedmass", this.adsorbedmass.ToString());
            rc.SetAttribute("restmass", this.restmass.ToString());

            rc.SetAttribute("timewatercomletion", this.timewatercomletion.ToString());
            rc.SetAttribute("daywatercomletion", this.daywatercomletion.ToString()); 
            rc.SetAttribute("dtimemaxwaterconc", this.dtimemaxwaterconc.ToString());
            rc.SetAttribute("timemaxwaterconc", this.timemaxwaterconc.ToString());
            rc.SetAttribute("daymaxwaterconc", this.daymaxwaterconc.ToString()); 
            rc.SetAttribute("maxconcentrationwater", this.maxconcentrationwater.ToString());
            rc.SetAttribute("ozcorrection", this.ozcorrection.ToString());
            rc.SetAttribute("ecoobjectsearchradius", this.ecoobjectsearchradius.ToString());

            rc.AppendChild(doc.ImportNode(this.anchorpointlist.toXmlNode(), true));
            // groundpolutionlist 
            rc.AppendChild(doc.ImportNode(this.groundpolutionlist.toXmlNode(), true));

            rc.AppendChild(doc.ImportNode(this.waterproperties.toXmlNode(), true));
            // ecoobjecstlist  
            rc.AppendChild(doc.ImportNode(this.ecoobjecstlist.toXmlNode(), true));
            return (XmlNode)rc;
        }

        private CoordinatesList createbordercoordinateslist() // построение граничных точек пятна загрязнения 
        {

            return new CoordinatesList();
        }
        private float calcradius()  // вычисление радиуса
        {
            return 0.0f;
        }
        private float calcsquare()   // вычисление площади загрязнения 
        {
            return 0.0f;
        }
        private EcoObjectsList createecoobjectslist()  // формирование списка природоохранных объектов
        {
            return new EcoObjectsList();
        }
        //private GroundPollutionList creategroundpolutionlist() // формирование списка наземных точек загрязнения объектов
        //{
        //    return new GroundPollutionList();
        //}

    }
}
 //{
                                                                                      // радиус поиска природоохранных объектов               

                //if (this.restmass > 0)
                //{
                //    this.ecoobjectsearchradius =
                //                                    this.restmass /                                                               // радиус поиска природоохранных объектов 
                //                                     (
                //                                         1.0f *                                                                   // мощность слоя грунтовых вод (1м) 
                //                                         2.0f * this.radius /                                                     // площадь трубы 
                //                                         2.0f *                                                                   // треугольник
                //                                         this.waterproperties.density *                                           // плотность воды  
                //                                         (this.spreadpoint.groundtype.porosity) *                                 // пористость грунта /2 c водой
                //                                         this.spreadpoint.groundtype.watercapacity *                              // капилярная влагоемкость грунта                                                      // максиальная маса нефтепродукта, кот. может быть адсорбирована грунтом (кг) 
                //                                         (float)Math.Pow(this.spreadpoint.petrochemicaltype.dynamicviscosity, 2) *       // динамическая вязкость ???      
                //                                         this.waterproperties.tension /                                           // коэфициент поверхностного натяжения воды
                //                                         (
                //                                         this.spreadpoint.petrochemicaltype.tension *                             // коэфициент поверхностного натяжения нефтепродукта 
                //                                         (float)Math.Pow(this.waterproperties.viscocity, 2)                       //  вязкость воды  
                //                                         )
                //                                      );

                //}
                //else this.ecoobjectsearchradius = this.radius;
  //   }
 //SpreadingCoefficient x = new SpreadingCoefficient();
                //this.spreadingcoefficient = x = new SpreadingCoefficient();
                //if (SpreadingCoefficient.GetByParms(this.spreadpoint.groundtype, this.spreadpoint.petrochemicaltype, this.spreadpoint.volume, 0.0f, out x))
                //{
                //    this.spreadingcoefficient = x;
                //}
