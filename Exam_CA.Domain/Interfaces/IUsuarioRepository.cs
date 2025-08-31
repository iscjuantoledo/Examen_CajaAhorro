using Exam_CA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ValidarUsuario(string usuario, string password);
        Task<bool> SaveDevice(int idusuario, string device, string platform);

    }
}
