using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Domain.Entites;
using Domain.Repositories;

namespace SimpleStorage.Repositories
{
    public class EmployeeAwardRepository : IEmployeeAwardRepository
    {
        private ConcurrentDictionary<string, Employee> employeesData;
        public EmployeeAwardRepository()
        {
            employeesData = new ConcurrentDictionary<string, Employee>();
        }

        public Employee ApplyEmployeeChanges(Employee employee)
        {
            return employeesData.AddOrUpdate(employee.Id, employee, (key, value) => value = employee);

        }

        public Employee GetEmployee(string employeeId)
        {
            Employee employee;
            employeesData.TryGetValue(employeeId, out employee);
            return employee;
        }
    }
}
