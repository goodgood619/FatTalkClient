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
    public class JoinChatViewModel :ViewModelBase
    {
        public MessengerClient messenger { get; set; }
        private string joinchatid; //초대할아이디
        public int Chatnumber { get; set; }
        public string Usernickname { get; set; }
        private string usernickname = string.Empty;
        private Imessanger _imessanger;
        public JoinChatViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            joinchatid = string.Empty; // 초대할아이디
            Chatnumber = 0;
            Usernickname = string.Empty;
            _imessanger = imessanger;
        }

        public string JoinchatId
        {
            get { return joinchatid; }
            set { joinchatid = value;RaisePropertyChanged("JoinchatId"); }
        }
        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Joinchat:
                    Validjoinchat(tcpmessage.check,tcpmessage.message);
                    break;
            }
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
        public void Validjoinchat(int check, string message)
        {
            // 채팅하면서 이제 중간에 계속 로그아웃을 했는지를 체크를 해줘야함
            switch (check)
            {
                case 0:
                    MessageBox.Show("초대할 아이디가 로그아웃된 상태입니다.");
                    JoinchatId = string.Empty;
                    break;
                case 1:
                    MessageBox.Show("친구가 초대되었습니다.");
                    JoinchatId = string.Empty;
                    break;
                case 2: //오류
                    MessageBox.Show("초대할 아이디가 존재하지 않습니다. 다시입력해주세요");
                    JoinchatId = string.Empty;
                    break;
                case 3:
                    MessageBox.Show("초대하려는 아이디와 초대할 아이디가 일치합니다. 다시입력해주세요");
                    JoinchatId = string.Empty;
                    break;
                case 4:
                    MessageBox.Show("이미 이 방에 초대가 되어있는 상태입니다. 다시 입력해주세요");
                    JoinchatId = string.Empty;
                    break;
                case 5: 
                    MessageBox.Show("초대할 아이디를 친구차단했거나, 초대할 아이디인 사람이 친구차단을 했습니다.");
                    JoinchatId = string.Empty;
                    break;
                case 6:
                    MessageBox.Show("현재 아이디가 로그아웃된 상태입니다.");
                    closeWindow();
                    break;
            }
        }
        public ICommand Joinchatcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(ExecuteJoinchat);
                return command;
            }
        }
        public void ExecuteJoinchat()
        {
            if (!messenger.requestJoinchatcommand(joinchatid, Chatnumber, Usernickname)) 
            {
                MessageBox.Show("서버와 연결이 끊겼습니다.");
            }
        }
    }
}
