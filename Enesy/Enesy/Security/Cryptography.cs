using System;
using System.Security.Cryptography;
using System.Text;

/// Source: http://dinhnn.com/2014/09/03/rsa-ta%CC%A3o-key-pairs-d%CC%89-ma%CC%83-hoa-encrypt-va-gia%CC%89i-ma%CC%83-decrypt/
/// Example:
/*
public string GenerateKeys()
{
    RSACryptography RSA = new RSACryptography();
    string publicKey, privateKey;

    // Generate RSA key pair
    RSA.GenerateKeys(out publicKey, out privateKey);

    string plainText = "93f99709-ce56-42a9-af7e-1d72c011c2dd";// Guid.NewGuid().ToString();

    // Encrypt
    string encryptedText = RSA.Encrypt(publicKey, plainText);

    // Decrypt
    string decryptedText = RSA.Decrypt(privateKey, encryptedText);

    return "<b>Token:</b> " + Server.HtmlEncode(plainText) + "<br />" + "<b>Public key:</b> " + Server.HtmlEncode(publicKey) + "<br />" + "<b>Private key:</b> " + Server.HtmlEncode(privateKey) + "<br />" + "<b>Encrypted text:</b> " + Server.HtmlEncode(encryptedText) + "<br />" + "<b>Decrypted text:</b> " + Server.HtmlEncode(decryptedText);
}
*/
namespace Security.Cryptography
{
    /// <summary>
    /// Performs asymmetric encryption and decryption using the implementation of
    /// the System.Security.Cryptography.RSA algorithm provided by the cryptographic
    /// service provider (CSP). This class cannot be inherited.
    /// Reference:
    /// http://jamiekurtz.com/2013/01/14/asp-net-web-api-security-basics/
    /// http://blogs.msdn.com/b/alejacma/archive/2008/10/23/how-to-generate-key-pairs-encrypt-and-decrypt-data-with-net-c.aspx
    /// http://www.technical-recipes.com/2013/using-rsa-to-encrypt-large-data-files-in-c/
    /// http://codebetter.com/johnvpetersen/2012/04/02/making-your-asp-net-web-apis-secure/
    /// </summary>
    public sealed class RSACryptography
    {

        #region Private Fields

        private const int KEY_SIZE = 2048; // The size of the RSA key to use in bits.
        private bool fOAEP = false;
        private RSACryptoServiceProvider rsaProvider = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the System.Security.Cryptography.RSACryptoServiceProvider
        /// class with the predefined key size and parameters.
        /// </summary>
        public RSACryptography()
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes a new instance of the System.Security.Cryptography.CspParameters class.
        /// </summary>
        /// <returns>An instance of the System.Security.Cryptography.CspParameters class.</returns>
        private CspParameters GetCspParameters()
        {
            // Create a new key pair on target CSP
            CspParameters cspParams = new CspParameters();
            cspParams.ProviderType = 1; // PROV_RSA_FULL 
            // cspParams.ProviderName; // CSP name
            // cspParams.Flags = CspProviderFlags.UseArchivableKey;
            cspParams.KeyNumber = (int)KeyNumber.Exchange;

            return cspParams;
        }

        /// <summary>  
        /// Gets the maximum data length for a given key  
        /// </summary>         
        /// <param name="keySize">The RSA key length  
        /// <returns>The maximum allowable data length</returns>  
        public int GetMaxDataLength()
        {
            if (fOAEP)
                return ((KEY_SIZE - 384) / 8) + 7;
            return ((KEY_SIZE - 384) / 8) + 37;
        }

        /// <summary>  
        /// Checks if the given key size if valid  
        /// </summary>         
        /// <param name="keySize">The RSA key length  
        /// <returns>True if valid; false otherwise</returns>  
        public static bool IsKeySizeValid()
        {
            return KEY_SIZE >= 384 &&
                   KEY_SIZE <= 16384 &&
                   KEY_SIZE % 8 == 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generate a new RSA key pair.
        /// </summary>
        /// <param name="publicKey">An XML string containing ONLY THE PUBLIC RSA KEY.</param>
        /// <param name="privateKey">An XML string containing a PUBLIC AND PRIVATE RSA KEY.</param>
        public void GenerateKeys(out string publicKey, out string privateKey)
        {
            try
            {
                CspParameters cspParams = GetCspParameters();
                cspParams.Flags = CspProviderFlags.UseArchivableKey;

                rsaProvider = new RSACryptoServiceProvider(KEY_SIZE, cspParams);

                // Export public key
                publicKey = rsaProvider.ToXmlString(false);

                // Export private/public key pair 
                privateKey = rsaProvider.ToXmlString(true);
            }
            catch (Exception ex)
            {
                // Any errors? Show them
                throw new Exception("Exception generating a new RSA key pair! More info: " + ex.Message);
            }
            finally
            {
                // Do some clean up if needed
            }

        } // GenerateKeys method

        /// <summary>
        /// Encrypts data with the System.Security.Cryptography.RSA algorithm.
        /// </summary>
        /// <param name="publicKey">An XML string containing the public RSA key.</param>
        /// <param name="plainText">The data to be encrypted.</param>
        /// <returns>The encrypted data.</returns>
        public string Encrypt(string publicKey, string plainText)
        {
            if ((plainText == null) || (plainText.Trim() == ""))
                throw new ArgumentException("Data are empty");

            int maxLength = GetMaxDataLength();
            if (Encoding.Unicode.GetBytes(plainText).Length > maxLength)
                throw new ArgumentException("Maximum data length is " + maxLength / 2);

            if (!IsKeySizeValid())
                throw new ArgumentException("Key size is not valid");

            if ((publicKey == null) || (publicKey.Trim() == ""))
                throw new ArgumentException("Key is null or empty");

            byte[] plainBytes = null;
            byte[] encryptedBytes = null;
            string encryptedText = "";

            try
            {
                CspParameters cspParams = GetCspParameters();
                cspParams.Flags = CspProviderFlags.NoFlags;

                rsaProvider = new RSACryptoServiceProvider(KEY_SIZE, cspParams);

                // [1] Import public key
                rsaProvider.FromXmlString(publicKey);

                // [2] Get plain bytes from plain text
                plainBytes = Encoding.Unicode.GetBytes(plainText);

                // Encrypt plain bytes
                encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

                // Get encrypted text from encrypted bytes
                // encryptedText = Encoding.Unicode.GetString(encryptedBytes); => NOT WORKING
                encryptedText = Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception ex)
            {
                // Any errors? Show them
                throw new Exception("Exception encrypting file! More info: " + ex.Message);
            }
            finally
            {
                // Do some clean up if needed
            }

            return encryptedText;

        } // Encrypt method

        /// <summary>
        /// Decrypts data with the System.Security.Cryptography.RSA algorithm.
        /// </summary>
        /// <param name="privateKey">An XML string containing a public and private RSA key.</param>
        /// <param name="encryptedText">The data to be decrypted.</param>
        /// <returns>The decrypted data, which is the original plain text before encryption.</returns>
        public string Decrypt(string privateKey, string encryptedText)
        {
            if ((encryptedText == null) || (encryptedText.Trim() == ""))
                throw new ArgumentException("Data are empty");

            if (!IsKeySizeValid())
                throw new ArgumentException("Key size is not valid");

            if ((privateKey == null) || (privateKey.Trim() == ""))
                throw new ArgumentException("Key is null or empty");

            byte[] encryptedBytes = null;
            byte[] plainBytes = null;
            string plainText = "";

            try
            {
                CspParameters cspParams = GetCspParameters();
                cspParams.Flags = CspProviderFlags.NoFlags;

                rsaProvider = new RSACryptoServiceProvider(KEY_SIZE, cspParams);

                // [1] Import private/public key pair
                rsaProvider.FromXmlString(privateKey);

                // [2] Get encrypted bytes from encrypted text
                // encryptedBytes = Encoding.Unicode.GetBytes(encryptedText); => NOT WORKING
                encryptedBytes = Convert.FromBase64String(encryptedText);

                // Decrypt encrypted bytes
                plainBytes = rsaProvider.Decrypt(encryptedBytes, false);

                // Get decrypted text from decrypted bytes
                plainText = Encoding.Unicode.GetString(plainBytes);
            }
            catch (Exception ex)
            {
                // Any errors? Show them
                throw new Exception("Exception decrypting file! More info: " + ex.Message);
            }
            finally
            {
                // Do some clean up if needed
            }

            return plainText;

        } // Decrypt method

        #endregion

    }
}