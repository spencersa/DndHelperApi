namespace DndHelperApiDal.Queries
{
    public static class RaceQueries
    {
        public const string SelectRace =
            @"SELECT  Id
                     ,RaceName
            FROM TestDb.dbo.Race";
    }
}
