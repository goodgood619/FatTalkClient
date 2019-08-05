using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using WpfApp1.Service;
using Microsoft.Practices.ServiceLocation;
//using CommonServiceLocator;
namespace WpfApp1.ViewModel
{
    public class ViewModelLocator
    {
       
        public ViewModelLocator()
        {

            //ServiceLocator.SetLocatorProvider(()=>SimpleIoc.Default);
            SimpleIoc.Default.Register<Imessanger, MessangerService>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<JoinViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();
            SimpleIoc.Default.Register<FindlogininfoViewModel>();
            SimpleIoc.Default.Register<PlusfriendViewModel>();
            SimpleIoc.Default.Register<JoinChatViewModel>();
        }
        public LoginViewModel login => SimpleIoc.Default.GetInstance<LoginViewModel>();
        public JoinViewModel join => SimpleIoc.Default.GetInstance<JoinViewModel>();
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public ChatViewModel Chat => SimpleIoc.Default.GetInstance<ChatViewModel>();
        public FindlogininfoViewModel FindIdPassword => SimpleIoc.Default.GetInstance<FindlogininfoViewModel>();
        public PlusfriendViewModel Plusfriend => SimpleIoc.Default.GetInstance<PlusfriendViewModel>();
        public JoinChatViewModel joinchat => SimpleIoc.Default.GetInstance<JoinChatViewModel>();
        public static void cleanup()
        {

        }
    }
}
