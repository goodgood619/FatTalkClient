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
        public MessangerService()
        {
            messenger = new MessengerClient();
        }
        public MessengerClient GetMessenger(Action<TCPmessage> execute)
        {
            messenger.domessage += execute;
            return messenger;
        }
        
    }
}
