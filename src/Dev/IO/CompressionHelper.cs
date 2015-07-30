using System.IO;
using System.IO.Compression;

namespace Dev.IO
{
    /// <summary>
    /// 压缩操作类
    /// </summary>
    public class CompressionHelper
    {
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        public static byte[] Compress(byte[] inputBytes)
        {
            if (inputBytes == null)
            {
                return null;
            }

            using (var outStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
                {
                    zipStream.Write(inputBytes, 0, inputBytes.Length);
                    zipStream.Close(); //很重要，必须关闭，否则无法正确解压
                    return outStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        public static byte[] Decompress(byte[] inputBytes)
        {

            using (var inputStream = new MemoryStream(inputBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        zipStream.CopyTo(outStream);
                        zipStream.Close();
                        return outStream.ToArray();
                    }
                }

            }
        }
    }
}