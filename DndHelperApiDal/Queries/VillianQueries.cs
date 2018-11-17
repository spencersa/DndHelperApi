namespace DndHelperApiDal.Queries
{
    public static class VillianQueries
    {
        public const string GetVillianObjectiveSchemes =
            @"SELECT vo.Objective,
                     vs.Scheme
              FROM TestDb.dbo.VillianObjective vo
              LEFT JOIN TestDb.dbo.VillianScheme vs ON vo.Id = vs.VillanObjectiveId";
    }
}
