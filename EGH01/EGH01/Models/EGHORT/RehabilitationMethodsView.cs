namespace EGH01.Models.EGHORT
{
    public class RehabilitationMethodsView            
    {
        public int    type_code { get; set; }   
        public string name      { get; set; }  
        public int list_type { get; set; } 
        public int list_cadastre { get; set; }
        public int list_petrochemical { get; set; }
        public int list_emergency { get; set; }
        public int list_penetration { get; set; }
        public int list_soil { get; set; }
        public int list_water { get; set; }
        public int list_waterArea { get; set; }
        public int list_soilCleaning { get; set; }
        public int list_waterCleaning { get; set; }
        public bool waterachieved { get; set; }
    }
}
 
