using System.ComponentModel.DataAnnotations;

namespace ProyectoTaylor.Models;

public class TaylorModel
{
    public string Funcion { get; set; } = string.Empty;
    public string ValorC { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cantidad de polinomios debe ser mayor que 0")]
    public int CantPolinomios { get; set; } = 1;
}
