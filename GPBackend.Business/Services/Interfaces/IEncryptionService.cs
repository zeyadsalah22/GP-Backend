namespace GPBackend.Services.Interfaces
{
    /// <summary>
    /// Service for encrypting and decrypting sensitive data like OAuth tokens
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts plain text using AES encryption
        /// </summary>
        /// <param name="plainText">The text to encrypt</param>
        /// <returns>Base64 encoded encrypted string</returns>
        string Encrypt(string plainText);

        /// <summary>
        /// Decrypts cipher text using AES encryption
        /// </summary>
        /// <param name="cipherText">Base64 encoded encrypted string</param>
        /// <returns>Decrypted plain text</returns>
        string Decrypt(string cipherText);
    }
}

