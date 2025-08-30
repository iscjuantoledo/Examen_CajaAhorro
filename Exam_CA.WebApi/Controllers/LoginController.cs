using Exam_CA.Application.DTOs;
using Exam_CA.Application.Interfaces;
using Exam_CA.Domain.Entities;
using Exam_CA.WebApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
     
        public IConfiguration configuration;
        public IUsuarioService _usuarioservice;
        public LoginController(IConfiguration _configuration, IUsuarioService usuarioservice  )
        {
            configuration = _configuration;
            _usuarioservice = usuarioservice;
        }

        [NonAction]
        private String CreateJwtTokenAsync(Usuario usuario)
        {
            var claimsdata = new[]
                    {
                       new Claim(CustomClaims.IdUsuario, usuario.Idusuario.ToString()),
                       new Claim(CustomClaims.Correo_electronico,usuario.Usuario1),
                       new Claim(CustomClaims.Nombre_pantalla,usuario.Nombre)
                    };

            string _key = configuration.GetSection("jwtSettings").GetSection("key").Value;
            string _site = configuration.GetSection("jwtSettings").GetSection("site").Value;
            int _minuteToExpire = int.Parse(configuration.GetSection("jwtSettings").GetSection("minutesToExpiration").Value);            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _site,
                audience: _site,
                //expires: DateTime.Now.AddHours(12),
                expires: DateTime.Now.AddMinutes(_minuteToExpire),
                claims: claimsdata,
                signingCredentials: signInCred
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;

        }

        [HttpPost()]
        public async Task<ActionResult<UsuarioDto>> GetToken([FromBody] LoginDto login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LoginDtoValidator validator = new LoginDtoValidator();
                    var result = validator.Validate(login);
                    if (!result.IsValid)
                    {
                        return BadRequest("Los valores que ingreso son incorrectos");
                    }

                    //if (login == null)
                    //    return BadRequest("Login resource must be asssigned");

                    Usuario usuario = await _usuarioservice.Valid(login.login, login.password);
                    if (usuario == null)
                        return BadRequest("Invalid credentials");

                    var token = CreateJwtTokenAsync(usuario);
                                     
                    UsuarioDto usuarioDto = new UsuarioDto(usuario.Idusuario, usuario.Usuario1, usuario.Nombre, token);

                    return Ok(usuarioDto);
                }
                catch
                {
                    return BadRequest("Internal Server Error");
                }
            }
            else
            {
                return BadRequest("Bad request");
            }

        }
    }
}
