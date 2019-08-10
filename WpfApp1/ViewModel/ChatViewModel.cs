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
        public ObservableCollection<Chatdata> Messages { get; set; }
        public int Chatnumber { get; set; }
        public string Usernickname { get; set; }
        private string usernickname = string.Empty;
        private string sendchatmessage = string.Empty;
        private Imessanger _imessanger;
        private JsonHelp jsonHelp;
        public ChatViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            Messages = new ObservableCollection<Chatdata>();
            Chatnumber = 0;
            Usernickname = string.Empty;
            _imessanger = imessanger;
            jsonHelp = new JsonHelp();
        }

        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Joinchat:
                    Validjoinchat();
                    break;
                case Command.Outchat:
                    Validoutchat(tcpmessage.check, tcpmessage.Chatnumber,tcpmessage.message);
                    break;
                case Command.Sendchat:
                    Validsendchat(tcpmessage.message,tcpmessage.Chatnumber);
                    break;
                case Command.ReceiveJoinchat: //다 추가시켜놔야겠네(메인뷰, 친구추가뷰,채팅뷰 3개추가)
                    Validreceivejoinchat(tcpmessage.check,tcpmessage.Chatnumber,tcpmessage.message);
                    break;
            }
        }
        public void Validreceivejoinchat(int check, int Chatnumber, string message)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("초대하신 아이디는 로그아웃되어있습니다.");
                    break;
                case 1:
                    MessageBox.Show("초대하신 아이디가 친구차단을 하였습니다.");
                    break;
                case 2:
                    MessageBox.Show("방에 초대되었습니다.");
                    ChatViewModel chatViewModel = new ChatViewModel(_imessanger);
                    chatViewModel.Chatnumber = Chatnumber;
                    chatViewModel.NICKNAME = message;
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        ChatView chatView = new ChatView(chatViewModel);
                        chatView.Show();
                    });

                    break;
            }

        }
        public void Validjoinchat()
        {
            //기존에 있는 방에 멤버들이 초대가 되었다는 메시지를 띄우기
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
        public void Validoutchat(int check,int Chatnumber,string message)
        {
            //방을 찾아서, 그 방에 있는 현재의 유저닉네임 삭제,(아직구현안함)
            //string message = ~님이 나갔습니다
            switch (check)
            {
                case 0: //방에 남아있는 사람들
                    Dictionary<string, string> outchatnick = jsonHelp.getnickinfo(message);
                    string outchatnickname = outchatnick[Jsonname.Nickname];
                    string query = $"'{outchatnickname}'님이 나갔습니다.";
                    App.Current.Dispatcher.InvokeAsync(() => {
                        Messages.Add(new Chatdata(query,"Chat out",Chatnumber));
                    });
                    break;

                case 1: //방을 나간사람
                    if (this.Chatnumber == Chatnumber)
                    {
                        MessageBox.Show("채팅방을 나갔습니다.");
                        closeWindow();
                    }
                    break;
            }
        }
        public void Validsendchat(string message,int Chatnumber)
        {
            //test용으로 메시지가 가는지부터 테스트(가는거는 됨, 여러 개의 방을 관리하는게 문제)
            // 생각, 만약안된다면(안될꺼같은데, a라는 클라가 , 몇번방에 속해있는지에 관한 리스트가 있으면, 
            // 그 리스트각각 방중에서, Chatnumber 일치되는 곳에다가 뿌려주면 됨
            JsonHelp jsonHelp = new JsonHelp();
            Dictionary<string, string> receivemessage = jsonHelp.getmessageinfo(message);
            Dictionary<string, string> sendusernickname = jsonHelp.getnickinfo(message);
            string s = receivemessage[Jsonname.Message];
            string snickname = sendusernickname[Jsonname.Nickname];
            if (this.Chatnumber == Chatnumber)
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    Messages.Add(new Chatdata(s, snickname, Chatnumber));
                });
            }
        }
        public string Sendchatting
        {
            get { return sendchatmessage; }
            set { sendchatmessage = value;RaisePropertyChanged("Sendchatting");}
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
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                JoinChatView joinChatView = new JoinChatView();
                joinChatView.Show();
            });
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
            //내가 보낸거
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                 Messages.Add(new Chatdata(sendchatmessage,messenger.userdata.nickname,Chatnumber));
            });
            if (!messenger.requestSendchatcommand(Chatnumber, messenger.userdata.nickname,sendchatmessage))
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
            if (!messenger.requestOutchatcommand(Chatnumber, messenger.userdata.nickname))
            {
                MessageBox.Show("서버와 연결이 끊겼습니다.");
            }
        }

    }
}
