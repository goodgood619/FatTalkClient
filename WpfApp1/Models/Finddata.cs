using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using GalaSoft.MvvmLight;
namespace WpfApp1.Models
{
    public class Finddata :ViewModelBase
    {
        public string Findid { get; set; }
        public string Findpassword { get; set; }
        public Finddata() : this(string.Empty,string.Empty)
        {

        }
        public Finddata(string Findid,string Findpassword)
        {
            this.Findid = Findid;
            this.Findpassword = Findpassword;
        }

        public void Reset()
        {
            Findid = string.Empty;
            Findpassword = string.Empty;
        }
    }
}
