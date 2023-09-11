using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeesController(IEmployeeService employeeService) 
        {
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await this.employeeService.GetAllAsync(); //this is from business access layer

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await this.employeeService.GetAsync(id); //this is from business access layer

                return Ok(result);
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.StatusCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            try
            {
                var item = await this.employeeService.UpdateAsync(input); //this is from business access layer

                return Ok(item);
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.StatusCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            try
            {
                var newEmployee = await this.employeeService.AddAsync(input); //this is from business access layer

                return Created($"/api/employees/{newEmployee.Id}", newEmployee.Id);
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.StatusCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }   
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await this.employeeService.DeleteAsync(id); //this is from business access layer

                return Ok(item.Id);
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.StatusCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        //public async Task<IActionResult> Calculate(int id,decimal absentDays,decimal workedDays)
        //Commented since this methods is POST and should accept model as body parameter not an individual parameters
        public async Task<IActionResult> Calculate(CalculateSalaryDto calculateSalaryDto)
        {
            try
            {
                var salary = await this.employeeService.CalculateSalaryAsync(calculateSalaryDto.Id, calculateSalaryDto.AbsentDays, calculateSalaryDto.WorkedDays); //this is from business access layer

                return Ok(Math.Round(salary, 2));
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.StatusCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
