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
    [Route("/Adults")]
    public class AdultsController : ControllerBase
    {
        private IFamilyService familyService;

        public AdultsController()
        {
            familyService = new SqliteFamilyService();
        }

        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetAdults()
        {
            try
            {
                IList<Adult> adults = await familyService.GetAdults();
                return Ok(JsonSerializer.Serialize(adults, new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<Adult>> GetAdultByID([FromQuery] int id)
        {
            try
            {
                Adult adult = await familyService.getAdultById(id);
                return Ok(JsonSerializer.Serialize(adult, new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> AddAdult([FromBody] Adult adult)
        {
            try
            {
                string result = await familyService.AddAdult(adult);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task RemoveAdult([FromQuery] int id)
        {
            try
            {
                await familyService.RemoveAdult(id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        public async Task UpdateAdult([FromBody] Adult adult)
        {
            try
            {
                await familyService.UpdateAdult(adult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(404, e.Message);
            }
        }
        
        
    }
}