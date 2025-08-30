using System;
using System.Collections.Generic;

namespace Exam_CA.Domain.Entities;

public partial class Cuentum
{
    public int Idcuenta { get; set; }

    public int? Idusuario { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Numero { get; set; }

    public int? Idtipo { get; set; }

    public decimal? Saldo { get; set; }

    public bool? Estatus { get; set; }

    public virtual Tipo? IdtipoNavigation { get; set; }

    public virtual Usuario? IdusuarioNavigation { get; set; }
}
