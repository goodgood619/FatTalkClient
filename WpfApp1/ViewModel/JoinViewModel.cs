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
    public class JoinViewModel : ViewModelBase
    {
        private string id;
        private string nickname;
        private string phone;
        public MessengerClient messengerClient;
        public string ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged("ID"); }
        }
        public string NICKNAME
        {
            get { return nickname; }
            set { nickname = value; RaisePropertyChanged("NICKNAME"); }
        }
        public string PHONE
        {
            get { return phone; }
            set { phone = value; RaisePropertyChanged("PHONE"); }
        }
        public JoinViewModel(Imessanger imessanger)
        {
            messengerClient = imessanger.GetMessenger(ResponseMessage);

        }
        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Join:
                    Validjoin(tcpmessage.check);
                    break;
                case Command.Idcheck:
                    Validid(tcpmessage.check);
                    break;
                case Command.Nicknamecheck:
                    Validnickname(tcpmessage.check);
                    break;
            }

        }
        public void Validnickname(int check)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("닉네임이 중복됩니다.");
                    break;
                case 1:
                    MessageBox.Show("사용가능한 닉네임입니다.");
                    break;
            }
        }
        public void Validjoin(int check)
        {
            switch (check)
            {
                case 0: //중복버튼 안누르고 할수도 있기 때문에 마지막도 체크를 해줘야 함
                    MessageBox.Show("ID가 중복됩니다.");
                    break;
                case 1: // 회원가입 OK됐다라고 띄워야함
                    MessageBox.Show("회원가입이 완료되었습니다.");
                    break;
            }

        }
        public void Validid(int check)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("ID가 중복되었습니다. 다시 입력해주세요.");
                    break;
                case 1:
                    MessageBox.Show("사용 가능한 ID입니다.");
                    break;
            }

        }
        public ICommand Idcheckcommand //send용
        {
            get
            {
                RelayCommand relayCommand = new RelayCommand(ExecuteIdcheck);
                return relayCommand;
            }
        }
        public ICommand JoinMembercommand //send용
        {
            get
            {
                RelayCommand<PasswordBox> relayCommand = new RelayCommand<PasswordBox>(ExecuteJoin);
                return relayCommand;
            }
        }
        public ICommand Backlogincommand
        {
            get
            {
                RelayCommand relayCommand = new RelayCommand(Executebacklogin);
                return relayCommand;
            }
        }
        public ICommand Nicknamecheckcommand
        {
            get
            {
                RelayCommand relayCommand = new RelayCommand(ExecuteNicknamecommand);
                return relayCommand;
            }
        }
        public ICommand Phonecheckcommand
        {
            get
            {
                RelayCommand relayCommand = new RelayCommand(ExecutePhonecheck);
                return relayCommand;
            }
        }
        public void ExecuteNicknamecommand()
        {
            if (string.IsNullOrEmpty(nickname))
            {
                MessageBox.Show("닉네임을 입력하세용");
            }
            else
            {
                messengerClient.requestNicknamecheck(nickname);
            }
        }
        public void ExecuteIdcheck() //send관련 메소드
        {
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("ID를 입력하세용");
            }
            else
            {
                messengerClient.requestIdcheck(id);
            }
        }
        public void ExecuteJoin(PasswordBox passwordBox) //send관련 메소드
        {
            string password = passwordBox.Password;

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("ID를 입력하세용");
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("비밀번호를 입력하세용");
            }
            else if (string.IsNullOrEmpty(nickname))
            {
                MessageBox.Show("닉네임을 입력하세용");
            }
            else if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("폰번호를 입력하세용");
            }
            else if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(phone))
            {
                messengerClient.requestJoin(id, password, nickname, phone);
            }
            else
            {
                MessageBox.Show("위의 4개중 하나이상 입력에서 누락되었습니다. 채우지 않은부분은 입력을 해주세요");
            }
        }
        public void Executebacklogin()
        {
            closeWindow();
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
        public void ExecutePhonecheck()
        {
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("폰번호를 입력하세용");
            }
            else
            {
                if (phone.Length < 10)
                {
                    MessageBox.Show("숫자 10자리를 입력을 해주세요");
                }
                else if(phone.Length == 10)
                {
                    bool notnumbercheck = false;
                    for(int i = 0; i < phone.Length; i++)
                    {
                        if (phone[i] >= '0' && phone[i] <= '9') continue;
                        else
                        {
                            notnumbercheck = true;
                            break;
                        }
                    }
                    if (notnumbercheck)
                    {
                        MessageBox.Show("핸드폰 번호 숫자 10자리를 입력해주세요");
                    }
                    else
                    {
                        MessageBox.Show("핸드폰 번호가 확인되었습니다.");
                    }
                }
                else
                {
                    MessageBox.Show("-이나 특수문자를 제외한 숫자 10자리를 입력해주세요");
                }
            }
        }
        
    }
}
