using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using GalaSoft.MvvmLight;
namespace WpfApp1.Models
{
    public class Chatdata : ViewModelBase
    {
        public string id { get; set; }
        public string nickname { get; set; }
        public string chatmessage { get; set; }
        public int Chatnumber { get; set; }
        public Chatdata()
        {
            id = string.Empty;
            nickname = string.Empty;
            chatmessage = string.Empty;
            Chatnumber = 0;
        }
        public Chatdata(string id,string nickname,string chatmessage,int Chatnumber)
        {
            this.id = id;
            this.nickname = nickname;
            this.chatmessage = chatmessage;
            this.Chatnumber = Chatnumber;
        }
        public void reset()
        {
            id = string.Empty;
            nickname = string.Empty;
            chatmessage = string.Empty;
            Chatnumber = 0;
        }
    }
}
