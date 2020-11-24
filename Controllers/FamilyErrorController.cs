using System;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("/FamilyError")]
    public class FamilyErrorController : ControllerBase
    {
        private IFamilyService familyService;

        public FamilyErrorController()
        {
            familyService = new SqliteFamilyService();
        }
        
        [HttpGet]
        public async Task<ActionResult<string>> GetError()
        {
            try
            {
                string error =await familyService.getFamilyError();
                return Ok(error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}