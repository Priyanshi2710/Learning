using Domain.Models;

namespace Service
{
    public interface IEmployeeService
    {
        Task<Employee> GetById(int employeeId);
        Task<IEnumerable<Employee>> GetAll();
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Delete(Employee employee);
    }
}
