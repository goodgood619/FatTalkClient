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
        public Chatdata()
        {
            
            Chatmessage = string.Empty;
            Sendnickname = string.Empty;
            Chatnumber = 0;
        }
        public Chatdata(string Chatmessage,string Sendnickname,int Chatnumber)
        {
            this.Chatmessage = Chatmessage;
            this.Sendnickname = Sendnickname;
            this.Chatnumber = Chatnumber;
        }
        public void reset()
        {
            Chatmessage = string.Empty;
            Sendnickname = string.Empty;
            Chatnumber = 0;
        }
    }
}
