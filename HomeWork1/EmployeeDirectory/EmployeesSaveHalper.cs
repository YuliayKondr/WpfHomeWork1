using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TransferObjects.Employee;

namespace HomeWork1.EmployeeDirectory
{
    internal static class EmployeesSaveHalper
    {
        private static IReadOnlyCollection<EmployeeDto> _employees;        

        public static IReadOnlyCollection<EmployeeDto> GetAllEmployees()
        {
            return _employees;
        }

        public static EmployeeDto? GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }
        
        public static void SetEmployees(IReadOnlyCollection<EmployeeDto> employees)
        {
            _employees = employees;
        }
    }
}
