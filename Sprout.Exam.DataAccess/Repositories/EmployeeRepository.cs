using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            try
            {
                await this.appDbContext.Employee.AddAsync(employee);
                await this.appDbContext.SaveChangesAsync();

                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> DeleteAsync(int id)
        {
            try
            {
                var employeeInDb = await this.appDbContext.Employee.FindAsync(id);

                if (employeeInDb != null)
                {
                    this.appDbContext.Remove(employeeInDb);
                    await this.appDbContext.SaveChangesAsync();
                }
                else
                    return null;

                return employeeInDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await this.appDbContext.Employee.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            try
            {
                var employeeInDb = await this.appDbContext.Employee.FindAsync(employee.Id);

                if (employeeInDb != null)
                {
                    employeeInDb.FullName = employee.FullName;
                    employeeInDb.Birthdate = employee.Birthdate;
                    employeeInDb.TIN = employee.TIN;
                    employeeInDb.EmployeeTypeId = employee.EmployeeTypeId;

                    await this.appDbContext.SaveChangesAsync();
                }
                else
                    return null;

                return employeeInDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
