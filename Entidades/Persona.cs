
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Persona
    {
        [Key]
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        
        public decimal Pulsacion { get; set; }
        public void CalcularPulsaciones()
        {
            if (Sexo.Equals("F") || Sexo.Equals("f"))
            {
                Pulsacion = (220 - Edad) / 10;
            }
            else
            {
                Pulsacion = (210 - Edad) / 10;
            }
        }
    }
}
