using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIDE.SocketServer.SocketServer
{
    public class SocketClient
    {
        String sIP;    //ClientIP
        int iClientID; //ClientID im Server
        int iStatusID;

        public int ClientID
        {
            get { return iClientID; }
            set { iClientID = value; }
        }

        public String IP
        {
            get { return sIP; }
            set { sIP = value; }
        }
    }
}
