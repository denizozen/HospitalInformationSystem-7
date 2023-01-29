using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class Manager : Person
    {
        private static Manager instance = null;
        public (String, String) WorkingHours { get; set; }
        private String Education { get; set; }
        private Double Salary { get; set; }

        private Manager(string fullName, string id, string phoneNumber, string gender, int age, DateTime birthDate, string homeAddress, DateTime joinedDate,
            (string,string) workingHours, string education)
            : base(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, joinedDate)
        {
            WorkingHours = workingHours;
            Education= education;
            Salary = 0;
            Password = "m123";
        }
        public static Manager InstanceManager(string fullName, string id, string phoneNumber, string gender, int age, DateTime birthDate, string homeAddress, DateTime joinedDate,
            (string, string) workingHours, string education)
        {
            if (instance == null)
            {
                instance = new Manager(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, joinedDate, workingHours, education);
            }
            return instance;
        }

        public void HireEmployee(Employee e)
        {
            if (e.Job == "Doctor")
            {
                Console.Write("Specialty: ");
                string specialty = Console.ReadLine();
                Console.Write("Room: ");
                string room = Console.ReadLine();
                e = new Doctor(e.FullName, e.ID, e.PhoneNumber, e.Gender, e.Age, e.BirthDate, e.HomeAddress, DateTime.Now, e.DepartmantName, e.Degree, e.Education, e.WorkingHours,
                    specialty, room);
                Doctor d = new Doctor(e.FullName, e.ID, e.PhoneNumber, e.Gender, e.Age, e.BirthDate, e.HomeAddress, DateTime.Now, e.DepartmantName, e.Degree, e.Education, e.WorkingHours,
                    specialty, room);
                for (int i = 0; i < Hospital.Departments.Count; i++)
                {
                    if (e.DepartmantName == Hospital.Departments[i].Name)
                    {
                        Hospital.Departments[i].MyDoctors.Add(d);
                    }
                }
                e.Salary = 25000;
            }
            else if (e.Job == "Recepcionist")
            {
                e = new Recepcionist(e.FullName, e.ID, e.PhoneNumber, e.Gender, e.Age, e.BirthDate, e.HomeAddress, DateTime.Now, e.DepartmantName, e.Degree, e.Education, e.WorkingHours);
                e.Salary = 8500;
            }
            else if (e.Job == "Stock Controller")
            {
                e = new StockController(e.FullName, e.ID, e.PhoneNumber, e.Gender, e.Age, e.BirthDate, e.HomeAddress, DateTime.Now, e.DepartmantName, e.Degree, e.Education, e.WorkingHours);
                e.Salary = 8500;
            }
            else
            {
                e = new Employee(e.FullName, e.ID, e.PhoneNumber, e.Gender, e.Age, e.BirthDate, e.HomeAddress, DateTime.Now, e.Job, e.DepartmantName, e.Degree, e.Education, e.WorkingHours);
                Console.WriteLine("Enter the salary: ");
                Hospital.FindEmployee(e.ID).Salary = Convert.ToDouble(Console.ReadLine());
            }
            Hospital.Employees.Add(e);
            for(int i=0;i<Hospital.Departments.Count;i++)
            {
                if (e.DepartmantName == Hospital.Departments[i].Name) 
                {
                    Hospital.Departments[i].MyEmployees.Add(e);                   
                }
            }
        }
        public void DischargeEmployee(Employee e)
        {
            Department dep = Hospital.FindDepartment(e.DepartmantName);
            if(dep != null)
            {
                dep.MyEmployees.Remove(e);
            }
            Hospital.Employees.Remove(e);     
        }
        public void EditEmployee(Employee employee, int choice)
            
        {
            if (choice == 1)
            {
                Console.Write("Enter new name : ");
                employee.FullName = Console.ReadLine(); ;
            }
            else if (choice == 2)
            {
                Console.Write("Enter new ID : ");
                employee.ID = Console.ReadLine();
            }
            else if (choice == 3)
            {
                Console.Write("Enter new phone number  : ");
                employee.PhoneNumber = Console.ReadLine();
            }
            else if (choice == 4)
            {
                Console.Write("Enter new age  : ");
                employee.Age = Convert.ToInt32(Console.ReadLine());
            }
            else if (choice == 5)
            {
                Console.Write("Enter new birth date  : (ex. dd/mm/yyyy) ");
                string line = Console.ReadLine();
                List<string> date_components = line.Split('/').ToList();
                DateTime birthDate = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]));
                employee.BirthDate = birthDate;
            }
            else if (choice == 6)
            {
                Console.Write("Enter new home address : ");
                employee.HomeAddress = Console.ReadLine();
            }
            else if (choice == 7)
            {
                Console.Write("Enter new degree : ");
                employee.Degree = Console.ReadLine();
            }
            else if (choice == 8)
            {
                Console.Write("Enter new education : ");
                employee.Education = Console.ReadLine();
            }
            else if (choice == 9)
            {
                Console.Write("Enter new working hours : (ex. 09.00-12.00/13.00-17.00)");
                string line = Console.ReadLine();
                string beforeLunch = line.Split('/').ToList()[0];
                string afterLunch = line.Split('/').ToList()[1];
                (string, string) workingHours = (beforeLunch, afterLunch);
                employee.WorkingHours = workingHours;
            }
            
            else if (choice == 10)
            {
                Console.Write("Enter new salary : ");
                employee.Salary = Convert.ToDouble(Console.ReadLine());
            }
            else if (choice == 11)
            {
                Doctor doctor = new Doctor();
                doctor = Hospital.FindDepartment(employee.DepartmantName).FindDoctor(employee.ID);
                if (doctor != null)
                {
                    Console.WriteLine("Enter new specialty:  ");
                    string specialty = Console.ReadLine();
                    doctor.Specialty = specialty;
                }
            }
            else if (choice == 12)
            {
                Doctor doctor = new Doctor();
                doctor = Hospital.FindDepartment(employee.DepartmantName).FindDoctor(employee.ID);
                if (doctor != null)
                {
                    Console.WriteLine("Enter new room:  ");
                    string room = Console.ReadLine();
                    doctor.Room = room;
                }
            }
            else if (choice == 13)
            {
                Doctor doctor = new Doctor();
                doctor = Hospital.FindDepartment(employee.DepartmantName).FindDoctor(employee.ID);
                if (doctor != null)
                {
                    Console.WriteLine("Enter new duration of examination:  ");
                    int duration = Convert.ToInt32(Console.ReadLine());
                    EditAppointmentTimes(doctor, duration);
                }
            }
            else { }

        }
        public void EditAppointmentTimes(Doctor doctor, int duration_examination)
        {
            String beforeLunch = doctor.WorkingHours.Item1;
            String afterLunch = doctor.WorkingHours.Item2;
            List<String> firstInterval = beforeLunch.Split('-').ToList();
            List<String> lastInterval = afterLunch.Split('-').ToList();
            List<TimeOnly> times = new List<TimeOnly>();
            times.Add(new TimeOnly(Convert.ToInt32(firstInterval[0].Split('.')[0]), Convert.ToInt32(firstInterval[0].Split('.')[1])));
            times.Add(new TimeOnly(Convert.ToInt32(firstInterval[1].Split('.')[0]), Convert.ToInt32(firstInterval[1].Split('.')[1])));
            times.Add(new TimeOnly(Convert.ToInt32(lastInterval[0].Split('.')[0]), Convert.ToInt32(lastInterval[0].Split('.')[1])));
            times.Add(new TimeOnly(Convert.ToInt32(lastInterval[1].Split('.')[0]), Convert.ToInt32(lastInterval[1].Split('.')[1])));
            List<TimeOnly> examinationTimes = new List<TimeOnly>();           
            examinationTimes.Add(times[0]);
            TimeOnly timee = times[0].AddMinutes(duration_examination);
            while (timee.IsBetween(times[0], times[1]))
            {
                examinationTimes.Add(timee);
                timee = timee.AddMinutes(duration_examination);
            }
            examinationTimes.Add(times[2]);
            timee = times[2].AddMinutes(duration_examination);
            while (timee.IsBetween(times[2], times[3]))
            {
                examinationTimes.Add(timee);
                timee = timee.AddMinutes(duration_examination);
            }
            doctor.AppointmentTimes = examinationTimes;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("Working Hours : " + WorkingHours + "\nEducation : " + Education);
        }
    }
}
