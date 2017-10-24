using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Types;
using EGH01DB.Objects;
using System.Data.SqlClient;
using System.Data;
using EGH01DB.Primitives;
using EGH01DB.Blurs;
using EGH01DB.Points;



namespace EGH01DB
{
    public partial class  ORTContext: IDBContext
    {
      public int id        {get; set;}
      public DateTime date {get; set;}
      public partial class ECORehabilitation: GEAContext.ECOClassification
      {

         public RiskObjectType           riskobjecttype           {get{ return this.groundblur.spreadpoint.riskobject.type;}}
         public bool                     isriskobjecttype         {get{ return this.groundblur.spreadpoint.isriskobject;}}
         public CadastreType             cadastretype             {get{ return this.groundblur.spreadpoint.cadastretype;}}
         public PetrochemicalCategories  petrochemicalcategories  {get{ return this.groundblur.spreadpoint.petrochemicaltype.petrochemicalcategories;}}
         public EmergencyClass           emergencyclass           {get; private set;}
         public PenetrationDepth         penetrationdepth         {get; private set;}
         public new SoilPollutionCategories  soilpollutioncategories  {get{return  base.soilpollutioncategories;}}
         bool                            waterachieved            {get{return this.groundblur.totalmass >= this.groundblur.adsorbedmass;}}
         public new  WaterPollutionCategories waterpollutioncategories {get{return base.waterpollutioncategories;}}
         public WaterProtectionArea      waterprotectionarea      {get; private set;}
         public List<RehabilitationMethod>  rehabilitationlist =     new List<RehabilitationMethod>();   // перечень выбранныхтехнологий и методов реабилитации     
         
         private  ORTContext db = null;
           
         public ECORehabilitation(GEAContext.ECOClassification ecoclassification):base(ecoclassification) 
         {
             db = new ORTContext();
             this.date = DateTime.Now;
  
             int mk = 0;
             {
               PenetrationDepth  x = null; 
               if (PenetrationDepth.GetByDepth(db, this.groundblur.depth, out x))
               {
                this.penetrationdepth = x;
               }
               else  this.penetrationdepth = PenetrationDepth.defaulttype;
             }
             {
               EmergencyClass  x = null; 
               if (EmergencyClass.GetByMass(db, this.groundblur.totalmass, out x))
               {
                this.emergencyclass = x;
               } 
               else  this.emergencyclass =  EmergencyClass.defaulttype;
              }
              
            
              this.waterprotectionarea = getWaterProtectionArea(db); 


             List<RehabilitationMethod>  source =  new List<RehabilitationMethod>();
             if (Helper.GetListRehabilitationMethod(db, ref source))
             {
               
               foreach (RehabilitationMethod rm in source)
               {
                               
                 bool r1 = ( this.isriskobjecttype   &&   this.riskobjecttype.type_code  > 0 &&  this.riskobjecttype.type_code  == rm.riskobjecttype.type_code);  
                                  
                 bool r2 = ( rm.cadastretype.type_code  ==  0 ||  this.cadastretype.type_code ==  rm.cadastretype.type_code);

                 bool r3 = (rm.emergencyclass.type_code  == 0 || this.emergencyclass.type_code ==  rm.emergencyclass.type_code);      
                   
                 bool r4 = (rm.penetrationdepth.type_code == 0 ||this.penetrationdepth.type_code ==  rm.penetrationdepth.type_code); 
                
                 bool r5 = (rm.soilpollutioncategories.code == 0 || this.soilpollutioncategories.code == rm.soilpollutioncategories.code);
               
                 bool r6 = (rm.waterpollutioncategories.code == 0 || this.waterpollutioncategories.code == rm.waterpollutioncategories.code);
                   
                 bool r7 = ( this.waterachieved ==  rm.waterachieved);   

                 bool r8  = (this.waterprotectionarea.type_code == rm.waterprotectionarea.type_code);
                              
                 int k = (r1?1:0)  + (r2?1:0) + (r3?1:0) + (r3?1:0) + (r4?1:0) +(r5?1:0) + (r6?1:0) + (r7?1:0);

                 mk = k > mk?k:mk; 

                // if (r1 && r2 && r3 && r4 && r5 && r6 && r7 ) rehabilitationlist.Add(rm);
                if (k >= 5) rehabilitationlist.Add(rm);
              
               }

             }        
         }
         
         private WaterProtectionArea getWaterProtectionArea(IDBContext db)
         {
            WaterProtectionArea rc =  WaterProtectionArea.defaulttype;
          
           foreach (EcoObject eo in this.groundblur.ecoobjecstlist)
           {
              if (rc != null && rc.type_code < eo.ecoobjecttype.waterprotectionarea.type_code) rc =  eo.ecoobjecttype.waterprotectionarea;  
              else  rc =  eo.ecoobjecttype.waterprotectionarea;                
           }            
           if (rc.type_code == 0)
           {
              int k = 0;
              while ( k < this.waterpolutionlist.Count && this.waterpolutionlist[k].pointtype != Point.POINTTYPE.ECO) k++;
              if (k <  this.waterpolutionlist.Count)        
              {
                   if (!WaterProtectionArea.GetByCode(db,1, out rc)) rc =  WaterProtectionArea.defaulttype;  
              }              
              
          } 
          return rc;   
       }   

        public new XmlNode toXmlNode(string comment="")
        { 
                XmlDocument doc = new XmlDocument();
                XmlElement rc = doc.CreateElement("ECORehabilitation");
                if (!string.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
                rc.SetAttribute("id",this.id.ToString()); 
                rc.SetAttribute("date", this.date.ToShortDateString());
                rc.SetAttribute("waterachieved", this.waterachieved?"да":"нет");
                {
                XmlNode n = this.riskobjecttype.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                }    
                {
                XmlNode n = this.cadastretype.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                }   
                {
                XmlNode n = this.petrochemicalcategories.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                } 
                {
                XmlNode n = this.emergencyclass.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                } 
                {
                XmlNode n = this.penetrationdepth.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                } 
                {
                XmlNode n = this.soilpollutioncategories.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                } 
                {
                XmlNode n = this.waterpollutioncategories.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                }
                {
                XmlNode n = this.waterprotectionarea.toXmlNode();
                rc.AppendChild(doc.ImportNode(n, true));
                }
                {
                    XmlElement x = doc.CreateElement("RehabilitationMethodList");
                    foreach(RehabilitationMethod r in this.rehabilitationlist)
                    {
                            x.AppendChild(doc.ImportNode(r.toXmlNode(), true));
                    }
                    rc.AppendChild(doc.ImportNode(x,true));
                }
                {
                    XmlNode x = base.toXmlNode();
                    rc.AppendChild(doc.ImportNode(x, true));
                } 
                return (XmlNode)rc;
         } 
        public  static bool Create(IDBContext dbcontext, ORTContext.ECORehabilitation rehabilitation, string comment = "")
             {
                    bool rc = false;
                    using (SqlCommand cmd = new SqlCommand("EGH.CreateReport", dbcontext.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        {
                            SqlParameter parm = new SqlParameter("@IdОтчета", SqlDbType.Int);
                            int new_report_id = 0;
                            if (GetNextId(dbcontext, out new_report_id)) rehabilitation.id = new_report_id;
                            parm.Value = rehabilitation.id;
                            cmd.Parameters.Add(parm);
                        }
                        {
                            SqlParameter parm = new SqlParameter("@ДатаОтчета", SqlDbType.DateTime);
                            parm.Value = rehabilitation.date;
                            cmd.Parameters.Add(parm);
                        }
                        {
                            SqlParameter parm = new SqlParameter("@Стадия", SqlDbType.NChar);
                            parm.Value = "Т";
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
                            parm.Value = rehabilitation.toXmlNode("").OuterXml;
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


  //rehabilitation_method = new RehabilitationMethod(code,
  //                                                                          risk_object_type,
  //                                                                          cadastre_type,
  //                                                                          petrochemical_category,
  //                                                                          emergency_class,
  //                                                                          penetration_depth,
  //                                                                          soilpollution_categories,
  //                                                                          waterachieved,
  //                                                                          water_pollution_categories,
  //                                                                          water_protection_area,
  //                                                                          soil_cleaning_method, 
  //                                                                          water_cleaning_method);

  
                //RiskObjectType               riskjbjecttype           = rm.riskobjecttype;
                //CadastreType                 cadastretype             = rm.cadastretype;
                //PetrochemicalCategories      petrochemicalcategories  = rm.petrochemicalcategory;
                //EmergencyClass               emergencyclass           = rm.emergencyclass;
                //PenetrationDepth             penetrationdepth         = rm.penetrationdepth;
                //SoilPollutionCategories      soilpollutioncategories  = rm.soilpollutioncategories; 
                //bool                         waterachieved            = rm.waterachieved;  
                //WaterPollutionCategories     waterpollutioncategories = rm.waterpollutioncategories;
                //WaterProtectionArea          waterprotectionarea      = rm.waterprotectionarea;              
                
                