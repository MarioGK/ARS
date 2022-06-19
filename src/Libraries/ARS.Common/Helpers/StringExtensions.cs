using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ARS.Common.Bases;

namespace ARS.Common.Helpers;

public static class StringExtensions
{
    public static string ToFriendlyCase(this string pascalString)
    {
        return Regex.Replace(pascalString, "(?!^)([A-Z])", " $1");
    }

    public static string RemoveDiacritics(this string text)
    {
        return string.Concat(
            text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                             UnicodeCategory.NonSpacingMark)
        ).Normalize(NormalizationForm.FormC);
    }

    public static string GetBetween(this string strSource, string strStart, string strEnd)
    {
        if (!strSource.Contains(strStart) || !strSource.Contains(strEnd))
        {
            return "";
        }

        var start = strSource.IndexOf(strStart, 0, StringComparison.Ordinal) + strStart.Length;
        var end = strSource.IndexOf(strEnd, start, StringComparison.Ordinal);
        return strSource.Substring(start, end - start);
    }

    public static string ToPortuguese(this bool value)
    {
        return value ? "Sim" : "Não";
    }

    public static string RemainingTime(this TimeSpan timeSpan)
    {
        return timeSpan == TimeSpan.Zero ? "Finalizada" : timeSpan.FormatHour();
    }

    public static string FormatHour(this TimeSpan timeSpan)
    {
        return timeSpan.ToString(@"hh\:mm");
    }

    public static string ToDateTimeWithHour(this DateTime value)
    {
        return value.ToString("dd/MM/yyyy HH:mm");
    }

    public static string ToDateTimeWithHour(this DateTime? value)
    {
        return !value.HasValue ? "" : value.Value.ToString("dd/MM/yyyy HH:mm");
    }
    
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }
    
    public static bool IsNotNullOrEmpty(this string value)
    {
        return !string.IsNullOrEmpty(value);
    }

    public static string ToS(this double d, string? format = null)
    {
        return string.IsNullOrEmpty(format) ? d.ToString(CultureInfo.InvariantCulture) : d.ToString(format);
    }

    public static string GetCollectionName(this object obj)
    {
        return GetCollectionName(obj.GetType());
    }
    
    public static string GetCollectionName(this Type type)
    {
        return $"{type.Name}SCollection";
    }

    public static string GetFullId(this BaseData data)
    {
            return $"{data.GetCollectionName()}/{data.Id}";
    }
}