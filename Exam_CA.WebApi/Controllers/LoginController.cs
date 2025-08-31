using Exam_CA.Application.DTOs;
using Exam_CA.Application.Interfaces;
using Exam_CA.Domain.Entities;
using Exam_CA.WebApi.Util;
using Exam_CA.WebApi.Validators;
using FluentValidation;
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
     
        public readonly IConfiguracionServices configuration;
        public IUsuarioService _usuarioservice;
        IValidator<LoginDto> _validator;
        public LoginController(IConfiguracionServices _configuration, IUsuarioService usuarioservice , IValidator<LoginDto> validator)
        {
            configuration = _configuration;
            _usuarioservice = usuarioservice;
            _validator = validator;
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

            string _key = configuration.GetJwtKey("key");
            string _site = configuration.GetJwtKey("site");
            int _minuteToExpire = int.Parse(configuration.GetJwtKey("minutesToExpiration"));            

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
                UsuarioDto usuarioDto=null;
                bool error = false;
                try
                {
                   
                    var result = this._validator.Validate(login);
                    if (!result.IsValid)
                    {
                        return BadRequest("Los valores que ingreso son incorrectos");
                    }

                    switch (login.grant_type) {
                        case "password":
                            Usuario usuario = await _usuarioservice.Valid(login.login, login.password);
                            if (usuario == null)
                            {
                                error = false;
                                break;
                            }

                            var token = CreateJwtTokenAsync(usuario);

                            /*save device*/

                            bool res = await _usuarioservice.SaveDevice(usuario.Idusuario, login.device, login.platform);

                            if (res)
                            {
                                usuarioDto = new UsuarioDto(usuario.Idusuario, usuario.Usuario1, usuario.Nombre, token);
                                error = true;                                
                            }
                            else                            
                                error = false;                                                            
                            break;
                        case "client_credentials":
                            Usuario utoken = new Usuario();
                            utoken.Idusuario = 0;
                            utoken.Usuario1 = "Client credential";
                            utoken.Nombre = "****";
                            var tokenc = CreateJwtTokenAsync(utoken);
                            usuarioDto = new UsuarioDto(utoken.Idusuario, utoken.Usuario1, utoken.Nombre, tokenc);
                            error = true;
                            break;
                        default:
                            error = false;
                            break;
                    }


                    if (error)                    
                        return Ok(usuarioDto);                    
                    else
                        return BadRequest("Credenciales incorrectas");
                    
                    
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
