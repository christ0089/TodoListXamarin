using System;
using SQLite;
namespace DevMty.Models
{
    // El objecto Tarea esta compuesto de un ID, Nombre, Notas, Dia de Inicio, Dia de Fin y un completado.
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }  
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool Done { get; set; }
    }
}