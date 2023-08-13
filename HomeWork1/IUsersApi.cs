using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObjects.Employee;

namespace HomeWork1
{
    public interface IUsersApi
    {
        [Header("Content-Type", "application/json")]
        [Get("/api/v2/users")]
        Task<Response<EmployeeDto[]>> GetEmployeesAsync([Query("size")]int size);


        [Header("Content-Type", "application/json")]
        [Get("/api/v2/users")]
        Task<Response<EmployeeDto>> GetEmployeeAsync();
    }
}
