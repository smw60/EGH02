using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Primitives;

namespace EGH01DB.Types
{
    public class SoilType
    {
        public string name { get; private set; }             // name - наименование почв
        public float filter { get; private set; }            // коэффициент фильтрации kf
        public float porosity { get; private set; }          // коэффициент пористости p  
        public float watercapacity { get; private set; }     // коэффициент влагоемкости kv
        public float gumus_height { get; private set; }             // высота почвенного покрова в см gumus
        public string klass { get; private set; }             // класс почв по классификатору klass
        public string soil_type { get; private set; }         // тип почв по классификатору type
        public static SoilType defaultype { get { return new SoilType("не определен", 1f, 1f, 1f, 0.2f, "не определен", "не определен"); } }

        public SoilType()
        {
            this.name = "";
            this.filter = 0.0f;
            this.porosity = 0.0f;
            this.watercapacity = 0.0f;
            this.gumus_height = 0.0f;
            this.klass = "";
            this.soil_type = "";
        }
        public SoilType(string name, float filter, float porosity, 
                        float watercapacity, float gumus_height,
                        string klass, string soil_type)
        {
            this.name = name;
            this.filter = filter;
            this.porosity = porosity;
            this.watercapacity = watercapacity;
            this.gumus_height = gumus_height;
            this.klass = klass;
            this.soil_type = soil_type;
        }
        public static bool GetByMap(IDBContext db, Coordinates coordinates, out SoilType soiltype)
        {
            // заглушка
            soiltype = SoilType.defaultype;
            return true;
        }


    }
}
