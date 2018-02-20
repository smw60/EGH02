using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using EGH01DB.Objects;
using EGH01DB.Blurs;
using EGH01DB.Types;
using EGH01DB.Primitives;
using EGH01DB.Points;

namespace EGH01DB
{
    public partial class  CEQContext
    {
        public class Report
        {
            public RGEContext.Report report;
            public float excessgroundconcentration = 0.0f;     // превышение концентрации в грунте (наземное пятно)
            public float exesswaterconcentration = 0.0f;       // превышение концентрации в воде   (наземное пятно)
            public float water_pdk_coef = 0.0f;
            public float pdk_coef = 0.0f;
            public int id = 0;
            public DateTime date = DateTime.Now;
            public string line
            {
                get
                {
                    return string.Format("{0}-П-{1:yyy-MM-dd}", this.id, this.date)
                          + string.Format(": {0}, {1}, {2}", this.report.V0, this.report.petrochemicaltype_name, this.report.riskobject_name);
                }
            }


            public Report(CEQContext db, RGEContext.Report report)
            {
                CadastreType _cadastretype = CadastreType.defaulttype;
                RiskObject _riskobject = RiskObject.defaulttype;
                this.report = report;
                this.pdk_coef = _cadastretype.pdk_coef;
                this.water_pdk_coef = _cadastretype.water_pdk_coef;

                if (report.riskobject_id > 0)
                {
                    if (RiskObject.GetById(db, report.riskobject_id, ref _riskobject))
                    {

                        if (_riskobject.cadastretype.pdk_coef > 0) this.pdk_coef = _riskobject.cadastretype.pdk_coef;
                        if (_riskobject.cadastretype.water_pdk_coef > 0) this.water_pdk_coef = _riskobject.cadastretype.water_pdk_coef;
                    }
                }
                this.excessgroundconcentration = this.report.C3 / (this.pdk_coef);
                this.exesswaterconcentration = this.report.C4 / this.water_pdk_coef;
            }

            public static string starttable
            {
                get
                {
                    return String.Format("<table style=\"width:95%; margin:1px>\"" +
                      "<tr>" +
                      "<th>Номер</th><th>Расстояние (м)</th><th>Концентрация (мг/дм.куб)</th><th>Кратность превышения</th> <th> Скорость(м/сут)</th><th> Наименование</th>" +
                      "</tr>"
                       );
                }
            }
            public static string endtable
            {
                get { return String.Format("</table>"); }
            }
            public string linetable(RGEContext.FEcoObjectsList.FEcoObject eobj)
            {
                return String.Format("<tr><td> {0}-{1} </td> <td> {2} </td>  <td> {3} </td> <td> {4} </td> <td> {5} </td>   <td> {6} </td>  </tr>",
                     eobj.prefix, eobj.id, Math.Round(eobj.distance, 1), Math.Round(eobj.c * Const.KG_to_MG / Const.M3_to_DM3, 0), Math.Round(eobj.c / this.water_pdk_coef, 2), Math.Round(eobj.v * Const.SEC_PER_DAY, 6), eobj.name);
            }
            public Report(XmlNode xn)
            {
                this.report = new EGH01DB.RGEContext.Report(xn.SelectSingleNode(".//ECOForecastX"));
                this.id = Helper.GetIntAttribute(xn, "id", 0);
                this.date = Helper.GetDateTimeAttribute(xn, "date", DateTime.Now);
                this.pdk_coef = Helper.GetFloatAttribute(xn, "pdk_coef", 0.0f);
                this.water_pdk_coef = Helper.GetFloatAttribute(xn, "water_pdk_coef", 0.0f);
                this.excessgroundconcentration = Helper.GetFloatAttribute(xn, "excessgroundconcentration", 0.0f);
                this.exesswaterconcentration = Helper.GetFloatAttribute(xn, "exesswaterconcentration", 0.0f);
            }

            public XmlNode toXmlNode(string comment = "")
            {
                XmlDocument doc = new XmlDocument();
                XmlElement rc = doc.CreateElement("CEQReport");
                if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
                UInt64 id = (UInt64)DateTime.Now.ToBinary();
                rc.SetAttribute("id", id.ToString());
                rc.SetAttribute("date", this.date.ToString());
                rc.SetAttribute("pdk_coef", this.pdk_coef.ToString());
                rc.SetAttribute("water_pdk_coef", this.water_pdk_coef.ToString());
                rc.SetAttribute("excessgroundconcentration", this.excessgroundconcentration.ToString());
                rc.SetAttribute("exesswaterconcentration", this.exesswaterconcentration.ToString());
                rc.AppendChild(doc.ImportNode(this.report.toXmlNode(), true));
                return (XmlNode)rc;
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