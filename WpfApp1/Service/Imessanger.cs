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
    public interface Imessanger
    {
        MessengerClient GetMessenger(Action<TCPmessage> action);
        ObservableCollection<Frienddata> frienddatas();

    }
}
