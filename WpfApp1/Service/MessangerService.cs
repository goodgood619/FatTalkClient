using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Modules;
using System.Collections.ObjectModel;
namespace WpfApp1.Service
{
    public class MessangerService : Imessanger
    {
        private MessengerClient messenger;
        private ObservableCollection<Frienddata> Friendlist;
        public MessangerService()
        {
            messenger = new MessengerClient();
            Friendlist = new ObservableCollection<Frienddata>();
        }
        public MessengerClient GetMessenger(Action<TCPmessage> execute)
        {
            messenger.domessage += execute;
            return messenger;
        }
        public ObservableCollection<Frienddata> frienddatas()
        {

            return Friendlist;
        }
    }
}
