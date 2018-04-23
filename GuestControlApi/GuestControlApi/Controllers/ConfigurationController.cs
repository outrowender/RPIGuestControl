using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestControlApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuestControlApi.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        MongoDbContext _db = new MongoDbContext();

        [HttpPost]
        [Route("new")]
        [Authorize(Roles = "admin")]
        public IActionResult Departamentos([FromBody]List<Departamentos> listaDepartamentos)
        {
          
            _db.Departamentos.InsertMany(listaDepartamentos);

            return StatusCode(200, listaDepartamentos);
        }
    }
}