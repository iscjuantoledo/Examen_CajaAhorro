using Exam_CA.Application.DTOs;
using Exam_CA.WebApi.Util;
using FluentValidation;

namespace Exam_CA.WebApi.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        List<string> conditions = new List<string>() { "refresh_token", "client_credentials", "password" };
        public readonly IConfiguracionServices configuration;
        public readonly IEncriptServices encript;
        public LoginDtoValidator(IConfiguracionServices _configuration, IEncriptServices _encript)
        {
            configuration = _configuration;
            encript = _encript;
            RuleFor(x => x.login)
                .Must(ValidateHasLogin).WithMessage("El login es requerido");            
            RuleFor(x => x.password)
                .Must(ValidateHasPassword).WithMessage("El password es requerido");
            RuleFor(l => l.timestamp).NotNull();
            RuleFor(l => l.login).EmailAddress().WithMessage("El login debe ser con el formato de email");
            RuleFor(x => x.grant_type)
                .NotEmpty().WithMessage("grant_type no puede estar vacio")
                .NotNull().WithMessage("grant_type no puede ser null")
                .Must(x => conditions.Contains(x))
                .WithMessage("Por favor use solo: " + String.Join(",", conditions));
            RuleFor(x => x.clientid)
                .NotEmpty().WithMessage("clientid no puede estar vacio")
                .NotNull().WithMessage("clientid no puede ser nulo")
                .Must(ValidateCliente).WithMessage("clientid no es valido");
            RuleFor(x => x.clientsecret)
                .NotEmpty().WithMessage("secretkey no puede estar vacio")
                .NotNull().WithMessage("secretkey no puede ser null")
                .Must(ValidateClientSecret).WithMessage("clientid not is valid");
            RuleFor(x => x.signature)
                .NotEmpty().WithMessage("signature no puede estar vacio")
                .NotNull().WithMessage("signature no puede ser null")
                .Must(ValidateSignature).WithMessage("signature not is valid");
        }

        private bool ValidateHasLogin(LoginDto auth, string login)
        {
            try
            {
                if (auth.grant_type == "password")
                {
                    if (login != null)
                        return true;
                    else
                        return false;
                }
                else
                    return true;

            }
            catch (Exception e)
            {                
                return false;
            }
        }
        private bool ValidateCliente(LoginDto auth, string clientid)
        {
            try
            {
                string _clientid = configuration.GetKey("clientid"); 
                if(_clientid == clientid)    
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {               
                return false;
            }
        }
        private bool ValidateClientSecret(LoginDto auth, string clientsecret)
        {
            try
            {
                string _clientsecret = configuration.GetKey("clientsecret");
                if (_clientsecret == clientsecret)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {                
                return false;
            }

        }
        private bool ValidateTimeStamp(LoginDto auth, double timestamp)
        {
            try
            {
                double unixTimestamp = (double)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                int minutes = int.Parse(configuration.GetKey("minutesepoch"));
                if ((unixTimestamp - timestamp) <= minutes)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {               
                return false;
            }

        }
        private bool ValidateSignature(LoginDto auth, string signature)
        {
            try
            {
                string firma = auth.clientid + auth.clientsecret + auth.timestamp;
                string result = this.encript.GetSHA256(firma);

                if (result.ToUpper() == signature.ToUpper())
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {               
                return false;
            }

        }
        private bool ValidateHasPassword(LoginDto auth, string password)
        {
            try
            {
                if (auth.grant_type == "password")
                {
                    if (password != null)
                        return true;
                    else
                        return false;
                }
                else
                    return true;

            }
            catch (Exception e)
            {                
                return false;
            }
        }
    }
}
