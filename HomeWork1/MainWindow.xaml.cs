using System;
using System.Collections.Generic;
using System.Windows;
using TransferObjects.Employee;
using HomeWork1.EmployeeDirectory;
using HomeWork1.Clients;
using DatabaseModel;
using DatabaseModel.Repositories;
using DatabaseModel.Models;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeWork1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {             
            InitializeComponent();            
        }
    }
}
