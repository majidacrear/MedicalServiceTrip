using Microsoft.AspNetCore.Http;
using System;

namespace Core
{
    public partial interface IWebHelper
    {
        /// <summary> 
        /// Encrypt text by key 
        /// </summary> 
        /// <param name="value">plain text</param> 
        /// <returns>encrypted text</returns> 
        string Encrypt(string value);

        /// <summary> 
        /// Encrypt text by key 
        /// </summary> 
        /// <param name="value">plain text</param> 
        /// <param name="key"> string key</param> 
        /// <returns>encrypted text</returns> 
        string Encrypt(string value, string key);

        /// <summary> 
        /// Encrypt text by key with initialization vector 
        /// </summary> 
        /// <param name="value">plain text</param> 
        /// <param name="key"> string key</param> 
        /// <param name="iv">initialization vector</param> 
        /// <returns>encrypted text</returns> 
        string Encrypt(string value, string key, string iv);

        /// <summary> 
        /// Decrypt text 
        /// </summary> 
        /// <param name="value">encrypted text</param> 
        /// <returns>plain text</returns> 
        string Decrypt(string value);

        /// <summary> 
        /// Decrypt text by key 
        /// </summary> 
        /// <param name="value">encrypted text</param> 
        /// <param name="key">string key</param> 
        /// <returns>plain text</returns> 
        string Decrypt(string value, string key);

        /// <summary> 
        /// Decrypt text by key with initialization vector 
        /// </summary> 
        /// <param name="value">encrypted text</param> 
        /// <param name="key"> string key</param> 
        /// <param name="iv">initialization vector</param> 
        /// <returns>encrypted text</returns> 
        string Decrypt(string value, string key, string iv);

        /// <summary> 
        /// Compute Hash 
        /// </summary> 
        /// <param name="plainText">plain text</param> 
        /// <param name="salt">salt string</param> 
        /// <returns>string</returns> 
        string ComputeHash(string plainText, string salt);

        /// <summary> 
        /// Compute Hash 
        /// </summary> 
        /// <param name="plainText">plain text</param> 
        /// <param name="salt">salt string</param> 
        /// <param name="hashName">Hash Name</param> 
        /// <returns>string</returns> 
        string ComputeHash(string plainText, string salt, HashName hashName);

        /// <summary>
        /// Get Random String
        /// </summary>
        /// <param name="size">salt size</param>
        /// <returns>return random salt string</returns>
        string RandomString(int size);

        int RandomStringSize { get; }

        string GetFromEmail();

        string GetSendGridApiKey();
    }
}
