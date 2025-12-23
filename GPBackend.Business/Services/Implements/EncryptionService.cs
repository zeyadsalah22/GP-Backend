using System;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace GPBackend.Services.Implements
{
    /// <summary>
    /// Implementation of encryption service using ASP.NET Core Data Protection API.
    /// This provides automatic key management, key rotation, and secure encryption.
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private readonly IDataProtector _protector;

        /// <summary>
        /// Initializes the encryption service with a dedicated data protector for Gmail tokens.
        /// </summary>
        /// <param name="dataProtectionProvider">The data protection provider from DI</param>
        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            // Create a purpose-specific protector for Gmail OAuth tokens
            // This ensures that encrypted data can only be decrypted by this specific purpose
            _protector = dataProtectionProvider.CreateProtector("GPBackend.GmailOAuthTokens");
        }

        /// <summary>
        /// Encrypts plaintext using ASP.NET Core Data Protection API.
        /// </summary>
        /// <param name="plainText">The text to encrypt</param>
        /// <returns>Base64-encoded encrypted string</returns>
        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            try
            {
                // Data Protection API handles all encryption details internally
                return _protector.Protect(plainText);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Encryption failed. Ensure Data Protection is properly configured.", ex);
            }
        }

        /// <summary>
        /// Decrypts ciphertext using ASP.NET Core Data Protection API.
        /// </summary>
        /// <param name="cipherText">The Base64-encoded encrypted string</param>
        /// <returns>Decrypted plaintext</returns>
        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            try
            {
                // Data Protection API handles all decryption details internally
                return _protector.Unprotect(cipherText);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Decryption failed. The data may be corrupted, encrypted with a different key, or the key has been rotated.", 
                    ex);
            }
        }
    }
}

