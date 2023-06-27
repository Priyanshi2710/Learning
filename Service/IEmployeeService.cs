using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service
{
    public interface IEmployeeService
    {
        Task<Employee> GetById(int id);
        Task<IEnumerable<Employee>> GetAll();
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Delete(int id);
    }
}
