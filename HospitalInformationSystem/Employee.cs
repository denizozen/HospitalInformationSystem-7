using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalInformationSystem
{
    class Employee : Person
    {        
        public String DepartmantName;
        public string Degree { get; set; }
        public String Education { get; set; }
        public Double Salary { get; set; }
        public string Job { get; set; }
        public (String, String) WorkingHours { get; set; } // (a.m.interval , p.m.interval)      
        public List<(DateTime, DateTime)> OverTime { get; set; } // (begin, finish)

        public Employee() : base()
        {
            DepartmantName = string.Empty;
            Degree = string.Empty;            
            Education = string.Empty;
            Salary = 0;
            Job= string.Empty;
            WorkingHours = (string.Empty, string.Empty);
            OverTime = new List<(DateTime, DateTime)>();
            Password = "e123";
        }
        public Employee(string fullName, string id, string phoneNumber, string gender, int age, DateTime birthDate, string homeAddress, DateTime joinedDate, 
            string job, string depName, string degree, string education,(string,string) workingHours) 
            : base(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, joinedDate)
        {
            DepartmantName = depName; 
            Degree = degree;
            Education = education;
            Salary = 0;
            Job = job;
            WorkingHours = workingHours;
            OverTime = new List<(DateTime, DateTime)>();
            Password = "e123";
        }

        public Department FindMyDepartment(String employeeID)
        {
            for (int i = 0; i < Hospital.Departments.Count; i++)
            {
                Department department = Hospital.Departments[i];
                for (int j = 0; j < department.MyEmployees.Count; j++)
                {
                    if (department.MyEmployees[i].ID == employeeID)
                    {
                        return department;
                    }
                }
            }
            throw new Exception();
        }
        public void WorkOvertime(DateTime begin, DateTime finish)
        {
            OverTime.Add((begin, finish));
        }
        public Double RaiseSalary(double salary)
        {
            return Salary + salary;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("Departmant Name : " + DepartmantName + "\nJob: " + Job + "\nDegree: " + Degree +
                "\nWorking Hours : " + WorkingHours + "\nEducation : " + Education);

        }
       
    }
}
