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
    public class BlockfriendViewModel :ViewModelBase
    {
        public MessengerClient messenger { get; set; }
        private string _blockid;
        private ObservableCollection<Frienddata> _Friendlist;
        public BlockfriendViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            _blockid = string.Empty;
            _Friendlist = new ObservableCollection<Frienddata>();
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
        public string Blockid
        {
            get { return _blockid; }
            set { _blockid = value;RaisePropertyChanged("Blockid");}
        }

        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Blockfriend:
                    Validblockfriend(tcpmessage.check,tcpmessage.message);
                    break;
                case Command.NotBlockfriend:
                    Validnotblockfriend(tcpmessage.check);
                    break;
            }
        }
        public void Validblockfriend(int check,string message)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("존재하지 않는 아이디입니다. 다시 입력해주세요");
                    Blockid = string.Empty;
                    break;
                case 1:
                    MessageBox.Show("친구 차단이 완료되었습니다. ");
                    JsonHelp json = new JsonHelp();
                    Dictionary<string, string> nickinfo = json.getnickinfo(message);
                    string deletenickname = nickinfo[Jsonname.Nickname];

                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        Frienddata frienddata = Friendlist.First(x => x.Fnickname == deletenickname);
                        if(frienddata!=null) Friendlist.Remove(frienddata);
                    });
                    Blockid = string.Empty;
                    break;
                case 2:
                    MessageBox.Show("차단하려는 친구의 아이디와 유저의 아이디와 일치합니다. 다시 입력해주세요");
                    Blockid = string.Empty;
                    break;
                case 3:
                    MessageBox.Show("이미 차단이 된 아이디입니다. 다시 입력해주세요");
                    Blockid = string.Empty;
                    break;
            }
        }
        public void Validnotblockfriend(int check)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("존재하지 않는 아이디입니다. 다시 입력해주세요");
                    Blockid = string.Empty;
                    break;
                case 1:
                    MessageBox.Show("친구 차단이 해제되었습니다");
                    Blockid = string.Empty;
                    break;
                case 2:
                    MessageBox.Show("차단해제하려는 친구의 아이디와 유저의 아이디와 일치합니다. 다시 입력해주세요");
                    Blockid = string.Empty;
                    break;
                case 3:
                    MessageBox.Show("이미 해제가 된 아이디입니다. 다시 입력해주세요");
                    Blockid = string.Empty;
                    break;

            }
        }
        public ICommand Blockfriendcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executeblockfriend);
                return command;
            }
        }
        public void Executeblockfriend()
        {
            if (string.IsNullOrEmpty(Blockid))
            {
                MessageBox.Show("아이디를 입력해주세요");
            }
            else
            {
                if (!messenger.requestBlockfriendcommand(messenger.userdata.nickname,Blockid))
                {
                    MessageBox.Show("서버와 연결이 끊겼습니다");
                }
            }
        }
        public ICommand Notblockfriendcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executenotblockcommand);
                return command;
            }
        }
        public void Executenotblockcommand()
        {
            if (string.IsNullOrEmpty(Blockid))
            {
                MessageBox.Show("아이디를 입력해주세요");
            }
            else
            {
                if (!messenger.requestNotBlockfriendcommand(messenger.userdata.nickname, Blockid))
                {
                    MessageBox.Show("서버와 연결이 끊겼습니다");
                }
            }
        }

    }
}
