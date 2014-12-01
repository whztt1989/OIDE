using FBType;
using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;

namespace OIDE.Scene.Model.Objects
{
//      public static Scene GetRootAsScene(ByteBuffer _bb, int offset) { return (new Scene()).__init(_bb.GetInt(offset) + offset, _bb); }
//  public Scene __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

//  public Colour ColourAmbient() { return ColourAmbient(new Colour()); }
//  public Colour ColourAmbient(Colour obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

//  public static void StartScene(FlatBufferBuilder builder) { builder.StartObject(1); }
//  public static void AddColourAmbient(FlatBufferBuilder builder, int colourAmbientOffset) { builder.AddOffset(0, colourAmbientOffset, 0); }
//  public static int EndScene(FlatBufferBuilder builder) { return builder.EndObject(); }

    public class FB_StaticObjectModel : ViewModelBase, IFBObject
    {
        private FBType.StaticEntity m_FBData = new FBType.StaticEntity();
        private ByteBuffer m_ByteBuffer = null;

        #region StaticObject Data

        private int m_Group;
        private FB_GameEntityModel m_GameEntityModel;

        #endregion

        #region Properties

        public int Group { get { return m_Group; } set { m_Group = value; RaisePropertyChanged("Group"); } }
        public ByteBuffer ByteBuffer { get { return m_ByteBuffer; } set { m_ByteBuffer = value; } }
        public FB_GameEntityModel GameEntityModel { get { return m_GameEntityModel; } set { m_GameEntityModel = value; RaisePropertyChanged("GameEntityModel"); } }


        #endregion

        public void Read()
        {
            if (m_ByteBuffer != null)
            {
                m_FBData = FBType.StaticEntity.GetRootAsStaticEntity(m_ByteBuffer, m_ByteBuffer.position()); // read 
                m_Group = m_FBData.Group();
                m_GameEntityModel = new FB_GameEntityModel() { FB_Data = m_FBData.GameEntity() } ;
            }
        }

        public ByteBuffer Create()
        {
            //m_ColourAmbient.A = 255;
            //m_ColourAmbient.R = 90;
            //m_ColourAmbient.B = 50;
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            // fbb.CreateString();

       
            FBType.Colour.StartColour(fbb);
            //FBType.Colour.AddA(fbb, m_ColourAmbient.A);
            //FBType.Colour.AddR(fbb, m_ColourAmbient.R);
            //FBType.Colour.AddB(fbb, m_ColourAmbient.B);
            //FBType.Colour.AddG(fbb, m_ColourAmbient.G);
            int coloffset = FBType.Colour.EndColour(fbb);

            FBType.Scene.StartScene(fbb);
            FBType.Scene.AddColourAmbient(fbb, coloffset);
            int sceneoffset = FBType.Scene.EndScene(fbb);
           
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


          return  m_ByteBuffer = fbb.DataBuffer(); //bytebuffer
            //--------------------------------------
        }
    }
}
