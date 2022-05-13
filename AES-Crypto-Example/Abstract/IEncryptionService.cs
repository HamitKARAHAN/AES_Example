using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AES_Crypto_Example.Abstract
{
    public interface IEncryptionService
    {
        public string Encrypt(string value, string password);

        public string Encrypt<T>(string value, string password) where T : SymmetricAlgorithm, new();

        public string Decrypt(string value, string password);

        public string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new();
    }
}
