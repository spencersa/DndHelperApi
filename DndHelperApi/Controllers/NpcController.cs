using DndHelperApiDal.Models;
using DndHelperApiDal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DndHelperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NpcController : ControllerBase
    {
        private readonly INpcService _npcService;

        public NpcController(INpcService npcService)
        {
            _npcService = npcService;
        }

        [HttpGet]
        [Route("GetNpcAbilities")]
        public async Task<NpcAbilities> GetNpcAbilities()
        {
            return await _npcService.GetNpcAbilitiesAsync();
        }

        [HttpGet]
        [Route("GetNpcIdeals")]
        public async Task<IEnumerable<NpcIdeals>> GetNpcIdeals()
        {
            return await _npcService.GetNpcIdealsAsync();
        }

        [HttpGet]
        [Route("GetNpcInteractionTraits")]
        public async Task<IEnumerable<NpcInteractionTrait>> GetNpcInteractionTraits()
        {
            return await _npcService.GetNpcInteractionTraitsAsync();
        }

        [HttpGet]
        [Route("GetNpcMannerisms")]
        public async Task<IEnumerable<NpcMannerism>> GetNpcMannerisms()
        {
            return await _npcService.GetNpcMannerismAsync();
        }

        [HttpGet]
        [Route("GetNpcAppearances")]
        public async Task<IEnumerable<NpcAppearance>> GetNpcAppearances()
        {
            return await _npcService.GetNpcAppearancesAsync();
        }

        [HttpGet]
        [Route("GetNpcBonds")]
        public async Task<IEnumerable<NpcBond>> GetNpcBonds()
        {
            return await _npcService.GetNpcBondsAsync();
        }

        [HttpGet]
        [Route("GetNpcFlaws")]
        public async Task<IEnumerable<NpcFlaw>> GetNpcFlaws()
        {
            return await _npcService.GetNpcFlawsAsync();
        }

        [HttpGet]
        [Route("GetNpcTalents")]
        public async Task<IEnumerable<NpcTalent>> GetNpcTalents()
        {
            return await _npcService.GetNpcTalentsAsync();
        }

        [HttpGet("GetNpcIdealsByAlignmentId")]
        public async Task<IEnumerable<NpcIdeals>> GetNpcIdealsByAlignmentId(int id)
        {
            return await _npcService.GetNpcIdealsByAlignmentIdAsync(id);
        }

        [HttpGet("GetRandomNpc")]
        public async Task<Npc> GetRandomNpc()
        {
            return await _npcService.GetRandomNpcAsync();
        }
    }
}
