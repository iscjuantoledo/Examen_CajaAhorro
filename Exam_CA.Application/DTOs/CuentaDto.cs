using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Application.DTOs
{
    public record CuentaDto(int idcuenta, string numero, string tipo, decimal? saldo);
}
