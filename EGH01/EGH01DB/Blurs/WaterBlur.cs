using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Objects;
using EGH01DB.Primitives;
using System.Xml;

namespace EGH01DB.Blurs
{

    public partial class WaterBlur              //  водное пятно -  пятно нефтеродукта c грунтовыми водами  
    { 
        public GroundBlur groudblur                 {get; private set;}  // пятно по поверхности 
        public CoordinatesList border               {get; private set;}  // координаты граничных точек водного пятна
        public float radius                         {get; private set;}  // радиус водного пятна (м)   -
        public float toobporosity                   {get; private set;}  // пористость водоносного слоя  
        public float toobheight                     {get; private set;}  // высота (мощность) водрносного слоя  
        public EcoObjectsList     ecoobjectslist    {get; private set;}  // список природоохранных объектов в водном пятне    
        public WaterPollutionList watepollutionlist {get; private set;}  // список точек в водном пятне    

       
        public WaterBlur(IDBContext db, GroundBlur groundblur)
        {
            this.groudblur = groundblur;
            this.toobporosity = this.groudblur.spreadpoint.groundtype.porosity / 2.0f;    // считаем такой пористость водоносного слоя          
            this.toobheight = 1.0f;                                                       // считаем мощность водоносного слоя = 1 

            this.radius = 
                    this.groudblur.restmass /                                                               // макс. радиус поиска природоохранных объектов 
                    (
                        this.toobheight *                                                                    // мощность слоя грунтовых вод (1м) 
                        2.0f * this.groudblur.radius /                                                       // площадь трубы 
                        2.0f *                                                                               // треугольник
                        this.groudblur.waterproperties.density *                                             // плотность воды  
                        this.groudblur.spreadpoint.groundtype.porosity  *                                     // пористость грунта /2 c водой
                        this.groudblur.spreadpoint.groundtype.watercapacity *                                 // капилярная влагоемкость грунта                                                      // максиальная маса нефтепродукта, кот. может быть адсорбирована грунтом (кг) 
                        (float)Math.Pow(this.groudblur.spreadpoint.petrochemicaltype.dynamicviscosity, 2) *   // динамическая вязкость ???      
                        this.groudblur.waterproperties.tension /                                              // коэфициент поверхностного натяжения воды
                        (
                        this.groudblur.spreadpoint.petrochemicaltype.tension *                                // коэфициент поверхностного натяжения нефтепродукта 
                        (float)Math.Pow(this.groudblur.waterproperties.viscocity, 2)                           //  вязкость воды  
                        )
                     );
            
  //this.limitadsorbedmass =                                                                     // максиальная маса нефтепродукта, кот. может быть адсорбирована грунтом (кг)    
  //                                   this.avgdeep *                                                           // средняя глубина грутовы вод 
  //                                   this.square *                                                            // площадь пролива 
  //                                   this.waterproperties.density *                                           // плотность воды  
  //                                   this.spreadpoint.groundtype.porosity *                                   // пористость грунта 
  //                                   this.spreadpoint.groundtype.watercapacity *                              // капилярная влагоемкость грунта                                                     // максиальная маса нефтепродукта, кот. может быть адсорбирована грунтом (кг) 
  //                                   (float)Math.Pow(this.spreadpoint.petrochemicaltype.dynamicviscosity, 2) *       // динамическая вязкость      
  //                                   this.waterproperties.tension /                                           // коэфициент поверхностного натяжения воды
  //                                   (
  //                                   this.spreadpoint.petrochemicaltype.tension *                             // коэфициент поверхностного натяжения нефтепродукта 
  //                                   (float)Math.Pow(this.waterproperties.viscocity, 2)                       //  вязкость воды  
  //                                   );







           this.ecoobjectslist = EcoObjectsList.CreateEcoObjectsList(db, this.groudblur.spreadpoint,  this.groudblur.radius,   this.radius);
           this.watepollutionlist = new WaterPollutionList();
           float sv = 0;
           foreach (EcoObject o in this.ecoobjectslist)
           {
               float h1 =  this.groudblur.spreadpoint.height- o.height;                           //раница высот  
               float h2 =  o.waterdeep - this.groudblur.spreadpoint.waterdeep;                     //раница глубин
               float h = (h2 == 0?h1:h2); 

               float d = this.groudblur.spreadpoint.coordinates.Distance(o.coordinates);           //растояние  
               float a = d <= 0.0f ? 0.0f : h / d;                                                 //гидравлический угол
               float v =  a<=0?0.0f:this.groudblur.spreadpoint.groundtype.waterfilter * a;         //горинтальная скорость 
               sv += v;                                                                            //сумма скоростей для нормирования 
               this.watepollutionlist.Add(new WaterPollution(o, d, a, this.groudblur.spreadpoint.petrochemicaltype, v, 0.0f, v>0.0f?d/v:Const.TIME_INFINITY));
           }
           this.border = new CoordinatesList();

           foreach (WaterPollution p in this.watepollutionlist)
           {
               if (p.speedhorizontal > 0.0f)
               {
                  // p.maxconcentration = ((p.speedhorizontal / sv) * this.groudblur.restmass) / (2.0f * this.groudblur.radius * p.distance * 1.0f);
                   p.maxconcentration = ((p.speedhorizontal / sv) * this.groudblur.restmass) / (2.0f * this.groudblur.radius * p.distance * 1.0f * p.groundtype.density);
               }
     
           }
        }

        public WaterBlur(XmlNode node)
        {
            XmlNode groudblur = node.SelectSingleNode(".//GroundBlur");
            if (groudblur != null) this.groudblur = new GroundBlur(groudblur);
            else this.groudblur = null;

            XmlNode coordinates_list = node.SelectSingleNode(".//CoordinatesList");
            if (coordinates_list != null) this.border = CoordinatesList.CreateCoordinatesList(coordinates_list);
            else this.border = null;

            this.radius = Helper.GetFloatAttribute(node, "radius", 0.0f);
            this.toobporosity = Helper.GetFloatAttribute(node, "toobporosity", 0.0f);
            this.toobheight = Helper.GetFloatAttribute(node, "toobheight", 0.0f);

            XmlNode eco_objects_list = node.SelectSingleNode(".//EcoObjectsList");
            if (eco_objects_list != null) this.ecoobjectslist = EcoObjectsList.CreateEcoObjectsList(eco_objects_list);
            else this.ecoobjectslist = null;

            XmlNode water_pollution_list = node.SelectSingleNode(".//WaterPollutionList");
            if (water_pollution_list != null) this.watepollutionlist = WaterPollutionList.CreateWaterPollutionList(water_pollution_list);
            else this.watepollutionlist = null;
            
        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("WaterBlur");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);

            rc.AppendChild(doc.ImportNode(this.groudblur.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.border.toXmlNode(), true));

            rc.SetAttribute("radius", this.radius.ToString());
            rc.SetAttribute("toobporosity", this.toobporosity.ToString());
            rc.SetAttribute("toobheight", this.toobheight.ToString());

            rc.AppendChild(doc.ImportNode(this.ecoobjectslist.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.watepollutionlist.toXmlNode(), true));
            
            return (XmlNode)rc;
        }
    }  
}


//this.ecoobjecstlist = EcoObjectsList.CreateEcoObjectsList(groudblur.spreadpoint, groudblur.radius, radius);
//this.pollutionlist = WaterPollutionList.CreateWaterPollutionList(groudblur.spreadpoint, groudblur.groundpolutionlist, groundblur.spreadpoint.petrochemicaltype, groudblur.radius, this.radius);