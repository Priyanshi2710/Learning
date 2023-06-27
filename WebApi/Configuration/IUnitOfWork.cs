

using Repository;
using Service;

namespace WebApi.Configuration
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        Task CompleteAsync();
        void Dispose();

        IEmployeeService EmployeeService { get; }
         
    }
}
