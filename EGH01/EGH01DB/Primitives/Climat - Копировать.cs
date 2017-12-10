using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using EGH01DB.Types;
using EGH01DB.Objects;
using EGH01DB.Points;
using EGH01DB.Primitives;

namespace EGH01DB.Primitives
{
    public class OilCapacity
    {
       
        public float  ocapacity {get; private set;} 
        public static OilCapacity defaultvalue { get { return new OilCapacity();} }

        public OilCapacity(IDBContext db )
        {
            this.ocapacity = 0.1f;
        }

        public OilCapacity()
        {
             this.ocapacity = 0.1f;
        }

    }
}
