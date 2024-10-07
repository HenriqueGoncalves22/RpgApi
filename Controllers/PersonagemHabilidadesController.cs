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
                .Include(p => p.PersonagemHabilidade)
                .ThenInclude(ps => ps.Habilidade)
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

 // (5) Método GetByPersonagemId
[HttpGet("GetByPersonagemId/{id}")]
public async Task<IActionResult> GetByPersonagemId(int id)
{
    var habilidades = await _context.TB_PERSONAGENS_HABILIDADES
        .Where(ph => ph.PersonagemId == id)
        .ToListAsync();

    if (habilidades == null || habilidades.Count == 0)
        return NotFound("Nenhuma habilidade encontrada para este personagem.");

    return Ok(habilidades);
}

// (6) Método GetHabilidades
[HttpGet("GetHabilidades")]
public async Task<IActionResult> GetHabilidades()
{
    var habilidades = await _context.TB_HABILIDADES.ToListAsync();
    return Ok(habilidades);
}

// (7) Método DeletePersonagemHabilidade
[HttpPost("DeletePersonagemHabilidade/{personagemId}/{habilidadeId}")]
public async Task<IActionResult> DeletePersonagemHabilidade(int personagemId, int habilidadeId)
{
    try
    {
        var personagemHabilidade = await _context.TB_PERSONAGENS_HABILIDADES
            .FirstOrDefaultAsync(ph => ph.PersonagemId == personagemId && ph.HabilidadeId == habilidadeId);

        if (personagemHabilidade == null)
        {
            return NotFound("Personagem e Habilidade não estão associados.");
        }
        _context.TB_PERSONAGENS_HABILIDADES.Remove(personagemHabilidade);
        int linhasAfetadas = await _context.SaveChangesAsync();

        return Ok($"Relação removida com sucesso: {linhasAfetadas}");
    }
    catch (System.Exception ex)
    {
        return BadRequest(ex.Message);
    }
}

    }
}