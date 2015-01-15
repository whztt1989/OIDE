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
using System.Xml.Serialization;

namespace OIDE.Scene.Model.Objects
{
//      public static Scene GetRootAsScene(ByteBuffer _bb, int offset) { return (new Scene()).__init(_bb.GetInt(offset) + offset, _bb); }
//  public Scene __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

//  public Colour ColourAmbient() { return ColourAmbient(new Colour()); }
//  public Colour ColourAmbient(Colour obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

//  public static void StartScene(FlatBufferBuilder builder) { builder.StartObject(1); }
//  public static void AddColourAmbient(FlatBufferBuilder builder, int colourAmbientOffset) { builder.AddOffset(0, colourAmbientOffset, 0); }
//  public static int EndScene(FlatBufferBuilder builder) { return builder.EndObject(); }

    [Serializable]
    public class FB_SceneModel : ViewModelBase, IFBObject
    {
        private XFBType.Scene m_FBData = new XFBType.Scene();
        #region sceneData

        private int m_coloroffset = 0; 
        private System.Windows.Media.Color m_ColourAmbient;

        #endregion

        #region Properties

        public System.Windows.Media.Color ColourAmbient { get { return m_ColourAmbient; } set { m_ColourAmbient = value; } }

        public int SetColourAmbient(System.Windows.Media.Color color)
        {
            int res = 0;
            m_ColourAmbient = color;

            //send to c++ DLL
            Byte[] tmp = CreateByteBuffer();

            //if (DLL_Singleton.Instance != null)
            //{
            //  todo  res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            //}
            return res;
        }

        #endregion

        public static FB_SceneModel XMLDeSerialize(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FB_SceneModel));
            // A FileStream is needed to read the XML document.
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                return (FB_SceneModel)serializer.Deserialize(fs);
            }
        }

        public void XMLSerialize(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());

            // Determine whether the directory exists.
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
                Directory.CreateDirectory(Path.GetDirectoryName(filename));

            if (!File.Exists(filename))
            {
                var fileStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fileStream.Close();
            }

            using (StreamWriter sw = new StreamWriter(filename))
            {
                // Serialize the purchase order, and close the TextWriter.
                serializer.Serialize(sw, this);
                sw.Close();
            }

        }

        /// <summary>
        /// reads flatbuffers byte data into object
        /// </summary>
        /// <param name="fbData"></param>
        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);

            m_FBData = XFBType.Scene.GetRootAsScene(byteBuffer); // read 
            XFBType.Colour colour = m_FBData.ColourAmbient();

            m_ColourAmbient = System.Windows.Media.Color.FromScRgb(colour.A(), colour.R(), colour.G(), colour.B());

            ByteBuffer byteBuffer2 = new ByteBuffer(fbData);
            var m_FBDataNOT = XFBType.Scene.GetRootAsScene(byteBuffer2); // read      
            XFBType.Colour colourNOT = m_FBDataNOT.ColourAmbient();
            m_ColourAmbient = System.Windows.Media.Color.FromScRgb(colourNOT.A(), colourNOT.R(), colourNOT.G(), colourNOT.B());
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

            int coloroffset = XFBType.Colour.CreateColour(fbb, m_ColourAmbient.R, m_ColourAmbient.G, m_ColourAmbient.B, m_ColourAmbient.A);
            //XFBType.Colour.StartColour(fbb);
            //XFBType.Colour.AddA(fbb, m_ColourAmbient.A);
            //XFBType.Colour.AddR(fbb, m_ColourAmbient.R);
            //XFBType.Colour.AddB(fbb, m_ColourAmbient.B);
            //XFBType.Colour.AddG(fbb, m_ColourAmbient.G);
            //int coloroffset = XFBType.Colour.EndColour(fbb);

            int sceneoffset = XFBType.Scene.CreateScene(fbb, coloroffset);
            //XFBType.Scene.StartScene(fbb);

            //XFBType.Scene.AddColourAmbient(fbb, coloroffset);

            //int sceneoffset = XFBType.Scene.EndScene(fbb);
           
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

            //Byte[] test2 = fbb.SizedByteArray();//.DataBuffer().Data;
            
            ByteBuffer bb = fbb.DataBuffer();
            byte[] foo = new byte[bb.Length - bb.position()];

            System.Buffer.BlockCopy (bb.Data, bb.position(), foo, 0, bb.Length - bb.position ());


            ByteBuffer bbReadoutTest = new ByteBuffer(foo);
            var m_FBDataNOT = XFBType.Scene.GetRootAsScene(bbReadoutTest); // read      
            XFBType.Colour colourNOT = m_FBDataNOT.ColourAmbient();


            //Byte[] test = fbb.DataBuffer().Data;//.DataBuffer().Data;
            //ByteBuffer byteBuffer = new ByteBuffer(test);
            //var m_FBDataNOT = XFBType.Scene.GetRootAsScene(byteBuffer); // read      
            //XFBType.Colour colourNOT = m_FBDataNOT.ColourAmbient();
            //m_ColourAmbient = System.Windows.Media.Color.FromScRgb(colourNOT.A(), colourNOT.R(), colourNOT.G(), colourNOT.B());

            //var m_FBData = XFBType.Scene.GetRootAsScene(fbb.DataBuffer()); // read      
            //XFBType.Colour colour = m_FBData.ColourAmbient();
            //m_ColourAmbient = System.Windows.Media.Color.FromScRgb(colour.A(), colour.R(), colour.G(), colour.B());
            return foo; //bytebuffer
            //--------------------------------------
        }
    }
}
