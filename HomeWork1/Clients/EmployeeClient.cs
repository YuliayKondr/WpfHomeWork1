using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObjects.Employee;

namespace HomeWork1.Clients
{
    public class EmployeeClient : IEmployeeClient
    {
        private readonly IUsersApi _api;

        public EmployeeClient(IUsersApi api)
        {
            _api = api;
        }

        public async Task<EmployeeDto> GetEmployeeAsync()
        {
            Response<EmployeeDto> employeeResponse = await _api.GetEmployeeAsync();

            if (employeeResponse.ResponseMessage.IsSuccessStatusCode)
            {
                return employeeResponse.GetContent();
            }

            return default;
        }

        public async Task<IReadOnlyCollection<EmployeeDto>> GetEmployeesByCountAsync(int count)
        {
            Response<EmployeeDto[]> employeesResponse = await _api.GetEmployeesAsync(count);

            if (employeesResponse.ResponseMessage.IsSuccessStatusCode)
            {
                return employeesResponse.GetContent();
            }

            return Array.Empty<EmployeeDto>();
        }
    }
}
