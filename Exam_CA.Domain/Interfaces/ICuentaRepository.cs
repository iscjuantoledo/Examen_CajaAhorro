//using Exam_CA.Domain.Entities;
using Exam_CA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Domain.Interfaces
{
    public interface ICuentaRepository
    {

        Task<IEnumerable<Cuentum>> GetCuentasByUsuario(int idusuario);
    }
}
