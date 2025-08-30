using Exam_CA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> Valid(string _cuenta, string _password);
    }
}
