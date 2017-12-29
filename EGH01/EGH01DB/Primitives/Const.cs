using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGH01DB.Primitives
{
    public  class Const
    {
        public const float SEC_PER_DAY = 86400.0f;
        public const float LATM = 111321.4f;                               // 1 град широты на экваторе
        public const float LNGM = 111134.9f;                               // 1 град долготы   
        public const float KG_to_MG = 1000000.0f;                          // кг в мг 
        public const float MG_to_KG = 0.000001f;                           // мг в кг
        public const float M3_to_DM3 = 1000.0f;

        public const float ZERO = 0.0000001f;                               // ноль 
        public const float TIME_INFINITY = float.MaxValue;                  //бесконечный интервал времени 
        public static readonly DateTime DATE_INFINITY = DateTime.MaxValue;  //бесконечная дата  

        public static bool isINFINITY(float v)
        {
            bool rc = false;
            if (rc =  (v == TIME_INFINITY)) rc = true;
            {

               try { DateTime dt = DateTime.Now.AddYears(100).AddSeconds(v);}
               catch (System.ArgumentOutOfRangeException) { rc = true; };

            }
            return rc;
        }
        public static bool isINFINITY(DateTime v) {return   (DateTime.MaxValue.AddYears(-100) <= v) ;}

        public static float  KG_to_M3(float kg, float ro ){return kg*ro;}
    
    }
}
