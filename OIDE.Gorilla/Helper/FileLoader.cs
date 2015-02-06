using System.IO;
using ADock.ViewModel.ProjVM;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using OIDE.Gorilla.Model;
using OIDE.Gorilla.Interface.Services;
using System.Windows.Shapes;
using System.Windows;

namespace OIDE.Gorilla.Helper
{
    public class FileLoader
    {
        private static int convertToInt(String intString)
        {
            Int32 tmp = 0;
            Int32.TryParse(intString, out tmp);
            return tmp;

        }
        private static float convertToFloat(String intString)
        {
            float tmp = 0;
            float.TryParse(intString, out tmp);
            return tmp;

        }
        public static void LoadFont(GorillaModel gorilla, StreamReader sr, String line)
        {
            FontModel font = new FontModel();


            font.size = convertToInt(line.Replace("[Font.", "").Replace("]", ""));

            while (!String.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (line.Contains("lineheight")) font.lineheight = convertToInt(line.Split(' ')[1]);
                else if (line.Contains("spacelength")) font.spacelength = convertToInt(line.Split(' ')[1]);
                else if (line.Contains("baseline")) font.baseline = convertToInt(line.Split(' ')[1]);
                else if (line.Contains("kerning") && !line.Contains("kerning_")) font.kerning = convertToFloat(line.Split(' ')[1]);
                else if (line.Contains("letterspacing")) font.letterspacing = convertToInt(line.Split(' ')[1]);
                else if (line.Contains("monowidth")) font.monowidth = convertToInt(line.Split(' ')[1]);
                else if (line.Contains("range"))
                {
                    String[] range = line.Split(' ');
                    font.rangeFrom = convertToInt(range[1]);
                    font.rangeTo = convertToInt(range[2]);
                }
                else if (line.Contains("glyph_"))
                {
                    String[] glyphString = line.Split(' ');
                    font.SetGlyph(convertToInt(glyphString[0].Split('_')[1]), new Glyph() { X = convertToInt(glyphString[1]), Y = convertToInt(glyphString[2]), width = convertToInt(glyphString[3]), height = convertToInt(glyphString[4]) });
                }
                else if (line.Contains("verticaloffset_"))
                {
                    String[] verticaloffset_String = line.Split(' ');
                    font.SetVerticalOffset(convertToInt(verticaloffset_String[0].Split('_')[1]), convertToInt(verticaloffset_String[1]));
                }
                else if (line.Contains("kerning_"))
                {
                    String[] kerning_String = line.Split(' ');
                    font.SetKerning(convertToInt(kerning_String[0].Split('_')[1]), new Kerning() { RightGlyphID = convertToInt(kerning_String[1]), KerningValue = convertToInt(kerning_String[2]) });
                }
                //glyph_33 0 0 8 21
                //verticaloffset_33 -11
                //            glyph_121 101 270 25 37
                //verticaloffset_121 -18
                //kerning_121 65 1
                //kerning_121 76 1
                //kerning_121 84 2
                //kerning_121 86 1


            }
            gorilla.Fonts.Add(font);
        }

        public static void LoadSprites(GorillaModel gorilla, StreamReader sr)
        {
            //todo

        }


        public static void LoadTexture(GorillaModel gorilla, StreamReader sr)
        {
            String line = null;
            while (!String.IsNullOrEmpty(line = sr.ReadLine()))
            {
                if (line.Contains("file"))
                {
                    gorilla.GorillaTexture.file = line;
                }
                else if (line.Contains("whitepixel"))
                {
                    String[] whitePixel = line.Split(' ');
                    gorilla.GorillaTexture.whitepixel = new Position() { x = convertToInt(whitePixel[1]), y = convertToInt(whitePixel[2]) };
                }


            }
            //[Texture]
            //file arial.png
            //whitepixel 34 22
        }


        public static void LoadGorillaFont(String filepath, GorillaModel gorilla)
        {
            filepath = @"D:\Projekte\coop\Build\arial.gorilla";
            // create reader & open file
            using (StreamReader sr = new StreamReader(filepath))
            {
                String line;

                while (!String.IsNullOrEmpty(line = sr.ReadLine()))
                {
                    Console.WriteLine(line);  // read a line of text

                    //load font data
                    if (line.Contains("[Font."))
                    {
                        LoadFont(gorilla, sr, line);
                    }
                    else if (line.Contains("[Sprites]"))
                    {
                        LoadSprites(gorilla, sr);
                    }
                    else if (line.Contains("[Texture]"))
                    {
                        LoadTexture(gorilla, sr);
                    }
                }

                // close the stream
                sr.Close();
            }
        }

        public static String GenerateGorillaCode(GorillaModel gorillaModel)
        {
            String code = "";
            try
            {
                // create a writer and open the file
                //using (StreamWriter writer = File.CreateText(filename))
                //{
                //    //Texture
                //    await writer.WriteLineAsync("[Texture]");
                //    await writer.WriteLineAsync("file " + gorillaModel.TextureName);
                //     await writer.WriteLineAsync("whitepixel 510 510");
                code += "[Texture]" + Environment.NewLine;
                code += "file " + gorillaModel.TextureName + Environment.NewLine;
                code += "whitepixel 510 510" + Environment.NewLine;

                //glyph_33 0 0 8 21
                //verticaloffset_33 -11
                //            glyph_121 101 270 25 37
                //verticaloffset_121 -18
                //kerning_121 65 1
                //kerning_121 76 1
                //kerning_121 84 2
                //kerning_121 86 1

                //fonts
                foreach (var font in gorillaModel.Fonts)
                {
                    code += "[Font." + font.size + "]" + Environment.NewLine;

                    foreach (var fontItem in font.Fonts)
                    {
                        if (fontItem.Glyph.X > 0)
                            code += "glyph_" + fontItem.Index + " " + fontItem.Glyph.X + " " + fontItem.Glyph.Y + " " + fontItem.Glyph.width + " " + fontItem.Glyph.height + Environment.NewLine;

                        if (fontItem.VerticalOffset > 0)
                            code += "verticaloffset_" + fontItem.Index + " " + fontItem.VerticalOffset + Environment.NewLine;

                        if (fontItem.Kerning.Any())
                        {
                            foreach (var kerning in fontItem.Kerning)
                            {
                                code += "kerning_" + fontItem.Index + " " + kerning.RightGlyphID + " " + kerning.KerningValue + Environment.NewLine;
                            }
                        }
                    }
                }
                //[Texture]
                //file dejavu.png
                //whitepixel 510 510
                //[Font.14]
                //lineheight 22
                //spacelength 6
                // [Sprites]
                //  mousepointer 54 464 15 22

                //sprites 
                //  await writer.WriteLineAsync("[Sprites]");
                code += "[Sprites]" + Environment.NewLine;
                foreach (var item in gorillaModel.GorillaItems.Where(x => x.GorillaType == GorillaType.Sprite))
                {
                    code += item.Name + " " + item.X + " " + item.Y + " " + item.Width + " " + item.Height + Environment.NewLine;
                    // await writer.WriteLineAsync(item.Name + " " + item.X + " " + item.Y + " " + item.Width + " " + item.Height);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            return code;

        }
    }
}
