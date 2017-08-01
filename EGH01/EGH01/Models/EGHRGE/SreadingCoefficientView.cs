using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGH01DB.Types;

namespace EGH01.Models.EGHRGE
{
    public class SpreadingCoefficientView
    {
        public int   code                   { get; set; } 
        public int   list_groundType        { get; set; }     // тип грунта 
        public int   list_petrochemicalType { get; set; }     // тип нефтепродукта
        public float min_volume             { get; set; }     // нижняя граница диапазона пролива   
        public float max_volume             { get; set; }     // верхняя граница диапазона пролива 
        public float min_angle              { get; set; }     // книжняя граница диапазона углов наклона 
        public float max_angle              { get; set; }     // верхняя граница диапазона углов наклона 
        public float koef                   { get; set; }     // коэффициент разлива в диапазоне 
	}
}