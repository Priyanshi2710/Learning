
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;

namespace WebApi.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;
     
        public IEmployeeRepository Employee { get; private set; }

        public IEmployeeService EmployeeService { get; private set; }

       

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
         
            Employee = new EmployeeRepository(context);

            EmployeeService = new EmployeeService((EmployeeRepository)Employee);
        
          
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
