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
    [Route("/Children")]
    public class ChildrenController : ControllerBase
    {
        private IFamilyService familyService;

        public ChildrenController()
        {
            familyService = new SqliteFamilyService();
        }

        [HttpGet]
        public async Task<ActionResult<IList<Child>>> GetChildren()
        {
            try
            {
                IList<Child> children =await familyService.GetChildren();
                return Ok(JsonSerializer.Serialize(children, new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<Child>> GetChildByID([FromQuery] int id)
        {
            try
            {
                Child child =await familyService.getChildById(id);
                return Ok(JsonSerializer.Serialize(child, new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> AddChild([FromBody] Child child)
        {
            try
            {
                string result =await familyService.AddChildren(child);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task RemoveChild([FromQuery] int id)
        {
            try
            {
               await familyService.RemoveChild(id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        public async Task UpdateChild([FromBody] Child child)
        {
            try
            {
                await familyService.UpdateChild(child);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(404, e.Message);
            }
        }
    }
}