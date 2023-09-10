using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.ViewModels
{
    public sealed class ShowEmployeeInfoParameter
    {
        public ShowEmployeeInfoParameter(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
    }
}
