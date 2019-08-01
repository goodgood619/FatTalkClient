using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
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
        public MessengerClient messenger { get; set; }
        public ObservableCollection<Finddata> Findinfo { get; set; }
        public ObservableCollection<Frienddata> Friendlist { get; set; }
        public ObservableCollection<string> Showfriend { get; set; }
        private int fcnt = 0;
        public string usernickname = string.Empty;
        public MainViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            usernickname = messenger.userdata.nickname;
            Friendlist = imessanger.frienddatas();
            Showfriend = new ObservableCollection<string>();
            fcnt = 0;
            // Findinfo = new ObservableCollection<Finddata>();
        }

        public string NICKNAME
        {
            get { return usernickname; }
            set { usernickname = messenger.userdata.nickname; RaisePropertyChanged("NICKNAME"); }
        }
        public int Fcnt
        {
            get { return fcnt; }
            set { fcnt = value; RaisePropertyChanged("Fcnt"); }
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
            switch (tcpmessage.Command)
            {
                case Command.logout:
                    Validlogout(tcpmessage.check);
                    break;
                case Command.Removefriend:
                    break;
                case Command.Refresh:
                    ValidFresh(tcpmessage.Friendcount);
                    break;
                case Command.Makechat:
                    break;
            }

        }

        public void Validlogout(int check)
        {
            messenger.userdata.Reset();
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                MainWindow login = new MainWindow();
                login.Show();
            });
            closeWindow();
        }
        public void ValidFresh(int friendcnt)
        {
            Fcnt = friendcnt;
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                //Friendlist.Clear();
                //여기서 각자 데이터를 받아만 올수 있다면, 그냥 add해주면 됨
            });
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
            if (!messenger.requestFreshcommand(usernickname))
            {
                MessageBox.Show("서버와 연결이 끊겼습니다.");
            }
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
            if (messenger.requestLogout(usernickname))
            {
                MessageBox.Show("로그아웃되었습니다.");
            }
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
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                PlusfriendView plusfriendView = new PlusfriendView();
                plusfriendView.ShowDialog();
            });

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