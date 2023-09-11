using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> DeleteAsync(int id);
    }
}
