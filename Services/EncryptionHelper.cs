using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHelper
{
    private static readonly string EncryptionKey = "your-encryption-key";  // Must be hashed to ensure it's the right length

    // Create a method to get the key as 32 bytes using SHA-256
    private static byte[] GetHashedKey()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey));
        }
    }

    public static string Encrypt(string text)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = GetHashedKey();  // Use the 32-byte key generated from SHA-256
            aesAlg.IV = aesAlg.Key.Take(16).ToArray();  // Use the first 16 bytes of the key as the IV

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(text);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = GetHashedKey();  // Use the 32-byte key
            aesAlg.IV = aesAlg.Key.Take(16).ToArray();

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
