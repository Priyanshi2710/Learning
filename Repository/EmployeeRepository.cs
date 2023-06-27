using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<bool> Update(Employee entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.EmpID == entity.EmpID)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return await Add(entity);
                }
                else
                {
                    context.Employees.Update(entity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.EmpID == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;


                dbSet.Remove(exist);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
       
    }
}