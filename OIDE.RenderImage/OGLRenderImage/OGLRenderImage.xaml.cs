#region license
/*
 * code based on  SharpGL
 * https://github.com/dwmkerr/sharpgl
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2014 Dave Kerr
 * 
 * 
 * modified by Konrad Huber 
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using OIDE.InteropEditor.DLL;

namespace OIDE.RenderImage.OGLRenderImage
{
    /// <summary>
    /// Interaktionslogik für OGLRenderImage.xaml
    /// </summary>
    public partial class OGLRenderImage : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLControl"/> class.
        /// </summary>
        public OGLRenderImage()
        {
            InitializeComponent();

        }

        public void init()
        {
            timer = new DispatcherTimer();

            Unloaded += OpenGLControl_Unloaded;
            Loaded += OpenGLControl_Loaded;
        }

        /// <summary>
        /// Handles the Loaded event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
        private void OpenGLControl_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SizeChanged += OpenGLControl_SizeChanged;

            UpdateOpenGLControl((int)RenderSize.Width, (int)RenderSize.Height);

            //  DispatcherTimer setup
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            int w = (int)this.image.ActualWidth;
            int h = (int)this.image.ActualHeight;

            IntPtr ddbackbuffer = DLL_Singleton.Instance.stateInit("", w, h);  // DDBACKBUFFER

            //##    this.image.Source = ddbackbuffer;

            timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000.0 / FrameRate));
        }

        /// <summary>
        /// Handles the Unloaded event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
        private void OpenGLControl_Unloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SizeChanged -= OpenGLControl_SizeChanged;

            timer.Stop();
            timer.Tick -= timer_Tick;
        }

        /// <summary>
        /// Handles the SizeChanged event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> Instance containing the event data.</param>
        void OpenGLControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateOpenGLControl((int)e.NewSize.Width, (int)e.NewSize.Height);
        }

        /// <summary>
        /// This method is used to set the dimensions and the viewport of the opengl control.
        /// </summary>
        /// <param name="width">The width of the OpenGL drawing area.</param>
        /// <param name="height">The height of the OpenGL drawing area.</param>
        private void UpdateOpenGLControl(int width, int height)
        {
            SizeChangedEventArgs e;

            DLL_Singleton.Instance.RenderTargetSize("", width, height);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            //  Call the base.
            base.OnApplyTemplate();
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timer_Tick(object sender, EventArgs e)
        {

            //  Start the stopwatch so that we can time the rendering.
            stopwatch.Restart();


            var hBitmap = DLL_Singleton.Instance.RenderOnceTexturePtr("", (int)RenderSize.Width, (int)RenderSize.Height);

            if (hBitmap != IntPtr.Zero)
            {
                var newFormatedBitmapSource = GetFormatedBitmapSource(hBitmap);

                //  Copy the pixels over.
                image.Source = newFormatedBitmapSource;
            }

            //  Stop the stopwatch.
            stopwatch.Stop();

            //  Store the frame time.
            frameTime = stopwatch.Elapsed.TotalMilliseconds;

        }

        /// <summary>
        /// This method converts the output from the OpenGL render context provider to a 
        /// FormatConvertedBitmap in order to show it in the image.
        /// </summary>
        /// <param name="hBitmap">The handle of the bitmap from the OpenGL render context.</param>
        /// <returns>Returns the new format converted bitmap.</returns>
        private static FormatConvertedBitmap GetFormatedBitmapSource(IntPtr hBitmap)
        {
            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
            //  meaning the drawing comes out transparent.

            FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(hBitmap);
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            newFormatedBitmapSource.EndInit();

            return newFormatedBitmapSource;
        }

        /// <summary>
        /// Called when the frame rate is changed.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnFrameRateChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            //  Get the control.
            OGLRenderImage me = o as OGLRenderImage;

            //  If we have the timer, set the time.
            if (me.timer != null)
            {
                //  Stop the timer.
                me.timer.Stop();

                //  Set the timer.
                me.timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000f / me.FrameRate));

                //  Start the timer.
                me.timer.Start();
            }
        }


        /// <summary>
        /// The dispatcher timer.
        /// </summary>
        DispatcherTimer timer = null;

        /// <summary>
        /// A stopwatch used for timing rendering.
        /// </summary>
        protected Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// The last frame time in milliseconds.
        /// </summary>
        protected double frameTime = 0;


        /// <summary>
        /// The frame rate dependency property.
        /// </summary>
        private static readonly DependencyProperty FrameRateProperty =
          DependencyProperty.Register("FrameRate", typeof(double), typeof(OGLRenderImage),
          new PropertyMetadata(28.0, new PropertyChangedCallback(OnFrameRateChanged)));

        /// <summary>
        /// Gets or sets the frame rate.
        /// </summary>
        /// <value>The frame rate.</value>
        public double FrameRate
        {
            get { return (double)GetValue(FrameRateProperty); }
            set { SetValue(FrameRateProperty, value); }
        }


        /// <summary>
        /// The DrawFPS property.
        /// </summary>
        private static readonly DependencyProperty DrawFPSProperty =
          DependencyProperty.Register("DrawFPS", typeof(bool), typeof(OGLRenderImage),
          new PropertyMetadata(false, null));

        /// <summary>
        /// Gets or sets a value indicating whether to draw FPS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if draw FPS; otherwise, <c>false</c>.
        /// </value>
        public bool DrawFPS
        {
            get { return (bool)GetValue(DrawFPSProperty); }
            set { SetValue(DrawFPSProperty, value); }
        }
    }
}
