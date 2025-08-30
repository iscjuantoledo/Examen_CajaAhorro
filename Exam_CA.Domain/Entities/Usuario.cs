using System;
using System.Collections.Generic;

namespace Exam_CA.Domain.Entities;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Usuario1 { get; set; }

    public string? Password { get; set; }

    public bool? Activo { get; set; }
    public string? Nombre { get; set; } 

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
