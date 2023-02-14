using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingAPI.Models
{
    public class EmployeeRepository
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Eno=1, Name="Naresh", city="Chennai"},
            new Employee { Eno=2, Name="Lokesh", city="Mumbai"},
            new Employee { Eno=3, Name="Harish", city="Jaipur"}
        };

        public IEnumerable<Employee> GetEmployees() {
            return employees;
        }

        public Employee GetEmployee(int id) {
            Employee emp = employees.Find(x => x.Eno == id);
              
                    return emp;
             
        }

        public void Add(Employee employee) {
            employees.Add(employee);
        }
    }
}