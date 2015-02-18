using FlatBuffers;
using Microsoft.Practices.Unity;
using Module.Protob.Interface;
using OIDE.Scene.Model.Objects.FBufferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OIDE.Scene.Model.Objects.ChildObject
{

    [Serializable]
    public class CubeObject : MeshObject, IFBObject
    {
        private float m_width;

        /// <summary>
        /// needed for propertygrid collection
        /// </summary>
        public CubeObject()
        {

        }
        public CubeObject(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        #region properties

        public float width { get { return m_width; } set { m_width = FB_Helper.UpdateSelectedObject(this, m_width, value); } }

        #endregion



        #region IFBObject

        [XmlIgnore]
        public object Parent { get; set; }


        public String AbsPathToXML { get; set; }
        public String RelPathToXML { get; set; }

        /// <summary>
        /// reads flatbuffers byte data into object
        /// </summary>
        /// <param name="fbData"></param>
        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);
            // XFBType.Mesh.
            //  m_FBData = XFBType.Mesh.GetRootAsMesh(byteBuffer); // read 
        }

        //not implemented
        public int Create(FlatBufferBuilder fbbParent) { return 0; }


        /// <summary>
        /// resets the flatbufferbuilder
        /// </summary>
        /// <returns>byte data</returns>
        public Byte[] CreateByteBuffer(IFBObject child = null)
        {
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            //      int soundoffset = XFBType.Sound.CreateSound(fbb, fbb.CreateString(m_Name), fbb.CreateString(m_FileName), fbb.CreateString(m_RessGrp));
            //      fbb.Finish(soundoffset); //!!!!! important ..
            return fbb.SizedByteArray(); //bytebuffer
            //--------------------------------------
        }

        #endregion
    }

}
