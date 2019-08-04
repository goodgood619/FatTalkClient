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
    public class ChatViewModel :ViewModelBase
    {
        public MessengerClient messenger { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        private string usernickname = string.Empty;
        private string sendchatmessage = string.Empty;
        public ChatViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            Messages = new ObservableCollection<string>();
        }

        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Joinchat:
                    break;
                case Command.Outchat:
                    break;
                case Command.Sendchat:
                    Validsendchat(tcpmessage.message,tcpmessage.Chatnumber);
                    break;
            }
        }
        public void Validsendchat(string message,int Chatnumber)
        {
            //test용으로 메시지가 가는지부터 테스트
            // 생각, 만약안된다면(안될꺼같은데, a라는 클라가 , 몇번방에 속해있는지에 관한 리스트가 있으면, 
            // 그 리스트각각 방중에서, Chatnumber 일치되는 곳에다가 뿌려주면 됨
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                Messages.Add(message);
            });
        }
        public string Sendchat
        {
            get { return sendchatmessage; }
            set { sendchatmessage = value;RaisePropertyChanged("Sendchat");}
        }
        public string NICKNAME
        {
            get { return messenger.userdata.nickname; }
            set { usernickname = messenger.userdata.nickname; RaisePropertyChanged("NICKNAME"); }
        }
        public ICommand Joinchatcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executejoinchat);
                return command;
            }
        }
        public void Executejoinchat()
        {

        }
        public ICommand Sendchatcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executesendchat);
                return command;
            }
        }
        public void Executesendchat()
        {
            if (!messenger.requestSendchatcommand(messenger.chatnumber, messenger.userdata.nickname, Sendchat))
            {
                MessageBox.Show("서버와 연결이 끊겼거나, 상대방이 채팅방을 나갔습니다");
            }
        }

        public ICommand Outchatcommand {
            get
            {
                RelayCommand command = new RelayCommand(Executeoutchat);
                return command;
            }
        }
        public void Executeoutchat()
        {

        }

    }
}
