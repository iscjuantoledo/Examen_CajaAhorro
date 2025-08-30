using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Application.DTOs
{
    public record LoginDto(string login, string password);
    public class UsuarioDto {
        public int idusuario { get; set; }
        public string cuenta { get; set; } 
        public string nombre { get; set; }  
        public string token { get; set; }
        public UsuarioDto(int _idusuario, string _cuenta, string _nombre, string _token)
        {
            this.idusuario = _idusuario;
            this.cuenta = _cuenta;
            this.nombre = _nombre;
            this.token = _token;
        }
    }
}
