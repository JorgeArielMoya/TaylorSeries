using MathNet.Symbolics;
using System.Text.RegularExpressions;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace ProyectoTaylor.Services;

public class TaylorCalculoService
{
    public double ParseValorC(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return 0;

        input = input.Trim().ToLower()
               .Replace("pi", Math.PI.ToString("R", System.Globalization.CultureInfo.InvariantCulture))
               .Replace("e", Math.E.ToString("R", System.Globalization.CultureInfo.InvariantCulture));

        var expr = Expr.Parse(input);
        var resultado = expr.Evaluate(new Dictionary<string, FloatingPoint>());
        return resultado.RealValue;
    }

    public string NormalizarFuncion(string input)
    {
        input = input.Trim().ToLower();
        input = Regex.Replace(input, @"(\d)([a-z])", "$1*$2");
        input = Regex.Replace(input, @"e\^(\([^)]+\)|\w+)", "exp($1)");
        return input;
    }

    public List<string> CalcularDerivadas(string funcion, string valorC, int cantPolinomios, out bool calculado)
    {
        var resultados = new List<string>();
        calculado = false;

        double valorCNumerico;
        try
        {
            valorCNumerico = ParseValorC(valorC);
        }
        catch
        {
            resultados.Add("Error: El valor de C no es válido.");
            return resultados;
        }

        try
        {
            var expr = Expr.Parse(NormalizarFuncion(funcion));
            var derivadaActual = expr;

            for (int i = 0; i <= cantPolinomios; i++)
            {
                var evaluada = derivadaActual.Evaluate(new Dictionary<string, FloatingPoint>
                {
                    { "x", valorCNumerico }
                });

                string valorFormateado = evaluada.RealValue.ToString("F2");
                string texto;

                string ExprToDisplay(SymbolicExpression e, string originalInput = null)
                {
                    string s = originalInput ?? e.ToString();
                    s = Regex.Replace(s, @"exp\(([^)]+)\)", "e^($1)");
                    return s;
                }

                if (i == 0)
                {
                    texto = $"f(x) = {ExprToDisplay(derivadaActual, funcion.Trim())} → f({valorC}) = {valorFormateado}";
                }
                else
                {
                    string superindice = ToSuperscriptStatic(i);
                    texto = $"f{superindice}(x) = {ExprToDisplay(derivadaActual)} → f{superindice}({valorC}) = {valorFormateado}";
                }

                resultados.Add(texto);
                derivadaActual = derivadaActual.Differentiate("x");
            }
            calculado = true;
        }
        catch (Exception ex)
        {
            resultados.Add($"Error: {ex.Message}");
        }

        return resultados;
    }

    private static string ToSuperscriptStatic(int n)
    {
        var superscripts = new Dictionary<char, char>
        {
            {'0','⁰'}, {'1','¹'}, {'2','²'}, {'3','³'}, {'4','⁴'},
            {'5','⁵'}, {'6','⁶'}, {'7','⁷'}, {'8','⁸'}, {'9','⁹'}
        };
        return string.Concat(n.ToString().Select(c => superscripts[c]));
    }
}