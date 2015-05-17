#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
//using MarkdownSharp;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Threading;
using OIDE.SocketServer.SocketServer;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace OIDE.SocketServer
{
    /// <summary>
    /// Interaction logic for SocketServerView.xaml
    /// </summary>
    public partial class SocketServerView : IContentView
    {
        private TcpListener serverSocket;
        //    private HandleClient[] hClients;
        private List<HandleClient> hClients = new List<HandleClient>();
        private int counter;
        //  private int iMaxClients;
        private Thread t;
        private bool bServerRun;
     //   private Markdown _md;
        private IStatusbarService _statusbar;

        public SocketServerView(IStatusbarService statusbar)
        {
          //  _md = new Markdown();
            this._statusbar = statusbar;
            InitializeComponent();
            //##     textEditor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void textEditor_TextChanged(object sender, EventArgs e)
        {
            var model = this.DataContext as SocketServerModel;
            if (model != null)
            {
           //     model.SetHtml(_md.Transform(textEditor.Text));
            }
        }

        private void Update()
        {
            //##     _statusbar.LineNumber = textEditor.Document.GetLineByOffset(textEditor.CaretOffset).LineNumber;
            //##     _statusbar.ColPosition = textEditor.TextArea.Caret.VisualColumn + 1;
            //##     _statusbar.CharPosition = textEditor.CaretOffset;
            //##     _statusbar.InsertMode = false;
            if (t == null || !t.IsAlive)
                Run();
        }

        private void Run()
        {
            t = new Thread(SimpleRun);
            t.Start();
        }

        private void SimpleRun(object obj)
        {
            uint i = 0;
            while (i < 1000)
            {
                _statusbar.Progress(true, i, 1000);
                Thread.Sleep(10);
                i++;
            }
            _statusbar.Progress(false, i, 1000);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bServerRun = true;
            t = new Thread(new ThreadStart(fServerStart));
            t.Start();

            //if (m_AsynchronousSocketListener == null)
            //    m_AsynchronousSocketListener = new AsynchronousSocketListener();

            //m_AsynchronousSocketListener.StartListening();
        }

        private void btnSendToClient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
         //   hClients[Convert.ToInt32(lbClients.SelectedItem)].fWriteToStream(cbMsg.SelectedIndex);
        }


        void fServerStart()
        {
            //SSL
            //X509Certificate serverCertificate = null;
            //serverCertificate = X509Certificate.CreateFromCertFile(@"./test.cer");

        //    PS_System.CData oData = new PS_System.CData();

            //   sDBData.Host = oData.fGetServerIP();//"SOFTDEV03\\SQLEXPRESS";//"TEST";//
            IPAddress localAddr = IPAddress.Parse(tbServerIP.Text);//oData.fGetServerIP()); //"192.168.115.208");
            TcpClient clientSocket = default(TcpClient);

            serverSocket = new TcpListener(localAddr, 13000);
            counter = 0;
            try
            {
                serverSocket.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler, der Server wurde bereits gestartet:", ex.Message);
                serverSocket.Stop();
                return;
            }
            //this.Invoke((MethodInvoker)delegate
            //{
            //    lbLog.Items.Add(" >> " + "Server Zertifikat: ./test.cer\t\n");
            //    lbLog.Items.Add(" >> " + "Server:" + tbServerIP.Text + " Port: 13000\t\n");
            //    lbLog.Items.Add(" >> " + "Server gestartet..\t\n");
            //});

            // hClients = new HandleClient[10];         

            counter = 0;
            Console.WriteLine("ServerLoop startet....");
            while (bServerRun)
            {
                try
                {
                    clientSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine("Client IP:" + clientSocket.Client.RemoteEndPoint);
                    // SetControlPropertyThreadSafe(lbLog, "Items.Add", " >> " + "Client Nr:" + Convert.ToString(counter) + " eingeloggt \t\n");
                    /*   this.Invoke((MethodInvoker)delegate
                       {
                           lbLog.Items.Add(" >> " + "Client Nr:" + Convert.ToString(counter) + " eingeloggt \t\n");
                           lbLog.Items.Add(" >> " + "Client IP:" + clientSocket.Client.RemoteEndPoint + " \t\n");
                       });*/
                }
                catch (SocketException socketEx)
                {
                    Console.WriteLine("Socket Fehler:" + socketEx.Message);
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine("IO Fehler:" + ioEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler:" + ex.Message);
                }

                hClients.Insert(counter, new HandleClient(counter));
                hClients[counter].ClientSocket = clientSocket;
                hClients[counter].startClient(Convert.ToString(counter));

                counter += 1;
            }

            Console.WriteLine("ServerLoop beendet....");
            serverSocket.Stop(); //Listener stoppen
        }

        /// <summary>
        /// Gibt den Client handler zurück. Ermittelt wird dieser per IP
        /// </summary>
        /// <param name="pClientIP">Client IP</param>
        /// <returns>HandleClient</returns>
        public HandleClient fGetClientHandlePIP(String pClientIP)
        {
            foreach (HandleClient hClient in hClients)
            {
                String[] temp = hClient.oClient.IP.Split(':');

                if (temp[0].Equals(pClientIP)) return hClient;
            }
            return null;
        }

        private void btnServerStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                bServerRun = false;
                serverSocket.Stop();
                t.Abort();
                foreach (HandleClient client in hClients)
                {
                    client.StopClient();
                }
                   
                Console.WriteLine("Server beendet");
            }
            catch
            {

            }
        }
    }
}