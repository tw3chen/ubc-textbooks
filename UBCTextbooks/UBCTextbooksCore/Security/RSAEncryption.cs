using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;

namespace UBCTextbooksCore.Security
{
    public class RSAEncryption
    {
        public static void GenerateKey(string targetFile, string keyTxt)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                //Save the private key
                string key = rsa.ToXmlString(true);
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                //keyBytes = ProtectedData.Protect(keyBytes, null, DataProtectionScope.LocalMachine);

                using (FileStream file = new FileStream(targetFile, FileMode.Create))
                {
                    file.Write(keyBytes, 0, keyBytes.Length);
                }
                using (StreamWriter file = new StreamWriter(keyTxt))
                {
                    file.Write(rsa.ToXmlString(false));
                }
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
        }

        public static byte[] Encrypt(string data, string publicKey)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publicKey);
                return rsa.Encrypt(Encoding.UTF8.GetBytes(data), true);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public static string Decrypt(byte[] data, string keyFile)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                ReadKey(rsa, keyFile);

                byte[] decrypteddata = rsa.Decrypt(data, true);
                return Convert.ToString(Encoding.UTF8.GetString(decrypteddata));
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        private static void ReadKey(RSACryptoServiceProvider rsa, string targetFile)
        {
            try
            {
                byte[] keyBytes;

                using (FileStream file = new FileStream(targetFile, FileMode.Open))
                {
                    keyBytes = new byte[file.Length];
                    file.Read(keyBytes, 0, (int)file.Length);
                }

                //keyBytes = ProtectedData.Unprotect(keyBytes, null, DataProtectionScope.LocalMachine);

                rsa.FromXmlString(Encoding.UTF8.GetString(keyBytes));
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
        }

        public static byte[] EncryptPassword(string password, string keyTxt, string keyFile)
        {
            try
            {
                if (!File.Exists(keyFile))
                {
                    GenerateKey(keyFile, keyTxt);
                }
                string publicKey = String.Empty;
                using (StreamReader file = new StreamReader(keyTxt))
                {
                    publicKey = file.ReadLine();
                }
                byte[] s = Encrypt(password, publicKey);
                string pass = Decrypt(s, keyFile);
                return Encrypt(password, publicKey);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }
    }
}
