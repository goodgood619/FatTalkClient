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

            //ServiceLocator.SetLocatorProvider(()=>(IServiceLocator)SimpleIoc.Default);
            SimpleIoc.Default.Register<Imessanger, MessangerService>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<JoinMember>();
        }
        public LoginViewModel login => SimpleIoc.Default.GetInstance<LoginViewModel>();
        public JoinMember join => SimpleIoc.Default.GetInstance<JoinMember>();

        public static void cleanup()
        {

        }
    }
}
