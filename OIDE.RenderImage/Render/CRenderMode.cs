#region License

//The MIT License (MIT)

//Copyright (c) 2014 Konrad Huber

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion

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
