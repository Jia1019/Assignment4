using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("/Families")]
    public class FamilyController :ControllerBase
    {
        private IFamilyService familyService;

        public FamilyController()
        {
            familyService = new SqliteFamilyService();
        }

        [HttpGet]
        public async Task<ActionResult<IList<Family>>> GetFamilies()
        {
            try
            {
                IList<Family> families = await familyService.GetFamilies();
                return Ok(JsonSerializer.Serialize(families, new JsonSerializerOptions {WriteIndented = true}));            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task AddFamily([FromBody] Family family)
        {
            try
            { 
                await familyService.AddFamily(family);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task RemoveFamily([FromQuery] int id)
        {
            try
            {
               await familyService.RemoveFamily(id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        public async Task UpdateFamily([FromBody] Family family)
        {
            try
            {
               await familyService.UpdateFamily(family);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(404, e.Message);
            }
        }
    }
}