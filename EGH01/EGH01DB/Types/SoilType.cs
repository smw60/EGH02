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


namespace EGH01DB.Types
{
    public class SoilType
    {
        public string name { get; private set; }             // name - наименование почв
        public float filter { get; private set; }            // усредненный коэффициент фильтрации из карт filter_coef_ave
        public float porosity { get; private set; }          // усредненный коэффициент пористости  из карт porosity_ave  
        public float watercapacity { get; private set; }     // коэффициент влагоемкости cap_moisture_capacity	Капиллярная влагоёмкость, %
        public float gumus_depth { get; private set; }             // высота почвенного покрова в см gumus
        public string mehsost { get; private set; }             // 	Механический состав почвы

        public string soil_type { get; private set; }             // 	Тип почв
        public string soil_subtype { get; private set; }             // 	Подтип почв

        public float humidity{ get; private set; }             // 	Влажность полевая, %

        public float petrol_filtration_coef { get; private set; }             // 	Коэффициент фильтрации бензина, м/сут
        public float fuel_oil_filtration_coef	{ get; private set; }             // Коэффициент фильтрации  мазута, м/сут
        public float diesel_filtration_coef	{ get; private set; }             // Коэффициент фильтрации дизельного топлива, м/сут

        public float hydrolytic_acidity	{ get; private set; }             // Гидролитическая кислотность,  м-экв. на 100 г почвы

        public float oil_capacity	{ get; private set; }             //  Коэффициент нефтеёмкости
        public float  density { get; private set; }             //  	Объёмная плотность, г/см³

        public string  carbon_content	{ get; private set; }             //  Содержание органического углерода, %
        public string filter_coef_interval	{ get; private set; }             //  Коэффициент фильтрации, м/сут
        public string porosity_interval	{ get; private set; }             //  Пористость в интервале от, до, %
        public string glina_interval	{ get; private set; }             //  Содержание глины в породе
        public string mg_interval	{ get; private set; }             //  Максимальная гигроскопичность, %
        public string ph_interval	{ get; private set; }             //  Кислотность почвы
        public string density_interval	{ get; private set; }             //  Удельная плотность, г/см³
        public string max_moisture_capacity_interval	{ get; private set; }             //  Предельная полевая влагоёмкость, %
        public string gumus_percentage_interval { get; private set; }             //  Содержание гумуса, %

        public string klass { get; private set; }             // класс почв по классификатору klass
        public string soil_class_code { get; private set; }         // тип почв по классификатору международной классификации
        public string wrb_code	{ get; private set; }         // Код почвы в международной классификационной системе WRB (уточнённые)
        public string wrb_new_code	{ get; private set; }         // Код почвы в международной классификационной системе WRB
        public string rgb_code	{ get; private set; }         // Код цветового обозначения
        public string shtrihovka	{ get; private set; }     // Тип штриховки


        public static SoilType defaultype { get { return new SoilType("не определен", 1f, 1f, 1f, 0.2f, "не определен", "не определен"); } }

        public SoilType()
        {
            this.name = "";
            this.filter = 0.0f;
            this.porosity = 0.0f;
            this.watercapacity = 0.0f;
            this.gumus_depth = 0.0f;
            this.klass = "";
            this.soil_type = "";
            this.mehsost = "";
            this.soil_subtype = "";
            this.humidity = 0.0f;
            this.petrol_filtration_coef = 0.0f;
            this.fuel_oil_filtration_coef = 0.0f;
            this.diesel_filtration_coef = 0.0f;
            this.hydrolytic_acidity = 0.0f;
            this.oil_capacity = 0.0f;
            this.density = 0.0f;
            this.filter_coef_interval = "";
            this.filter_coef_interval = "";
            this.porosity_interval = "";
            this.glina_interval = "";
            this.mg_interval = "";
            this.ph_interval = "";
            this.density_interval = "";
            this.max_moisture_capacity_interval = "";
            this.gumus_percentage_interval = "";
            this.soil_class_code = "";
            this.wrb_code = "";
            this.wrb_new_code = "";
            this.rgb_code = "";
            this.shtrihovka = "";
        }
        public SoilType(string name, float filter, float porosity, 
                        float watercapacity, float gumus_height,
                        string klass, string soil_type)
        {
            this.name = name;
            this.filter = filter;
            this.porosity = porosity;
            this.watercapacity = watercapacity;
            this.gumus_depth = gumus_height;
            this.klass = klass;
            this.soil_type = soil_type;
        }

        public SoilType(string name, 
            float filter,
            float porosity ,
            float watercapacity,
            float gumus_depth,
            string mehsost,
            string soil_type,
            string soil_subtype,
            float humidity,
            float petrol_filtration_coef,
            float fuel_oil_filtration_coef,	
            float diesel_filtration_coef,
            float hydrolytic_acidity,	
            float oil_capacity,	
            float  density,
            string  carbon_content,
            string filter_coef_interval,
            string porosity_interval,
            string glina_interval,	
            string mg_interval,
            string ph_interval,
            string density_interval,
            string max_moisture_capacity_interval,
            string gumus_percentage_interval,
            string klass,
            string soil_class_code,
            string wrb_code,
            string wrb_new_code,
            string rgb_code,
            string shtrihovka)
        {
            this.name = name;
            this.filter = filter;
            this.porosity = porosity;
            this.watercapacity = watercapacity;
            this.gumus_depth = gumus_depth;
            this.klass = klass;
            this.soil_type = soil_type;
            this.mehsost = mehsost;
            this.soil_subtype = soil_subtype;
            this.humidity = humidity;
            this.petrol_filtration_coef = petrol_filtration_coef;
            this.fuel_oil_filtration_coef = fuel_oil_filtration_coef;
            this.diesel_filtration_coef = diesel_filtration_coef;
            this.hydrolytic_acidity = hydrolytic_acidity;
            this.oil_capacity = oil_capacity;
            this.density = density;
            this.filter_coef_interval = filter_coef_interval;
            this.filter_coef_interval = filter_coef_interval;
            this.porosity_interval = porosity_interval;
            this.glina_interval = glina_interval;
            this.mg_interval = mg_interval;
            this.ph_interval = ph_interval;
            this.density_interval = density_interval;
            this.max_moisture_capacity_interval = max_moisture_capacity_interval;
            this.gumus_percentage_interval = gumus_percentage_interval;
            this.soil_class_code = soil_class_code;
            this.wrb_code = wrb_code;
            this.wrb_new_code = wrb_new_code;
            this.rgb_code = rgb_code;
            this.shtrihovka = shtrihovka;
        }
        static public bool GetByMap(EGH01DB.IDBContext dbcontext, Coordinates coordinates, out SoilType soiltype)  // тип почвы, высота почвенного слоя и коэффициенты
        {
            bool rc = false;
            soiltype = new SoilType();
            using (SqlCommand cmd = new SqlCommand("MAP.InSoilMap", dbcontext.connection))
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
                    if (rc = reader.Read())
                    {
                        string name = (string)reader["name"];
                        float filter = (float)reader["filter_coef_ave"];
                        float porosity = (float)reader["porosity_ave"];
                        float watercapacity = (float)reader["cap_moisture_capacity"];
                        float gumus_depth = (float)reader["gumus_depth"];

                        string mehsost = (string)reader["mehsost"];
                        string soil_type = (string)reader["soil_type"];
                        string soil_subtype = (string)reader["soil_subtype"];

                        float humidity = (float)reader["humidity"];
                        float petrol_filtration_coef = (float)reader["petrol_filtration_coef"];
                        float fuel_oil_filtration_coef = (float)reader["fuel_oil_filtration_coef"];
                        float diesel_filtration_coef = (float)reader["diesel_filtration_coef"];

                        float hydrolytic_acidity = (float)reader["hydrolytic_acidity"];
                        float oil_capacity = (float)reader["oil_capacity"];
                        float density = (float)reader["density"];

                        string carbon_content = (string)reader["carbon_content"];
                        string filter_coef_interval = (string)reader["filter_coef_interval"];
                        string porosity_interval = (string)reader["porosity_interval"];
                        string glina_interval = (string)reader["glina_interval"];
                        string mg_interval = (string)reader["mg_interval"];
                        string ph_interval = (string)reader["ph_interval"];
                        string density_interval = (string)reader["density_interval"];
                        string max_moisture_capacity_interval = (string)reader["max_moisture_capacity_interval"];
                        string gumus_percentage_interval = (string)reader["gumus_percentage_interval"];

                        string klass = (string)reader["klass"];
                        string soil_class_code = (string)reader["soil_class_code"];
                        string wrb_code = (string)reader["wrb_code"];
                        string wrb_new_code = (string)reader["wrb_new_code"];
                        string rgb_code = (string)reader["rgb_code"];
                        string shtrihovka = (string)reader["shtrihovka"];

                        soiltype = new SoilType(name, filter, porosity, watercapacity, gumus_depth, mehsost, soil_type, soil_subtype,
                                                humidity, petrol_filtration_coef, fuel_oil_filtration_coef,	diesel_filtration_coef, hydrolytic_acidity,	
                                                oil_capacity, density, carbon_content, filter_coef_interval, porosity_interval, glina_interval,	
                                                mg_interval, ph_interval, density_interval, max_moisture_capacity_interval, gumus_percentage_interval,
                                                klass, soil_class_code, wrb_code, wrb_new_code, rgb_code, shtrihovka);
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

    }
}
