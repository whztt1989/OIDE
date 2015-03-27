using OIDE.Scene.Interface.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OIDE.Scene.Interface
{
    [Serializable]
    public class CollectionOfISceneItem : ObservableCollection<ISceneItem>, IXmlSerializable
    {
        public CollectionOfISceneItem() : base() { }

        public System.Xml.Schema.XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            try
            {
                reader.ReadStartElement("Items");
                while (reader.IsStartElement("ISceneItem"))
                {
                    Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName"));
                    XmlSerializer serial = new XmlSerializer(type);

                    reader.ReadStartElement("ISceneItem");
                    this.Add((ISceneItem)serial.Deserialize(reader));
                    reader.ReadEndElement();
                }
                reader.ReadEndElement();
            }
            catch
            {

            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (ISceneItem dispatcher in this)
            {
                writer.WriteStartElement("ISceneItem");
                writer.WriteAttributeString("AssemblyQualifiedName", dispatcher.GetType().AssemblyQualifiedName);
                XmlSerializer xmlSerializer = new XmlSerializer(dispatcher.GetType());
                xmlSerializer.Serialize(writer, dispatcher);
                writer.WriteEndElement();
            }
        }
    }
}
