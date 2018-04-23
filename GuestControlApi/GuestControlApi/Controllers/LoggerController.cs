using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GuestControlApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace GuestControlApi.Controllers
{

    [Route("api/[controller]")]
    public class LoggerController : Controller
    {
        MongoDbContext _db = new MongoDbContext();

        [HttpPost]
        [Route("new")]
        [Authorize(Roles = "admin")]
        public IActionResult Registrar([FromBody]LogInput r)
        {

            var newRegistro = new Registros
            {
                Departamento = r.Departamento,
                Nome = r.Nome,
                Obs = r.Obs,
                Placa = r.Placa,
                Hora = DateTime.Now,
                Usuario = User.FindFirst("subjectId")?.Value,
                Fotos = r.Fotos.Select(e => new ArquivoLog
                {
                    FileId = new ObjectId(e.FileId),
                    Descricao = e.Descricao
                }).ToList()
            };

            _db.Registros.InsertOne(newRegistro);

            return StatusCode(201, newRegistro);
        }

        [HttpPost]
        [Route("file")]
        [Authorize(Roles = "admin")]
        [DisableRequestSizeLimit]
        public IActionResult foto()
        {
            var files = Request.Form.Files;

            if (files == null) return StatusCode(400);

            var gfs = new GridFSBucket(_db.Registros.Database);

            var listFiles = new List<string>();
            foreach (var item in files)
            {
                var objId = gfs.UploadFromStream(item.FileName, item.OpenReadStream());

                listFiles.Add(objId.ToString());
            }

            return StatusCode(201, new
            {
                hora = DateTime.Now,
                envios = listFiles.Count,
                arquivos = listFiles
            });
        }

        [HttpGet]
        [Route("download/{id}")]
        public IActionResult download(string id)
        {
            var aa = new ObjectId(id);

            var gfs = new GridFSBucket(_db.Registros.Database);

            var k = gfs.DownloadAsBytes(aa);

            return File(k, "APPLICATION/octet-stream", "aaaaa");
        }

    }
}