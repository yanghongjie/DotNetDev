using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Data
{
    /// <summary>
    /// 对象克隆
    /// </summary>
    public class ObjectCloneHelper
    {
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="RealObject">克隆的对象</param>
        /// <returns>返回克隆的对象</returns>
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }
    }
}
