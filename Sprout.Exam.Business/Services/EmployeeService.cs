using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> AddAsync(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(createEmployeeDto.FullName))
                    throw new CustomException("Fullname is requred.", 400);

                if (String.IsNullOrWhiteSpace(createEmployeeDto.Tin))
                    throw new CustomException("TIN is requred.", 400);

                if (createEmployeeDto.TypeId <= 0)
                    throw new CustomException("Employee Type is requred.", 400);

                var employeeToSave = new Employee()
                {
                    FullName = createEmployeeDto.FullName,
                    Birthdate = createEmployeeDto.Birthdate,
                    TIN = createEmployeeDto.Tin,
                    EmployeeTypeId = createEmployeeDto.TypeId
                };

                employeeToSave = await this.employeeRepository.AddAsync(employeeToSave);

                var employeeDto = new EmployeeDto()
                {
                    Id = employeeToSave.Id,
                    FullName = employeeToSave.FullName,
                    Birthdate = employeeToSave.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = employeeToSave.TIN,
                    TypeId = employeeToSave.EmployeeTypeId
                };

                return employeeDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
            try
            {
                var employeeInDb = (await this.GetAllAsync())
                                              .FirstOrDefault(e => e.Id == id);

                if (employeeInDb == null)
                    throw new CustomException("Employee is not found", 404);

                return employeeInDb;
            }
            catch (CustomException customException)
            {
                throw new CustomException(customException.Message, customException.StatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            try
            {
                var employeesDto = (await this.employeeRepository
                                         .GetAllAsync())
                                         .Select
                                         (
                                             e => new EmployeeDto()
                                             {
                                                 Id = e.Id,
                                                 FullName = e.FullName,
                                                 Birthdate = e.Birthdate.ToString("yyyy-MM-dd"),
                                                 Tin = e.TIN,
                                                 TypeId = e.EmployeeTypeId
                                             }
                                         ).ToList();

                return employeesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> UpdateAsync(EditEmployeeDto editEmployeeDto)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(editEmployeeDto.FullName))
                    throw new CustomException("Fullname is requred.", 400);

                if (String.IsNullOrWhiteSpace(editEmployeeDto.Tin))
                    throw new CustomException("TIN is requred.", 400);

                if (editEmployeeDto.TypeId <= 0)
                    throw new CustomException("Employee Type is requred.", 400);

                var employeeToUpdate = new Employee()
                {
                    Id = editEmployeeDto.Id,
                    FullName = editEmployeeDto.FullName,
                    Birthdate = editEmployeeDto.Birthdate,
                    TIN = editEmployeeDto.Tin,
                    EmployeeTypeId = editEmployeeDto.TypeId
                };

                employeeToUpdate = await this.employeeRepository.UpdateAsync(employeeToUpdate);

                if (employeeToUpdate == null)
                    throw new CustomException("Employee is not found.", 404);

                var employeeDto = new EmployeeDto()
                {
                    Id = employeeToUpdate.Id,
                    FullName = employeeToUpdate.FullName,
                    Birthdate = employeeToUpdate.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = employeeToUpdate.TIN,
                    TypeId = employeeToUpdate.EmployeeTypeId
                };

                return employeeDto;
            }
            catch (CustomException customException)
            {
                throw new CustomException(customException.Message, customException.StatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> DeleteAsync(int id)
        {
            try
            {
                var employeeToDelete = await this.employeeRepository.DeleteAsync(id);

                if (employeeToDelete == null)
                    throw new CustomException("Employee is not found.", 404);

                var employeeDto = new EmployeeDto()
                {
                    Id = employeeToDelete.Id,
                    FullName = employeeToDelete.FullName,
                    Birthdate = employeeToDelete.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = employeeToDelete.TIN,
                    TypeId = employeeToDelete.EmployeeTypeId
                };

                return employeeDto;
            }
            catch (CustomException customException)
            {
                throw new CustomException(customException.Message, customException.StatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> CalculateSalaryAsync(int id, decimal absentDays, decimal workedDays)
        {
            decimal salary = 0;
            try
            {
                var employeeInDb = (await this.GetAllAsync())
                                              .FirstOrDefault(e => e.Id == id);

                if (employeeInDb == null)
                    throw new CustomException("Employee is not found", 404);

                var employeeType = (EmployeeType)employeeInDb.TypeId;

                if (employeeType == EmployeeType.Regular)
                {
                    decimal monthyRate = 20000;
                    decimal absences = absentDays * (monthyRate / 22);
                    decimal tax = 12;
                    decimal taxAmount = monthyRate * (tax / 100);

                    salary = monthyRate - absences - taxAmount;
                }
                else
                {
                    decimal dailyRate = 500;
                    salary = dailyRate * workedDays;
                }
                   
                return salary;
            }
            catch (CustomException customException)
            {
                throw new CustomException(customException.Message, customException.StatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
