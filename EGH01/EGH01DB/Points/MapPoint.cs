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
        public EcoObject ecoobject { get; private set; }       // информция о природоохр. объекте   
        public SoilType soiltype { get; private set; }         // почва

        public MapePoint(IDBContext db, Coordinates coordinates)
            : base(MapePoint.getPoint(db, coordinates))
        {
            this.district = MapePoint.getDistrict(db, coordinates);
            this.climat = MapePoint.getClimat(db, coordinates);
            this.ecoobject = MapePoint.getEcoobject(db, coordinates);  // если null, то  точка не на территории природохранного объекта
            this.soiltype = MapePoint.getSoilType(db, coordinates);

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
            if (!District.GetByMap(db, coordinates, out rc)) rc = District.defaulttype;
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



    }
}
