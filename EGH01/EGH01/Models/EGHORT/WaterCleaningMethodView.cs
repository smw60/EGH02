//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;


namespace EGH01.Models.EGHORT
{
    public class WaterCleaningMethodView                 // категория методов ликвидации загрязнения грунтовых вод
    {
        public int    type_code          { get; set; }   // код категории
        public string name               { get; set; }   // наименование категории
        public string method_description { get; set; }   // описание метода

    }
}