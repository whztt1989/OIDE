using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIDE.Gorilla.Model.Objects
{
    public class GorillaData
    {
        private String m_PathToGorillaFile;
        private String m_TexturePath;

        private String m_AlphabetFile;
        private SquareSize m_SquareTextureSize;
        private String m_PathToFontGorillaFile;
        private String m_ImageExtensions;
        private String m_FontImagePath;
        private String m_ImageFolder;

        public String AlphabetFile { get { return m_AlphabetFile; } set { m_AlphabetFile = value; } }
        public SquareSize SquareTextureSize { get { return m_SquareTextureSize; } set { m_SquareTextureSize = value; } }
        public String PathToFontGorillaFile { get { return m_PathToFontGorillaFile; } set { m_PathToFontGorillaFile = value; } }
        public String ImageExtensions { get { return m_ImageExtensions; } set { m_ImageExtensions = value; } }
        public String FontImagePath { get { return m_FontImagePath; } set { m_FontImagePath = value; } }
        public String ImageFolder { get { return m_ImageFolder; } set { m_ImageFolder = value; } }


        public String GorillaFile { get { return m_PathToGorillaFile; } set { m_PathToGorillaFile = value; } }
        public String Texture { get { return m_TexturePath; } set { m_TexturePath = value; } }
    }
}
