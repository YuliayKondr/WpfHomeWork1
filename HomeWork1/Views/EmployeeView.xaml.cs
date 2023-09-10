 using DatabaseModel.Models;
using DatabaseModel.Repositories;
using Microsoft.EntityFrameworkCore;
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
using TransferObjects.Employee;

namespace HomeWork1.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        private EmployeeEntity? _currentEmployee;
        private AddressEntity? _currentAddress;
        private SubscriptionEntity? _currentSubscription;

        public EmployeeView()
        {
            InitializeComponent();            
        }        

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
    }
}
