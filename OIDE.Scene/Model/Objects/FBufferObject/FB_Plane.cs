using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatBuffers;
using Module.Protob.Interface;

namespace OIDE.Scene.Model.Objects.ObjectData
{
    [Serializable]
    public class FB_PlaneModel : IFBObject
    {
       
    //   XFBType::Vec3f normal { get;set;}
    //constant:float;
    //width:float;
    //height:float;
    //xsegments:uint;
    //ysegments:uint;
    //normals:bool;
    //numTexCoordSets:uint;
    //xTile:float;
    //yTile:float;
    //upVector:Vec3f;

        private XFBType.Sound m_FBData = new XFBType.Sound();

        #region sceneData

        private String m_Name;
        private String m_FileName;
        private String m_RessGrp;

        #endregion

        #region Properties

        public String AbsPathToXML { get; set; }
        public String RelPathToXML { get; set; }
        public String Name { get { return m_Name; } }
        public String FileName { get { return m_FileName; } }
        public String RessGrp { get { return m_RessGrp; } }

        public int SetName(String name)
        {
            int res = 0;
            m_Name = name;

            Byte[] tmp = CreateByteBuffer();  //send to c++ DLL

            //if (DLL_Singleton.Instance != null)
            //{
            //  todo  res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            //}
            return res;
        }

        public int SetFileName(String FileName)
        {
            int res = 0;
            m_FileName = FileName;

            Byte[] tmp = CreateByteBuffer();  //send to c++ DLL

            //if (DLL_Singleton.Instance != null)
            //{
            //  todo  res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            //}
            return res;
        }

        public int SetRessGrp(String RessGrp)
        {
            int res = 0;
            m_RessGrp = RessGrp;

            Byte[] tmp = CreateByteBuffer();  //send to c++ DLL

            //if (DLL_Singleton.Instance != null)
            //{
            //  todo  res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            //}
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
            // XFBType.Mesh.
            //  m_FBData = XFBType.Mesh.GetRootAsMesh(byteBuffer); // read 
            m_Name = m_FBData.Name();
            m_FileName = m_FBData.FileName();
            m_RessGrp = m_FBData.RessGrp();
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
    }
}
