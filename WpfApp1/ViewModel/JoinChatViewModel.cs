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
        private string joinchatid;
        public JoinChatViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            joinchatid = string.Empty;
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
                    Validjoinchat(tcpmessage.check,tcpmessage.message,tcpmessage.Chatnumber);
                    break;
                case Command.ReceiveJoinchat:
                    break;
            }
        }

        public void Validjoinchat(int check, string message, int Chatnumber)
        {
            //방을 찾아서, 그 방에있는 멤버를 추가시킴(아직구현안함)
            switch (check)
            {
                case 0:
                    MessageBox.Show("초대할 친구가 차단하여 초대가 불가능합니다.");
                    break;
                case 1:
                    MessageBox.Show("친구가 초대되었습니다.");
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
            if (!messenger.requestJoinchatcommand(joinchatid,messenger.chatnumber,messenger.userdata.nickname))
            {
                MessageBox.Show("서버와 연결이 끊겼습니다.");
            }
        }
    }
}
