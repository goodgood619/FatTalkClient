using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.ViewModel;
namespace WpfApp1
{
    /// <summary>
    /// ChangeroomnameView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChangeroomnameView : Window
    {
        public ChangeroomnameView(ChangeroomnameViewModel changeroomnameViewModel)
        {
            InitializeComponent();
            DataContext = changeroomnameViewModel as object;
        }
    }
}
