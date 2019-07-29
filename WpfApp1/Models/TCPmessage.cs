using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class TCPmessage
    {
        public Command Command { get; set;}
        public int check { get; set; }
        public string message { get; set;}
        public int Usernumber { get; set; }
        public TCPmessage()
        {
            Command = Command.Null;
            check = 0;
            message = string.Empty;
            Usernumber = 0;
        }

        public TCPmessage(byte[] data)
        {
            Command = (Command)BitConverter.ToInt32(data,0);
            check = BitConverter.ToInt32(data, 4);
            Usernumber = BitConverter.ToInt32(data, 8);
            int mlength = BitConverter.ToInt32(data,12);
            if (mlength > 0)
            {
                message = Encoding.Unicode.GetString(data, 16, mlength);
            }

        }
        public byte[] tobytedata()
        {
            List<byte> bytedata = new List<byte>();
            bytedata.AddRange(BitConverter.GetBytes((int)Command));
            bytedata.AddRange(BitConverter.GetBytes(check));
            bytedata.AddRange(BitConverter.GetBytes((int)Usernumber));
            bytedata.AddRange(BitConverter.GetBytes(Encoding.Unicode.GetByteCount(message)));
            bytedata.AddRange(Encoding.Unicode.GetBytes(message));
            return bytedata.ToArray();
        }
    }
    
    public enum Command
    {
        Null,
        login,
        logout,
        Join,
        Idcheck,
        Findid,
        Makechat, 
        Outchat,
        Joinchat,
        Refresh,
        Plusfriend,
        Removefriend,
        Sendchat,
        Nicknamecheck
    }
}
