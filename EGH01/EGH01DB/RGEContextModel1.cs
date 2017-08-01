using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Points;
using EGH01DB.Types;
using EGH01DB.Primitives;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using EGH01DB.Objects;
using EGH01DB.Blurs;

namespace EGH01DB
{

    public partial class RGEContext
    {
        public partial class ECOForecast         //  модель прогнозирования 
        {
            public static bool Create(IDBContext dbcontext, ECOForecast ecoforecast, string comment = "")
            {
                bool rc = false;
                using (SqlCommand cmd = new SqlCommand("EGH.CreateReport", dbcontext.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    {
                        SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                        int new_report_id = 0;
                        if (GetNextId(dbcontext, out new_report_id)) ecoforecast.id = new_report_id;
                        parm.Value = ecoforecast.id;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@ДатаОтчета", SqlDbType.DateTime);
                        parm.Value = ecoforecast.date;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@Стадия", SqlDbType.NChar);
                        parm.Value = "П";
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@Родитель", SqlDbType.Int);
                        parm.IsNullable = true;
                        parm.Value = 0;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@ТекстОтчета", SqlDbType.Xml);
                        parm.IsNullable = true;
                        parm.Value = ecoforecast.toXmlNode("EcoForeCast").OuterXml;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@Комментарий", SqlDbType.NVarChar);
                        parm.IsNullable = true;
                        parm.Value = comment;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                        parm.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(parm);
                    }
                
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rc = ((int)cmd.Parameters["@exitrc"].Value > 0);
                    }
                    catch (Exception e)
                    {
                        rc = false;
                    };
                    return rc;
            }
            }
            public static bool GetNextId(EGH01DB.IDBContext dbcontext, out int next_id)
            {
                bool rc = false;
                next_id = -1;
                using (SqlCommand cmd = new SqlCommand("EGH.GetNextReportId", dbcontext.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    {
                        SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                        parm.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                        parm.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(parm);
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                        next_id = (int)cmd.Parameters["@IdОтчета"].Value;
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                    }
                    catch (Exception e)
                    {
                        rc = false;
                    };
                    return rc;
                }
            }
            public static bool GetById(EGH01DB.IDBContext dbcontext, int id, out ECOForecast ecoforecast, out string comment)
            {
                bool rc = false;
                ecoforecast = new ECOForecast();
                comment = "";
                using (SqlCommand cmd = new SqlCommand("EGH.GetReportbyId", dbcontext.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    {
                        SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                        parm.Value = id;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                        parm.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(parm);
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            DateTime date = (DateTime)reader["ДатаОтчета"];
                            string stage = (string)reader["Стадия"];
                            int predator = (int)reader["Родитель"];
                            comment = (string)reader["Комментарий"];
                         //
                            string xmlContent = (string)reader["ТекстОтчета"];
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xmlContent);
                            XmlNode newNode = doc.DocumentElement;
                            //

                            if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) ecoforecast = new ECOForecast(newNode);
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        rc = false;
                    };

                }
                return rc;
            }
            static public bool Delete(EGH01DB.IDBContext dbcontext, ECOForecast ecoforecast)
            {
                bool rc = false;
                using (SqlCommand cmd = new SqlCommand("EGH.DeleteReport", dbcontext.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    {
                        SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                        parm.Value = ecoforecast.id;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                        parm.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(parm);
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                    }
                    catch (Exception e)
                    {
                        rc = false;
                    };
                }
                return rc;
            }
            static public bool DeleteById(EGH01DB.IDBContext dbcontext, int id)
            {
                return Delete(dbcontext, new ECOForecast(id));
            }
            public static bool UpdateCommentById(IDBContext db, int id, string comment)
            {
                bool rc = false;
                using (SqlCommand cmd = new SqlCommand("EGH.UpdateReport", db.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    {
                        SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                        parm.Value = id;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@Комментарий", SqlDbType.NVarChar);
                        parm.Value = comment;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                        parm.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(parm);
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                        rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                    }
                    catch (Exception e)
                    {
                        rc = false;
                    };
                }
                return rc;
            }
        }

        //public class ECOForecastlist : List<ECOForecast>
        //{
        //    public static bool Get(       //  получить списки ECOForecastlist и соотв. комментов 
        //                           IDBContext db, 
        //                           string stage,                                // стадия  
        //                           out ECOForecastlist ecoforecastlist,         // список объектов 
        //                           out List<string> comments                    // список комментов соотв. списку объектов 
        //                          )
        //  {
        //      ecoforecastlist = new ECOForecastlist();
        //      comments = new List<string>();
              
        //      return true;
        //  }
          
        //  public ECOForecastlist()
        //  {


        //  } 
          
            //public XmlNode toXmlNode(string comment = "")
            //{
            //    XmlDocument doc = new XmlDocument();
            //    XmlElement rc = doc.CreateElement("ECOForecastList");
            //    if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            //    this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            //    return (XmlNode)rc;
            //}
          
        
        ///}
     }
}
