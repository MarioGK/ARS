using LiteDB.Async;

namespace ARS.Common.Helpers;

public static class DatabaseHelpers
{
    public static ILiteDatabaseAsync StartDatabase()
    {
        //Register the database
        var database = new LiteDatabaseAsync("ARS.db");
        

        return database;
    }
}