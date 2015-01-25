using System.IO;
using ADock.ViewModel.ProjVM;
using System;

namespace CLGorilla.Common
{
    class COGorilla
    {
        public void GenerateGorillaFile()
        {
            try
            {
                // create a writer and open the file
                TextWriter tw = null;

                foreach (var item in ADock.Workspace.This.VMTV.List)
                {
                    if (item is CVMProject)
                    {
                        CVMProject project = item as CVMProject;

                        //-------------------------
                        //Project
                        //-------------------------
                        foreach (var projItem in project.Items)
                        {
                            if (projItem.Name.Equals("UI"))
                            {
                                CVMCategory uiCategory = projItem as CVMCategory;
                                foreach (var UIItem in uiCategory.Items)
                                {
                                    //------------------------------
                                    //Gorilla File
                                    //------------------------------
                                    if (UIItem is CVMGorilla)
                                    {
                                        CVMGorilla gorillaItem = UIItem as CVMGorilla;
                                        tw = new StreamWriter(gorillaItem.FilePath);

                                        tw.WriteLine("[Header]");
                                        tw.WriteLine("file.gorilla...");
                                    }
                                    //------------------------------
                                    //Category
                                    //------------------------------
                                    else if (UIItem is CVMCategory)
                                    {
                                        CVMCategory category = UIItem as CVMCategory;
                                       //    if (tw == null)
                                           //         break; // Error Gorillfile not set!


                                           if (category.Category == CategoryType.Image)
                                           {
                                               tw.WriteLine("[Sprites]");
                                           }

                                           //-------------------------
                                           // Elements
                                           //-------------------------
                                           foreach (var catItem in category.Items)
                                           {
                                               //-------------------------
                                               //Image
                                               //-------------------------
                                               if (catItem is CVMImage)
                                               {
                                                   CVMImage image = catItem as CVMImage;
                                                   tw.WriteLine(image.FilePath);
                                               }
                                               //-------------------------
                                               //Font
                                               //-------------------------
                                               else if (category.Category == CategoryType.Font)
                                               {
                                                   CVMFont font = catItem as CVMFont;

                                                   tw.WriteLine("[Font.18]");
                                                   //foreach (var fontLetter in font.Letters)
                                                   //{
                                                   //    tw.WriteLine("ffffff");
                                                   //}
                                               }
                                           }
                                       
                                    }
                                }
                            }
                        }
                    }
                }

                // close the stream
                tw.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
