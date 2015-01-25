using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using System.Xml.Serialization;

namespace OIDE.Scene.Model.Objects
{
    public class FB_StaticObjectModel : FB_EntityBaseModel, IFBObject
    {
        #region private members
       
        private XFBType.StaticEntity m_FBData = new XFBType.StaticEntity();
        private int m_Group;
        private FB_EntityBaseModel m_EntityBaseModel;

        #endregion
        
        #region Properties

       [XmlIgnore]
       public String AbsPathToXML { get; set; }

       public String RelPathToXML { get; set; }

       public int Group { get { return m_Group; } set { m_Group = value; } }
       public FB_EntityBaseModel EntityBaseModel { get { return m_EntityBaseModel; } set { m_EntityBaseModel = value; } }


        #endregion

       #region methods

       public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);
            m_FBData = XFBType.StaticEntity.GetRootAsStaticEntity(byteBuffer); // read 

            base.Read(m_FBData.Entitybase());
            
           //     m_Group = XFBType.Group(); //per node!
            
        }

        public Byte[] CreateByteBuffer()
        {
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            int soOffset  = XFBType.StaticEntity.CreateStaticEntity(fbb, base.Create(fbb));
            fbb.Finish(soOffset); //!!!!! important ..

            return fbb.SizedByteArray();  //bytebuffer
            //--------------------------------------
        }

       #endregion
    }
}
