using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wide.Interfaces;
using XFBType;

namespace OIDE.Scene.Model.Objects
{
    public abstract class FB_EntityBaseModel
    {
        private XFBType.EntityBase m_FBData;

        #region sceneData

        private String m_AnimationInfo;
        private String m_AnimationTree;
        private String m_Boneparent;
        private Boolean m_CastShadows;

        #endregion

        #region Properties

        public String AnimationInfo { get { return m_AnimationInfo; } }
        public String AnimationTree { get { return m_AnimationTree; }  }
        public String Boneparent { get { return m_Boneparent; } }
        public Boolean CastShadows { get { return m_CastShadows; } }


        public int SetAnimationInfo(String animationInfo) { m_AnimationInfo = animationInfo;
        return 0;
        }
        public int SetAnimationTree(String animationTree) { m_AnimationTree = animationTree; return 0; }
        public int SetBoneparent(String boneParent) { m_Boneparent = boneParent; return 0; }
        public int SetCastShadows(Boolean castShadows) { m_CastShadows = castShadows; return 0; }

        #endregion

        public void Read(EntityBase data)
        {
            m_FBData = data;

            m_AnimationInfo = m_FBData.AnimationInfo();
            m_AnimationTree = m_FBData.AnimationTree();
            m_Boneparent = m_FBData.Boneparent();
            Boolean.TryParse(m_FBData.CastShadows().ToString(), out m_CastShadows);
        }

        public int Create(FlatBufferBuilder fbb)
        {
            return XFBType.EntityBase.CreateEntityBase(fbb,0,0,0,0,0,0,0,0,0,0,0);
        }

        public EntityBase CreateDataForByteBuffer()
        {
            //m_ColourAmbient.A = 255;
            //m_ColourAmbient.R = 90;
            //m_ColourAmbient.B = 50;
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            //FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            //// fbb.CreateString();

       
            //FBType.Colour.StartColour(fbb);
            ////FBType.Colour.AddA(fbb, m_ColourAmbient.A);
            ////FBType.Colour.AddR(fbb, m_ColourAmbient.R);
            ////FBType.Colour.AddB(fbb, m_ColourAmbient.B);
            ////FBType.Colour.AddG(fbb, m_ColourAmbient.G);
            //int coloffset = FBType.Colour.EndColour(fbb);

            //FBType.Scene.StartScene(fbb);
            //FBType.Scene.AddColourAmbient(fbb, coloffset);
            //int sceneoffset = FBType.Scene.EndScene(fbb);
           
            //int s_offset = fbb.CreateString("bockmist");
            //int s_offset2 = fbb.CreateString("bockmist2");
            //int s_offset3 = fbb.CreateString("bockmist3");
            //FBType.Sound.StartSound(fbb);
            //FBType.Sound.AddName(fbb, s_offset);
            //FBType.Sound.AddFileName(fbb, s_offset2);
            //FBType.Sound.AddRessGrp(fbb, s_offset3);
            //int sound_offset = FBType.Sound.EndSound(fbb);
//
        //    fbb.Finish(sceneoffset); //!!!!! important ..
            // Dump to output directory so we can inspect later, if needed
            //using (var ms = new MemoryStream(fbb.DataBuffer().Data))//, fbb.DataBuffer().position(), fbb.Offset()))
            //{
            //    var data = ms.ToArray();


            //FBType.Sound test = FBType.Sound.GetRootAsSound(fbb.DataBuffer(), fbb.DataBuffer().position());
            //    string gg = test.FileName(); // funtzt 

              //  File.WriteAllBytes(@"Resources/monsterdata_cstest.mon", data);
         //   }

         //   FBType.Colour test = FBType.Colour.GetRootAsColour(fbb.DataBuffer(), 0);
         //   float mist =  test.A();
         //   m_ColourAmbient = System.Windows.Media.Color.FromScRgb(test.A(), test.R(), test.G(), test.B());
         
            //FBType.Scene.StartScene(fbb);
            //FBType.Scene.AddColourAmbient(fbb, coloffset);
            //int mon = FBType.Scene.EndScene(fbb);
            //fbb.Finish(mon);


            return null;//m_ByteBuffer = fbb.DataBuffer(); //bytebuffer
            //--------------------------------------
        }
    }
}
