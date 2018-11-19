using DndHelperApiCore;
using DndHelperApiCore.Models;
using DndHelperApiDal.Models;
using DndHelperApiDal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DndHelperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillianController : ControllerBase
    {
        private readonly IVillianService _villianService;
        private readonly IRandomizer _randomizer;

        public VillianController(IVillianService villianService, IRandomizer randomizer)
        {
            _villianService = villianService;
            _randomizer = randomizer;
        }

        [HttpGet]
        [Route("GetVillianObjectives")]
        public async Task<IEnumerable<VillianObjective>> GetVillianObjectives()
        {
            return await _villianService.GetVillianObjectivesAsync();
        }

        // GET api/values
        [HttpGet]
        [Route("GetVillianObjectivesWithSchemes")]
        public async Task<IEnumerable<VillianObjectiveSchemes>> GetVillianObjectivesWithSchemes()
        {
            return await _villianService.GetVillianObjectiveSchemesAsync();
        }

        [HttpGet]
        [Route("GetVillianMethods")]
        public async Task<IEnumerable<VillianMethod>> GetVillianMethods()
        {
            return await _villianService.GetVillianMethodsAsync();
        }

        [HttpGet]
        [Route("GetVillianMethodsWithSubMethods")]
        public async Task<IEnumerable<VillianMethod>> GetVillianMethodsWithSubMethods()
        {
            return await _villianService.GetVillianMethodsWithSubMethodsAsync();
        }

        [HttpGet]
        [Route("GetVillianWeaknesses")]
        public async Task<IEnumerable<VillianWeakness>> GetVillianWeaknesses()
        {
            return await _villianService.GetVillianWeaknessesAsync();
        }

        [HttpGet]
        [Route("GetRandomVillian")]
        public async Task<Villian> GetRandomVillian()
        {
            return await _randomizer.GetRandomVillianAsync();
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
