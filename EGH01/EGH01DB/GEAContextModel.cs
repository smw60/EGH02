using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01DB.Blurs;

namespace EGH01DB
{
      /// EGHGEA/ClassificationEvalution

   public partial  class GEAContext: IDBContext
   {
        public class  ECOClassification: CEQContext.ECOEvalution
        {
              public int                  id   {get; set;}
              public DateTime             date {get; set;}  
              public SoilPollutionCategories  soilpollutioncategories  {get; set;}
              public WaterPollutionCategories waterpollutioncategories {get; set;}
             

              private string errormssageformat = "ECOClassification: Ошибка в данных. {0}";
              public string               line { get
                                                  {
                                                   return  string.Format("{0}-C-{1:yyyy-MM-dd}/{2}-Р-{3:yyyy-MM-dd}", this.id, this.date,base.id, base.date) 
                                                   + string.Format(": {0}, {1}, {2}", this.incident.volume, this.incident.petrochemicaltype.name, this.incident.riskobject.name);
                                                  }
                                                } 


             public ECOClassification(CEQContext.ECOEvalution ecoevalution):base (ecoevalution)
             { 
                     GEAContext db = new GEAContext(); 
                     this.id = 0;
                     this.date = DateTime.Now; 
                     {
                           SoilPollutionCategories spc =  this.soilpollutioncategories =  null;
                           SoilPollutionCategories.GetByVolume_Cadastre(db, this.excessgroundconcentration, this.groundblur.spreadpoint.cadastretype.type_code,  out spc);              
                           this.soilpollutioncategories = spc;
                           
                     }
                
                    { 
  
                         WaterPollutionCategories wpc = this.waterpollutioncategories = null;
                         WaterPollutionCategories.GetByExcess_Cadastre(db, this.exesswaterconcentration, this.groundblur.spreadpoint.cadastretype.type_code,  out wpc);              
                         this.waterpollutioncategories = wpc;
                        
                     }
                    
                    foreach(WaterPollution wp in this.waterpolutionlist)
                    {
                      WaterPollutionCategories x = null;  
                      WaterPollutionCategories.GetByExcess_Cadastre(db, wp.excessconcentration,wp.cadastretype.type_code, out x);
                      wp.waterpollutioncategories = x ;
                    }  
                   

             }
             public ECOClassification(XmlNode node):base (node.SelectSingleNode(".//ECOEvalution"))
             { 
               this.id =   Helper.GetIntAttribute(node, "id"); 
               this.date = Helper.GetDateTimeAttribute(node,"date", DateTime.Now);
                {
                    XmlNode x = node.SelectSingleNode(".//SoilPollutionCategories");
                    if (x != null) this.soilpollutioncategories =  new  SoilPollutionCategories(x);
                    else this.soilpollutioncategories = null;
                }  
                {
                    XmlNode x = node.SelectSingleNode(".//WaterPollutionCategories");
                    if (x != null) this.waterpollutioncategories =  new  WaterPollutionCategories(x);
                    else this.waterpollutioncategories = null;
                } 
    

             }                
             public new XmlNode toXmlNode(string comment="")
             { 
                  XmlDocument doc = new XmlDocument();
                  XmlElement rc = doc.CreateElement("ECOClassification");
                  if (!string.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
                  rc.SetAttribute("id",this.id.ToString()); 
                  rc.SetAttribute("date", this.date.ToString());
                  {
                    XmlNode n = this.soilpollutioncategories.toXmlNode();
                    rc.AppendChild(doc.ImportNode(n, true));
                  }
                  {
                      XmlNode n = this.waterpollutioncategories.toXmlNode();
                      rc.AppendChild(doc.ImportNode(n, true));
                  } 
                  {
                    XmlNode n = base.toXmlNode();
                    rc.AppendChild(doc.ImportNode(n, true));
                  }  
                  return (XmlNode)rc;
             } 
             public  static bool Create(IDBContext dbcontext, ECOClassification classification, string comment = "")
             {
                    bool rc = false;
                    using (SqlCommand cmd = new SqlCommand("EGH.CreateReport", dbcontext.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        {
                            SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                            int new_report_id = 0;
                            if (GetNextId(dbcontext, out new_report_id)) classification.id = new_report_id;
                            parm.Value = classification.id;
                            cmd.Parameters.Add(parm);
                        }
                        {
                            SqlParameter parm = new SqlParameter("@ДатаОтчета", SqlDbType.DateTime);
                            parm.Value = classification.date;
                            cmd.Parameters.Add(parm);
                        }
                        {
                            SqlParameter parm = new SqlParameter("@Стадия", SqlDbType.NChar);
                            parm.Value = "С";
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
                            parm.Value = classification.toXmlNode("Отладка").OuterXml;
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
             public static ECOClassification GetById(IDBContext db, int id)
             { 
               ECOClassification rc = null;
               using (SqlCommand cmd = new SqlCommand("EGH.GetECOClassificationById", db.connection))
               {
                        cmd.CommandType = CommandType.StoredProcedure;
                        {
                            SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                            parm.Value = id;
                            cmd.Parameters.Add(parm);
                        }
                        try
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                 string xmlContent = (string)reader["ТекстОтчета"];
                                 if (!xmlContent.Trim().Equals(""))
                                 {
                                   XmlDocument doc = new XmlDocument();
                                   doc.LoadXml(xmlContent);
                                   rc =  new GEAContext.ECOClassification(doc.DocumentElement);
                                 }
                            } 
                            reader.Close();
                         } catch (Exception e)
                         { 
                           rc = null;
                         };
                }
                return rc;
            } 
       }
   }

       
}
