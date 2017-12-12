using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Globalization;
using EGH01DB.Types;
using EGH01DB.Objects;
using EGH01DB.Points;

namespace EGH01DB.Primitives
{
    public partial class  Helper
    {

        static public bool FloatTryParse(string s, out float f)
        {
            f = 0.0f;            
            CultureInfo ci = new CultureInfo("en-US");
            if (s.IndexOf(",") >=0) ci = new CultureInfo("ru-RU");     
            return float.TryParse(s, NumberStyles.Any,ci, out f); 
        }
        static public float GeoAngle(Point point1, Point point2)
        {
            float rc = 0.0f;
            float d = point1.coordinates.Distance(point2.coordinates);  // расстояние 
            if (d > 1.0f) rc = (point1.height - point2.height) / d;     // угол    
            return rc;
        }


     }
}
    