using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.WebApi.Util
{
    public class ConfiguracionServices : IConfiguracionServices
    {
        private readonly IConfiguration config;
        
        public ConfiguracionServices(IConfiguration _config) { 
            this.config=_config;
            //this._StringConnection = this.config.GetConnectionString("dbevento").ToString();
            //this._StringKey= this.config.GetSection("jwtSettings").GetSection("llave").Value;
            //this._StringSite = this.config.GetSection("jwtSettings").GetSection("sitio").Value;
            //this._expiration = int.Parse(this.config.GetSection("jwtSettings").GetSection("minutosExpiracion").Value);
            //this._minutesepoch = int.Parse(this.config.GetSection("timeSettings").GetSection("minutesepoch").Value);
        }

        public string StringConnection(string name) {           
            return this.config.GetConnectionString(name).ToString();
        }

        public string GetJwtKey(string name) {
            return this.config.GetSection("jwtSettings").GetSection(name).Value;
        }

        public string GetKey(string name)
        {
            return this.config.GetSection("client").GetSection(name).Value;
        }

        public string GetAwsKey(string name)
        {
            return this.config.GetSection("aws").GetSection(name).Value;
        }

    }
}
