using System;
using System.Security.Cryptography;
using System.Text;
using Dev.Extensions;
using Dev.Properties;

namespace Dev.Security
{
    /// <summary>
    ///     结合RSA，DES的通信加密解密操作类
    /// </summary>
    public class SecurityHelper
    {
        private static readonly string Separator = Convert.ToBase64String(Encoding.UTF8.GetBytes("#@|JohnYang.Net|@#"));

        /// <summary>
        ///     解密接收到的加密数据并验证完整性，如果验证通过返回明文
        /// </summary>
        /// <param name="data">接收到的加密数据</param>
        /// <param name="privateKey">接收方私钥</param>
        /// <param name="hashType">摘要哈希方式，值必须为MD5或SHA1</param>
        /// <param name="publicKey">发送方公钥</param>
        /// <returns>解密并验证成功后，返回明文</returns>
        public static string DecryptAndVerifyData(string data, string privateKey, string publicKey, string hashType)
        {
            data.CheckNotNullOrEmpty("data");
            privateKey.CheckNotNullOrEmpty("privateKey");
            hashType.CheckNotNullOrEmpty("hashType");
            hashType = hashType.ToUpper();
            hashType.Required(str => hashType == "MD5" || hashType == "SHA1", Resources.Security_RSA_Sign_HashType);

            string[] separators = {Separator};
            //0为DES密钥密文，1为 正文+摘要 的密文
            string[] datas = data.Split(separators, StringSplitOptions.None);
            //用接收端私钥RSA解密获取DES密钥
            byte[] desKey = RsaHelper.Decrypt(Convert.FromBase64String(datas[0]), privateKey);
            //DES解密获取 正文+摘要 的明文
            data = new DesHelper(desKey).Decrypt(datas[1]);
            //0为正文明文，1为摘要
            datas = data.Split(separators, StringSplitOptions.None);
            data = datas[0];
            if (RsaHelper.VerifyData(data, datas[1], hashType, publicKey))
            {
                return data;
            }
            return null;
        }

        /// <summary>
        ///     加密要发送的数据，包含签名，DES加密，RSA加密DES密钥等步骤
        /// </summary>
        /// <param name="data">要加密的正文明文数据</param>
        /// <param name="privateKey">发送方私钥</param>
        /// <param name="publicKey">接收方公钥</param>
        /// <param name="hashType">摘要哈希方式，值必须为MD5或SHA1</param>
        /// <returns>已加密待发送的密文</returns>
        public static string EncryptData(string data, string privateKey, string publicKey, string hashType)
        {
            data.CheckNotNull("data");
            privateKey.CheckNotNullOrEmpty("privateKey");
            publicKey.CheckNotNullOrEmpty("publicKey");
            hashType.CheckNotNullOrEmpty("hashType");
            hashType = hashType.ToUpper();
            hashType.Required(str => hashType == "MD5" || hashType == "SHA1", Resources.Security_RSA_Sign_HashType);
            //获取正文摘要
            string signData = RsaHelper.SignData(data, hashType, privateKey);
            data = new[] {data, signData}.ExpandAndToString(Separator);
            //使用DES加密 正文+摘要
            var des = new DesHelper();
            data = des.Encrypt(data);
            //RSA加密DES密钥
            string enDesKey = Convert.ToBase64String(RsaHelper.Encrypt(des.Key, publicKey));
            return new[] {enDesKey, data}.ExpandAndToString(Separator);
        }

        /// <summary>
        ///     校验单向加密
        /// </summary>
        /// <param name="data">未加密前</param>
        /// <param name="salt">加密盐</param>
        /// <param name="encryptedData">加密后</param>
        /// <returns>校验结果</returns>
        public static bool Check(string data, string salt, string encryptedData)
        {
            return String.CompareOrdinal(Encrypting(data, salt), encryptedData.ToUpper()) == 0;
        }

        /// <summary>
        ///     单向加密
        /// </summary>
        /// <param name="payload">需加密字符串</param>
        /// <param name="salt">加密盐</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypting(string payload, string salt)
        {
            HashAlgorithm mySha256 = SHA256.Create();
            byte[] value = Encoding.UTF8.GetBytes(String.Format("--{0}--{1}--", salt, payload));
            byte[] hashValue = mySha256.ComputeHash(value);
            return BitConverter.ToString(hashValue).Replace("-", "").ToUpper();
        }
    }
}