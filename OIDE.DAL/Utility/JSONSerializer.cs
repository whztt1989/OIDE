using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
//using System.Xml;
//using System.Xml.Serialization;

namespace DAL.Utility
{
    public class JSONSerializer
    {
        /// <summary>
        /// Serializes the data in the object to the designated file path
        /// </summary>
        /// <typeparam name="T">Type of Object to serialize</typeparam>
        /// <param name="dataToSerialize">Object to serialize</param>
        /// <param name="filePath">FilePath for the XML file</param>
        public static void Serialize<T>(T dataToSerialize, string filePath)
        {
            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite))
                {

                    StreamWriter writer = new StreamWriter(stream);
                    JsonTextWriter jsonWriter = new JsonTextWriter(writer);
                    JsonSerializer ser = new JsonSerializer() { Formatting = Formatting.Indented };
                    ser.Serialize(jsonWriter, dataToSerialize);
                    jsonWriter.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : JSONSerializer.Serialize" + ex.Message);
            }
        }


        /// <summary>
        /// Deserializes the data in the XML file into an object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">FilePath to XML file</param>
        /// <returns>Object containing deserialized data</returns>
        public static T Deserialize<T>(string filePath)
        {
            T serializedData = default(T);

            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(stream);
                    JsonTextReader jsonReader = new JsonTextReader(reader);
                    JsonSerializer ser = new JsonSerializer();
                    serializedData = ser.Deserialize<T>(jsonReader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : JSONSerializer.Deserialize" + ex.Message);
            }
                 
            return serializedData;
        }


//        Person p = new Person() { Name = "John Doe", Age = 42 };
//XmlHelper.Serialize<Person>(p, @"D:\text.xml");
 
//Person p2 = new Person();
//p2 = XmlHelper.Deserialize<Person>(@"D:\text.xml");
 
//Console.WriteLine("Name: {0}", p2.Name);
//Console.WriteLine("Age: {0}", p2.Age);
 
//Console.Read();
    }
}
