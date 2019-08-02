using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using GalaSoft.MvvmLight;
namespace WpfApp1.Models
{
    public class Frienddata : ViewModelBase
    {
        public string Fnickname { get; set; }
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                this.RaisePropertyChanged("IsSelected");
            }
        }
        public Frienddata() : this(string.Empty)
        {

        }


        public Frienddata(string Fnickname)
        {
            this.Fnickname = Fnickname;
            //this.friendlists = friendlists;

        }

        public void Reset()
        {
            Fnickname = string.Empty;
            //friendlists.RemoveRange(0, friendlists.Count);
        }
    }

}
