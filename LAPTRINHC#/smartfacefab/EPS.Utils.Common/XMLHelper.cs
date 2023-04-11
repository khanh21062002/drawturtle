using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EPS.Utils.Common
{
    public class XMLHelper
    {
        public static T XMLToObject<T>(string filePath) where T : class
        {
            T data;

            using (FileStream parametersFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                data = (T)serializer.Deserialize(parametersFileStream);
            }

            return data;
        }
    }
}
