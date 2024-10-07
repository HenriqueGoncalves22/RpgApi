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
[HttpPut("NovaSenha")]
public async Task<IActionResult> AlterarSenha(Usuario user)
{
    try
    {
        Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
        user.PasswordString = string.Empty; 
        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        var usuarioExistente = await _context.TB_USUARIOS
            .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(user.Username.ToLower()));

        if (usuarioExistente == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        usuarioExistente.PasswordHash = hash;
        usuarioExistente.PasswordSalt = salt;
        
        await _context.SaveChangesAsync();

        return Ok("Senha alterada com sucesso!.");
    }
    catch (System.Exception ex)
    {
        return BadRequest(ex.Message + " - " + ex.InnerException?.Message);
    }
}

// (2) Método GetAllUsers
[HttpGet("GetAllUsers")]
public async Task<IActionResult> GetAll()
{
    var usuarios = await _context.TB_USUARIOS.ToListAsync();
    return Ok(usuarios);
}

// (3) AutenticarUsuario
[HttpPost("Autenticar")]
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
[HttpGet("GetByNome/{nome}")]
public async Task<IActionResult> GetByNome(string nome)
{
    var personagens = await _context.TB_PERSONAGENS
        .Include(p => p.Usuario) 
        .Where(p => p.Nome.Contains(nome))
        .ToListAsync();

    if (personagens == null || personagens.Count == 0)
        return NotFound("Nenhum personagem encontrado.");

    return Ok(personagens);
}
}
}