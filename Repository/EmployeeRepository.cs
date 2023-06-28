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
                if (entity == null)
                {
                    return false;
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

        public override async Task<bool> Delete(Employee entity)
        {
            try
            {
                
                if (entity == null) return false;

                dbSet.Remove(entity);
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