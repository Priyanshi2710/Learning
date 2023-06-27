using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetById(int id);
        Task<bool> InsertEmployee(Employee entity);
       
        Task<bool> UpateEmployee(Employee entity);
        Task<bool> DeleteEmployee(int id);
    }
}
