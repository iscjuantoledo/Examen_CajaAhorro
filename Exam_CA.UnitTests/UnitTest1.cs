using Exam_CA.Domain.Entities;
using Exam_CA.Domain.Interfaces;
using Exam_CA.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exam_CA.UnitTests
{
    public class UnitTest1
    {
        [Fact(DisplayName ="Repository Login")]
        public async Task Test1()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbSeg>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            var dbContext = new DbSeg(optionsBuilder.Options);


            IUsuarioRepository userRepository = new Exam_CA.Infraestructure.Repositories.UsuarioRepository(dbContext);

            Usuario valores = await userRepository.ValidarUsuario("iscjuantoledo@gmail.com","123456789");

            Trace.WriteLine("test trace");
            Debug.WriteLine("test debug");
            Console.WriteLine("test console" + valores.ToString());


            Assert.NotNull(valores);
            

        }

        [Fact(DisplayName = "Cuentas usuario")]
        public async Task Test2()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbSeg>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            var dbContext = new DbSeg(optionsBuilder.Options);


            ICuentaRepository cuentaRepository = new Exam_CA.Infraestructure.Repositories.CuentaRepository(dbContext);

            IEnumerable<Cuentum> valores = await cuentaRepository.GetCuentasByUsuario(1);

            Trace.WriteLine("test trace");
            Debug.WriteLine("test debug");
            Console.WriteLine("test console" + valores.ToString());


            Assert.NotNull(valores);
            Assert.True(valores.Any());

        }
    }
}