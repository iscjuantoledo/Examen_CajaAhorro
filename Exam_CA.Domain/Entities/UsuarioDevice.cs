using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.Domain.Entities;


public partial class UsuarioDevice
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Iddevice { get; set; }
    public int? Idusuario { get; set; }
    public DateTime? Fecha { get; set; }
    public string? Device { get; set; }
    public string? Platform { get; set; }    
    public bool? Ultimo { get; set; }
    
}
