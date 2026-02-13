namespace razorpagesExample.Repository
{
    using System.Collections.Generic;
    using razorpagesExample.Models;

    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Photo = "john.jpg",
                    Department = "HR",
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Photo = "jane.jpg",
                    Department = "IT",
                },
                new Employee
                {
                    Id = 3,
                    Name = "Bob Johnson",
                    Email = "bob.johnson@example.com",
                    Photo = "bob.jpg",
                    Department = "Finance",
                },
                new Employee
                {
                    Id = 4,
                    Name = "Alice Williams",
                    Email = "alice.williams@example.com",
                    Photo = "alice.jpg",
                    Department = "Marketing",
                },
            };
        }

        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
