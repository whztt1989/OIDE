using FlatBuffers;
using Microsoft.Practices.Unity;
using Module.Protob.Interface;
using OIDE.Scene.Interface.Services;
using OIDE.Scene.Model.Objects.FBufferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OIDE.Scene.Model.Objects.ChildObject
{
    /// <summary>
    /// Plane Object
    /// </summary>
    [Serializable]
    public class PlaneObject : MeshObject, IFBObject
    {
        #region private members

        private Vector3 m_normal;
        private float m_constant;
        private float m_width;
        private float m_height;
        private uint m_xsegments;
        private uint m_ysegments;
        private bool m_normals;
        private uint m_numTexCoordSets;
        private float m_xTile;
        private float m_yTile;
        private Vector3 m_upVector;

        #endregion

        public PlaneObject(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        #region Properties

        public String AbsPathToXML { get; set; }
        public String RelPathToXML { get; set; }

        public Vector3 normal { get { return m_normal; } set { m_normal = FB_Helper.UpdateSelectedObject(this, m_normal, value); } }
        public float constant { get { return m_constant; } set { m_constant = FB_Helper.UpdateSelectedObject(this, m_constant, value); } }
        public float width { get { return m_width; } set { m_width = FB_Helper.UpdateSelectedObject(this, m_width, value); } }
        public float height { get { return m_height; } set { m_height = FB_Helper.UpdateSelectedObject(this, m_height, value); } }
        public uint xsegments { get { return m_xsegments; } set { m_xsegments = FB_Helper.UpdateSelectedObject(this, m_xsegments, value); } }
        public uint ysegments { get { return m_ysegments; } set { m_ysegments = FB_Helper.UpdateSelectedObject(this, m_ysegments, value); } }
        public bool normals { get { return m_normals; } set { m_normals = FB_Helper.UpdateSelectedObject(this, m_normals, value); } }
        public uint numTexCoordSets { get { return m_numTexCoordSets; } set { m_numTexCoordSets = FB_Helper.UpdateSelectedObject(this, m_numTexCoordSets, value); } }
        public float xTile { get { return m_xTile; } set { m_xTile = FB_Helper.UpdateSelectedObject(this, m_xTile, value); } }
        public float yTile { get { return m_yTile; } set { m_yTile = FB_Helper.UpdateSelectedObject(this, m_yTile, value); } }
        public Vector3 upVector { get { return m_upVector; } set { m_upVector = FB_Helper.UpdateSelectedObject(this, m_upVector, value); } }


        #endregion

        /// <summary>
        /// needed for propertygrid collection
        /// </summary>
        public PlaneObject()
        {
            m_normal = new Vector3();
            m_upVector = new Vector3();

        }

        #region IFBObject

        [XmlIgnore]
        public object Parent { get; set; }

        /// <summary>
        /// reads flatbuffers byte data into object
        /// </summary>
        /// <param name="fbData"></param>
        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);
            // XFBType.Mesh.
            //  m_FBData = XFBType.Mesh.GetRootAsMesh(byteBuffer); // read 
            //m_Name = m_FBData.Name();
            //m_FileName = m_FBData.FileName();
            //m_RessGrp = m_FBData.RessGrp();
        }

        //not implemented
        public int Create(FlatBufferBuilder fbbParent) { return 0; }


        /// <summary>
        /// resets the flatbufferbuilder
        /// </summary>
        /// <returns>byte data</returns>
        public Byte[] CreateByteBuffer(IFBObject child = null)
        {
            //normaly done in entitybase

            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
           // int soundoffset = XFBType.Sound.CreateSound(fbb, fbb.CreateString(m_Name), fbb.CreateString(m_FileName), fbb.CreateString(m_RessGrp));
          //  fbb.Finish(soundoffset); //!!!!! important ..
            return fbb.SizedByteArray(); //bytebuffer
            //--------------------------------------
        }

        #endregion
    }

}
