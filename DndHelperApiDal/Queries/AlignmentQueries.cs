namespace DndHelperApiDal.Queries
{
    public static class AlignmentQueries
    {
        public const string SelectAlignment =
            @"SELECT  Id
                     ,AlignmentName
            FROM TestDb.dbo.Alignment";
    }
}
