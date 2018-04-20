using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestControlApi.app;
using GuestControlApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace GuestControlApi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        MongoDbContext _db = new MongoDbContext();

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]InterfaceLogin content)
        {
            var user = _db.Pessoas.Find(w => w.Usuario == content.login && w.Senha == Functions.GetHash(content.senha))?.FirstOrDefault();

            return StatusCode(user != null ? 200 : 400, user);
        }

        [HttpPut]
        [Route("new")]
        public IActionResult New()
        {
            var user = new Pessoas
            {
                Email = "wndrptrckone@gmail.com",
                Nome = "Wender Patrick",
                Senha = Functions.GetHash("wndrptrckone"),
                Usuario = "wender.galvao"
            };

            _db.Pessoas.InsertOne(user);

            return StatusCode(201);
        }
    }
}