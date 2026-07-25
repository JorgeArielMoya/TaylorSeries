using Microsoft.AspNetCore.Components;
using ProyectoTaylor.Models;
using ProyectoTaylor.Services;

namespace ProyectoTaylor.Components.Pages.TaylorPages
{
    public partial class Taylor
    {
        [Inject]
        private TaylorCalculoService CalculoService { get; set; } = default!;

        [Inject]
        private TaylorHtmlService HtmlService { get; set; } = default!;

        private TaylorModel Modelo { get; set; } = new();
        private List<string> ResultadosDerivadas { get; set; } = new();
        private bool _calculado = false;

        private void CalcularDerivadas()
        {
            ResultadosDerivadas = CalculoService.CalcularDerivadas(
                Modelo.Funcion,
                Modelo.ValorC,
                Modelo.CantPolinomios,
                out _calculado
            );
        }

        private string GenerarPolinomioHTML()
        {
            return HtmlService.GenerarPolinomioHTML(
                ResultadosDerivadas,
                Modelo.ValorC,
                Modelo.CantPolinomios,
                _calculado
            );
        }
    }
}