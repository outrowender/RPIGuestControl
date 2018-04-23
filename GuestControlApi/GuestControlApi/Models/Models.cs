using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestControlApi.Models
{
    public class InterfaceLogin
    {
        public string login { get; set; }
        public string senha { get; set; }
    }

    public class InterfaceDepartamentos
    {
        public string Cod { get; set; }
        public string Descricao { get; set; }
    }

    public class LogInput
    {
        public string Departamento { get; set; }
        public string Nome { get; set; }
        public string Placa { get; set; }
        public string Obs { get; set; }
        public List<FotoInput> Fotos { get; set; }
    }

    public class FotoInput
    {
        public string FileId { get; set; }
        public string Descricao { get; set; }
    }

}
