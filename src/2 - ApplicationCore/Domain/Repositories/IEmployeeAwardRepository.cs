using System;
using Domain.Entites;

namespace Domain.Repositories
{
    public interface IEmployeeAwardRepository
    {
        Employee ApplyEmployeeChanges(Employee employee);
        Employee GetEmployee(string employeeId);
    }
}
