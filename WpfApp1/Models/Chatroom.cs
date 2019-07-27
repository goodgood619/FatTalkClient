using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GalaSoft.MvvmLight;
namespace WpfApp1.Models
{
    public class Chatroom :ViewModelBase
    {
        public int Chatnumber { get; set; }
        public string Chatroomname { get; set; }
        public Chatroom()
        {
            Chatnumber = 0;
            Chatroomname = string.Empty;
        }
        public Chatroom(int Chatnumber,string Chatroomname)
        {
            this.Chatnumber = Chatnumber;
            this.Chatroomname = Chatroomname;
        }
        public void Reset()
        {
            Chatnumber = 0;
            Chatroomname = string.Empty;
        }
    }
}
