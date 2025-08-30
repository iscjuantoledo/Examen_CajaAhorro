using Exam_CA.Application.DTOs;
using Exam_CA.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Exam_CA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ICuentaService _cuentaService;
        public AccountController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet("{idusuario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> GetAll(int idusuario)
        {
            var header = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);         

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(header.Parameter))
                return BadRequest(null);

            if (!isTokenActive(header.Parameter))
                return Unauthorized();

            var cuentas = await _cuentaService.GetAllAsync(idusuario);

            if (cuentas != null && cuentas.Count() > 0)
                return Ok(cuentas);
            else
            {
                return NoContent();
            }
        }

        [HttpGet("Token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> GetUserAll()
        {
            var header = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(header.Parameter))
                return BadRequest(null);

            if (!isTokenActive(header.Parameter))
                return Unauthorized();

            var jwtToken = handler.ReadJwtToken(header.Parameter);

            int idusuario = this.TokenValue(jwtToken,"IdUsuario");

            if (idusuario > 0)
            {
                var cuentas = await _cuentaService.GetAllAsync(idusuario);

                if (cuentas != null && cuentas.Count() > 0)
                    return Ok(cuentas);
                else
                {
                    return NoContent();
                }
            }
            else {
                return BadRequest();
            }
            
        }

        [NonAction]
        private bool isTokenActive(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                if (handler.CanReadToken(token))
                {
                    var infoToken = handler.ReadJwtToken(token);
                    if (DateTime.UtcNow > Convert.ToDateTime(infoToken.ValidTo).ToUniversalTime())
                        throw new Exception("Token ha caducado");
                    else
                        return true;
                }
                else
                    throw new Exception("Formato de token no válido");
            }
            catch (Exception e)
            {
                return false;
            }

        }
        [NonAction]
        private int TokenValue(JwtSecurityToken jwtToken,string value)
        {
            if (jwtToken.Claims.Count() == 0)
                throw new Exception(null);

            try
            {                
                return int.Parse(jwtToken.Claims.SingleOrDefault(claim => claim.Type == value).Value);                              
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
