﻿using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using OIDE.InteropEditor.DLL;
using System.Xml.Serialization;
using OIDE.Scene.Model.Objects.FBufferObject;

namespace OIDE.Scene.Model.Objects
{
    [Serializable]
    public class FB_SoundModel : IFBObject
    {
        private XFBType.Sound m_FBData = new XFBType.Sound();
      
        #region sceneData

        private String m_Name;
        private String m_FileName;
        private String m_RessGrp;

        #endregion

        #region Properties

        [XmlIgnore]
        public object Parent { get; set; }

        public String AbsPathToXML { get; set; }
        public String RelPathToXML { get; set; }
        public String Name { get { return m_Name; } set { m_Name = FB_Helper.UpdateSelectedObject(this, m_Name, value); } }
        public String FileName { get { return m_FileName; } set { m_FileName = FB_Helper.UpdateSelectedObject(this, m_FileName, value); } }
        public String RessGrp { get { return m_RessGrp; } set { m_RessGrp = FB_Helper.UpdateSelectedObject(this, m_RessGrp, value); } }


        #endregion
        
        /// <summary>
        /// reads flatbuffers byte data into object
        /// </summary>
        /// <param name="fbData"></param>
        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);

            m_FBData = XFBType.Sound.GetRootAsSound(byteBuffer); // read 
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
