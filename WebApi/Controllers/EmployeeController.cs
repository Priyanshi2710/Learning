using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Service;
using System.IO;
using System.Net;
using WebApi.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
      
        private readonly IEmployeeService _employeeService;


        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var emp = await _employeeService.GetAll();


            return Ok(emp);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please enter correct employee id " + id);
            }
            else
            {
                var item = await _employeeService.GetById(id);
                
                if (item == null)
                {
                    return NotFound("Employee is not found for the employee id " + id);
                }
                else
                {
                    return Ok(item);
                }
            }
        }

        [HttpPost(Name = "AddEmployee")]
        public async Task<ActionResult<Employee>> Add(CreateEmployee entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Validation validate = new Validation();
                    string msg = validate.validation(entity);
                    if (msg == "")
                    {
                        var emp = new Employee()
                        {
                            Firstname = entity.Firstname,
                            Lastname = entity.Lastname,
                            Email = entity.Email,
                            MaritalStatus = entity.MaritalStatus,
                            Gender = entity.Gender,
                            Birthdate = entity.Birthdate,
                            Salary = entity.Salary,
                            Address = entity.Address,
                            CityCode = entity.CityCode,
                            CountryCode = entity.CountryCode,
                            StateCode = entity.StateCode,
                            Password = entity.Password,
                            Created = DateTime.Today,
                            Updated = DateTime.Today
                        };
                        await _employeeService.Add(emp);

                       // await _employeeService.CompleteAsync();

                        return Ok("Employee created successfully." + emp.EmpID);

                    }
                    else
                    {
                        return BadRequest(msg);

                    }
                }
                else
                {

                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut, ActionName("UploadFile")]
        public async Task<IActionResult> UploadFileAsync([FromForm] FileUpload upload)
        {

            try
            {
                if (upload.File == null || upload.File.Length == 0)
                {
                    return BadRequest("File not uploaded");
                }
                else
                {
                    var empid = upload.id;
                    var data = await _employeeService.GetById(empid);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (data.EmpID != 0)
                        {
                            var file = upload.File;


                            string extension = Path.GetExtension(file.FileName);
                            var newname = empid.ToString();
                            string newFileName = newname.Trim() + extension; //pass newName here

                            var path = Path.Combine(Directory.GetCurrentDirectory(), "Image", newFileName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            data.EmpPhoto = newFileName;
                            await _employeeService.Update(data);

                            return Ok("file uploaded successfully..." + path);

                        }
                    }
                }


                return ValidationProblem();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] CreateEmployee entity, int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Please provide employee id");
                }
                else
                {
                    Validation validate = new Validation();
                    string msg = validate.validation(entity);
                    if (msg == "")
                    {
                        var data = await _employeeService.GetById(id);

                        if (data.EmpID == id)
                        {

                            data.Firstname = entity.Firstname;
                            data.Lastname = entity.Lastname;
                            data.Email = entity.Email;
                            data.MaritalStatus = entity.MaritalStatus;
                            data.Gender = entity.Gender;
                            data.Birthdate = entity.Birthdate;
                            data.Salary = entity.Salary;
                            data.Address = entity.Address;
                            data.CityCode = entity.CityCode;
                            data.CountryCode = entity.CountryCode;
                            data.StateCode = entity.StateCode;
                            data.Password = entity.Password;
                            data.Updated = DateTime.Today;

                            await _employeeService.Update(data);

                            return Ok("Record updated successfully for employee id .." + id);

                        }

                        else
                        {
                            return BadRequest("No data found for employee id of " + id);
                        }
                    }
                    else
                    {
                        return BadRequest(msg);

                    }

                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
             
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if(id == 0)
            {
                    return BadRequest("Please provide employee id...");
            }
            else
            {
                var item = await _employeeService.GetById(id);
                if (item == null)
                {
                    return NotFound("Employee is not found for the employee id " + id);
                }
                else
                {
                    await _employeeService.Delete(item);
                    return Ok("Record successfully deleted...");
                }
                           
            }
        }
    }

    public class Validation : ValidationMsg
    {
        public string validation(CreateEmployee entity)
        {
            string msg = "";
            var Today = DateTime.Today;
            DateTime date = (DateTime)entity.Birthdate;
            if (entity.Salary < 500)
            {
                return SalaryMsg;
            }
            if(Today.Year - date.Year< 18)
            {
                return Age;
            }
            if (Today.Year - date.Year == 18)
            {
               if(Today.Month < date.Month)
                {
                    return Age;
                }
               if(Today.Month == date.Month)
                {
                    if (Today.Day < date.Day)
                    {
                        return Age;
                    }
                }
            }
            return msg;
           
        }
    }


}
