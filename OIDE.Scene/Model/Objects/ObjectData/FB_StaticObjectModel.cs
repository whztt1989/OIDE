using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;

namespace OIDE.Scene.Model.Objects
{
    public class FB_StaticObjectModel : IFBObject
    {
        private XFBType.StaticEntity m_FBData = new XFBType.StaticEntity();

        #region StaticObject Data

        private int m_Group;
        private FB_EntityBaseModel m_EntityBaseModel;

        #endregion
        
        #region Properties

       public int Group { get { return m_Group; } set { m_Group = value; } }
       public FB_EntityBaseModel EntityBaseModel { get { return m_EntityBaseModel; } set { m_EntityBaseModel = value; } }


        #endregion

        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);
            m_FBData = XFBType.StaticEntity.GetRootAsStaticEntity(byteBuffer); // read 
           //     m_Group = XFBType.Group(); //per node!
            m_EntityBaseModel = new FB_EntityBaseModel() { FB_Data = m_FBData.Entitybase() };
            
        }

        public Byte[] CreateByteBuffer()
        {
            //m_ColourAmbient.A = 255;
            //m_ColourAmbient.R = 90;
            //m_ColourAmbient.B = 50;
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            // fbb.CreateString();

            XFBType.Colour.StartColour(fbb);
            //FBType.Colour.AddA(fbb, m_ColourAmbient.A);
            //FBType.Colour.AddR(fbb, m_ColourAmbient.R);
            //FBType.Colour.AddB(fbb, m_ColourAmbient.B);
            //FBType.Colour.AddG(fbb, m_ColourAmbient.G);
            int coloffset = XFBType.Colour.EndColour(fbb);

            int sceneoffset = XFBType.Scene.CreateScene(fbb, coloffset);
          
            fbb.Finish(sceneoffset); //!!!!! important ..

            return fbb.SizedByteArray();  //bytebuffer
            //--------------------------------------
        }
    }
}
