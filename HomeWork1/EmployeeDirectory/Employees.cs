using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TransferObjects.Employee;

namespace HomeWork1.EmployeeDirectory
{
    internal static class Employees
    {
        private static IReadOnlyCollection<EmployeeDto> _employees;

        static Employees()
        {
            _employees = GetTestEmployees();
        }

        public static IReadOnlyCollection<EmployeeDto> GetAllEmployees()
        {
            return _employees;
        }

        public static EmployeeDto? GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        private static IReadOnlyCollection<EmployeeDto> GetTestEmployees()
        {
            string employeeJson = "[{\"id\":5232,\"uid\":\"1fd69958-6750-4bf4-b1f3-6cb41d6c2513\",\"password\":\"PIrLOqBVW3\",\"first_name\":\"Lewis\",\"last_name\":\"McLaughlin\",\"username\":\"lewis.mclaughlin\",\"email\":\"lewis.mclaughlin@email.com\",\"avatar\":\"https://robohash.org/optiomagnamvoluptate.png?size=300x300&set=set1\",\"gender\":\"Bigender\",\"phone_number\":\"+1-787 (648) 741-4045 x0403\",\"social_insurance_number\":\"770547602\",\"date_of_birth\":\"1997-02-24\",\"employment\":{\"title\":\"Hospitality Associate\",\"key_skill\":\"Fast learner\"},\"address\":{\"city\":\"Destinymouth\",\"street_name\":\"Hand Trace\",\"street_address\":\"8192 Darrin Roads\",\"zip_code\":\"86024-7620\",\"state\":\"Washington\",\"country\":\"United States\",\"coordinates\":{\"lat\":-4.477309852691235,\"lng\":-9.659633433039148}},\"credit_card\":{\"cc_number\":\"4813003651058\"},\"subscription\":{\"plan\":\"Business\",\"status\":\"Idle\",\"payment_method\":\"Bitcoins\",\"term\":\"Payment in advance\"}},{\"id\":2216,\"uid\":\"8db634d4-85b2-4110-bd92-d268ad19b5ad\",\"password\":\"5LPb3F1x6e\",\"first_name\":\"Pablo\",\"last_name\":\"Marvin\",\"username\":\"pablo.marvin\",\"email\":\"pablo.marvin@email.com\",\"avatar\":\"https://robohash.org/doloreshictemporibus.png?size=300x300&set=set1\",\"gender\":\"Genderfluid\",\"phone_number\":\"+93 135-026-8043\",\"social_insurance_number\":\"714021870\",\"date_of_birth\":\"1963-12-29\",\"employment\":{\"title\":\"Investor Supervisor\",\"key_skill\":\"Confidence\"},\"address\":{\"city\":\"Lake Gayle\",\"street_name\":\"Gerlach Parks\",\"street_address\":\"32366 Dalila Orchard\",\"zip_code\":\"81444-0087\",\"state\":\"Arkansas\",\"country\":\"United States\",\"coordinates\":{\"lat\":-25.49116956626294,\"lng\":88.18924024192927}},\"credit_card\":{\"cc_number\":\"4586428688364\"},\"subscription\":{\"plan\":\"Gold\",\"status\":\"Pending\",\"payment_method\":\"Alipay\",\"term\":\"Full subscription\"}}]";

            EmployeeDto[] employees = JsonConvert.DeserializeObject<EmployeeDto[]>(employeeJson);

            return employees;
        }
    }
}
