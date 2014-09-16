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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using OIDE.InteropEditor.DLL;

namespace OIDE.RenderImage
{
    public partial class RenderPanel : System.Windows.Forms.UserControl
    {
        uint TIME_INTERVAL_IN_MILLISECONDS = 1;
  
       // private System.Threading.Timer _timer;

        public RenderPanel()
        {
            InitializeComponent();

            //_timer = new Timer(Callback, null, TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);

         //   System.Threading.Timer RenderTimer = new System.Threading.Timer(new System.Threading.TimerCallback(timer1_Tick));
          //  RenderTimer.Change(1, 1);
        }

        //private void Callback(Object state)
        //{
        //    Stopwatch watch = new Stopwatch();

        //    watch.Start();
        //    // Long running operation

        //    _timer.Change(Math.Max(0, TIME_INTERVAL_IN_MILLISECONDS - watch.ElapsedMilliseconds), Timeout.Infinite);
        //}

        //private void Callback(Object state)
        //{
        //    // Long running operation
        //    _timer.Change(TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);
        //}

        public void Start()
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //DLL_Singleton.stateUpdate();

          bool res =   DLL_Singleton.Instance.stateUpdate();
        }
    }
}
