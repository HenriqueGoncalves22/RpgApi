using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;
using RpgApi.Models;

namespace GamingAPI.Models
{
    public class Arma
    {
        public int Id {get; set;}
        public string Nome {get; set;} = "";
        public int Dano {get; set;}
        public Personagem? Personagem { get; set; }
        public int PersonagemId { get; set; }


    }
}