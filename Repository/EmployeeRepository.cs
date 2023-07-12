using Domain.Models;

namespace Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context)
        {
        }

        public override Task<bool> Update(Employee entity)
        {
            try
            {
                if (entity == null)
                {
                    return Task.FromResult(false);
                }
                else
                {
                    context.Employees.Update(entity);
                    context.SaveChanges();
                    return Task.FromResult(true);
                }
            }
            catch (Exception)
            {

                return Task.FromResult(false);
            }
        }

        public override Task<bool> Delete(Employee entity)
        {
            try
            {

                if (entity == null) return Task.FromResult(false);

                dbSet.Remove(entity);
                context.SaveChanges();

                return Task.FromResult(true);
            }
            catch (Exception)
            {

                return Task.FromResult(false);
            }
        }

    }
}