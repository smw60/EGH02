using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGH01DB.Primitives;

namespace EGH01DB.Types
{
    public class SoilType
    {
        public string name { get; private set; }             // name - наименование почв
        public float filter { get; private set; }            // коэффициент фильтрации kf
        public float porosity { get; private set; }          // коэффициент пористости porosity_ave  
        public float watercapacity { get; private set; }     // коэффициент влагоемкости cap_moisture_capacity	Капиллярная влагоёмкость, %
        public float gumus_height { get; private set; }             // высота почвенного покрова в см gumus
        public float mehsost { get; private set; }             // 	Механический состав почвы

        public float soil_type { get; private set; }             // 	Тип почв
        public float soil_subtype { get; private set; }             // 	Подтип почв

        public float humidity{ get; private set; }             // 	Влажность полевая, %

        public float petrol_filtration_coef { get; private set; }             // 	Коэффициент фильтрации бензина, м/сут
        public float fuel_oil_filtration_coef	{ get; private set; }             // Коэффициент фильтрации  мазута, м/сут
        public float diesel_filtration_coef	{ get; private set; }             // Коэффициент фильтрации дизельного топлива, м/сут

        public float hydrolytic_acidity	{ get; private set; }             // Гидролитическая кислотность,  м-экв. на 100 г почвы

        public float oil_capacity	{ get; private set; }             //  Коэффициент нефтеёмкости

        public float  density { get; private set; }             //  	Объёмная плотность, г/см³
        public float  carbon_content	{ get; private set; }             //  Содержание органического углерода, %
        public string filter_coef_interval	{ get; private set; }             //  Коэффициент фильтрации, м/сут
        public string porosity_interval	{ get; private set; }             //  Пористость в интервале от, до, %
        public string glina_interval	{ get; private set; }             //  Содержание глины в породе
        public string mg_interval	{ get; private set; }             //  Максимальная гигроскопичность, %
        public string ph_interval	{ get; private set; }             //  Кислотность почвы
        public string density_interval	{ get; private set; }             //  Удельная плотность, г/см³
        public string max_moisture_capacity_interval	{ get; private set; }             //  Предельная полевая влагоёмкость, %
        public string gumus_percentage_interval { get; private set; }             //  Содержание гумуса, %

        public string klass { get; private set; }             // класс почв по классификатору klass
        public string soil_type_code { get; private set; }         // тип почв по классификатору международной классификации
        public string  wrb_code	{ get; private set; }         // Код почвы в международной классификационной системе WRB (уточнённые)
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
            this.gumus_height = 0.0f;
            this.klass = "";
            this.soil_type = "";
        }
        public SoilType(string name, float filter, float porosity, 
                        float watercapacity, float gumus_height,
                        string klass, string soil_type)
        {
            this.name = name;
            this.filter = filter;
            this.porosity = porosity;
            this.watercapacity = watercapacity;
            this.gumus_height = gumus_height;
            this.klass = klass;
            this.soil_type = soil_type;
        }
        public static bool GetByMap(IDBContext db, Coordinates coordinates, out SoilType soiltype)
        {
            // заглушка
            soiltype = SoilType.defaultype;
            return true;
        }


    }
}
