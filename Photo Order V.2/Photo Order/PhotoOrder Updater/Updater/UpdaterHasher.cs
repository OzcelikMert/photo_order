using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PhotoOrder_Updater.Updater {
    /// <summary>
    /// The type of the hash to create
    /// </summary>
    internal enum HashType
    {
        MD5,
        SHA1,
        SHA512
    }

    /// <summary>
    /// Class used to generate hash sums of files
    /// </summary>
    internal static class UpdaterHasher
    {
        /// <summary>
        /// Generate a hash of a file
        /// </summary>
        /// <param name="filePath">The file to hash</param>
        /// <param name="algorithm">The type of hash</param>
        /// <returns>The computed hash</returns>
        internal static string HashFile(string filePath, HashType algorithm) {
            switch (algorithm) {
                case HashType.MD5:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.SHA1:
                    return MakeHashString(SHA1.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.SHA512:
                    return MakeHashString(SHA512.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                default:
                    return "";
            }
        }
        /// <summary>
        /// Converts byte[] to string
        /// </summary>
        /// <param name="hash">The hash to convert</param>
        /// <returns>Hash as string</returns>
        private static string MakeHashString(byte[] hash) {
            StringBuilder stringBuilder = new StringBuilder(hash.Length * 2);

            foreach (byte item in hash)
                stringBuilder.Append(item.ToString("X2").ToLower());

            return stringBuilder.ToString();
        }
    }
}
