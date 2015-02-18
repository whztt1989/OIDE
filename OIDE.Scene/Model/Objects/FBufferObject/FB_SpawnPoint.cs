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
    [Serializable]
    public class FB_SpawnPointModel : ViewModelBase, IFBObject
    {
        private XFBType.Scene m_FBData = new XFBType.Scene();
        #region sceneData

        private int m_coloroffset = 0; 
        private System.Windows.Media.Color m_ColourAmbient;

        #endregion

        #region Properties

        [XmlIgnore]
        public object Parent { get; set; }


        public String AbsPathToXML { get; set; }
        public String RelPathToXML { get; set; }
        public System.Windows.Media.Color ColourAmbient { get { return m_ColourAmbient; } set { m_ColourAmbient = value; } }

        public int SetColourAmbient(System.Windows.Media.Color color)
        {
            int res = 0;
            m_ColourAmbient = color;

            //send to c++ DLL
           // Byte[] tmp = CreateByteBuffer();

            //if (DLL_Singleton.Instance != null)
            //{
            //  todo  res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            //}
            return res;
        }

        #endregion

        //public static FB_SceneModel XMLDeSerialize(String filename)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(FB_SceneModel));
        //    // A FileStream is needed to read the XML document.
        //    using (FileStream fs = new FileStream(filename, FileMode.Open))
        //    {
        //        return (FB_SceneModel)serializer.Deserialize(fs);
        //    }
        //}

        //public void XMLSerialize(String filename)
        //{
        //    XmlSerializer serializer = new XmlSerializer(this.GetType());

        //    // Determine whether the directory exists.
        //    if (!Directory.Exists(Path.GetDirectoryName(filename)))
        //        Directory.CreateDirectory(Path.GetDirectoryName(filename));

        //    if (!File.Exists(filename))
        //    {
        //        var fileStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //        fileStream.Close();
        //    }

        //    using (StreamWriter sw = new StreamWriter(filename))
        //    {
        //        // Serialize the purchase order, and close the TextWriter.
        //        serializer.Serialize(sw, this);
        //        sw.Close();
        //    }

        //}

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

        //not implemented
        public int Create(FlatBufferBuilder fbbParent) { return 0; }

        
        /// <summary>
        /// resets the flatbufferbuilder
        /// </summary>
        /// <returns>byte data</returns>
        public Byte[] CreateByteBuffer(IFBObject child = null)
        {
            //m_ColourAmbient.A = 255;
            //m_ColourAmbient.R = 90;
            //m_ColourAmbient.B = 50;
            //--------------------------------------
            //create flatbuffer data
            //--------------------------------------
          
            // fbb.CreateString();
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);

            //int coloroffset = XFBType.Colour.CreateColour(fbb, m_ColourAmbient.R, m_ColourAmbient.G, m_ColourAmbient.B, m_ColourAmbient.A);
            //XFBType.Scene.StartScene(fbb);
            //XFBType.Scene.AddColourAmbient(fbb, coloroffset);
            //fbb.Finish(XFBType.Scene.EndScene(fbb)); //!!!!! important ..
       
            // Dump to output directory so we can inspect later, if needed
            //using (var ms = new MemoryStream(fbb.DataBuffer().Data))//, fbb.DataBuffer().position(), fbb.Offset()))
            //{
            //    var data = ms.ToArray();
            //FBType.Sound test = FBType.Sound.GetRootAsSound(fbb.DataBuffer(), fbb.DataBuffer().position());
            //    string gg = test.FileName(); // funtzt 
              //  File.WriteAllBytes(@"Resources/monsterdata_cstest.mon", data);
         //   }

            return fbb.SizedByteArray(); //bytebuffer
            //--------------------------------------
        }
    }
}
