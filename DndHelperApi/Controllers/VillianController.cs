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

        public VillianController(IVillianService villianService)
        {
            _villianService = villianService;
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
