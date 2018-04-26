using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestControlApi.app;
using GuestControlApi.Auth;
using GuestControlApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GuestControlApi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        MongoDbContext _db = new MongoDbContext();
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]InterfaceLogin content)
        {
            var user = _db.Pessoas.Find(w => w.Usuario == content.login && w.Senha == Functions.GetHash(content.senha))?.FirstOrDefault();

            if (user == null) return StatusCode(400, "Não encontrado");

            var payload = new PayloadJwt
            {
                subId = user.Usuario,
                sub = user.Email,
                name = user.Nome,
                roles = new List<string>
                {
                    "logger",
                    "admin"
                }
            };

            var token = new TokenBuilder(_configuration).Build(payload);

            return StatusCode(200, new
            {
                user.Nome,
                id = user._id.ToString(),
                token,
                payload.roles
            });
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

        [HttpGet]
        [Route("ok")]
        public IActionResult ok()
        {
            return StatusCode(200, "foice");
        }
    }
}