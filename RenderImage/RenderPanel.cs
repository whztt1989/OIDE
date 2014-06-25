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
using PInvokeWrapper.DLL;

namespace RenderImage
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

            DLL_Singleton.Instance.stateUpdate();
        }
    }
}
