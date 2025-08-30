using Exam_CA.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Application.Interfaces
{
    public interface ICuentaService
    {
        Task<IEnumerable<CuentaDto>> GetAllAsync(int idusuario);
    }
}
