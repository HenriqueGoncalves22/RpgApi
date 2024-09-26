using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [Route("[controller]")]
    public class PersonagensExercicioController : Controller
    {
        public static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida = 100, Forca = 17, Defesa = 23, Inteligencia = 33, Classe = ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida = 100, Forca = 15, Defesa = 25, Inteligencia = 30, Classe = ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida = 100, Forca = 18, Defesa = 21, Inteligencia = 35, Classe = ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida = 100, Forca = 18, Defesa = 18, Inteligencia = 37, Classe = ClasseEnum.Mago},
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida = 100, Forca = 20, Defesa = 17, Inteligencia = 31, Classe = ClasseEnum.Cavaleiro},
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida = 100, Forca = 21, Defesa = 13, Inteligencia = 34, Classe = ClasseEnum.Clerigo},
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida = 100, Forca = 25, Defesa = 11, Inteligencia = 35, Classe = ClasseEnum.Mago}
        };

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(personagens);
        }

        [HttpGet("GetByClasse/{Classe}")]
        public IActionResult Get(ClasseEnum Classe)
        {
            List<Personagem> listaFinal = personagens.FindAll(p => p.Classe == Classe);
            return Ok(listaFinal);
        }

        [HttpPost]
        public IActionResult PostValidacaoMago(Personagem novoPersonagem)
        {
            if (novoPersonagem.Classe == ClasseEnum.Mago)
                if (novoPersonagem.Inteligencia < 35)
                    return BadRequest("Inteligencia não pode ter o valor menor que 35 para classe mago.");

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpPost]
        public IActionResult PostValidacao(Personagem novoPersonagem)
        {
            if(novoPersonagem.Defesa > 10 || novoPersonagem.Inteligencia > 30)
                return BadRequest("A defesa não pode ser menor que 10 ou o inteligencia maior que 30.");

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            return Ok("Quantidade de personagens: " + personagens.Count + "\nA soma da inteligencia é: " + personagens.Sum(p => p.Inteligencia));
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigo()
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro).OrderByDescending(p => p.PontosVida).ToList();
            return Ok(listaBusca);
        }

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Nome.Contains(nome));
            if(listaBusca.IsNullOrEmpty())
                return BadRequest("NotFound");
            
            return Ok(listaBusca);
        }
    }
}