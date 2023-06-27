using Domain.Models;
using Repository;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository repo { get; set; }

        public EmployeeService(IEmployeeRepository repo)
        {
            this.repo = repo;
        }
        public Task Add(Employee employee)
        {
           return repo.Add(employee);
        }

        public Task Delete(int id)
        {
            return repo.Delete(id);
        }

        public Task<IEnumerable<Employee>> GetAll()
        {
            return repo.All();
        }

        public Task<Employee> GetById(int id)
        {
            return repo.GetById(id);
        }

        public Task Update(Employee employee)
        {
            return repo.Update(employee);
        }
    }
}