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
        


        
        //static public bool GetListRiskObjectByLike(EGH01DB.IDBContext dbcontext, string findstring, ref List<RiskObject> listobj)
        //{
        //    bool rc = false;
        //    using (SqlCommand cmd = new SqlCommand("EGH.GetRiskObjectListByLike", dbcontext.connection))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
                
        //        {
        //            SqlParameter parm = new SqlParameter("@findstring", SqlDbType.VarChar);
        //            parm.Value = findstring;
        //            cmd.Parameters.Add(parm);
        //        }
        //        {
        //            SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
        //            parm.Direction = ParameterDirection.ReturnValue;
        //            cmd.Parameters.Add(parm);
        //        }
        //        try
        //        {
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            List<int>  listid = new List<int>();
        //            while (reader.Read()) listid.Add((int)reader["IdТехногенногоОбъекта"]);
        //            reader.Close();

        //            if (rc = listid.Count > 0)
        //            {  
        //                RiskObject o = null;
        //                foreach (int id in listid)
        //                { 
        //                   if (RiskObject.GetById(dbcontext,id, ref o))
        //                   {
        //                       listobj.Add(o);
        //                   }
        //                 }
        //                rc = (listid.Count == listobj.Count());
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            rc = false;
        //        };
        //        return rc;

        //    }

        //}
     }
    
 }