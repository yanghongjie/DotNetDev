﻿using System;
using System.Security.Cryptography;
using System.Text;
using Dev.Extensions;

namespace Dev.Security
{
    /// <summary>
    ///     字符串Hash操作类
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        ///     获取字符串的MD5哈希值
        /// </summary>
        public static string GetMd5(string value, Encoding encoding = null)
        {
            value.CheckNotNull("value");
            if (encoding == null)
            {
                encoding = Encoding.ASCII;
            }
            byte[] bytes = encoding.GetBytes(value);
            return GetMd5(bytes);
        }

        /// <summary>
        ///     获取字节数组的MD5哈希值
        /// </summary>
        public static string GetMd5(byte[] bytes)
        {
            bytes.CheckNotNullOrEmpty("bytes");
            var sb = new StringBuilder();
            MD5 hash = new MD5CryptoServiceProvider();
            bytes = hash.ComputeHash(bytes);
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        ///     获取字符串的SHA1哈希值
        /// </summary>
        public static string GetSha1(string value)
        {
            value.CheckNotNullOrEmpty("value");

            var sb = new StringBuilder();
            var hash = new SHA1Managed();
            byte[] bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(value));
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        ///     获取字符串的Sha256哈希值
        /// </summary>
        public static string GetSha256(string value)
        {
            value.CheckNotNullOrEmpty("value");

            var sb = new StringBuilder();
            var hash = new SHA256Managed();
            byte[] bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(value));
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        ///     获取字符串的Sha512哈希值
        /// </summary>
        public static string GetSha512(string value)
        {
            value.CheckNotNullOrEmpty("value");

            var sb = new StringBuilder();
            var hash = new SHA512Managed();
            byte[] bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(value));
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        ///     校验
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="salt">salt</param>
        /// <param name="encryptedData">校验的数据</param>
        /// <returns>是否相同</returns>
        public static bool Check(string data, string salt, string encryptedData)
        {
            return String.CompareOrdinal(Encrypt(data, salt), encryptedData.ToUpper()) == 0;
        }

        /// <summary>
        ///     加密
        /// </summary>
        /// <param name="payload">原始数据</param>
        /// <param name="salt">salt</param>
        /// <returns>加密数据</returns>
        public static string Encrypt(string payload, string salt)
        {
            HashAlgorithm mySha256 = SHA256.Create();
            byte[] value = Encoding.UTF8.GetBytes(String.Format("--{0}--{1}--", salt, payload));
            byte[] hashValue = mySha256.ComputeHash(value);
            return BitConverter.ToString(hashValue).Replace("-", "").ToUpper();
        }
    }
}