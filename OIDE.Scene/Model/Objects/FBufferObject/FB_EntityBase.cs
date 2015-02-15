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
    [Serializable]
    public class FB_EntityBaseModel : IFBObject
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
        private Boolean m_ShowDebug;
        private Boolean m_ShowAABB;
        private uint m_Group;

        #endregion

        #region Properties

        [XmlIgnore]
        public String AbsPathToXML { get; set; }

        public String RelPathToXML { get; set; }
        public String AnimationInfo { get { return m_AnimationInfo; } set { m_AnimationInfo = value; } }
        public String AnimationTree { get { return m_AnimationTree; } set { m_AnimationTree = value; } }
        public String Boneparent { get { return m_Boneparent; } set { m_Boneparent = value; } }
        public Boolean CastShadows { get { return m_CastShadows; } set { m_CastShadows = value; } }
        public uint Renderqueue { get { return m_Renderqueue; } set { m_Renderqueue = value; } }
        public EntityTypes EntType { get { return m_EntType; } set { m_EntType = value; } }
        public uint Mode { get { return m_Mode; } set { m_Mode = value; } }
        public Boolean ShowDebug { get { return m_ShowDebug; }  set { m_ShowDebug = value; } }
        public Boolean ShowAABB { get { return m_ShowAABB; }  set { m_ShowAABB = value; } }

        public uint Group { get { return m_Group; } set { m_Group = value; } }


        //todo
        public int SetAnimationInfo(String animationInfo) { m_AnimationInfo = animationInfo; return 0; }
        public int SetAnimationTree(String animationTree) { m_AnimationTree = animationTree; return 0; }
        public int SetBoneparent(String boneParent) { m_Boneparent = boneParent; return 0; }
        public int SetCastShadows(Boolean castShadows) { m_CastShadows = castShadows; return 0; }
        public int SetRenderqueue(uint renderqueue) { m_Renderqueue = renderqueue; return 0; }
        public int SetEntType(EntityTypes entType) { m_EntType = entType; return 0; }
        public int SetMode(uint mode) { m_Mode = mode; return 0; }
        public int SetShowDebug(Boolean debug) { m_ShowDebug = debug; return 0; }
        public int SetShowAABB(Boolean debug) { m_ShowAABB = debug; return 0; }
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

        public void Read(Byte[] fbData)
        {
            //not implemented
        }

        public void Read(XFBType.EntityBase fbBase)
        {
            throw new Exception("not implemented - only possible with derived object");
        }

        /// <summary>
        /// EntityBase is just a baseclass. the offset is needed for inherited object
        /// </summary>
        /// <param name="fbb"></param>
        /// <returns></returns>
        public int Create(FlatBufferBuilder fbbParent)
        {
            int animInfoOffset = m_AnimationInfo != null ?  fbbParent.CreateString(m_AnimationInfo) : 0;
            int animTreeOffset = m_AnimationTree != null ? fbbParent.CreateString(m_AnimationTree) : 0;
            int BoneparentOffset = m_Boneparent != null ? fbbParent.CreateString(m_Boneparent) : 0;
            int debugOffset = XFBType.Debug.CreateDebug(fbbParent, m_ShowDebug ? (byte)0x01 : (byte)0x00, m_ShowAABB ? (byte)0x01 : (byte)0x00);
            int physicsOffset = 0;
            if (m_Physics.Any())
            {
                XFBType.EntityBase.StartPhysicsVector(fbbParent, m_Physics.Count);
                foreach (var physic in m_Physics)
                    fbbParent.AddOffset(physic.Create(fbbParent));
                physicsOffset = XFBType.PhysicsObject.EndPhysicsObject(fbbParent);// fbbParent.EndVector();
            } 
            
            int materialOffset = 0;
            if (m_Materials.Any())
            {
                XFBType.EntityBase.StartMaterialsVector(fbbParent, m_Materials.Count);
                foreach (var material in m_Materials)
                    fbbParent.AddOffset(XFBType.Material.CreateMaterial(fbbParent, fbbParent.CreateString(material.Name ?? ""), fbbParent.CreateString(material.RessGrp ?? "")));
                materialOffset = fbbParent.EndVector();   
            }
          
            int soundsOffset = 0;
            if (m_Sounds.Any())
            {
                XFBType.EntityBase.StartSoundsVector(fbbParent, m_Sounds.Count);
                foreach (var sound in m_Sounds)
                   fbbParent.AddOffset(XFBType.Sound.CreateSound(fbbParent, fbbParent.CreateString(sound.Name ?? ""), fbbParent.CreateString(sound.FileName ?? ""), fbbParent.CreateString(sound.RessGrp ?? "")));

                soundsOffset = fbbParent.EndVector();   
            }

            int meshesOffset = 0;
            if (m_Meshes.Any())
            {
                XFBType.EntityBase.StartMeshesVector(fbbParent, m_Meshes.Count);
             
                foreach (var mesh in m_Meshes)
                {
                   // int tt =  fbbParent.CreateString("KK");
                   // fbbParent.StartObject(1);
                   // //Mesh.AddCube(builder, cube);
                   // //Mesh.AddPlane(builder, plane);
                   // //Mesh.AddRessGrp(builder, RessGrp);
                   // Mesh.AddName(fbbParent, tt);
                   //int meshoffset = Mesh.EndMesh(fbbParent);
                   //fbbParent.AddOffset(meshoffset);

                   // XFBType.Mesh.CreateMesh(fbbParent, fbbParent.CreateString("KK"), fbbParent.CreateString(""), 0, 0);
             

                 //   XFBType.Mesh.CreateMesh(fbbParent, fbbParent.CreateString(mesh.Name ?? ""), fbbParent.CreateString(mesh.RessGrp ?? ""), 0, 0);
             

                    var plane = mesh as PlaneModel;
                    if (plane != null)
                    {
                        int planeOffset = 0;
                        XFBType.OgrePlane.StartOgrePlane(fbbParent);
                        XFBType.OgrePlane.AddConstant(fbbParent, plane.constant);
                        XFBType.OgrePlane.AddHeight(fbbParent, plane.height);
                        XFBType.OgrePlane.AddNormal(fbbParent, XFBType.Vec3f.CreateVec3f(fbbParent, plane.normal.X, plane.normal.Y, plane.normal.Z));
                        XFBType.OgrePlane.AddNormals(fbbParent, plane.normals ? (byte)0x01 : (byte)0x00);
                        XFBType.OgrePlane.AddNumTexCoordSets(fbbParent, plane.numTexCoordSets);
                        XFBType.OgrePlane.AddUpVector(fbbParent, XFBType.Vec3f.CreateVec3f(fbbParent, plane.upVector.X, plane.upVector.Y, plane.upVector.Z));
                        XFBType.OgrePlane.AddWidth(fbbParent, plane.width);
                        XFBType.OgrePlane.AddXsegments(fbbParent, plane.xsegments);
                        XFBType.OgrePlane.AddXTile(fbbParent, plane.xTile);
                        XFBType.OgrePlane.AddYsegments(fbbParent, plane.ysegments);
                        XFBType.OgrePlane.AddYTile(fbbParent, plane.yTile);
                        planeOffset = XFBType.OgrePlane.EndOgrePlane(fbbParent);

                      fbbParent.AddOffset(XFBType.Mesh.CreateMesh(fbbParent, fbbParent.CreateString(mesh.Name ?? ""), fbbParent.CreateString(mesh.RessGrp ?? ""), planeOffset, 0));
             
                        continue;
                    }
                    var cube = mesh as CubeModel;
                    if (cube != null)
                    {
                        fbbParent.AddOffset(XFBType.Mesh.CreateMesh(fbbParent, fbbParent.CreateString(mesh.Name), fbbParent.CreateString(mesh.RessGrp), 0, XFBType.OgreCube.CreateOgreCube(fbbParent, cube.width)));
                        continue;
                    }
                }


                meshesOffset = fbbParent.EndVector();   
            }

            XFBType.EntityBase.StartEntityBase(fbbParent);

            if (m_AnimationInfo != null) XFBType.EntityBase.AddAnimationInfo(fbbParent, animInfoOffset);
            if (m_AnimationTree != null) XFBType.EntityBase.AddAnimationTree(fbbParent, animTreeOffset);
            XFBType.EntityBase.AddDebug(fbbParent, debugOffset);
            XFBType.EntityBase.AddMode(fbbParent, m_Mode);
            if (m_Boneparent != null)  XFBType.EntityBase.AddBoneparent(fbbParent, BoneparentOffset);
            if (m_Physics.Any()) XFBType.EntityBase.AddPhysics(fbbParent, physicsOffset);
            if (m_Materials.Any()) XFBType.EntityBase.AddMaterials(fbbParent, materialOffset);
            if (m_Sounds.Any()) XFBType.EntityBase.AddSounds(fbbParent, soundsOffset);
            if (m_Meshes.Any()) XFBType.EntityBase.AddMeshes(fbbParent, meshesOffset);
         
            XFBType.EntityBase.AddType(fbbParent, (ushort)m_EntType);
            XFBType.EntityBase.AddCastShadows(fbbParent, m_CastShadows ? (byte)0x01 : (byte)0x00);

            return XFBType.EntityBase.EndEntityBase(fbbParent);
            // return XFBType.EntityBase.CreateEntityBase(fbbParent, Meshes_OS, Sounds_OS, Materials_OS, Physics_OS, Type_OS, Boneparent_OS, Mode_OS, CastShadows_OS, Debug_OS, AnimationTree_OS, AnimationInfo_OS);
        }

        public Byte[] CreateByteBuffer(IFBObject child)
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
