using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OIDE.AssetBrowser.Utilities
{
    public static class ObjectSerialize
    {
        public static void SerializeObjectToXML<T>(T item, string FilePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                xs.Serialize(wr, item);
            }
        }

        public static T DeSerializeObjectToXML<T>(T item, string FilePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                xs.Serialize(wr, item);
            }

            //deserialize
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
           // A FileStream is needed to read the XML document.
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                XmlReader reader = XmlReader.Create(fs);

                // Use the Deserialize method to restore the object's state.
                return (T)xmlSerializer.Deserialize(reader);
              //  fs.Close();
            }  
        }
    }
}
