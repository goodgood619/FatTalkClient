using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using WpfApp1.Models;
using WpfApp1.Modules;
namespace WpfApp1.Modules
{
  // test this
    public abstract class Tcpclient
    {
        public abstract void ResponseMessage(TCPmessage tcpmessage);
        private Socket client = null;
        private byte[] data;
        public Tcpclient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            data = new byte[32000];
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("0.0.0.0"),33212); // ServerIp
            client.BeginConnect(ep, connect_callback, null);
        }

        private void connect_callback(IAsyncResult ar)
        {
            client.EndConnect(ar);
            client.BeginReceive(data, 0, data.Length, 0, receive_callback,null);
        }

        private void receive_callback(IAsyncResult ar)
        {
            try
            {
                client.EndReceive(ar);
                TCPmessage receivemessage = new TCPmessage(data);
                ResponseMessage(receivemessage);
                client.BeginReceive(data, 0, data.Length, 0, receive_callback, null);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected bool Send(TCPmessage tcpmessage)
        {
            bool ok = false;
            try
            {
                byte[] bytesend = tcpmessage.tobytedata();
                client.BeginSend(bytesend, 0, bytesend.Length, 0, send_callback, null);
                ok = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return ok;
        }
        
        private void send_callback(IAsyncResult ar)
        {
            try
            {
                
                client.EndSend(ar);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
