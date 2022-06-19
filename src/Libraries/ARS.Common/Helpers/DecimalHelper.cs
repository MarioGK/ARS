using System.Globalization;

namespace ARS.Common.Helpers;

public static class DecimalHelper
{
    public static decimal Round(this decimal valor, int casasDecimais)
    {
        var valorNovo = decimal.Round(valor, casasDecimais, MidpointRounding.AwayFromZero);
        var valorNovoStr = valorNovo.ToString("F" + casasDecimais, CultureInfo.CurrentCulture);
        return decimal.Parse(valorNovoStr);
    }

    public static decimal? Round(this decimal? valor, int casasDecimais)
    {
        if (valor == null)
        {
            return null;
        }

        return Round(valor.Value, casasDecimais);
    }

    public static decimal RoundLower(this decimal valor, int casasDecimais)
    {
        var divisor = (decimal) Math.Pow(10, casasDecimais);
        var dividendo = (long) Math.Truncate(divisor * valor);
        return dividendo / divisor;
    }
}