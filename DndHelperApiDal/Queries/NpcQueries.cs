namespace DndHelperApiDal.Queries
{
    public class NpcQueries
    {
        public const string SelectNpcAbilityHigh =
            @"SELECT  Id
                     ,Ability
            FROM TestDb.dbo.NpcAbilityHigh";

        public const string SelectNpcAbilityLow =
            @"SELECT  Id
                     ,Ability
            FROM TestDb.dbo.NpcAbilityLow";

        public const string SelectNpcAppearance =
            @"SELECT  Id
                     ,Feature
            FROM TestDb.dbo.NpcAppearance";

        public const string SelectNpcBond =
            @"SELECT  Id
                     ,Bond
            FROM TestDb.dbo.NpcBond";

        public const string SelectNpcFlaw =
            @"SELECT  Id
                     ,Flaw
            FROM TestDb.dbo.NpcFlaw";

        public const string SelectNpcInteractionTrait =
            @"SELECT  Id
                     ,Trait
            FROM TestDb.dbo.NpcInteractionTrait";

        public const string SelectNpcMannerism =
            @"SELECT  Id
                     ,Mannerism
            FROM TestDb.dbo.NpcMannerism";

        public const string SelectNpcTalent =
            @"SELECT  Id
                     ,Talent
            FROM TestDb.dbo.NpcTalent";

        public const string SelectNpcIdeal =
            @"SELECT ni.Id
                    ,ni.Ideal
                    ,a.AlignmentName
            FROM [TestDb].[dbo].[NpcIdeals] ni
            INNER JOIN [TestDb].[dbo].Alignment a ON ni.AlignmentId = a.Id";

        public const string SelectNpcIdealByAlignmentId =
            @"SELECT ni.Id
                    ,ni.Ideal
                    ,a.AlignmentName
              FROM [TestDb].[dbo].[NpcIdeals] ni
              INNER JOIN [TestDb].[dbo].Alignment a ON ni.AlignmentId = a.Id
              WHERE a.Id = @Id";
    }
}
