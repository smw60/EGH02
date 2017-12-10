using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Objects;
using EGH01DB.Types;
using EGH01DB.Primitives;

namespace EGH01DB.Points
{
    public class MapePoint : Point
    {
        public District district { get; private set; }         // район область 
        public Climat climat { get; private set; }             // температура  
        public EcoObject ecoobject { get; private set; }       // информция о природоохранном объекте, если точка попала в объект   
        public SoilType soiltype { get; private set; }         // почва
        public GroundType groundtype { get; private set; }     // информация о грунте
        public float height { get; private set; }              // высота над уровнем моря
        public float waterdeep { get; private set; }           // глубина грунтовых вод
        

        public MapePoint(IDBContext db, Coordinates coordinates)
            : base(MapePoint.getPoint(db, coordinates))
        {
            this.district = MapePoint.getDistrict(db, coordinates);
            this.climat = MapePoint.getClimat(db, coordinates);
            this.ecoobject = MapePoint.getEcoobject(db, coordinates);  // если null, то  точка не на территории природохранного объекта
            this.soiltype = MapePoint.getSoilType(db, coordinates);
            this.groundtype = MapePoint.getGroundType(db, coordinates);
            this.height = MapePoint.getHeight(db, coordinates);
            this.waterdeep = MapePoint.getWaterdeep(db, coordinates);
        }

        public static Point getPoint(IDBContext db, Coordinates coordinates)
        {
            Point rc = null;
            if (!Point.GetByMap(db, coordinates, out rc)) rc = Point.defaultvalue;
            return rc;
        }

        public static District getDistrict(IDBContext db, Coordinates coordinates)
        {
            District rc = null;
            if (!District.GetByMap(db, coordinates, out rc))  rc = District.defaulttype;
            return rc;
        }
        public static Climat getClimat(IDBContext db, Coordinates coordinates)
        {
            Climat rc = null;
            if (!Climat.GetByMap(db, coordinates, out rc)) rc = new Climat(db, coordinates);
            return rc;
        }
        public static EcoObject getEcoobject(IDBContext db, Coordinates coordinates)
        {
            EcoObject rc = null;
            if (!EcoObject.GetByMap(db, coordinates, out rc)) rc = null;
            // если null, то координаты не принадлежат объету
            return rc;
        }
        public static SoilType getSoilType(IDBContext db, Coordinates coordinates)
        {
            SoilType rc = null;
            if (!SoilType.GetByMap(db, coordinates, out rc)) rc = SoilType.defaultype;
            return rc;
        }
         public static GroundType getGroundType(IDBContext db, Coordinates coordinates)
        {
            GroundType rc = null;
            if (!GroundType.GetByMap(db, coordinates, out rc)) rc = GroundType.defaulttype;
            return rc;
        }
         public static float getHeight(IDBContext db, Coordinates coordinates)
         {
             float rc;
             if (!MapHelper.GetHeight(db, coordinates, out rc)) rc = 0.0f;
             return rc;
         }
         public static float getWaterdeep(IDBContext db, Coordinates coordinates)
         {
             float rc = 0.0f; ;
             string aeration_power;
             float average_aeration_power;
             float max_aeration_power;
             string litology;
             if (MapHelper.GetAerationZone(db, coordinates, out aeration_power, out average_aeration_power, out max_aeration_power, out litology)) rc = average_aeration_power;
             return rc;
         }



    }
}
