using Exam_CA.Application.DTOs;
using Exam_CA.Application.Interfaces;
using Exam_CA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Application.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _cuentaRepository;

        public CuentaService(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        public async Task<IEnumerable<CuentaDto>> GetAllAsync(int idusuario)
        {
            try {
                var cuentas = await _cuentaRepository.GetCuentasByUsuario(idusuario);
                if (cuentas != null) {
                    return cuentas.Select(p => new CuentaDto(p.Idcuenta, p.Numero, p.IdtipoNavigation.DescTipo.ToString(), p.Saldo));
                }
                else {
                    return null;
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener las cuentas del usuario.", ex);
                return null;
            }

        }
    }
}
