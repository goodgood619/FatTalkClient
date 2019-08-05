using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; //Icommand 만들기 관련

using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using WpfApp1.Models;
using WpfApp1.Modules;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class FindlogininfoViewModel : ViewModelBase
    {
        private string id;
        private string phone;
        public MessengerClient messenger { get; set; }
        public ObservableCollection<Finddata> Findinfo { get; set; }
        public JsonHelp jsonHelp = new JsonHelp();
        public string ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged("ID"); }
        }
        public string PHONE
        {
            get { return phone;}
            set { phone = value;RaisePropertyChanged("PHONE");}
        }
        public FindlogininfoViewModel(Imessanger imessanger)
        {
            messenger = imessanger.GetMessenger(ResponseMessage);
            Findinfo = new ObservableCollection<Finddata>();
            id = string.Empty;
            phone = string.Empty;
        }
        public void ResponseMessage(TCPmessage tcpmessage)
        {
            switch (tcpmessage.Command)
            {
                case Command.Findid:
                    ValidFindlogininfo(tcpmessage.message, tcpmessage.check);
                    break;
            }
        }
        public void ValidFindlogininfo(string message, int check)
        {
            switch (check)
            {
                //여기서 무언가 하는건가(UI에 할내용을 넣어주면됨)
                case 0:
                    MessageBox.Show("ID 혹은 Phone이 존재하지 않습니다. 다시 입력해주세요");
                    ID = string.Empty;
                    PHONE = string.Empty;
                    break;
                case 1:
                    Dictionary<string, string> getlogininfo = jsonHelp.getlogininfo(message);
                    string findid = getlogininfo[Jsonname.ID];
                    string findpassword = getlogininfo[Jsonname.Password];
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        Findinfo.Add(new Finddata(findid, findpassword));
                    });
                    break;
            }
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
            else if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Phone번호를 입력해주세요.");
            }
            else
            {
                if (Findinfo.Count > 0) Findinfo.RemoveAt(0); // 계속 list가 쌓여가지고 어쩔수 없이 또 버튼을 누르게 되면 제거
                if (!messenger.requestFindid(id,phone))
                {
                    MessageBox.Show("서버와 연결이 끊겼습니다.");

                }
            }
        }
    }
}
