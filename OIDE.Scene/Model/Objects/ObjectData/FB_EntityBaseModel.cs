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
using OIDE.Scene.Model.Objects.ObjectData;

namespace OIDE.Scene.Model.Objects
{
    public abstract class FB_EntityBaseModel
    {
        #region private members
    
        private XFBType.EntityBase m_FBData;

        private String m_AnimationInfo;
        private String m_AnimationTree;
        private String m_Boneparent;
        private Boolean m_CastShadows;
        private uint m_Renderqueue;
        private EntityTypes m_EntType;
        private uint m_Mode;
        private Boolean m_Debug;
        private uint m_Group;

        #endregion

        #region Properties

        [XmlIgnore]
        public String AbsPathToXML { get; set; }

        public String RelPathToXML { get; set; }
        public String AnimationInfo { get { return m_AnimationInfo; } }
        public String AnimationTree { get { return m_AnimationTree; }  }
        public String Boneparent { get { return m_Boneparent; } }
        public Boolean CastShadows { get { return m_CastShadows; } }
        public uint Renderqueue { get { return m_Renderqueue; } }
        public EntityTypes EntType { get { return m_EntType; } }
        public uint Mode { get { return m_Mode; } }
        public Boolean Debug { get { return m_Debug; } }
        public uint Group { get { return m_Group; } }


        //todo
        public int SetAnimationInfo(String animationInfo) { m_AnimationInfo = animationInfo; return 0;   }
        public int SetAnimationTree(String animationTree) { m_AnimationTree = animationTree; return 0; }
        public int SetBoneparent(String boneParent) { m_Boneparent = boneParent; return 0; }
        public int SetCastShadows(Boolean castShadows) { m_CastShadows = castShadows; return 0; }
        public int SetRenderqueue(uint renderqueue) { m_Renderqueue = renderqueue; return 0; }
        public int SetEntType(EntityTypes entType) { m_EntType = entType; return 0; }
        public int SetMode(uint mode) { m_Mode = mode; return 0; }
        public int SetDebug(Boolean debug) { m_Debug = debug; return 0; }
        public int SetGroup(uint group) { m_Group = group; return 0; }
    


        private List<MeshModel> m_Meshes = new List<MeshModel>();
        public List<MeshModel> Meshes { get { return m_Meshes; } set { m_Meshes = value; } }

        private List<MaterialObject> m_Materials = new List<MaterialObject>();
        public List<MaterialObject> Materials { get { return m_Materials; } set { m_Materials = value; } }

        private List<PhysicObject> m_Physics = new List<PhysicObject>();
        public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }

        private List<SoundObject> m_Sounds = new List<SoundObject>();
        public List<SoundObject> Sounds { get { return m_Sounds; } set { m_Sounds = value; } }


        #endregion

        #region methods

        public void Read(EntityBase data)
        {
            m_FBData = data;

            m_AnimationInfo = m_FBData.AnimationInfo();
            m_AnimationTree = m_FBData.AnimationTree();
            m_Boneparent = m_FBData.Boneparent();
            Boolean.TryParse(m_FBData.CastShadows().ToString(), out m_CastShadows);
        }

        /// <summary>
        /// EntityBase is just a baseclass. the offset is needed for inherited object
        /// </summary>
        /// <param name="fbb"></param>
        /// <returns></returns>
        public int Create(FlatBufferBuilder fbb)
        {
            int AnimationInfo_OS = fbb.CreateString(m_AnimationInfo);
            int AnimationTree_OS = fbb.CreateString(m_AnimationTree);
            int Debug_OS = 0;
            uint Mode_OS = 0;
            int Boneparent_OS = fbb.CreateString(m_Boneparent);

            int Physics_OS = 0;
            foreach (var physic in m_Physics)
            {
                XFBType.EntityBase.StartPhysicsVector(fbb, m_Physics.Count);
                XFBType.EntityBase.AddPhysics(fbb, physic.Create(fbb));
                Physics_OS = fbb.EndVector();
            }
            int Materials_OS = 0;
            foreach (var material in m_Materials)
            {
                XFBType.EntityBase.StartMaterialsVector(fbb, m_Materials.Count);
                XFBType.EntityBase.AddPhysics(fbb, XFBType.Material.CreateMaterial(fbb, 0, 0));
                Materials_OS = fbb.EndVector();
            }

            int Sounds_OS = 0;
            foreach (var sound in m_Sounds)
            {
                XFBType.EntityBase.StartSoundsVector(fbb, m_Sounds.Count);
                XFBType.EntityBase.AddSounds(fbb, XFBType.Sound.CreateSound(fbb, 0, 0, 0));
                Sounds_OS = fbb.EndVector();
            }

            int Meshes_OS = 0;
            foreach (var sound in m_Meshes)
            {
                XFBType.EntityBase.StartMeshesVector(fbb, m_Meshes.Count);
                XFBType.EntityBase.AddMeshes(fbb, XFBType.Mesh.CreateMesh(fbb, 0, 0, 0,0));
                Meshes_OS = fbb.EndVector();
            }
            
            ushort Type_OS = (ushort)m_EntType;
            byte CastShadows_OS = m_CastShadows ? (byte)0x01 : (byte)0x00;

            return XFBType.EntityBase.CreateEntityBase(fbb, Meshes_OS, Sounds_OS, Materials_OS, Physics_OS, Type_OS, Boneparent_OS, Mode_OS, CastShadows_OS, Debug_OS, AnimationTree_OS, AnimationInfo_OS);
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

        #endregion
    }
}
