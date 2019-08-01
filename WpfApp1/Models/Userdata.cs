using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using System.ComponentModel; //이게무엇인가?
namespace WpfApp1.Models
{
    public class Userdata : ViewModelBase
    {

        public int Usernumber { get; set; }
        public string id { get; set; }
        public string nickname { get; set; }
        public Userdata()
        {
            Usernumber = 0;
            id = string.Empty;
            nickname = string.Empty;
        }
        public Userdata(string id, string nickname,string pasword, int Usernumber)
        {
            this.id = id;
            this.nickname = nickname;
            this.Usernumber = Usernumber;
        }
        public void Reset()
        {
            id = string.Empty;
            nickname = string.Empty;
            Usernumber = 0;
        }
    }
}
