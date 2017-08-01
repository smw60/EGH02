using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

using EGH01DB.Points;
using EGH01DB.Types;
using EGH01DB.Primitives;
using EGH01DB.Objects;
using EGH01DB.Blurs;

namespace EGH01DB.Primitives
{

// доп. таблица    СтильОтчета  - внутренняя таблица исп. только внутреннего применения  
//   Стадия       ncahr (1)
//   СтильОтчета  nvarchar(max)
//  лежит в базе // blinova
//
//
    public class Report                                       // отчет
    {
        public int       id             {get;  private set;}    // идентификатор отчета   
        public Report    parent         {get;  private set;}    // идентификатор родительского отчета
        public string    stage          {get;  private set;}    // тип отчета: П, Р, С, Т.
        public DateTime  date           {get;  private set;}    // дата формирования отчета 
        public XmlNode   xmlcontetnt    {get;  private set;}    // отчет 
        public XmlNode   xslhtmlstyle   {get;  private set;}    // xslt-стиль преобразования в html  // в БД?
        public string    comment        {get;  private set;}    // комментарий  
        public string    line           {get{return string.Format("{0}-{1}-{2:yyyy-MM-dd}", this.id, this.stage, this.date);}}           


        public Report()
        {
          this.id              = -1;
          this.parent          = null;
          this.stage           = string.Empty;
          this.date            = DateTime.MinValue;
          this.xmlcontetnt     = null;
          this.xslhtmlstyle    = null;  
          this.comment         = string.Empty;        
        }
        public Report(int id)
        {
            this.id = id;
            this.parent = null;
            this.stage = string.Empty;
            this.date = DateTime.MinValue;
            this.xmlcontetnt = null;
            this.xslhtmlstyle = null;
            this.comment = string.Empty;
        } 
      
        public Report(int id, string stage,  DateTime date, XmlNode xmlcontetnt, XmlNode  xslhtmlstyle, string comment = "")
        {
          this.id              = id;
          this.parent        = null;
          this.stage           = stage;
          this.date            = date;
          this.xmlcontetnt     = xmlcontetnt;
          this.xslhtmlstyle    = xslhtmlstyle;
          this.comment         = comment;
        }


        public Report(int id, Report parent, string stage, DateTime date, XmlNode xmlcontetnt, XmlNode xslhtmlstyle, string comment = "")
        {
          this.id              = id;
          this.parent          = parent;
          this.stage           = stage;
          this.date            = date;
          this.xmlcontetnt     = xmlcontetnt;
          this.xslhtmlstyle    = xslhtmlstyle;
          this.comment         = comment;
        }    

   
       
        public string ToHTML()                                // преобразование XML-XSLT->HTML  
        { 
          //// примерно так - не проверил!!!
          //string rc = string.Empty;
          //XmlDocument xmldoc =(XmlDocument)this.xmlcontetnt;
          //XslCompiledTransform xsldoc =  new XslCompiledTransform();     
          //xsldoc.Load(xslhtmlstyle.OuterXml);
          //StringWriter sw = new StringWriter();
          //xsldoc.Transform(xmldoc.CreateNavigator(), null, sw);
          //rc = sw.ToString();
          //return rc;                              // HTML -строка 
            string rc = string.Empty;

            XslCompiledTransform xsldoc = new XslCompiledTransform();
            xsldoc.Load(xslhtmlstyle);
            StringWriter sw = new StringWriter();

            XmlDocument xmldoc = new XmlDocument();
            string xml_outer_string = this.xmlcontetnt.OuterXml;
            xmldoc.LoadXml(xml_outer_string);

            xsldoc.Transform(xmldoc.CreateNavigator(), null, sw);
            rc = sw.ToString();
            return rc;              

        }  
     
        public static bool Create(IDBContext dbcontext, Report report)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateReport", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                    int new_report_id = 0;
                    if (GetNextId(dbcontext, out new_report_id)) report.id = new_report_id;
                    parm.Value = report.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДатаОтчета", SqlDbType.DateTime);
                    parm.Value = report.date;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Стадия", SqlDbType.NChar);
                    parm.Value = report.stage;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Родитель", SqlDbType.Int);
                    parm.IsNullable = true;
                    parm.Value = report.parent.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТекстОтчета", SqlDbType.Xml);
                    parm.IsNullable = true;
                    parm.Value = report.xmlcontetnt;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Комментарий", SqlDbType.NVarChar);
                    parm.IsNullable = true;
                    parm.Value = report.comment;
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
        public static bool GetById(EGH01DB.IDBContext dbcontext, int id, out Report report, out string comment)
        {
            bool rc = false;
            report = new Report();
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
                        int parent = (int)reader["Родитель"];
                        Report parent_report = new Report(parent);
                        comment = (string)reader["Комментарий"];
              
                        string xmlContent = (string)reader["ТекстОтчета"];
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xmlContent);
                        XmlNode xml_content = doc.DocumentElement;

                        string xsl_htm_lstyle = (string)reader["СтильОтчета"];
                        XmlDocument doc_xslt = new XmlDocument();
                        doc.LoadXml(xsl_htm_lstyle);
                        XmlNode xsl_content = doc.DocumentElement;
         
                        if (rc = (int)cmd.Parameters["@exitrc"].Value > 0) report = new Report(id, stage, date, xml_content, xsl_content, comment);
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
        static public bool Delete(EGH01DB.IDBContext dbcontext, Report report)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteReport", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                    parm.Value = report.id;
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
            return Delete(dbcontext, new Report(id));
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

   public class ReportsList: List<Report>
   {
     
     static public  bool GetByStage(IDBContext db, string stage, out ReportsList list)
     {
         bool rc = false;
         list = new ReportsList();
         using (SqlCommand cmd = new SqlCommand("EGH.GetStageReportList", db.connection))
         {
             cmd.CommandType = CommandType.StoredProcedure;
             {
                 SqlParameter parm = new SqlParameter("@Стадия", SqlDbType.NChar);
                 parm.Value = stage;
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
                 while (reader.Read())
                 {
                     int report_id = (int)reader["IdОтчета"];
                     DateTime date = (DateTime)reader["ДатаОтчета"];
                     // string stage = (string)reader["Стадия"];
                     int predator = (int)reader["Родитель"];
                     Report parent = new Report(predator);
                     string comment = (string)reader["Комментарий"];

                     string xmlContent = (string)reader["ТекстОтчета"];
                     XmlDocument doc_xml = new XmlDocument();
                     doc_xml.LoadXml(xmlContent);
                     XmlNode xml_Node = doc_xml.DocumentElement;

                     string xslContent = (string)reader["ТекстОтчета"];
                     XmlDocument doc_xsl = new XmlDocument();
                     doc_xsl.LoadXml(xslContent);
                     XmlNode xsl_Node = doc_xsl.DocumentElement;
                     Report report = new Report(report_id, parent, stage, date, xml_Node, xsl_Node, comment);
                     
                     list.Add(report);

                 }
                 rc = ((int)cmd.Parameters["@exitrc"].Value > 0);
                 reader.Close();
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
