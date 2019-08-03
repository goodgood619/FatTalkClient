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
        private int fcnt = 0;
        private string usernickname = string.Empty;
        private ObservableCollection<Frienddata> _Friendlist;
        private ObservableCollection<Frienddata> _Removelist;
        public MainViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            usernickname = messenger.userdata.nickname;
            Friendlist = imessanger.frienddatas();
            _Removelist = new ObservableCollection<Frienddata>();
            fcnt = 0;
        }
        public ObservableCollection<Frienddata> Removelist
        {
            get
            {
                return this._Removelist;
            }
            set
            {
                if (this._Removelist != value)
                {
                    this._Removelist = value;
                    this.RaisePropertyChanged(() => this._Removelist);
                }
            }
        }
        public ObservableCollection<Frienddata> Friendlist
        {
            get
            {
                return _Friendlist;
            }
            set
            {
                _Friendlist = value;
                RaisePropertyChanged("Friendlist");
            }
        }
        public System.Collections.IList SelectFriendlist
        {
            get
            {
                return Removelist;
            }
            set
            {
                Removelist.Clear();
                foreach(Frienddata s in value){
                    Removelist.Add(s);
                }
            }
        }
        public string NICKNAME
        {
            get { return messenger.userdata.nickname; }
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
                    ValidRemove(tcpmessage.check);
                    break;
                case Command.Refresh:
                    ValidFresh(tcpmessage.Friendcount,tcpmessage.message);
                    break;
                case Command.Makechat:
                    break;
            }

        }
        public void ValidRemove(int check)
        {
            if (check == 1)
            {
                MessageBox.Show("친구 삭제가 완료되었습니다. 새로고침을 눌러주세요");
            }
        }
        public void Validlogout(int check)
        {
            messenger.userdata.Reset();
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                Fcnt = 0;
                Friendlist.Clear();
                NICKNAME = string.Empty;
                MainWindow login = new MainWindow();
                login.Show();
            });
            closeWindow();
        }
        public void ValidFresh(int friendcnt,string message)
        {
            JsonHelp jsonHelp = new JsonHelp();
            string[] refresharray = jsonHelp.getRefreshnickarray(message);
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                Fcnt = friendcnt;
                Friendlist.Clear();
                //여기서 각자 데이터를 받아만 올수 있다면, 그냥 add해주면 됨
                for(int i=0;i<refresharray.Length;i++){
                    Friendlist.Add(new Frienddata(refresharray[i]));
                }
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
            if (!messenger.requestFreshcommand(messenger.userdata.nickname))
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
            if (messenger.requestLogout(messenger.userdata.nickname))
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
            string[] sendarray = null;
            sendarray = new string[Removelist.Count];
            for(int i = 0; i < Removelist.Count; i++)
            {
                sendarray[i] = Removelist[i].Fnickname.ToString();
            }
            if (!messenger.requestDeletefriendcommand(sendarray,messenger.userdata.nickname))
            {
                MessageBox.Show("서버와 연결이 끊겼습니다");
            }
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