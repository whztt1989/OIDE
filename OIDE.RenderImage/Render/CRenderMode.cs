using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CLXEditor.Render
{

    public class RenderMode
    {
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

        #region Render Timing Modes

        #region Loop Mode

        Thread renderLoopThread;

     

        private void LoopPriorityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (renderLoopThread != null)
            {
                //    object obj = LoopPriorityListBox.SelectedItem;
                //      renderLoopThread.Priority = (ThreadPriority)obj;
            }
        }

      

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

     

        #endregion

        #endregion

    }
}
