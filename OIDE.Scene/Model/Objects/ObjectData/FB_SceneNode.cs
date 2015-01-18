using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using OIDE.InteropEditor.DLL;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Xml.Serialization;
using OIDE.Scene.Service;
using OIDE.Scene.Interface.Services;

namespace OIDE.Scene.Model.Objects
{
    public class FB_SceneNode : ViewModelBase, IFBObject
    {
        private XFBType.Node m_FBData = new XFBType.Node();
        #region sceneData

        private int m_coloroffset = 0;
        private Quaternion m_Rotation;
        private Vector3 m_Location;
        private Vector3 m_Scale;

        #endregion

        #region Properties

        [XmlIgnore]
        [ExpandableObject]
        public Quaternion Rotation { get { return m_Rotation; } }
        [XmlIgnore]
        [ExpandableObject]
        public Vector3 Location { get { return m_Location; } }
        [XmlIgnore]
        [ExpandableObject]
        public Vector3 Scale { get { return m_Scale; } }


        public int SetRotation(Quaternion rotation)
        {
            var m_oldRotation = rotation;
            m_Rotation = rotation;

            //send to c++ DLL
            Byte[] tmp = CreateByteBuffer();
            int res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            if (res != 0) //fehler beim senden
                m_Rotation = m_oldRotation;

            return res;
        }

        public int SetLocation(Vector3 location)
        {
            var m_oldLocation = location;
            m_Location = location;

            //send to c++ DLL
            Byte[] tmp = CreateByteBuffer();
            int res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            if (res != 0) //fehler beim senden
                m_Location = m_oldLocation;

            return res;
        }

        public int SetScale(Vector3 scale)
        {
            var m_oldScale = scale;
            m_Scale = scale;

            //send to c++ DLL
            Byte[] tmp = CreateByteBuffer();
            int res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            if (res != 0) //fehler beim senden
                m_Scale = m_oldScale;

            return res;
        }

        #endregion

        /// <summary>
        /// reads flatbuffers byte data into object
        /// </summary>
        /// <param name="fbData"></param>
        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);

            m_FBData = XFBType.Node.GetRootAsNode(byteBuffer); // read 

            m_Rotation = new Quaternion() { W = m_FBData.Transform().Rot().W() ,  X = m_FBData.Transform().Rot().X() ,  Y = m_FBData.Transform().Rot().Y(), Z = m_FBData.Transform().Rot().Z()};
            m_Location = new Vector3() { X = m_FBData.Transform().Loc().X() ,  Y = m_FBData.Transform().Loc().Y(), Z = m_FBData.Transform().Loc().Z()};
            m_Scale = new Vector3() { X = m_FBData.Transform().Scl().X(), Y = m_FBData.Transform().Scl().Y(), Z = m_FBData.Transform().Scl().Z() };
        }
        
        /// <summary>
        /// resets the flatbufferbuilder
        /// </summary>
        /// <returns>byte data</returns>
        public Byte[] CreateByteBuffer()
        {
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);

            int rotOffset = XFBType.Quat4f.CreateQuat4f(fbb,m_Rotation.W, m_Rotation.X, m_Rotation.Y, m_Rotation.Z);
            int locOffset = XFBType.Vec3f.CreateVec3f(fbb, m_Location.X, m_Location.Y, m_Location.Z);
            int sclOffset = XFBType.Vec3f.CreateVec3f(fbb, m_Scale.X, m_Scale.Y, m_Scale.Z);

            XFBType.TransformStateData.StartTransformStateData(fbb);
            XFBType.TransformStateData.AddRot(fbb, rotOffset);
            XFBType.TransformStateData.AddLoc(fbb, locOffset);
            XFBType.TransformStateData.AddScl(fbb, sclOffset);
            int transformoffset = XFBType.TransformStateData.EndTransformStateData(fbb);

            XFBType.Node.StartNode(fbb);

            XFBType.Node.AddTransform(fbb, transformoffset);
            XFBType.Node.AddGroup(fbb, 0);
            XFBType.Node.AddVisible(fbb, 0x01);

            int sceneoffset = XFBType.Node.EndNode(fbb);

            fbb.Finish(sceneoffset); //!!!!! important ..

            return fbb.SizedByteArray(); //bytebuffer
            //--------------------------------------
        }
    }
}
