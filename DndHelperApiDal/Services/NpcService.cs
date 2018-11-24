using DndHelperApiCore.Extensions;
using DndHelperApiDal.Models;
using DndHelperApiDal.Queries;
using DndHelperApiDal.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DndHelperApiDal.Services
{
    public interface INpcService
    {
        Task<NpcAbilities> GetNpcAbilitiesAsync();
        Task<IEnumerable<NpcAppearance>> GetNpcAppearancesAsync();
        Task<IEnumerable<NpcBond>> GetNpcBondsAsync();
        Task<IEnumerable<NpcFlaw>> GetNpcFlawsAsync();
        Task<IEnumerable<NpcIdeals>> GetNpcIdealsAsync();
        Task<IEnumerable<NpcInteractionTrait>> GetNpcInteractionTraitsAsync();
        Task<IEnumerable<NpcMannerism>> GetNpcMannerismAsync();
        Task<IEnumerable<NpcTalent>> GetNpcTalentsAsync();
        Task<IEnumerable<NpcIdeals>> GetNpcIdealsByAlignmentIdAsync(int alignmentId);
        Task<Npc> GetRandomNpcAsync();
    }

    public class NpcService : INpcService
    {
        private readonly IRepository _repository;
        private readonly IAlignmentService _alignmentService;
        private readonly IRaceService _raceService;

        public NpcService(IRepository repository, IAlignmentService alignmentService, IRaceService raceService)
        {
            _repository = repository;
            _alignmentService = alignmentService;
            _raceService = raceService;
        }

        public async Task<NpcAbilities> GetNpcAbilitiesAsync()
        {
            var npcAbilitiesHigh = _repository.QueryAsync<NpcAbility>(NpcQueries.SelectNpcAbilityHigh, CommandType.Text);
            var npcAbilitiesLow = _repository.QueryAsync<NpcAbility>(NpcQueries.SelectNpcAbilityLow, CommandType.Text);

            await Task.WhenAll(npcAbilitiesHigh, npcAbilitiesLow);

            return new NpcAbilities
            {
                NpcAbilitiesHigh = npcAbilitiesHigh.Result,
                NpcAbilitiesLow = npcAbilitiesLow.Result
            };
        }

        public async Task<IEnumerable<NpcIdeals>> GetNpcIdealsAsync() =>
            await _repository.QueryAsync<NpcIdeals>(NpcQueries.SelectNpcIdeal, CommandType.Text);

        public async Task<IEnumerable<NpcInteractionTrait>> GetNpcInteractionTraitsAsync() =>
            await _repository.QueryAsync<NpcInteractionTrait>(NpcQueries.SelectNpcInteractionTrait, CommandType.Text);

        public async Task<IEnumerable<NpcMannerism>> GetNpcMannerismAsync() =>
            await _repository.QueryAsync<NpcMannerism>(NpcQueries.SelectNpcMannerism, CommandType.Text);

        public async Task<IEnumerable<NpcAppearance>> GetNpcAppearancesAsync() =>
            await _repository.QueryAsync<NpcAppearance>(NpcQueries.SelectNpcAppearance, CommandType.Text);

        public async Task<IEnumerable<NpcBond>> GetNpcBondsAsync() =>
            await _repository.QueryAsync<NpcBond>(NpcQueries.SelectNpcBond, CommandType.Text);

        public async Task<IEnumerable<NpcFlaw>> GetNpcFlawsAsync() =>
            await _repository.QueryAsync<NpcFlaw>(NpcQueries.SelectNpcFlaw, CommandType.Text);

        public async Task<IEnumerable<NpcTalent>> GetNpcTalentsAsync() =>
            await _repository.QueryAsync<NpcTalent>(NpcQueries.SelectNpcTalent, CommandType.Text);

        public async Task<IEnumerable<NpcIdeals>> GetNpcIdealsByAlignmentIdAsync(int alignmentId)
        {
            return await _repository.QueryAsync<NpcIdeals>(NpcQueries.SelectNpcIdealByAlignmentId, new { Id = alignmentId }, CommandType.Text);
        }

        public async Task<Npc> GetRandomNpcAsync()
        {
            var raceTask = _raceService.GetNpcRacesAsync();
            var alignmentTask = _alignmentService.GetAlignments();
            var abilitiesTask = GetNpcAbilitiesAsync();
            var appearanceTask = GetNpcAppearancesAsync();
            var bondTask = GetNpcBondsAsync();
            var flawTask = GetNpcFlawsAsync();
            var interactionTraitTask = GetNpcInteractionTraitsAsync();
            var mannerismTask = GetNpcMannerismAsync();
            var talentTask = GetNpcTalentsAsync();

            await Task.WhenAll(raceTask, alignmentTask, abilitiesTask, appearanceTask, bondTask, flawTask, interactionTraitTask, mannerismTask, talentTask);

            var alignment = alignmentTask.Result.PickRandom();
            var ideal = await GetNpcIdealsByAlignmentIdAsync(alignment.Id);

            return new Npc
            {
                Race = raceTask.Result.PickRandom().RaceName,
                Alignment = alignment.AlignmentName,
                AbilityHigh = abilitiesTask.Result.NpcAbilitiesHigh.PickRandom().Ability,
                AbilityLow = abilitiesTask.Result.NpcAbilitiesLow.PickRandom().Ability,
                Appearance = string.Join(", ", appearanceTask.Result.PickRandom(3).Select(x => x.Feature)),
                Bond = bondTask.Result.PickRandom().Bond,
                Flaw = flawTask.Result.PickRandom().Flaw,
                Ideal = ideal.PickRandom().Ideal,
                InteractionTrait = interactionTraitTask.Result.PickRandom().Trait,
                Mannerism = mannerismTask.Result.PickRandom().Mannerism,
                Talent = talentTask.Result.PickRandom().Talent
            };
        }
    }
}
