using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Points;
using EGH01DB.Types;
using EGH01DB.Primitives;
using System.Data.SqlClient;
using System.Data;
using System.Xml;


namespace EGH01DB.Objects
{
    public class RiskObject : Point    // техногенные объекты, связанные с нефтепродуктами
    {
        public int id { get; private set; }  // идентификатор 
        public RiskObjectType type { get; private set; }     // код типа 
        public CadastreType cadastretype { get; private set; }   // кадастровый тип земли
        public string name { get; private set; }  // наименование объекта
        public District district { get; private set; }  //  район
        public Region region { get; private set; } //  область
        public string address { get; private set; } // адрес объекта
        public string ownership { get; private set; }  //  принадлежность организации
        public string phone { get; private set; }  // изменить в следующей версии на набор данных!
        public string fax { get; private set; }  //
        public string email { get; private set; } 
        public DateTime foundationdate { get; private set; }  // дата ввода в эксплуатацию
        public DateTime reconstractiondate { get; private set; }  // дата последней реконструкции
        public int numberofrefuel { get; private set; }  // количество заправок в сутки // !!!свои поля для каждого вида или всем одинаковые и прятать????
        public int volume { get; private set; }  // объем хранения нефтепродуктов
        public bool watertreatment { get; private set; }  // наличие очистных сооружений для дождевого стока
        public bool watertreatmentcollect { get; private set; } // наличие резервуара для сбора пролива !!! надо бы еще его размер для контроля!!!!
        public byte[] map { get; private set; } // сюда карту?
        public float groundtank { get; private set; }  //  емкость наземного резервуара
        public float undergroundtank { get; private set; } // емкость подземного резервуара
        public string fueltype { get; private set; } // типы топлива, пока только строкой - в отдельную таблицу, если по ним будет отбор
        public int numberofthreads { get; private set; } // количество ниток для нефтепровода или участка нефтедобычи
        public float tubediameter { get; private set; } //  диаметр трубы
        public float productivity { get; private set; } // производительность тонн в сутки для участка нефтедобычи 
        public string geodescription { get; private set; } // географическое описание
        public static readonly string PREFIX = "OО"; 

        // дополнительная инфомация из паспорта объекта 

        static public RiskObject defaulttype { get { return new RiskObject(0); } }  // выдавать при ошибке  
        public RiskObject()
        {
            this.id = -1;
            this.type = new RiskObjectType();
            this.cadastretype = new CadastreType();
            this.name = string.Empty;
            this.address = string.Empty;
            this.district = new District();
            this.region = new Region();
            this.ownership = string.Empty;
            this.phone = string.Empty;
            this.fax = string.Empty;
            this.email = string.Empty;
            this.foundationdate = DateTime.Now;
            this.reconstractiondate = DateTime.Now;
            this.numberofrefuel = -1;
            this.volume = -1;
            this.watertreatment = false;
            this.watertreatmentcollect = false;
            this.map = new byte[0];
            this.groundtank = 0.0f;
            this.undergroundtank = 0.0f;
            this.fueltype = string.Empty;
            this.numberofthreads = -1;
            this.tubediameter = 0.0f;
            this.productivity = 0.0f;
            this.geodescription = string.Empty;
        }

        public RiskObject(int id, 
                            Point point, 
                            RiskObjectType type, 
                            CadastreType cadastertype,
                            string name, 
                            District district, 
                            Region region, 
                            string address, 
                            string ownership, 
                            string phone, 
                            string fax,
                            string email,
                            DateTime foundationdate, 
                            DateTime reconstractiondate,
                            int numberofrefuel, 
                            int volume,
                            bool watertreatment, 
                            bool watertreatmentcollect,
                            byte[] map,
                            float groundtank,
                            float undergroundtank,
                            string fueltype,
                            int numberofthreads,
                            float tubediameter,
                            float productivity,
                            string geodescription)
            : base(point)
        {
            this.id = id;
            this.type = type;
            this.cadastretype = cadastertype;
            this.name = name;
            this.address = address;
            this.district = district;
            this.region = region;
            this.ownership = ownership;
            this.phone = phone;
            this.fax = fax;
            this.email = email;
            this.foundationdate = foundationdate;
            this.reconstractiondate = reconstractiondate;
            this.numberofrefuel = numberofrefuel;
            this.volume = volume;
            this.watertreatment = watertreatment;
            this.watertreatmentcollect = watertreatmentcollect;
            this.map = map;//даша
            this.groundtank = groundtank;
            this.undergroundtank = undergroundtank;
            this.fueltype = fueltype;
            this.numberofthreads = numberofthreads;
            this.tubediameter = tubediameter;
            this.productivity = productivity;
            this.geodescription = geodescription;
        }
        public RiskObject(int id)
        {
            this.id = id;
            this.type = new RiskObjectType();
            this.cadastretype = new CadastreType();
            this.name = string.Empty;
            this.address = string.Empty;
            this.district = new District();
            this.region = new Region();
            this.ownership = string.Empty;
            this.phone = string.Empty;
            this.fax = string.Empty;
            this.email = string.Empty;
            this.foundationdate = DateTime.Now;
            this.reconstractiondate = DateTime.Now;
            this.numberofrefuel = -1;
            this.volume = -1;
            this.watertreatment = false;
            this.watertreatmentcollect = false;
            this.map = new byte[0];
            this.groundtank = 0.0f;
            this.undergroundtank = 0.0f;
            this.fueltype = string.Empty;
            this.numberofthreads = -1;
            this.tubediameter = 0.0f;
            this.productivity = 0.0f;
            this.geodescription = string.Empty;
        }
        public RiskObject(int id, Point point)
            : base(point)
        {
            this.id = id;
            this.type = null;
            this.cadastretype = null;
            this.name = string.Empty;
            this.address = string.Empty;
            this.district = new District();
            this.region = new Region();
            this.ownership = string.Empty;
            this.phone = string.Empty;
            this.fax = string.Empty;
            this.email = string.Empty;
            this.foundationdate = DateTime.Now;
            this.reconstractiondate = DateTime.Now;
            this.numberofrefuel = -1;
            this.volume = -1;
            this.watertreatment = false;
            this.watertreatmentcollect = false;
            this.map = new byte[0];
            this.groundtank = 0.0f;
            this.undergroundtank = 0.0f;
            this.fueltype = string.Empty;
            this.numberofthreads = -1;
            this.tubediameter = 0.0f;
            this.productivity = 0.0f;
            this.geodescription = string.Empty;
        }
        
        public RiskObject(XmlNode node)
            : base(new Point(node.SelectSingleNode(".//Point")))
        {
            this.id = Helper.GetIntAttribute(node, "id", -1);

            XmlNode cad = node.SelectSingleNode(".//CadastreType");
            if (cad != null) this.cadastretype = new CadastreType(cad);
            else this.cadastretype = null;

            XmlNode risk_object_type = node.SelectSingleNode(".//RiskObjectType");
            if (risk_object_type != null) this.type = new RiskObjectType(risk_object_type);
            else this.type = null;

            this.name = Helper.GetStringAttribute(node, "name", "");
            this.address = Helper.GetStringAttribute(node, "address", "");

            XmlNode district = node.SelectSingleNode(".//District");
            if (district != null) this.district = new District(district);
            else this.district = null;

            XmlNode region = node.SelectSingleNode(".//Region");
            if (region != null) this.region = new Region(region);
            else this.region = null;

            this.ownership = Helper.GetStringAttribute(node, "ownership", "");
            this.phone = Helper.GetStringAttribute(node, "phone", "");
            this.fax = Helper.GetStringAttribute(node, "fax", "");
            this.email = Helper.GetStringAttribute(node, "email", "");

            this.foundationdate = Helper.GetDateTimeAttribute(node, "foundationdate", DateTime.MinValue);
            this.reconstractiondate = Helper.GetDateTimeAttribute(node, "reconstractiondate", DateTime.MinValue);

            this.numberofrefuel = Helper.GetIntAttribute(node, "numberofrefuel", -1);
            this.volume = Helper.GetIntAttribute(node, "volume", -1);

            this.watertreatment = Helper.GetBoolAttribute(node, "watertreatment", false);
            this.watertreatmentcollect = Helper.GetBoolAttribute(node, "watertreatmentcollect", false);
          
            // this.map = new byte[0];

            this.groundtank = Helper.GetFloatAttribute(node, "groundtank", 0.0f);
            this.undergroundtank = Helper.GetFloatAttribute(node, "undergroundtank", 0.0f);
            this.fueltype = Helper.GetStringAttribute(node, "fueltype", "");
            this.numberofthreads = Helper.GetIntAttribute(node, "numberofthreads", -1);
            this.tubediameter = Helper.GetFloatAttribute(node, "tubediameter", 0.0f);
            this.productivity = Helper.GetFloatAttribute(node, "productivity", 0.0f);
            this.geodescription = Helper.GetStringAttribute(node, "geodescription", "");
            
        }
        public string Line { get 
                               { return 
                                     this.name.TrimStart().PadRight(40,' ')           
                                   + this.region.name.TrimStart().PadRight(30, ' ')   
                                   + this.district.name.TrimStart().PadRight(30, ' ');
                               }
                            }

        static public bool Create(EGH01DB.IDBContext dbcontext, RiskObject risk_object)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.CreateRiskObject", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdТехногенногоОбъекта", SqlDbType.Int);
                    int new_risk_object_id = 0;
                    if (GetNextId(dbcontext, out new_risk_object_id)) risk_object.id = new_risk_object_id;
                    parm.Value = risk_object.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНазначенияЗемель", SqlDbType.Int);
                    parm.Value = risk_object.cadastretype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТехногенногоОбъекта", SqlDbType.NVarChar);
                    parm.Value = risk_object.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@РайонТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.district.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОбластьТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.region.region_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@АдресТехногенногоОбъекта", SqlDbType.NVarChar);
                    parm.Value = risk_object.address;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Принадлежность", SqlDbType.NVarChar);
                    parm.Value = risk_object.ownership;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Телефон", SqlDbType.VarChar);
                    parm.Value = risk_object.phone;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Факс", SqlDbType.VarChar);
                    parm.Value = risk_object.fax;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@EMail", SqlDbType.VarChar);
                    parm.Value = risk_object.email;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = risk_object.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = risk_object.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = risk_object.groundtype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГлубинаГрунтовыхВод", SqlDbType.Real);
                    parm.Value = risk_object.waterdeep;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВысотаУровнемМоря", SqlDbType.Real);
                    parm.Value = risk_object.height;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДатаВводаЭкспл", SqlDbType.DateTime);
                    parm.Value = risk_object.foundationdate;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДатаПоследнейРеконструкции", SqlDbType.DateTime);
                    parm.Value = risk_object.reconstractiondate;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КолВоЗаправокСут", SqlDbType.Int);
                    parm.Value = risk_object.numberofrefuel;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОбъемХранения", SqlDbType.Int);
                    parm.Value = risk_object.volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОчистнДождСток", SqlDbType.Bit);
                    parm.Value = risk_object.watertreatment;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОчистнСборПроливов", SqlDbType.Bit);
                    parm.Value = risk_object.watertreatmentcollect;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Карта", SqlDbType.VarBinary);
                    parm.Value = risk_object.map;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ЕмкостьНаземногоРезервуара", SqlDbType.Real);
                    parm.Value = risk_object.groundtank;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ЕмкостьПодземногоРезервуара", SqlDbType.Real);
                    parm.Value = risk_object.undergroundtank;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипТоплива", SqlDbType.NVarChar);
                    parm.Value = risk_object.fueltype;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоличествоНиток", SqlDbType.Int);
                    parm.Value = risk_object.numberofthreads;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДиаметрТрубы", SqlDbType.Real);
                    parm.Value = risk_object.tubediameter;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Производительность", SqlDbType.Real);
                    parm.Value = risk_object.productivity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГеографическоеОписание", SqlDbType.NVarChar);
                    parm.Value = risk_object.geodescription;
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
                    rc = ((int)cmd.Parameters["@exitrc"].Value == risk_object.id);
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }

        }
        static public bool Delete(EGH01DB.IDBContext dbcontext, RiskObject risk_object)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.DeleteRiskObject", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.id;
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
            return Delete(dbcontext, new RiskObject(id));
        }
        static public bool Update(EGH01DB.IDBContext dbcontext, RiskObject risk_object)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.UpdateRiskObject", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.id;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.type.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КодТипаНазначенияЗемель", SqlDbType.Int);
                    parm.Value = risk_object.cadastretype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@НаименованиеТехногенногоОбъекта", SqlDbType.NVarChar);
                    parm.Value = risk_object.name;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@РайонТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.district.code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОбластьТехногенногоОбъекта", SqlDbType.Int);
                    parm.Value = risk_object.region.region_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@АдресТехногенногоОбъекта", SqlDbType.NVarChar);
                    parm.Value = risk_object.address;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Принадлежность", SqlDbType.NVarChar);
                    parm.Value = risk_object.ownership;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Телефон", SqlDbType.VarChar);
                    parm.Value = risk_object.phone;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Факс", SqlDbType.VarChar);
                    parm.Value = risk_object.fax;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@EMail", SqlDbType.VarChar);
                    parm.Value = risk_object.email;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = risk_object.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = risk_object.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипГрунта", SqlDbType.Int);
                    parm.Value = risk_object.groundtype.type_code;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГлубинаГрунтовыхВод", SqlDbType.Real);
                    parm.Value = risk_object.waterdeep;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ВысотаУровнемМоря", SqlDbType.Real);
                    parm.Value = risk_object.height;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДатаВводаЭкспл", SqlDbType.DateTime);
                    parm.Value = risk_object.foundationdate;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДатаПоследнейРеконструкции", SqlDbType.DateTime);
                    parm.Value = risk_object.reconstractiondate;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КолВоЗаправокСут", SqlDbType.Int);
                    parm.Value = risk_object.numberofrefuel;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОбъемХранения", SqlDbType.Int);
                    parm.Value = risk_object.volume;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОчистнДождСток", SqlDbType.Bit);
                    parm.Value = risk_object.watertreatment;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ОчистнСборПроливов", SqlDbType.Bit);
                    parm.Value = risk_object.watertreatmentcollect;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Карта", SqlDbType.VarBinary);
                    parm.Value = risk_object.map;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ЕмкостьНаземногоРезервуара", SqlDbType.Real);
                    parm.Value = risk_object.groundtank;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ЕмкостьПодземногоРезервуара", SqlDbType.Real);
                    parm.Value = risk_object.undergroundtank;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ТипТоплива", SqlDbType.NVarChar);
                    parm.Value = risk_object.fueltype;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@КоличествоНиток", SqlDbType.Int);
                    parm.Value = risk_object.numberofthreads;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДиаметрТрубы", SqlDbType.Real);
                    parm.Value = risk_object.tubediameter;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Производительность", SqlDbType.Real);
                    parm.Value = risk_object.productivity;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ГеографическоеОписание", SqlDbType.NVarChar);
                    parm.Value = risk_object.geodescription;
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
        static public bool GetNextId(EGH01DB.IDBContext dbcontext, out int next_id)
        {
            bool rc = false;
            next_id = -1;
            using (SqlCommand cmd = new SqlCommand("EGH.GetNextRiskObjectId", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdТехногенногоОбъекта", SqlDbType.Int);
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
                    next_id = (int)cmd.Parameters["@IdТехногенногоОбъекта"].Value;
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        static public bool GetById(EGH01DB.IDBContext dbcontext, int id, ref RiskObject risk_object)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetRiskObjectById", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@IdТехногенногоОбъекта", SqlDbType.Int);
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
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        Coordinates coordinates = new Coordinates((float)x, (float)y);
                        string ground_type_name = (string)reader["НаименованиеТипаГрунта"];
                        float porosity = (float)reader["КоэфПористости"];
                        float holdmigration = (float)reader["КоэфЗадержкиМиграции"];
                        float waterfilter = (float)reader["КоэфФильтрацииВоды"];
                        float diffusion = (float)reader["КоэфДиффузии"];
                        float distribution = (float)reader["КоэфРаспределения"];
                        float sorption = (float)reader["КоэфСорбции"];
                        float watercapacity = (float)reader["КоэфКапВлагоемкости"];
                        float soilmoisture = (float)reader["ВлажностьГрунта"];
                        float аveryanovfactor = (float)reader["КоэфАверьянова"];
                        float permeability = (float)reader["Водопроницаемость"];
                        float density = (float)reader["СредняяПлотностьГрунта"];
                        GroundType ground_type = new GroundType((int)reader["ТипГрунта"], 
                                                                    (string)ground_type_name, 
                                                                    (float)porosity, 
                                                                    (float)holdmigration,
                                                                    (float)waterfilter,
                                                                    (float)diffusion,
                                                                    (float)distribution,
                                                                    (float)sorption,
                                                                    (float)watercapacity,
                                                                    (float)soilmoisture,
                                                                    (float)аveryanovfactor,
                                                                    (float)permeability,
                                                                    (float)density);

                        float waterdeep = (float)reader["ГлубинаГрунтовыхВод"];
                        float height = (float)reader["ВысотаУровнемМоря"];
                        Point point = new Point(coordinates, ground_type, (float)waterdeep, (float)height);
                       
                        DateTime foundationdate = (DateTime)reader["ДатаВводаЭкспл"];
                        DateTime reconstractiondate = (DateTime)reader["ДатаПоследнейРеконструкции"];

                        int numberofrefuel = (int)reader["КолВоЗаправокСут"];
                        int volume = (int)reader["ОбъемХранения"];

                        float groundtank = (float)reader["ЕмкостьНаземногоРезервуара"];
                        float undergroundtank = (float)reader["ЕмкостьПодземногоРезервуара"];

                        bool watertreatment = (bool)reader["ОчистнДождСток"];
                        bool watertreatmentcollect = (bool)reader["ОчистнСборПроливов"];

                        string risk_object_type_name = (string)reader["НаименованиеТипаТехногенногоОбъекта"];
                        RiskObjectType risk_object_type = new RiskObjectType((int)reader["КодТипаТехногенногоОбъекта"], (string)risk_object_type_name);

                        string cadastre_type_name = (string)reader["НаименованиеНазначенияЗемель"];
                        float pdk = (float)reader["ПДК"];
                        float water_pdk_coef = (float)reader["ПДКводы"];
                        string ground_doc_name = (string)reader["НормДокументЗемля"];
                        string water_doc_name = (string)reader["НормДокументВода"];

                        CadastreType cadastre_type = new CadastreType((int)reader["КодТипаНазначенияЗемель"], (string)cadastre_type_name,
                                                        (float)pdk, (float)water_pdk_coef,
                                                        ground_doc_name, water_doc_name);

                        string name = (string)reader["НаименованиеТехногенногоОбъекта"];
                        string address = (string)reader["АдресТехногенногоОбъекта"];

                        int region_code = (int)reader["ОбластьТехногенногоОбъекта"];
                        string region_name = (string)reader["Область"];
                        Region region = new Region(region_code, region_name);

                        int district_code = (int)reader["РайонТехногенногоОбъекта"];
                        string district_name = (string)reader["Район"];
                        District district = new District(district_code, region, district_name);

                        string ownership = (string)reader["Принадлежность"];
                        string phone = (string)reader["Телефон"];
                        string fax = (string)reader["Факс"];
                        string email = (string)reader["EMail"];

                        byte[] map = (Byte[])reader["Карта"];//даша

                        string fueltype = (string)reader["ТипТоплива"];
                        int numberofthreads = (int)reader["КоличествоНиток"];
                        float productivity = (float)reader["Производительность"];
                        float tubediameter = (float)reader["ДиаметрТрубы"];
                        string geodescription = (string)reader["ГеографическоеОписание"];

                        risk_object = new RiskObject(id, point, risk_object_type, cadastre_type,
                                                               name, district, region, address, ownership, phone, fax, email, 
                                                               foundationdate, reconstractiondate,
                                                               numberofrefuel, volume,
                                                               watertreatment, watertreatmentcollect, map,
                                                               groundtank, undergroundtank, fueltype, numberofthreads, (float)tubediameter, (float)productivity, geodescription);
                    }
                    reader.Close();
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0; 
                }
                catch (Exception e)
                {
                    rc = false;
                };
                
                return rc;
            }

        }
        static public bool CreateAPNear(EGH01DB.IDBContext dbcontext, RiskObject riskobject, float angle, float distance, out AnchorPoint anchor_point)
        {
            bool rc = false;
            int id = -1;
            anchor_point = new AnchorPoint();
            using (SqlCommand cmd = new SqlCommand("EGH.GetCoordinatesByAngle", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@lat1", SqlDbType.Real);
                    parm.Value = riskobject.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@lng1", SqlDbType.Real);
                    parm.Value = riskobject.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@angle", SqlDbType.Real);
                    parm.Value = angle;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@distance", SqlDbType.Real);
                    parm.Value = distance;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@lat2", SqlDbType.Real);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@lng2", SqlDbType.Real);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                //{
                //    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                //    parm.Direction = ParameterDirection.ReturnValue;
                //    cmd.Parameters.Add(parm);
                //}
                try
                {
                    cmd.ExecuteNonQuery();
                    // if (rc = ((int)cmd.Parameters["@exitrc"].Value) > 0)
                    // {
                    float x = (float)cmd.Parameters["@lat2"].Value;
                    float y = (float)cmd.Parameters["@lng2"].Value;
                    Coordinates coordinates = new Coordinates((float)x, (float)y);

                    int new_ground_type_code = riskobject.groundtype.type_code;
                    float new_waterdeep = riskobject.waterdeep;
                    float new_height = riskobject.height;
                    GroundType new_ground_type = new GroundType(new_ground_type_code);
                    Point point = new Point(coordinates, new_ground_type, new_waterdeep, new_height);

                    int new_cadastre_type_code = riskobject.cadastretype.type_code;
                    CadastreType cadastretype = new CadastreType(new_cadastre_type_code);

                    anchor_point = new AnchorPoint(id, point, cadastretype);
                    if (AnchorPoint.Create(dbcontext, anchor_point)) rc = true;
                    // }

                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("RiskObject");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            rc.SetAttribute("id", this.id.ToString());
            XmlNode n = base.toXmlNode("");
            rc.AppendChild(doc.ImportNode(n, true));
            rc.AppendChild(doc.ImportNode(this.type.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.cadastretype.toXmlNode(), true));
            rc.SetAttribute("name", this.name.Trim());
            rc.SetAttribute("address", this.address.Trim());

            rc.AppendChild(doc.ImportNode(this.district.toXmlNode(), true));
            rc.AppendChild(doc.ImportNode(this.region.toXmlNode(), true));

            rc.SetAttribute("ownership", this.ownership.Trim());
            rc.SetAttribute("phone", this.phone.Trim());
            rc.SetAttribute("fax", this.fax.Trim());
            rc.SetAttribute("email", this.email.Trim());

            rc.SetAttribute("foundationdate", this.foundationdate.ToShortDateString());
            rc.SetAttribute("reconstractiondate", this.reconstractiondate.ToShortDateString());
            rc.SetAttribute("numberofrefuel", this.numberofrefuel.ToString());
            rc.SetAttribute("volume", this.volume.ToString());

            rc.SetAttribute("watertreatment",  this.watertreatment?"есть":"нет");
            rc.SetAttribute("watertreatmentcollect", this.watertreatmentcollect?"есть":"нет");
            rc.SetAttribute("numberofrefuel", this.numberofrefuel.ToString());
            rc.SetAttribute("volume", this.volume.ToString());

            //    this.map = new byte[0];

            rc.SetAttribute("groundtank", this.groundtank.ToString());
            rc.SetAttribute("undergroundtank", this.undergroundtank.ToString());
            rc.SetAttribute("fueltype", this.fueltype.Trim());
            rc.SetAttribute("numberofthreads", this.numberofthreads.ToString());
            rc.SetAttribute("tubediameter", this.tubediameter.ToString());
            rc.SetAttribute("productivity", this.productivity.ToString());
            rc.SetAttribute("geodescription", this.geodescription.Trim());

            return (XmlNode)rc;
        }
    }


    public class RiskObjectsList : List<RiskObject>    
    {
       List<EGH01DB.Objects.RiskObject> list_risk = new List<EGH01DB.Objects.RiskObject>();
        public RiskObjectsList()
        {

        }
        public RiskObjectsList(List<RiskObject> list)
            : base(list)
        {

        }
        public RiskObjectsList(EGH01DB.IDBContext dbcontext)
            : base(Helper.GetListRiskObject(dbcontext))
        {

        }
        public XmlNode toXmlNode(string comment = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rc = doc.CreateElement("RiskObjectList");
            if (!String.IsNullOrEmpty(comment)) rc.SetAttribute("comment", comment);
            this.ForEach(m => rc.AppendChild(doc.ImportNode(m.toXmlNode(), true)));
            return (XmlNode)rc;
        }
        
        public static bool CreateRiskObjectsList(EGH01DB.IDBContext dbcontext, Point center, float distance, ref RiskObjectsList risk_object_list)
        {
            bool rc = false;

            using (SqlCommand cmd = new SqlCommand("EGH.GetListRiskObjectOnDistanceLessThanD", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = center.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = center.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Расстояние", SqlDbType.Real);
                    parm.Value = distance;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    risk_object_list = new RiskObjectsList();
                    while (reader.Read())
                    {
                        int id = (int)reader["IdТехногенногоОбъекта"];
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        Coordinates coordinates = new Coordinates((float)x, (float)y);
                        Point point = new Point(coordinates);
                        //delta = (float)reader["Расстояние"];
                        RiskObject risk_object = new RiskObject(id, point);
                        risk_object_list.Add(risk_object);
                    }
                    rc = risk_object_list.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        public static bool CreateRiskObjectsList(EGH01DB.IDBContext dbcontext, Point center, float distance1, float distance2, ref RiskObjectsList risk_object_list)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetListRiskObjectOnDistanceLessThanD2MoreThanD1", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@ШиротаГрад", SqlDbType.Real);
                    parm.Value = center.coordinates.latitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@ДолготаГрад", SqlDbType.Real);
                    parm.Value = center.coordinates.lngitude;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Расстояние1", SqlDbType.Real);
                    parm.Value = distance1;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@Расстояние2", SqlDbType.Real);
                    parm.Value = distance2;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    risk_object_list = new RiskObjectsList();
                    while (reader.Read())
                    {
                        int id = (int)reader["IdТехногенногоОбъекта"];
                        float x = (float)reader["ШиротаГрад"];
                        float y = (float)reader["ДолготаГрад"];
                        Coordinates coordinates = new Coordinates((float)x, (float)y);
                        Point point = new Point(coordinates);
                        //delta = (float)reader["Расстояние"];
                        RiskObject risk_object = new RiskObject(id, point);
                        risk_object_list.Add(risk_object);
                    }
                    rc = risk_object_list.Count > 0;
                    reader.Close();
                }
                catch (Exception e)
                {
                    rc = false;
                };
                return rc;
            }
        }
        public static bool GetListRiskObjectByLike(EGH01DB.IDBContext dbcontext, string findstring, ref List<RiskObject> listobj)
        {
            bool rc = false;
            using (SqlCommand cmd = new SqlCommand("EGH.GetRiskObjectListByLike", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                {
                    SqlParameter parm = new SqlParameter("@findstring", SqlDbType.VarChar);
                    parm.Value = findstring;
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
                    List<int> listid = new List<int>();
                    while (reader.Read()) listid.Add((int)reader["IdТехногенногоОбъекта"]);
                    reader.Close();

                    if (rc = listid.Count > 0)
                    {
                        RiskObject o = null;
                        foreach (int id in listid)
                        {
                            if (RiskObject.GetById(dbcontext, id, ref o))
                            {
                                listobj.Add(o);
                            }
                        }
                        rc = (listid.Count == listobj.Count());
                    }

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
