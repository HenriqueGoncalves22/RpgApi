using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamingAPI.Models;
using GamingAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Controllers;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        public UsuariosController(DataContext context)
        {
            _context = context;
        }
        private async Task<bool> UsuarioExistente(string username)
        {
            if (await _context.TB_USUARIOS.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(Usuario user)
        {
            try
            {
                if (await UsuarioExistente(user.Username))
                    throw new System.Exception("Nome de usuário já existe");

                Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
                user.PasswordString = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                await _context.TB_USUARIOS.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

// (1) Método AlterarSenha
[HttpPut("AlterarSenha")]
public async Task<IActionResult> AlterarSenhaUsuario(Usuario credenciais)
{
      try
         {
             Usuario? usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do login
                .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

             if (usuario == null) //Se não achar nenhum usuário pelo login, retorna mensagem.
                 throw new System.Exception("Usuário não encontrado.");

             Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
            usuario.PasswordHash = hash; //Se o usuário existir, executa a criptografia 
            usuario.PasswordSalt = salt; //guardando o hash e o salt nas propriedades do usuário 

             _context.TB_USUARIOS.Update(usuario);
              int linhasAfetadas = await _context.SaveChangesAsync(); //Confirma a alteração no banco
              return Ok(linhasAfetadas); //Retorna as linhas afetadas (Geralmente sempre 1 linha msm)
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        
}

// (2) Método GetAllUsers
[HttpGet("GetAllUsers")]
public async Task<IActionResult> GetAll()
{
    
   try
     {
         List<Usuario> lista = await _context.TB_USUARIOS.ToListAsync();
         return Ok(lista);
   }
  catch (System.Exception ex)
      {
         return BadRequest(ex.Message + " - " + ex.InnerException);
     }
}

// (3) AutenticarUsuario
[HttpPost("AutenticarUsuario")]
public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
{
    try
    {
        var usuario = await _context.TB_USUARIOS
           .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

        if (usuario == null)
        {
            throw new System.Exception("Usuário não encontrado.");
        }
        else if (!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
        {
            throw new System.Exception("Senha incorreta.");
        }
        else
        {
            usuario.DataAcesso = DateTime.Now;
            _context.TB_USUARIOS.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }
    }
    catch (System.Exception ex)
    {
        return BadRequest(ex.Message + " - " + ex.InnerException?.Message);
    }
}

// (4) Método GetByNome
[HttpGet("{id}")]
public async Task<IActionResult> GetSingle (int id)
{
    try
    {
       Personagem? p = await _context.TB_PERSONAGENS
                .Include(ar => ar.Arma)
                .Include(us => us.Usuario)
                .Include(p => p.PersonagemHabilidade)
                    .ThenInclude(ps => ps.Habilidade)
                .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
   }
   catch (System.Exception ex)
     {
     return BadRequest(ex.Message);
     }

}
}
}