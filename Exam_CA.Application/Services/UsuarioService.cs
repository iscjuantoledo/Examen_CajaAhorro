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

        public async Task<bool> SaveDevice(int _idusuario, string _device, string _platform) {
            return await this._usuarioRepository.SaveDevice(_idusuario,_device,_platform);
        }
    }
}
