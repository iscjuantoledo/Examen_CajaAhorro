using Exam_CA.Domain.Entities;
using Exam_CA.Domain.Interfaces;
using Exam_CA.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;


//using Exam_CA.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Infraestructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbSeg _context;
        public UsuarioRepository(DbSeg context)
        {
            _context = context;
        }
        async Task<Usuario> IUsuarioRepository.ValidarUsuario(string usuario, string password)
        {
            try { 
                return await _context.Usuarios.Where(u => u.Usuario1 == usuario && u.Password == password && u.Activo==true).FirstOrDefaultAsync();
            }
            catch(Exception ex) 
            {
                throw new Exception("Error al validar el usuario.", ex);
                return null;
            }
        }
    }
}
