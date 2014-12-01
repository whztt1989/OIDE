using FBType;
using FlatBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIDE.Scene.ViewModel.Objects
{
//      public static Scene GetRootAsScene(ByteBuffer _bb, int offset) { return (new Scene()).__init(_bb.GetInt(offset) + offset, _bb); }
//  public Scene __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

//  public Colour ColourAmbient() { return ColourAmbient(new Colour()); }
//  public Colour ColourAmbient(Colour obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

//  public static void StartScene(FlatBufferBuilder builder) { builder.StartObject(1); }
//  public static void AddColourAmbient(FlatBufferBuilder builder, int colourAmbientOffset) { builder.AddOffset(0, colourAmbientOffset, 0); }
//  public static int EndScene(FlatBufferBuilder builder) { return builder.EndObject(); }
    public struct Colour
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }
    }

    public class SceneDataViewModel 
    {
        private ByteBuffer m_ByteBuffer = null;
        private FBType.Scene m_FBData;

        private System.Windows.Media.Color m_ColourAmbient;

        public System.Windows.Media.Color ColourAmbient { get { return m_ColourAmbient; } set { m_ColourAmbient = value; } }

        public SceneDataViewModel()
        {
            m_FBData = new FBType.Scene();   
        }

        public void Read()
        {
            if (m_ByteBuffer != null)
             m_FBData = FBType.Scene.GetRootAsScene(m_ByteBuffer, 0); // read 
            FBType.Colour colour = m_FBData.ColourAmbient();
            
            m_ColourAmbient = System.Windows.Media.Color.FromScRgb(colour.A(), colour.R(), colour.G(), colour.B());
        }

        public void Create()
        {
            m_ColourAmbient.A = 255;
            m_ColourAmbient.R = 90;
            m_ColourAmbient.B = 50;
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            // fbb.CreateString();

            //FBType.Colour.StartColour(fbb);
            //FBType.Colour.AddA(fbb, m_ColourAmbient.A);
            //FBType.Colour.AddB(fbb, m_ColourAmbient.B);
            //FBType.Colour.AddG(fbb, m_ColourAmbient.G);
            //int coloffset = FBType.Colour.EndColour(fbb);

            FBType.Sound.StartSound(fbb);
        //    FBType.Sound.AddFileName(fbb,);
            int sound_offset = FBType.Sound.EndSound(fbb);


         //   FBType.Colour test = FBType.Colour.GetRootAsColour(fbb.DataBuffer(), 0);
         //   float mist =  test.A();
         //   m_ColourAmbient = System.Windows.Media.Color.FromScRgb(test.A(), test.R(), test.G(), test.B());
         
            //FBType.Scene.StartScene(fbb);
            //FBType.Scene.AddColourAmbient(fbb, coloffset);
            //int mon = FBType.Scene.EndScene(fbb);
            //fbb.Finish(mon);


           m_ByteBuffer = fbb.DataBuffer(); //bytebuffer
            //--------------------------------------
        }
    }
}
