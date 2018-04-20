using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GuestControlApi.Models
{
    public class Pessoas
    {
        public Pessoas()
        {
            _id = new ObjectId();
        }

        [BsonId]
        public ObjectId _id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    public class Departamentos
    {
        public Departamentos()
        {
            _id = new ObjectId();
        }

        [BsonId]
        public ObjectId _id { get; set; }
        public string Cod { get; set; }
        public string Descricao { get; set; }
    }

    public class Registros
    {
        public Registros()
        {
            _id = new ObjectId();
        }

        [BsonId]
        public ObjectId _id { get; set; }
        public DateTime Hora { get; set; }
        public List<Foto> Fotos { get; set; }
    }

    public class Foto
    {
        public Foto()
        {
            _id = new ObjectId();
        }

        [BsonId]
        public ObjectId _id { get; set; }
        public BsonObjectId Registro { get; set; }
    }


}
