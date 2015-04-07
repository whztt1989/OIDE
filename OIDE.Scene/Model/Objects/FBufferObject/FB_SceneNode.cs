using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FlatBuffers;
using Module.Protob.Interface;
using OIDE.InteropEditor.DLL;
using OIDE.Scene.Interface.Services;
using System.Xml.Serialization;

namespace OIDE.Scene.Model.Objects.FBufferObject
{
    [Serializable]
    public class FB_SceneNode : IFBObject
    {
        #region private members

        private XFBType.Node m_FBData = new XFBType.Node();

        private Quaternion m_Rotation;
        private Vector3 m_Location;
        private Vector3 m_Scale;
        private Boolean m_IsVisible;
        private Boolean m_IsEnabled;

        #endregion

        #region Properties

        [XmlIgnore]
        public object Parent { get; set; }

        public Boolean IsVisible { get { return m_IsVisible; } set { m_IsVisible = FB_Helper.UpdateSelectedObject(this, m_IsVisible, value); } }
        public Boolean IsEnabled { get { return m_IsEnabled; } set { m_IsEnabled = FB_Helper.UpdateSelectedObject(this, m_IsEnabled, value); } }
        public Quaternion Rotation { get { return m_Rotation; } set { m_Rotation = FB_Helper.UpdateSelectedObject(this, m_Rotation, value); } }
        public Vector3 Location { get { return m_Location; } set { m_Location = FB_Helper.UpdateSelectedObject(this, m_Location, value); ; } }
        public Vector3 Scale { get { return m_Scale; } set { m_Scale = FB_Helper.UpdateSelectedObject(this, m_Scale, value); } }

        public String AbsPathToXML { get; set; }
        public String RelPathToXML { get; set; }

        #endregion

        #region Methods 


        /// <summary>
        /// reads flatbuffers byte data into object
        /// </summary>
        /// <param name="fbData"></param>
        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);

            m_FBData = XFBType.Node.GetRootAsNode(byteBuffer); // read 

            m_Rotation = new Quaternion() { W = m_FBData.Transform().Rot().W(), X = m_FBData.Transform().Rot().X(), Y = m_FBData.Transform().Rot().Y(), Z = m_FBData.Transform().Rot().Z() };
            m_Location = new Vector3() { X = m_FBData.Transform().Loc().X(), Y = m_FBData.Transform().Loc().Y(), Z = m_FBData.Transform().Loc().Z() };
            m_Scale = new Vector3() { X = m_FBData.Transform().Scl().X(), Y = m_FBData.Transform().Scl().Y(), Z = m_FBData.Transform().Scl().Z() };
        }

        //not implemented
        public int Create(FlatBufferBuilder fbbParent) { return 0; }

        /// <summary>
        /// resets the flatbufferbuilder
        /// </summary>
        /// <returns>byte data</returns>
        public Byte[] CreateByteBuffer(IFBObject child = null)
        {
            try
            {
                //--------------------------------------
                //create flatbuffer data
                //--------------------------------------
                FlatBufferBuilder fbb = new FlatBufferBuilder(1);

                XFBType.TransformStateData.StartTransformStateData(fbb);
                //struct must be serialized inline
                XFBType.TransformStateData.AddRot(fbb, XFBType.Quat4f.CreateQuat4f(fbb, m_Rotation.W, m_Rotation.X, m_Rotation.Y, m_Rotation.Z));
                XFBType.TransformStateData.AddLoc(fbb, XFBType.Vec3f.CreateVec3f(fbb, m_Location.X, m_Location.Y, m_Location.Z));
                XFBType.TransformStateData.AddScl(fbb, XFBType.Vec3f.CreateVec3f(fbb, m_Scale.X, m_Scale.Y, m_Scale.Z));
                int transformoffset = XFBType.TransformStateData.EndTransformStateData(fbb);

                XFBType.Node.StartNode(fbb);

                XFBType.Node.AddTransform(fbb, transformoffset);
                XFBType.Node.AddGroup(fbb, 0);
                XFBType.Node.AddVisible(fbb, m_IsVisible);

                int sceneoffset = XFBType.Node.EndNode(fbb);

                fbb.Finish(sceneoffset); //!!!!! important ..

                return fbb.SizedByteArray(); //bytebuffer
                //--------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("error in FB:SceneNode.CreateByteBuffer :" + ex.Message);
            }

            return new Byte[0];
        }

        #endregion
    }
}
