using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CA.WebApi.Util
{
    public class EncriptServices : IEncriptServices
    {
        public byte[] stringToBytes(string key) {

            byte[] plainbits;
            plainbits = System.Text.Encoding.Unicode.GetBytes(key.ToCharArray());
            byte[] resultad = System.Security.Cryptography.SHA256.Create().ComputeHash(plainbits);
            return resultad;

        }
        [Obsolete("Metodo deprecado, hace uso de una libreria obsoleta")]
        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        [Obsolete("Metodo deprecado, hace uso de una libreria obsoleta")]
        public string GetSHA256(string _pwd, string _key, int _iteration)
        {
            string _pwdKey = _pwd + _key;
            SHA256Managed _objSha256 = new SHA256Managed();
            byte[] stream = null;
            try
            {
                stream = Encoding.UTF8.GetBytes(_pwdKey);
                for (int i = 0; i <= _iteration - 1; i++)
                    stream = _objSha256.ComputeHash(stream);
            }
            finally { _objSha256.Clear(); }
            return Convert.ToBase64String(stream);
        }        
        public string GeneraHashMD5(string cadena)
        {
            MD5 md5;            
            byte[] data;
            StringBuilder sBuilder;
            
            md5 = MD5.Create();

            data = md5.ComputeHash(Encoding.UTF8.GetBytes(cadena));
            sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }
        public string Encrypt_AES(string _phrase, string _key) {
            try {
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(_phrase);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(_key);

                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesEncrypted = this.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

                string encryptedResult = Convert.ToBase64String(bytesEncrypted);
                return encryptedResult;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public string Decrypt_AES(string _phrase, string _key) {
            try {

                byte[] bytesToBeDecrypted = Convert.FromBase64String(_phrase);
                byte[] passwordBytesdecrypt = Encoding.UTF8.GetBytes(_key);
                passwordBytesdecrypt = SHA256.Create().ComputeHash(passwordBytesdecrypt);

                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytesdecrypt);

                string decryptedResult = Encoding.UTF8.GetString(bytesDecrypted);
                return decryptedResult;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }        
    }
}
