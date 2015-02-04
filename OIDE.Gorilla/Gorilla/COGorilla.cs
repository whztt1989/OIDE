using System.IO;
using ADock.ViewModel.ProjVM;
using System;
using System.Collections.ObjectModel;
using OIDE.Gorilla.Interface.Services;
using System.Linq;

namespace CLGorilla.Common
{
    public class COGorilla
    {
        public static String GenerateGorillaCode(IGorilla gorillaModel)
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

                    //fonts
                    foreach (var item in gorillaModel.GorillaItems.Where(x => x.GorillaType == GorillaType.Font))
                    {
                        if (item.GorillaType == GorillaType.Sprite)
                        {

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
                        if (item.GorillaType == GorillaType.Sprite)
                        {
                        code += item.Name + " " + item.X + " " + item.Y + " " + item.Width + " " + item.Height + Environment.NewLine;
              // await writer.WriteLineAsync(item.Name + " " + item.X + " " + item.Y + " " + item.Width + " " + item.Height);
                        }
                    }

            //    }

                //if (projItem.Name.Equals("UI"))
                //{
                //    CVMCategory uiCategory = projItem as CVMCategory;
                //    foreach (var UIItem in uiCategory.Items)
                //    {
                //        //------------------------------
                //        //Gorilla File
                //        //------------------------------
                //        if (UIItem is CVMGorilla)
                //        {
                //            CVMGorilla gorillaItem = UIItem as CVMGorilla;
                //            tw = new StreamWriter(gorillaItem.FilePath);

                //            tw.WriteLine("[Header]");
                //            tw.WriteLine("file.gorilla...");
                //        }
                //        //------------------------------
                //        //Category
                //        //------------------------------
                //        else if (UIItem is CVMCategory)
                //        {
                //            CVMCategory category = UIItem as CVMCategory;
                //           //    if (tw == null)
                //               //         break; // Error Gorillfile not set!


                //               if (category.Category == CategoryType.Image)
                //               {
                //                   tw.WriteLine("[Sprites]");
                //               }

                //               //-------------------------
                //               // Elements
                //               //-------------------------
                //               foreach (var catItem in category.Items)
                //               {
                //                   //-------------------------
                //                   //Image
                //                   //-------------------------
                //                   if (catItem is CVMImage)
                //                   {
                //                       CVMImage image = catItem as CVMImage;
                //                       tw.WriteLine(image.FilePath);
                //                   }
                //                   //-------------------------
                //                   //Font
                //                   //-------------------------
                //                   else if (category.Category == CategoryType.Font)
                //                   {
                //                       CVMFont font = catItem as CVMFont;

                //                       tw.WriteLine("[Font.18]");
                //                       //foreach (var fontLetter in font.Letters)
                //                       //{
                //                       //    tw.WriteLine("ffffff");
                //                       //}
                //                   }
                //               }

                //        }
                //    }
                //}


                // close the stream
                // tw.Close();
            }
            catch (Exception ex)
            {

            }
            return code;
      
        }
    }
}
