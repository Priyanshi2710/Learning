using Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
           
        }
        [EnableCors("AllowAllOrigins")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var emp = await _employeeService.GetAll();
                if(emp == null)
                {
                    return NotFound("No records are available.");
                }
                return Ok(emp);

            }catch (Exception ex)
            {
                return NotFound(new {error = ex.Message});
            }
            
        }
        [EnableCors("AllowAllOrigins")]
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            try
            {
                if (employeeId == 0)
                {
                    return NotFound("Please enter correct employee id " + employeeId);
                }
                else
                {
                    var item = await _employeeService.GetById(employeeId);

                    if (item == null)
                    {
                        return NotFound("Employee not found" + employeeId);
                    }
                    else
                    {
                        return Ok(item);
                    }
                }
            }catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
            
        }
        [EnableCors("AllowAllOrigins")]
        [HttpPost(Name = "AddEmployee")]
        public async Task<ActionResult<Employee>> Add(CreateEmployee entity)
        {
            try
            {
                if (ModelState.IsValid)
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
                        return Ok("Employee is created" + emp.EmpID);

                }
                else
                {

                    return NotFound(ModelState);
                }
            }
            catch (Exception ex)
            {

                return NotFound(new { error = ex.Message });
            }
        }
        [EnableCors("AllowAllOrigins")]
        [HttpPut, ActionName("UploadFile")]
        public async Task<IActionResult> UploadFileAsync([FromForm] FileUpload upload)
        {

            try
            {
                if (upload.File == null || upload.File.Length == 0)
                {
                    return NotFound("File not uploaded");
                }
                else
                {
                    var employeeId = upload.id;
                    var data = await _employeeService.GetById(employeeId);
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
                            var newname = employeeId.ToString();
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
                return NotFound(new { error = ex.Message });
            }
        }
        [EnableCors("AllowAllOrigins")]
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Update(CreateEmployee entity, int employeeId)
        {
            try
            {
                if (employeeId == 0)
                {
                    return NotFound("Please provide employee id");
                }
                else
                {
                    Validation validate = new Validation();
                    string msg = validate.validation(entity);
                    if (msg == "")
                    {
                        var data = await _employeeService.GetById(employeeId);

                        if (data.EmpID == employeeId)
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

                            return Ok("Record updated successfully for employee id .." + employeeId);

                        }

                        else
                        {
                            return NotFound("No data found for employee id of " + employeeId);
                        }
                    }
                    else
                    {
                        return BadRequest(msg);

                    }

                }
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
        [EnableCors("AllowAllOrigins")]
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                if (employeeId == 0)
                {
                    return NotFound("Please provide employee id...");
                }
                else
                {
                    var item = await _employeeService.GetById(employeeId);
                    if (item == null)
                    {
                        return NotFound("Employee is not found for the employee id " + employeeId);
                    }
                    else
                    {
                        await _employeeService.Delete(item);
                        return Ok("Record successfully deleted...");
                    }

                }

            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
           
        }
    }

    public class Validation : ValidationMsg
    {
        public string validation(CreateEmployee entity)
        {
            string msg = "";
            
            if (entity.Salary < 500)
            {
                return SalaryMsg;
            }
            
            return msg;

        }
    }


}
