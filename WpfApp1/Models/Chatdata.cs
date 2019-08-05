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
        public string Chatmessage { get; set; }
        public string Sendnickname { get; set; }
        public int Chatnumber { get; set; }
        public List<string> Userlist { get; set; }
        public Chatdata()
        {
            
            Chatmessage = string.Empty;
            Sendnickname = string.Empty;
            Chatnumber = 0;
            Userlist = new List<string>();
        }
        public Chatdata(string Chatmessage,string Sendnickname,int Chatnumber,List<string> Userlist)
        {
            this.Chatmessage = Chatmessage;
            this.Sendnickname = Sendnickname;
            this.Chatnumber = Chatnumber;
            this.Userlist = Userlist;
        }
        public void reset()
        {
            Chatmessage = string.Empty;
            Sendnickname = string.Empty;
            Chatnumber = 0;
            Userlist.RemoveRange(0, Userlist.Count);
        }
    }
}
