using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.WebApi.Util
{
    public interface IEncriptServices
    {
        byte[] stringToBytes(string key);
        string GetSHA256(string str);
        string GetSHA256(string _pwd, string _key, int _iteration);
        string GeneraHashMD5(string cadena);
        string Encrypt_AES(string _phrase, string _key);
        string Decrypt_AES(string _phrase, string _key);
    }
}
