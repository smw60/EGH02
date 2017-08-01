namespace EGH01.Models.EGHORT
{
    public class EmergencyClassView            // Классификация аварий
    {
        public int    type_code { get; set; }   // код категории
        public string name      { get; set; }   // наименование категории
        public float  minmass   { get; set; }   // минимальное значение диапазона
        public float  maxmass   { get; set; }   // максимальное значение диапазона
    }
}