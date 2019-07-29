using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using GalaSoft.MvvmLight;
namespace WpfApp1.Models
{
    public class Frienddata :ViewModelBase
    {
        public string Id { get; set; }
        public string nickname { get; set; }
        public List<Friendlist> friendlists { get; set; }
        public Frienddata() : this(string.Empty, string.Empty, new List<Friendlist>())
        {

        }
        public Frienddata(string Id,string nickname,List<Friendlist> friendlists)
        {
            this.Id = Id;
            this.nickname = nickname;
            this.friendlists = friendlists;
        }

        public void Reset()
        {
            Id = string.Empty;
            nickname = string.Empty;
            friendlists.RemoveRange(0, friendlists.Count);
        }
    }
    public class Friendlist
    {
        public string FId { get; set; }
        public string Fnickname { get; set; }
        public int Connectin { get; set; }

    }
}
