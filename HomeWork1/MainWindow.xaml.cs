using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransferObjects.Employee;
using HomeWork1.EmployeeDirectory;
using System.Net.Http;
using RestEase;
using HomeWork1.Clients;

namespace HomeWork1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEmployeeClient _employeeClient;

        public MainWindow(IEmployeeClient employeeClient)
        {
            _employeeClient = employeeClient;

            InitializeComponent();            
        }

        private async void ShowInfoEnployee(object sender, RoutedEventArgs e)
        {
            if (ListEmployees.SelectedItem != null && ListEmployees.SelectedItem is EmployeeDto)
            {
                EmployeeDto selectedEmployee = (EmployeeDto)ListEmployees.SelectedItem;

                EmployeeView employeeView = new EmployeeView();

                employeeView.EmployeeId = selectedEmployee.Id;

                employeeView.ShowDialog();

                ListEmployees.SelectedItem = null;
            }           
        }        

        private async void Load_Click(object sender, RoutedEventArgs e)
        {    
            if(!int.TryParse(CountInput.Text, out int countEmployees) || countEmployees < 1 || countEmployees > 10)
            {
                MessageBox.Show("Введіть число від 1 до 10", "Кількість співробітників", MessageBoxButton.OK, MessageBoxImage.Warning);

                CountInput.Text = string.Empty;

                return;
            }

            try
            {
                List<EmployeeDto> employees = new List<EmployeeDto>();

                if(countEmployees == 1)
                {
                    EmployeeDto employee = await _employeeClient.GetEmployeeAsync();

                    employees.Add(employee);
                }
                else
                {
                    IReadOnlyCollection<EmployeeDto> employeesFromServer = await _employeeClient.GetEmployeesByCountAsync(countEmployees);

                    employees.AddRange(employeesFromServer);
                } 
                
                EmployeesSaveHalper.SetEmployees(employees);
                
                ListEmployees.ItemsSource = EmployeesSaveHalper.GetAllEmployees();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
