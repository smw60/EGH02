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


        public static bool GetByMap(IDBContext dbcontext, Coordinates coordinates, out Climat climat)
        {
            bool rc = false;
            climat = new Climat(dbcontext, coordinates);
            using (SqlCommand cmd = new SqlCommand("MAP.InMonthTempMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point", SqlDbType.VarChar);
                    parm.Value = coordinates.GetMapPoint();
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    float[] temperature = defaulttemperature;
                    while (reader.Read())
                    {
                        int id = (int)reader["Obj_Id"];
                        string period = (string)reader["period"];
                        int p = Convert.ToInt32(period);
                        temperature[p-1] = (float)reader["temperature"];
                    }
                    reader.Close();
                    climat = new Climat(dbcontext, coordinates, temperature);
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }

    }
}
