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
    public class PlusfriendViewModel : ViewModelBase
    {
        private string plusid;
        public MessengerClient messanger { get; set; }
        public ObservableCollection<Frienddata> Friendlist { get; set; }
        public PlusfriendViewModel(Imessanger imessanger)
        {
            messanger = imessanger.GetMessenger(ResponseMessage);
            Friendlist = imessanger.frienddatas();
        }
        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Plusfriend:
                    Validplusfriend(tcpmessage.check, tcpmessage.Usernumber, tcpmessage.message);
                    break;
            }
        }
        public void Validplusfriend(int check, int usernumber, string message)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("존재하지 않는 아이디입니다. 다시 입력해주세요");
                    Plusid = string.Empty;
                    break;
                case 1:
                    MessageBox.Show("친구 추가가 완료되었습니다. ");
                    JsonHelp json = new JsonHelp();
                    Dictionary<string, string> nickinfo = json.getnickinfo(message);
                    string plusnickname = nickinfo[Jsonname.Nickname];

                    App.Current.Dispatcher.InvokeAsync(() =>
                    {


                        Friendlist.Add(new Frienddata(messanger.userdata.nickname, plusnickname));

                    });
                    Plusid = string.Empty;
                    break;
                case 2:
                    MessageBox.Show("추가하려는 친구의 아이디와 유저의 아이디와 일치합니다. 다시 입력해주세요");
                    Plusid = string.Empty;
                    break;
                case 3:
                    MessageBox.Show("이미 친구로 추가된 아이디입니다. 다시 입력해주세요");
                    Plusid = string.Empty;
                    break;
            }
        }
        public string Plusid
        {
            get
            {
                return plusid;
            }
            set { plusid = value; RaisePropertyChanged("Plusid"); }
        }

        public ICommand Plusfriendcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(ExecutePlusfriend);
                return command;
            }
        }
        public void ExecutePlusfriend()
        {
            if (string.IsNullOrEmpty(plusid))
            {
                MessageBox.Show("아이디를 입력해주세요.");
            }
            else
            {
                if (!messanger.requestPlusfriend(plusid, messanger.userdata.id))
                {
                    MessageBox.Show("서버와 연결이 끊겼습니다.");
                }
            }
        }
    }
}
