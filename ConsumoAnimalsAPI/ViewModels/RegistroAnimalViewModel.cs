using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumoAnimalsAPI.ViewModels
{
    public class RegistroAnimalViewModel
    {
        public int IdRegistroAnimal { get; set; }

        public int IdTipoAnimal { get; set; }

        public int IdEstadoAnimal { get; set; }

        public int IdSituacaoAnimal { get; set; }

        public string Descricao { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
