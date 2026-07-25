using System.Text;
using System.Text.RegularExpressions;

namespace ProyectoTaylor.Services;

public class TaylorHtmlService
{
    private readonly TaylorCalculoService _calculoService;

    public TaylorHtmlService(TaylorCalculoService calculoService)
    {
        _calculoService = calculoService;
    }

    public string GenerarPolinomioHTML(List<string> resultadosDerivadas, string valorC, int cantPolinomios, bool calculado)
    {
        if (!calculado || resultadosDerivadas.Count < cantPolinomios + 1)
            return "";

        double valorCNum;
        try
        {
            valorCNum = _calculoService.ParseValorC(valorC);
        }
        catch
        {
            return "<span class='text-danger'>Error en el valor de C</span>";
        }

        var sb = new StringBuilder();
        sb.Append("<span class='polinomio-resultado'>");

        bool primero = true;
        System.Numerics.BigInteger factorial = 1;

        for (int i = 0; i <= cantPolinomios; i++)
        {
            if (i > 0) factorial *= i;

            var linea = resultadosDerivadas[i];
            var partes = linea.Split('=');

            if (!double.TryParse(partes.Last().Trim(),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double coef))
                continue;

            if (Math.Abs(coef) < 1e-10) continue;

            string signo = coef < 0 ? " − " : (primero ? "" : " + ");
            double absCoef = Math.Abs(coef);

            string coefStr = absCoef.ToString("F4").TrimEnd('0').TrimEnd('.');
            string coefDisplay = Math.Abs(absCoef - 1.0) < 1e-10 && i > 0 ? "" : coefStr + " ";

            sb.Append($"<span class='taylor-sign'>{signo}</span>");

            bool cEsCero = Math.Abs(valorCNum) < 1e-10;
            string cMostrado = MostrarConPi(valorC);
            string xTermino = cEsCero ? "x" : $"(x − {cMostrado})";

            string numerador = i == 0
                ? coefStr
                : i == 1
                    ? $"{coefDisplay}{xTermino}"
                    : $"{coefDisplay}{xTermino}<sup>{ToSuperscript(i)}</sup>";

            if (factorial > 1)
            {
                sb.Append($@"<span class='taylor-frac'>
                    <span class='taylor-num'>{numerador}</span>
                    <span class='taylor-den'>{i}!</span>
                    </span>");
            }
            else
            {
                sb.Append($"<span>{numerador}</span>");
            }

            primero = false;
        }

        sb.Append("</span>");
        return sb.ToString();
    }

    public string MostrarConPi(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return Regex.Replace(
            input, @"\bpi\b", "π",
            RegexOptions.IgnoreCase
        );
    }

    private string ToSuperscript(int n)
    {
        var superscripts = new Dictionary<char, char>
        {
            {'0','⁰'}, {'1','¹'}, {'2','²'}, {'3','³'}, {'4','⁴'},
            {'5','⁵'}, {'6','⁶'}, {'7','⁷'}, {'8','⁸'}, {'9','⁹'}
        };

        return string.Concat(n.ToString().Select(c => superscripts[c]));
    }
}