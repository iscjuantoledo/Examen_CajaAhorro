using Exam_CA.Application.DTOs;
using Exam_CA.Application.Interfaces;
using Exam_CA.Domain.Entities;
using Exam_CA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Application.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

        }

        public async Task<Usuario> Valid(string _cuenta, string _password)
        {            
            Usuario user = await this._usuarioRepository.ValidarUsuario(_cuenta, _password);
            return user;
        }
    }
}
