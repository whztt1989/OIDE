using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MogreInWpf;

namespace CLXEditor.Render
{
    /// <summary>
    /// RenderMode
    /// </summary>
    enum RMode
    {
        WPF,
        Loop,
        Frame        
    }

    public class RenderMode
    {
        private MogreImage mogreImageSource;
        private RMode mRMode;
        private Boolean mAutoUpdateViewportSize = true;
        private Grid mParentGrid;
        private Boolean mInitialized;

        public Boolean Initialized { get { return mInitialized; } }
        public static readonly Size FallbackViewportSize = new Size(800, 600);

        public Boolean AutoUpdateViewportSize { get { return mAutoUpdateViewportSize; } }

        public RenderMode(Grid parentGrid)
        {
            mParentGrid = parentGrid;   
        }

        public MogreImage init()
        {
            try
            {
            mogreImageSource = new MogreImage();
          
          
            

                mogreImageSource.InitOgreImage();
             
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;    
            }

#if HIT_TEST_EVERY_FRAME
            mogreImageSource.PostRender += new EventHandler(mogreImageSource_PostRender);
#endif
            mInitialized = true;

            return mogreImageSource;
        }


        public Size PreferredMogreViewportSize
        {
            get
            {
                if (mParentGrid.ActualHeight == 0 || mParentGrid.ActualWidth == 0)
                {
                    return FallbackViewportSize; // Doesn't seem to be used
                }
                return new Size(mParentGrid.ActualWidth, mParentGrid.ActualHeight);
            }
        }

        public void MogreImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mogreImageSource.ViewportSize = PreferredMogreViewportSize;
        }

        private void UpdateRenderTiming()
        {
            if (mogreImageSource == null) return;

            string mode = "WPF";// SelectedRenderTimingMode;

      //      IsTimingLoopMode = mode == "Loop";
      //      IsTimingTimerMode = mode == "Timer";

         //   UpdateMogreImageTimerInterval();
        }

        #region Render Timing Modes

        #region Loop Mode

        Thread renderLoopThread;

        //private bool IsTimingLoopMode
        //{
        //    set
        //    {
        //        if (isTimingLoopMode == value) return;
        //        isTimingLoopMode = value;

        //        mogreImageSource.ManualRender = value;

        //        if (value)
        //        {
        //            renderLoopThread = new Thread(new ThreadStart(() => RenderLoop()))
        //            {
        //                Name = "Manual Render Loop Thread",
        //                Priority = ThreadPriority.Normal,
        //            };
        //            renderLoopThread.Start();
        //        }
        //        else
        //        {
        //            renderLoopThread = null;
        //        }
        //    }
        //} private bool isTimingLoopMode;

        private void LoopPriorityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (renderLoopThread != null)
            {
                //    object obj = LoopPriorityListBox.SelectedItem;
                //      renderLoopThread.Priority = (ThreadPriority)obj;
            }
        }

        //private void RenderLoop()
        //{
        //    var action = new Action(mogreImageSource.RenderFrame);

        //    while (isTimingLoopMode)
        //    {
               
        //      //  Dispatcher.Invoke(action);
        //        //mogreImageSource.RenderFrame();
        //    }
        //}

        #endregion

        #region Timer Mode

        private bool IsTimingTimerMode
        {
            set 
            {
                if (isTimingTimerMode == value) return;
                isTimingTimerMode = value;
            }
        } private bool isTimingTimerMode;

        //private void UpdateMogreImageTimerInterval()
        //{
        //    if (mogreImageSource == null) return;

        //    string mode = "WPF";// SelectedRenderTimingMode;
        //    if (mode == "WPF")
        //    {
        //        mogreImageSource.FrameRate = null;
        //        return;
        //    }

        //    int frameRate;
        //    try
        //    {
        //        frameRate = 60;//Convert.ToInt32(TimerRateTextBox.Text);
        //        if (frameRate == 0) throw new Exception("Framerate cannot be zero");
        //    }
        //    catch (Exception)
        //    {
        //        mogreImageSource.FrameRate = 1;
        //        return;
        //    }

        //    mogreImageSource.FrameRate = frameRate;
        //}

        #endregion

        #endregion

#if HIT_TEST_EVERY_FRAME
        void mogreImageSource_PostRender(object sender, EventArgs e)
        {
            UpdateHitTest();
        }
#endif

    }
}
