using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.WebApi.Util
{
    public interface IConfiguracionServices
    {
        string StringConnection(string name);
        string GetJwtKey(string name);
        string GetKey(string name);
        string GetAwsKey(string name);
    }
}
