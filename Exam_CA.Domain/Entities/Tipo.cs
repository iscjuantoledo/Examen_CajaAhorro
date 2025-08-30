using System;
using System.Collections.Generic;

namespace Exam_CA.Domain.Entities;

public partial class Tipo
{
    public int Idtipo { get; set; }

    public string? DescTipo { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
