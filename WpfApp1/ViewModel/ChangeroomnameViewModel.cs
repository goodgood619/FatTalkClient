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
    public class ChangeroomnameViewModel : ViewModelBase
    {
        public MessengerClient messenger { get; set; }
        public int Chatnumber { get; set; }
        public string Usernickname { get; set; }
        private string _Roomchangename;
        public ChangeroomnameViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            Chatnumber = 0;
            _Roomchangename = string.Empty;
            Usernickname = string.Empty;
        }
        public string Roomchangename
        {
            get { return _Roomchangename; }
            set { _Roomchangename = value;RaisePropertyChanged("Roomchangename");}
        }
        public void ResponseMessage(TCPmessage message)
        {
            switch (message.Command)
            {
                case Command.Changeroomname:
                    Validchangeroomname(message.check);
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
        public void Validchangeroomname(int check)
        {
            switch (check)
            {
                case 0:
                    MessageBox.Show("이미 이 계정은 로그아웃이 되었습니다.");
                    closeWindow();
                    break;
                case 1:
                    MessageBox.Show("성공적으로 변경되었습니다");
                    Roomchangename = string.Empty;
                    break;
            }
        }
        public ICommand Changeroomnamecommand
        {
            get
            {
                RelayCommand command = new RelayCommand(Executechangeroomname);
                return command;
            }
        }
        public void Executechangeroomname()
        {
            if (!messenger.requestChangeroomname(Chatnumber,Roomchangename,Usernickname))
            {
                MessageBox.Show("서버와 연결이 끊겼습니당");
            }
        }
    }
}
