using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace EGH01DB
{
    public partial class  CEQContext
    {
        public  partial class ECOEvalution: RGEContext.ECOForecast
        {
         public  static bool Create(IDBContext dbcontext, ECOEvalution ecoevalution , string comment = "")
         {
                bool rc = false;
                using (SqlCommand cmd = new SqlCommand("EGH.CreateReport", dbcontext.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    {
                        SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                        int new_report_id = 0;
                        if (GetNextId(dbcontext, out new_report_id)) ecoevalution.id = new_report_id;
                        parm.Value = ecoevalution.id;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@ДатаОтчета", SqlDbType.DateTime);
                        parm.Value = ecoevalution.date;
                        cmd.Parameters.Add(parm);
                    }
                    {
                        SqlParameter parm = new SqlParameter("@Стадия", SqlDbType.NChar);
                        parm.Value = "Р";
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
                        parm.Value = ecoevalution.toXmlNode("Отладка").OuterXml;
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
        
        }

    }
}



 //           }
 //           public static bool GetNextId(EGH01DB.IDBContext dbcontext, out int next_id)
 //           {
 //               bool rc = false;
 //               next_id = -1;
 //               using (SqlCommand cmd = new SqlCommand("EGH.GetNextReportId", dbcontext.connection))
 //               {
 //                   cmd.CommandType = CommandType.StoredProcedure;
 //                   {
 //                       SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
 //                       parm.Direction = ParameterDirection.Output;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   {
 //                       SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
 //                       parm.Direction = ParameterDirection.ReturnValue;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   try
 //                   {
 //                       cmd.ExecuteNonQuery();
 //                       next_id = (int)cmd.Parameters["@IdОтчета"].Value;
 //                       rc = (int)cmd.Parameters["@exitrc"].Value > 0;
 //                   }
 //                   catch (Exception e)
 //                   {
 //                       rc = false;
 //                   };
 //                   return rc;
 //               }
 //           }
 //           public static bool GetById(EGH01DB.IDBContext dbcontext, int id, out ECOForecast ecoforecast, out string comment)
 //           {
 //               bool rc = false;
 //               ecoforecast = new ECOForecast();
 //               comment = "";
 //               using (SqlCommand cmd = new SqlCommand("EGH.GetReportbyId", dbcontext.connection))
 //               {
 //                   cmd.CommandType = CommandType.StoredProcedure;
 //                   {
 //                       SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
 //                       parm.Value = id;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   {
 //                       SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
 //                       parm.Direction = ParameterDirection.ReturnValue;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   try
 //                   {
 //                       cmd.ExecuteNonQuery();
 //                       SqlDataReader reader = cmd.ExecuteReader();
 //                       if (reader.Read())
 //                       {
 //                           DateTime date = (DateTime)reader["ДатаОтчета"];
 //                           string stage = (string)reader["Стадия"];
 //                           int predator = (int)reader["Родитель"];
 //                           comment = (string)reader["Комментарий"];
 //                        //
 //                           string xmlContent = (string)reader["ТекстОтчета"];
 //                           XmlDocument doc = new XmlDocument();
 //                           doc.LoadXml(xmlContent);
 //                           XmlNode newNode = doc.DocumentElement;
 //                           //

 //                           if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) ecoforecast = new ECOForecast(newNode);
 //                       }
 //                       reader.Close();
 //                   }
 //                   catch (Exception e)
 //                   {
 //                       rc = false;
 //                   };

 //               }
 //               return rc;
 //           }
 //           static public bool Delete(EGH01DB.IDBContext dbcontext, ECOForecast ecoforecast)
 //           {
 //               bool rc = false;
 //               using (SqlCommand cmd = new SqlCommand("EGH.DeleteReport", dbcontext.connection))
 //               {
 //                   cmd.CommandType = CommandType.StoredProcedure;
 //                   {
 //                       SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
 //                       parm.Value = ecoforecast.id;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   {
 //                       SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
 //                       parm.Direction = ParameterDirection.ReturnValue;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   try
 //                   {
 //                       cmd.ExecuteNonQuery();
 //                       rc = (int)cmd.Parameters["@exitrc"].Value > 0;
 //                   }
 //                   catch (Exception e)
 //                   {
 //                       rc = false;
 //                   };
 //               }
 //               return rc;
 //           }
 //           static public bool DeleteById(EGH01DB.IDBContext dbcontext, int id)
 //           {
 //               return Delete(dbcontext, new ECOForecast(id));
 //           }
 //           public static bool UpdateCommentById(IDBContext db, int id, string comment)
 //           {
 //               bool rc = false;
 //               using (SqlCommand cmd = new SqlCommand("EGH.UpdateReport", db.connection))
 //               {
 //                   cmd.CommandType = CommandType.StoredProcedure;
 //                   {
 //                       SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
 //                       parm.Value = id;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   {
 //                       SqlParameter parm = new SqlParameter("@Комментарий", SqlDbType.NVarChar);
 //                       parm.Value = comment;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   {
 //                       SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
 //                       parm.Direction = ParameterDirection.ReturnValue;
 //                       cmd.Parameters.Add(parm);
 //                   }
 //                   try
 //                   {
 //                       cmd.ExecuteNonQuery();
 //                       rc = (int)cmd.Parameters["@exitrc"].Value > 0;
 //                   }
 //                   catch (Exception e)
 //                   {
 //                       rc = false;
 //                   };
 //               }
 //               return rc;
 //           }