

using Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Net.Http.Headers;
using System.Data;
using ClosedXML.Excel;
using System.Reflection;

namespace WebApi.Controllers
{
    public class EmployeeViewController : Controller
    {
        private readonly IConfiguration _configuration;
        public EmployeeViewController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        // GET: ViewController
        public async Task<ActionResult> Index(int page = 1)
        {
            int pageSize = _configuration.GetValue<int>("MySettings:pageSize");
            ViewBag.Message = TempData["Message"];
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            string apiUrl = $"{url}Employee/";

            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("/Employee");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
                //returning the employee list to view
                //int pageSize = 3; // Number of items per page



                int totalItems = EmpInfo.Count(); // Total number of items

                var paginatedData = EmpInfo.Skip((page - 1) * pageSize).Take(pageSize);
                ViewData["EmployeeData"] = paginatedData;

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                return View();

            }
        }
        [HttpPost]
        public async Task<FileResult> ExportToExcel(int CurrentPage)
        {
            int pageSize = _configuration.GetValue<int>("MySettings:pageSize");
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            string apiUrl = $"{url}Employee/";

            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("/Employee");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }

                //returning the employee list to view
                if (CurrentPage > 0)
                {
                    //int pageSize = 3;
                    int totalItems = EmpInfo.Count(); // Total number of items
                    EmpInfo = EmpInfo.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();


                }
                DataTable dt = new DataTable(typeof(Employee).Name);
                PropertyInfo[] PropertyInfos = typeof(Employee).GetProperties();

                foreach (PropertyInfo prop in PropertyInfos)
                {
                    dt.Columns.Add(prop.Name);
                }


                foreach (var item in EmpInfo)
                {
                    var values = new object[PropertyInfos.Length];
                    for (int i = 0; i < PropertyInfos.Length; i++)
                    {
                        values[i] = PropertyInfos[i].GetValue(item, null);
                    }
                    dt.Rows.Add(values);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        if (CurrentPage > 0)
                        {
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeReport.xlsx");
                        }
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllEmployeeReport.xlsx");
                    }
                }
            }



        }


        // GET: ViewController/Details/5
        public ActionResult Details(int EmployeeID)
        {
            Employee employee = null;

            using (var client = new HttpClient())
            {

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
                string apiUrl = $"{baseUrl}Employee/";
                // client.BaseAddress = new Uri("https://localhost:7286/Employee/");
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(EmployeeID.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Employee>();
                    readTask.Wait();

                    employee = readTask.Result;
                    ViewData["D"] = apiUrl;
                }
            }
            return View(employee);

        }

        // GET: ViewController/Create
        public ActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        // POST: ViewController/Create
        [HttpPost]

        public ActionResult Create(CreateEmployee employee)
        {
            
            try
            {
                
                if (!ModelState.IsValid)
                {
                   
                        TempData["Message"] = "Kindly fill all details.";
                        return RedirectToAction("Create");
                    
                }
                    

                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
                string apiUrl = $"{url}Employee/";

                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(apiUrl);

                    var content = new StringContent(JsonConvert.SerializeObject(employee), null, "application/json");

                    request.Content = content;
                    var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();


                    return RedirectToAction("Index");

                }

            }
            catch 
            {
                return View();
            }
        }

        // GET: ViewController/Edit/5
        public ActionResult Edit(int EmployeeID)
        {
            CreateEmployee employee = null;

            using (var client = new HttpClient())
            {
                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
                string apiUrl = $"{url}Employee/";
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(EmployeeID.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<CreateEmployee>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
                else
                {
                    TempData["Message"] = "Employee not found";
                    return View();
                }
            }
            return View(employee);

        }

        // POST: ViewController/Edit/5
        [HttpPost]

        public ActionResult Edit(CreateEmployee employee, int EmployeeID)
        {

            try
            {

                if (!ModelState.IsValid)
                {

                    TempData["Message"] = "No details are changed";
                    return RedirectToAction("Edit");

                }



                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
                string apiUrl = $"{url}Employee/{EmployeeID}";
                var request = new HttpRequestMessage(HttpMethod.Put, apiUrl);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(apiUrl);

                    var content = new StringContent(JsonConvert.SerializeObject(employee), null, "application/json");

                    request.Content = content;
                    var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();
                    // Console.WriteLine(await response.Content.ReadAsStringAsync());
                    TempData["Message"] = "Data updated Successfully";
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View(ViewBag.Message);
            }
        }
        public ActionResult Confirm(int EmployeeID)
        {
            Employee employee = null;

            using (var client = new HttpClient())
            {
                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
                string apiUrl = $"{url}Employee/";
                // client.BaseAddress = new Uri("https://localhost:7286/Employee/");

                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(EmployeeID.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Employee>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
            }
            ViewData["ID"] = EmployeeID;
            return View(employee);

        }


        // GET: ViewController/Delete/5
        [HttpPost]

        public ActionResult Delete(int EmployeeID)
        {
            try
            {
                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
                string apiUrl = $"{url}Employee/{EmployeeID}";
                var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent("", null, "application/json");
                request.Content = content;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(apiUrl);
                    
                    var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    TempData["Message"] = "Employee data deleted Successfully";
                    return RedirectToAction("Index");
                }

            }
            catch (NullReferenceException)
            {

                return View();
            }

        }

    }
}