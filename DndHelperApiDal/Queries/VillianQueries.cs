namespace DndHelperApiDal.Queries
{
    public static class VillianQueries
    {
        public const string GetVillianObjectives =
            @"SELECT  vo.Id
                     ,vo.Objective
              FROM TestDb.dbo.VillianObjective vo";

        public const string GetVillianObjectiveSchemes =
            @"SELECT  vo.Id
                     ,vo.Objective
                     ,vs.Scheme
              FROM TestDb.dbo.VillianObjective vo
              LEFT JOIN TestDb.dbo.VillianScheme vs ON vo.Id = vs.VillanObjectiveId";
    }
}
