using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIDE.VFS.VFS_Types.zip
{
    public class VFS_Zip
    {
        //Example 1
        public static void SimpleZip(string dirToZip, string zipName)
        {
            ZipFile.CreateFromDirectory(dirToZip, zipName);
        }

        //Example 2
        public static void SimpleZip(string dirToZip,
                              string zipName,
                              CompressionLevel compression,
                              bool includeRoot)
        {
            ZipFile.CreateFromDirectory(dirToZip, zipName, compression, includeRoot);
        }

        //Example 3
        public static void SimpleUnzip(string zipName, string dirToUnzipTo)
        {
            ZipFile.ExtractToDirectory(zipName, dirToUnzipTo);
        }

        //Example 4
        public static void SmarterUnzip(string zipName, string dirToUnzipTo)
        {
            //This stores the path where the file should be unzipped to,
            //including any subfolders that the file was originally in.
            string fileUnzipFullPath;

            //This is the full name of the destination file including
            //the path
            string fileUnzipFullName;

            //Opens the zip file up to be read
            using (ZipArchive archive = ZipFile.OpenRead(zipName))
            {
                //Loops through each file in the zip file
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    //Outputs relevant file information to the console
                    Console.WriteLine("File Name: {0}", file.Name);
                    Console.WriteLine("File Size: {0} bytes", file.Length);
                    Console.WriteLine("Compression Ratio: {0}", ((double)file.CompressedLength / file.Length).ToString("0.0%"));

                    //Identifies the destination file name and path
                    fileUnzipFullName = Path.Combine(dirToUnzipTo, file.FullName);

                    //Extracts the files to the output folder in a safer manner
                    if (!System.IO.File.Exists(fileUnzipFullName))
                    {
                        //Calculates what the new full path for the unzipped file should be
                        fileUnzipFullPath = Path.GetDirectoryName(fileUnzipFullName);

                        //Creates the directory (if it doesn't exist) for the new path
                        Directory.CreateDirectory(fileUnzipFullPath);

                        //Extracts the file to (potentially new) path
                        file.ExtractToFile(fileUnzipFullName);
                    }
                }
            }
        }

        public static void ManuallyCreateZipFile(string zipName)
        {
            //Creates a new, blank zip file to work with - the file will be
            //finalized when the using statement completes
            using (ZipArchive newFile = ZipFile.Open(zipName, ZipArchiveMode.Create))
            {
                //Here are two hard-coded files that we will be adding to the zip
                //file.  If you don't have these files in your system, this will
                //fail.  Either create them or change the file names.
                newFile.CreateEntryFromFile(@"C:\Temp\File1.txt", "File1.txt");
                newFile.CreateEntryFromFile(@"C:\Temp\File2.txt", "File2.txt", CompressionLevel.Fastest);
            }
        }

        public static void ManuallyUpdateZipFile(string zipName)
        {
            //Opens the existing file like we opened the new file (just changed
            //the ZipArchiveMode to Update
            using (ZipArchive modFile = ZipFile.Open(zipName, ZipArchiveMode.Update))
            {
                //Here are two hard-coded files that we will be adding to the zip
                //file.  If you don't have these files in your system, this will
                //fail.  Either create them or change the file names.  Also, note
                //that their names are changed when they are put into the zip file.
                modFile.CreateEntryFromFile(@"C:\Temp\File1.txt", "File10.txt");
                modFile.CreateEntryFromFile(@"C:\Temp\File2.txt", "File20.txt", CompressionLevel.Fastest);

                //We could also add the code from Example 4 here to read
                //the contents of the open zip file as well.
            }
        }

        //Example 7
        public static void CallingImprovedExtractToDirectory(string zipName, string dirToUnzipTo)
        {
            //This performs a similar function to Example 3, only now we are doing it
            //safely (we won't crash because of predictable and preventable errors). 
            //The result is something you don't have to think about - it just works.
            Compression.ImprovedExtractToDirectory(zipName, dirToUnzipTo, Compression.Overwrite.IfNewer);
        }

        //Example 8
        public static void CallingImprovedExtractToFile(string zipName, string dirToUnzipTo)
        {
            //Opens the zip file up to be read
            using (ZipArchive archive = ZipFile.OpenRead(zipName))
            {
                //Loops through each file in the zip file
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    //Outputs relevant file information to the console
                    Console.WriteLine("File Name: {0}", file.Name);
                    Console.WriteLine("File Size: {0} bytes", file.Length);
                    Console.WriteLine("Compression Ratio: {0}", ((double)file.CompressedLength / file.Length).ToString("0.0%"));

                    //This is the new call
                    Compression.ImprovedExtractToFile(file, dirToUnzipTo, Compression.Overwrite.Always);
                }
            }
        }

        //Example 9
        public static void CallingAddToArchive(string zipName)
        {
            //This creates our list of files to be added
            List<string> filesToArchive = new List<string>();

            //Here we are adding two hard-coded files to our list
            filesToArchive.Add(@"C:\Temp\File1.txt");
            filesToArchive.Add(@"C:\Temp\File2.txt");

            Compression.AddToArchive(zipName,
                filesToArchive,
                Compression.ArchiveAction.Replace,
                Compression.Overwrite.IfNewer,
                CompressionLevel.Optimal);
        }
    }
}
