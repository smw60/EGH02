using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGH01DB.Primitives
{
    public class Climat
    {
        public Coordinates coordinates { get; private set; }
        public float[] temperature { get; private set; }

        private static float[] defaulttemperature = { -1f, -1f, 0f, 1f, 10f, 15f, 20f, 20f, 10f, 5f, 1f, -1f };
        public static Climat defaultvalue { get { return new Climat(Coordinates.defaultvalue); } }

        public Climat(IDBContext db, Coordinates coordinates)
        {
            this.coordinates = coordinates;
            this.temperature = Climat.defaulttemperature;
        }
        public Climat(Coordinates coordinates)
        {
            this.coordinates = coordinates;
            this.temperature = Climat.defaulttemperature;
        }



        public Climat(IDBContext db, Coordinates coordinates, float[] temperature)
        {
            this.coordinates = coordinates;
            this.temperature = temperature;

        }


        public static bool GetByMap(IDBContext db, Coordinates coordinates, out Climat climat)
        {
            // заглушка
            climat = new Climat(db, coordinates);
            return true;

        }


    }
}
