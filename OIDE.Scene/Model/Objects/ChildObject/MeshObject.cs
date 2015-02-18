using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using OIDE.Scene.Interface.Services;
using OIDE.Scene.Model.Objects.ObjectData;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using OIDE.Scene.Model.Objects.FBufferObject;
using Module.Protob.Interface;
using FlatBuffers;
using OIDE.Scene.Model.Objects.ChildObject;
using System.Xml.Serialization;

namespace OIDE.Scene.Model.Objects
{
    [System.Xml.Serialization.XmlInclude(typeof(CubeObject))]
    [System.Xml.Serialization.XmlInclude(typeof(PlaneObject))]
    [Serializable]
    public class MeshObject : Wide.Interfaces.ViewModelBase, IFBObject
    {
        private String m_Name;
        private String m_FileName;
        private String m_RessGrp;

        /// <summary>
        /// needed for propertygrid collection
        /// </summary>
        public MeshObject()
        {
        }

        public MeshObject(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }
        
        #region MeshData


        #endregion

        #region private properties

        [Browsable(false)]
        private IUnityContainer UnityContainer { get; set; }

        #endregion

        #region Properties

        public String Name { get { return m_Name; } set { m_Name = FB_Helper.UpdateSelectedObject(this, m_Name, value); } }
        public String FileName { get { return m_FileName; } set { m_FileName = FB_Helper.UpdateSelectedObject(this, m_FileName, value); } }
        public String RessGrp { get { return m_RessGrp; } set { m_RessGrp = FB_Helper.UpdateSelectedObject(this, m_RessGrp, value); } }


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
            int soundoffset = XFBType.Sound.CreateSound(fbb, fbb.CreateString(m_Name), fbb.CreateString(m_FileName), fbb.CreateString(m_RessGrp));
            fbb.Finish(soundoffset); //!!!!! important ..
            return fbb.SizedByteArray(); //bytebuffer
            //--------------------------------------
        }

        #endregion
    }
}
