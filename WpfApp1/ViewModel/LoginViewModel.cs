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
    public class LoginViewModel : ViewModelBase //ViewModelBase를 상속안하면
    {
        private string id;
        public MessengerClient messenger { get; set; }
        public string ID
        {
            get { return id; }
            set { id = value;  RaisePropertyChanged("ID"); } //RaiseProperty가 실행이 안됨
        }
        public LoginViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            ID = string.Empty;
        }
        public void ResponseMessage(TCPmessage message)
        {
            switch (message.Command)
            {
                case Command.login:
                    Validate(message.check);
                    break;
            }

        }
        public void Validate(int check)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("ID가 틀렸습니다.");
                    break;
                case 1:
                    MessageBox.Show("비밀번호가 틀렸습니다.");
                    break;
                case 2:
                    MessageBox.Show("뚱톡에 오신걸 환영합니다.");
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        MainView mainview = new MainView();
                        mainview.Show();
                        closeWindow();

                    });
                    
                    break;
                case 3:
                    MessageBox.Show("ID 그리고 비밀번호가 둘다 틀렸습니다.");
                    break;
            }
        }

        public void closeWindow()
        {
            App.Current.Dispatcher.InvokeAsync(() =>
            {
                foreach(Window window in Application.Current.Windows)
                {
                    if(window.DataContext == this)
                    {
                        window.Close();
                    }
                }
            });
        }
        public ICommand logincommand
        {
            get
            {
                RelayCommand<PasswordBox> command = new RelayCommand<PasswordBox>(ExecuteLogin);
                return command;
            }
        }
        
        public ICommand JoinUicommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Joinmember);
                return command;
            }
        }
        public void Joinmember()
        {
            JoinMember joinMember = new JoinMember();
            joinMember.ShowDialog();
        }
        public ICommand FindIdcommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Findid);
                return command;
            }
        }
        public void Findid()
        {
            Findlogininfo findlogininfo = new Findlogininfo();
            findlogininfo.ShowDialog();
        }
        public void ExecuteLogin(PasswordBox passwordBox)
        {
            string password = passwordBox.Password;
            if (string.IsNullOrEmpty(ID))
            {
                MessageBox.Show("ID를 입력하세용");
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password를 입력하세용");
            }
            else
            {
                if (!messenger.requestLogin(id, password))
                {
                    MessageBox.Show("서버와 연결이 끊겼습니당");

                }
            }
        }
    }
    
    
}
