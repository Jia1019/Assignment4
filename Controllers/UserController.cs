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
    [Route("/Users")]
    public class UserController : ControllerBase
    {
        private IFamilyService familyService;

        public UserController()
        {
            familyService = new SqliteFamilyService();
        }

        [HttpGet]
        public async Task<ActionResult<IList<User>>> GetUsers()
        {
            try
            {
                IList<User> users =await familyService.GetUsers();
                return Ok(JsonSerializer.Serialize(users, new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}