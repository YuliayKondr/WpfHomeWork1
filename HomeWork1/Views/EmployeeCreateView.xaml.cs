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

namespace HomeWork1.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeCreateView.xaml
    /// </summary>
    public partial class EmployeeCreateView : Window
    {
        public EmployeeCreateView()
        {
            InitializeComponent();
        }

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
