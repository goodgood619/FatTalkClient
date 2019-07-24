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
    public class FindlogininfoViewModel :ViewModelBase
    {
        private string id;
        public MessengerClient messenger { get; set; }
        public string ID
        {
            get {return id;}
            set { id = value; RaisePropertyChanged("ID");}
        }
        public FindlogininfoViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            
        }
        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Findid:
                    ValidFindlogininfo();
                    break;
            }
        }
        public void ValidFindlogininfo()
        {
            //여기서 무언가 하는건가
            App.Current.Dispatcher.InvokeAsync(() =>
            {

            });
        }

        public ICommand Findidinfocommand
        {
            get
            {
                RelayCommand command = new RelayCommand(ExecuteFindid);
                return command;
            }
        }
        public void ExecuteFindid()
        {
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("ID를 입력해주세요");
            }
            else
            {
                if (!messenger.requestIdcheck(id))
                {
                    MessageBox.Show("서버와 연결이 끊겼습니다.");

                }
            }
        }
    }
}
