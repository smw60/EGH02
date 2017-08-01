//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;


namespace EGH01.Models.EGHORT
{
    public class SoilCleaningMethodView                 // категория методов ликвидации загрязнения почвогрунтов
    {
        public int    type_code          { get; set; }   // код категории
        public string name               { get; set; }   // наименование категории
        public string method_description { get; set; }   // описание метода

	}
}