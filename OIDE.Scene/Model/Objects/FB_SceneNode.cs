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
//      public static Scene GetRootAsScene(ByteBuffer _bb, int offset) { return (new Scene()).__init(_bb.GetInt(offset) + offset, _bb); }
//  public Scene __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

//  public Colour ColourAmbient() { return ColourAmbient(new Colour()); }
//  public Colour ColourAmbient(Colour obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

//  public static void StartScene(FlatBufferBuilder builder) { builder.StartObject(1); }
//  public static void AddColourAmbient(FlatBufferBuilder builder, int colourAmbientOffset) { builder.AddOffset(0, colourAmbientOffset, 0); }
//  public static int EndScene(FlatBufferBuilder builder) { return builder.EndObject(); }

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
            //m_ColourAmbient.A = 255;
            //m_ColourAmbient.R = 90;
            //m_ColourAmbient.B = 50;
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
          
            // fbb.CreateString();
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
           
            //int s_offset = fbb.CreateString("bockmist");
            //int s_offset2 = fbb.CreateString("bockmist2");
            //int s_offset3 = fbb.CreateString("bockmist3");
            //FBType.Sound.StartSound(fbb);
            //FBType.Sound.AddName(fbb, s_offset);
            //FBType.Sound.AddFileName(fbb, s_offset2);
            //FBType.Sound.AddRessGrp(fbb, s_offset3);
            //int sound_offset = FBType.Sound.EndSound(fbb);

            fbb.Finish(sceneoffset); //!!!!! important ..
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


            return fbb.DataBuffer().Data; //bytebuffer
            //--------------------------------------
        }
    }
}
