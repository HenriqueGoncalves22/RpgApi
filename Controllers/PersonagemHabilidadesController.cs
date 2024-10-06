using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagemHabilidadesController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonagemHabilidadesController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPersonagemHabilidade)
        {
            try
            {
                Personagem personagem = await _context.TB_PERSONAGENS
                .Include(p => p.Arma)
                .Include(p => p.PersonagemHabilidades).ThenInclude(ps => ps.Habilidade)
                .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId);

                if (personagem == null)
                   throw new System.Exception("Personagem não encontrado para o Id informado.");
                
                Habilidade habilidade = await _context.TB_HABILIDADES.FirstOrDefaultAsync(h => h.Id == novoPersonagemHabilidade.HabilidadeId);
                if (habilidade == null)
                   throw new System.Exception("Habilidade não encontrada.");

                PersonagemHabilidade ph = new PersonagemHabilidade();
                ph.Personagem = personagem;
                ph.Habilidade = habilidade;
                await _context.TB_PERSONAGENS_HABILIDADES.AddAsync(ph);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);

            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 /*(5) Criar um método na classe PersonagemHabilidadesController.cs que retorne uma lista de
PersonagemHabilidade de acordo com o id do personagem passado por parâmetro. Using de
System.Collections.Generic e System.Linq */
        [HttpGet("GetBy{id}")]
        public IActionResult GetSingle(string habilidade)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.habilidade.Contains(habilidade));
            if(listaBusca.IsNullOrEmpty())
                return BadRequest("NotFound");
            
            return Ok(listaBusca);
        }

/*(6) Criar um método na classe PersonagemHabilidadesController.cs que retorne uma lista de Habilidades
 com a rota chamada GetHabilidades. */
        [HttpGet("GetHabilidades")]
        public IActionResult GetAll()
        {
            return Ok(PersonagemHabilidades);
        }

 /*(7) Criar um método na controller PersonagemHabilidadesController.cs que remova os dados da tabela 
 PersonagemHabilidades. Esse método terá que ser do tipo Post (com rota chamada DeletePersonagemHabilidade)
pelo fato de ter que receber o objeto como parâmetro, contendo o id do personagem e da habilidade. 
Use o FirstOrDefaultAsync que exige o using System.Linq.*/
        [HttpPost("DeletePersonagemHabilidade/`{id}/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Personagem pRemover = await _context.TB_PERSONAGENS_HABILIDADES.FirstOrDefaultAsync(p => p.Id == id);
                _context.TB_PERSONAGENS.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}