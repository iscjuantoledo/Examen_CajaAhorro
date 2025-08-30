using Exam_CA.Domain.Entities;
using Exam_CA.Domain.Interfaces;
using Exam_CA.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Infraestructure.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly DbSeg _context;
        public CuentaRepository(DbSeg context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Cuentum>> GetCuentasByUsuario(int idusuario)
        {
            try {
                return await _context.Cuenta.Where(c => c.Idusuario == idusuario).ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al obtener las cuentas del usuario.", ex);
                return null;
            }   
        }
    }
}
