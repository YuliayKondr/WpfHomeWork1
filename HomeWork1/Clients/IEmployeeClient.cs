using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObjects.Employee;

namespace HomeWork1.Clients
{
    public interface IEmployeeClient
    {
        Task<IReadOnlyCollection<EmployeeDto>> GetEmployeesByCountAsync(int count);

        Task<EmployeeDto> GetEmployeeAsync();
    }
}
