using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> AddAsync(CreateEmployeeDto createEmployeeDto);
        Task<EmployeeDto> GetAsync(int id);
        Task<EmployeeDto> UpdateAsync(EditEmployeeDto editEmployeeDto);
        Task<EmployeeDto> DeleteAsync(int id);
        Task<decimal> CalculateSalaryAsync(int id, decimal absentDays, decimal workedDays);
    }
}
