namespace ARS.Common.Helpers;

public static class DateHelpers
{
    public static DateOnly DateOnlyNow()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}