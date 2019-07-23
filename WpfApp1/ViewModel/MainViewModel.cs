using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; //Icommand 만들기 관련

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using WpfApp1.Models;
using WpfApp1.Modules;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        public MessengerClient messengerClient { get; set; }

        public MainViewModel(Imessanger imessanger)
        {
            messengerClient = imessanger.GetMessenger(ResponseMessage);

        }
        public void closeWindow()
        {
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
                    }
                }
            });
        }
        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command) {
                case Command.logout:
                    Validlogout(tcpmessage.check);
                    break;
            }

        }

        public void Validlogout(int check)
        {
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                MainWindow login = new MainWindow();
                login.Show();
            });
            closeWindow();
        }

        public ICommand Freshcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executefresh);
                return command;
            }
        }
        public void Executefresh()
        {

        }
        public ICommand Logoutcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executelogout);
                return command;
            }
        }
        public void Executelogout()
        {

        }
        public ICommand Plusfriendcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executeplusfriend);
                return command;
            }
        }
        public void Executeplusfriend()
        {

        }
        public ICommand Deletefriendcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executedeletefriend);
                return command;
            }
        }
        public void Executedeletefriend()
        {
            
        }
        public ICommand Chatfriendcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(ExecuteChat);
                return command;
            }
        }
        public void ExecuteChat()
        {

        }
    }
}