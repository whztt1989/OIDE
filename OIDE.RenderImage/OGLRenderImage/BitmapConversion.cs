﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OIDE.RenderImage.OGLRenderImage
{
    /// <summary>
    /// This class handles conversion to and from various bitmap types.
    /// </summary>
    public static class BitmapConversion
    {
        static BitmapConversion()
        {
            const string gcCyclesCount = "MaxCyclesBeforeGC";
            const int defaultCyclesCount = 10;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(gcCyclesCount))
            {
                if (int.TryParse(ConfigurationManager.AppSettings[gcCyclesCount], out MaxCyclesBeforeGc) == false)
                    MaxCyclesBeforeGc = defaultCyclesCount;
            }
            else
            {
                MaxCyclesBeforeGc = defaultCyclesCount;
            }

        }
        private static int _gcCycles;
        private static readonly int MaxCyclesBeforeGc;


        /// <summary>
        /// Converts an HBitmap the bitmap to a bitmap source.
        /// </summary>
        /// <param name="hBitmap">The hbitmap.</param>
        /// <returns>A BitmapSource.</returns>
        public static BitmapSource HBitmapToBitmapSource(IntPtr hBitmap)
        {
            BitmapSource bitSrc = null;

            try
            {
                if (hBitmap != IntPtr.Zero)
                {

                    bitSrc = Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {
                Win32.DeleteObject(hBitmap);

                if (_gcCycles > MaxCyclesBeforeGc)
                {
                    GC.Collect();
                    _gcCycles = 0;
                }
                ++_gcCycles;
            }

            return bitSrc;
        }


        /// <summary>
        /// Convert a DIB section to a BitmapSource.
        /// </summary>
        /// <param name="dibSection">The dib section.</param>
        /// <returns>The BitmapSource.</returns>
        public static BitmapSource DIBSectionToBitmapSource(DIBSection dibSection)
        {
            BitmapSource bitSrc = null;

            try
            {
                bitSrc = Imaging.CreateBitmapSourceFromMemorySection(dibSection.Bits,
                    dibSection.Width, dibSection.Height, PixelFormats.Bgra32, dibSection.Width * 4, 0);
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }

            return bitSrc;
        }
    }
}
