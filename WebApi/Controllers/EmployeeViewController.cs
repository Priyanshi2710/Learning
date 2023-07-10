using Azure.Core;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApi.Controllers
{
    public class EmployeeViewController : Controller
    {
        // GET: ViewController
        public async Task<ActionResult> Index()
        {
            string apiUrl = "https://localhost:7286/Employee";

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
                return View(EmpInfo);
            }
        }


        // GET: ViewController/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7286/Employee/");
                //HTTP GET
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Employee>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
            }
            return View(employee);
            
        }

        // GET: ViewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewController/Create
        [HttpPost]

        public ActionResult Create(CreateEmployee employee)
        {
            string apiUrl = "https://localhost:7286/Employee/";


            try
            {
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
                    // Console.WriteLine(await response.Content.ReadAsStringAsync());
                    ViewBag.Message = "Data Insert Successfully";
                    return RedirectToAction("Index");

                }

            }
            catch
            {
                return View();
            }
        }

        // GET: ViewController/Edit/5
        public ActionResult Edit(int id)
        {
            CreateEmployee employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7286/Employee/");
                //HTTP GET
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<CreateEmployee>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
            }
            return View(employee);
           
        }

        // POST: ViewController/Edit/5
        [HttpPost]
       
        public  ActionResult Edit(CreateEmployee employee,int id)
        {
            
            try
            {
                string apiUrl = "https://localhost:7286/Employee/" + id;
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
                    ViewBag.Message = "Data updated Successfully";
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Confirm(int id)
        {
            return View();
        }


        // GET: ViewController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                string apiUrl = "https://localhost:7286/Employee/" + id;
                var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent("", null, "application/json");
                request.Content = content;
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(apiUrl);

                    var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();
                    // Console.WriteLine(await response.Content.ReadAsStringAsync());
                    ViewBag.Message = "Data Deleted Successfully";
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }

        }
        
    }
}
