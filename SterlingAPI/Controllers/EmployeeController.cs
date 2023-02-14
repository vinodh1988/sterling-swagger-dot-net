using SterlingAPI.AuthAttribute;
using SterlingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SterlingAPI.Controllers
{
    [RoutePrefix("api/employees")]
    [BasicAuthentication]
    public class EmployeeController : ApiController
    {
        EmployeeRepository repo;

        EmployeeController()
        {
            repo = new EmployeeRepository();
        }
        // GET: api/Employee

        /// <summary>
        /// Gives list of employees in JSON Format
        /// </summary>
        /// <returns>Json Employees list</returns>
        /// 	<response code="200">Employee processed and returned</response>
        ///	<response code = "500" > Server Side Error - May be Database Related</response>

        [Route("")]
        public IEnumerable<Employee> Get()
        {
            return repo.GetEmployees();
        }

        [Route("{id:int}")]
        // GET: api/Employee/5
        public IHttpActionResult Get(int id)
        {
            Employee result = repo.GetEmployee(id);
            if (result != null)
                return Ok(result);
            else
                return new StatusCodeResult(HttpStatusCode.NoContent, this);

        }


        // POST: api/Employee
        /// <summary>
        ///   Adding A New Employee
        /// </summary>
        /// <remarks>
        /// Example record
        /// {
        /// "eno":3,
        /// "name":"Rajeev"
        /// "city":"Chennai"
        /// }
        /// </remarks>
        /// <param name="employee"></param>
        /// <returns>Nothing</returns>
        /// 
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]Employee employee)
        {
            if (employee == null)
                return BadRequest();
            
               repo.Add(employee);
            return new StatusCodeResult(HttpStatusCode.Created, this);
        }
        /// <summary>
        ///  Adding a New Employee using form fields
        /// </summary>
        /// <param name="eno">Employee number</param>
        /// <param name="name"> Name</param>
        /// <param name="city"> City </param>
        /// <returns></returns>
        [Route("formpost")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]int eno, [FromBody] String name, [FromBody] String city) {
            Employee emp = new Employee {
                Eno=eno,Name=name,city=city
            };
            repo.Add(emp);
            return new StatusCodeResult(HttpStatusCode.Created, this);
        }

        [Route("upload")]
        [HttpPost]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
        /*

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }*/
    }
}
