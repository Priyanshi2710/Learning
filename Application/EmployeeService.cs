using Repository;
namespace Application
{
    public class EmployeeService : IEmployeeService 
    {
        private readonly IEmployeeRepository<Employee> emprepo;

        public EmployeeService(IEmployeeRepository<Employee> repo )
        {
            this.emprepo = repo;
        }
        public Task<bool> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertEmployee(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpateEmployee(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}