using Exam_CA.Domain.Entities;
using Exam_CA.Domain.Interfaces;
using Exam_CA.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

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

        async Task<bool> IUsuarioRepository.SaveDevice(int idusuario, string device, string platform)
        {
            try
            {
                var udevice = await _context.UsuarioDevices.Where(u => u.Idusuario == idusuario && u.Ultimo==true).FirstOrDefaultAsync();
                if (udevice != null)
                {
                    udevice.Ultimo = false;
                    _context.UsuarioDevices.Update(udevice);
                    //await _context.SaveChangesAsync();
                    
                }
                UsuarioDevice nuevodevice = new UsuarioDevice();
                nuevodevice.Idusuario = idusuario;
                nuevodevice.Fecha = DateTime.Now;
                nuevodevice.Device = device;   
                nuevodevice.Platform = platform;
                nuevodevice.Ultimo = true;
                _context.UsuarioDevices.Add(nuevodevice);

                int rowsafected= await _context.SaveChangesAsync();
                if (rowsafected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el dispositivo.", ex);
                return false;
            }
        }
    }
}
