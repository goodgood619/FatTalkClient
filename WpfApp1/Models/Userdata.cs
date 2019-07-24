using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using System.ComponentModel; //이게무엇인가?
namespace WpfApp1.Models
{
    public class Userdata :ViewModelBase
    {
            
        public int number { get; set; }
        public string id { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public Userdata()
        {
            number = 0;
            id = string.Empty;
            password = string.Empty;
            nickname = string.Empty;    
        }
        public Userdata(string id,string password,string nickname,int number)
        {
            this.id = id;
            this.password = password;
            this.nickname = nickname;
            this.number = number;
        }
        public void Reset()
        {
            id = string.Empty;
            password = string.Empty;
            nickname = string.Empty;
            number = 0;
        }
    }
}
