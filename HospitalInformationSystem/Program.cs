
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{

    class Program
    {
        public static void HomePageMenu()
        {
            Console.WriteLine("Press 1 to see general information about our hospital");
            Console.WriteLine("Press 2 to see Departments and Doctors");
            Console.WriteLine("Press 3 to take online appointment");
            Console.WriteLine("Press 4 to see appointments");
            Console.WriteLine("Press 5 to see Personal Information");
        }
        public static void ITPageMenu()
        {
            Console.WriteLine("Press 1 for Patients");
            Console.WriteLine("Press 2 for Employees");
            Console.WriteLine("Press 3 for Equipments");
        }
        public static void EditPage()
        {
            
            Console.WriteLine("Press 1 to add ");
            Console.WriteLine("Press 2 to edit ");
            Console.WriteLine("Press 3 to delete ");
        }
        public static void ITLoginPage(string position, Person temp)
        {
            while (true)
            {
                Console.Write("ID: ");
                string id = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                if (position == "employee")
                {
                    if (Hospital.FindEmployee(id) != null)
                    {
                        temp = Hospital.FindEmployee(id);
                    }
                }
                else if (position == "manager")
                {
                    if (Hospital._Manager.ID != null)
                    {
                        temp = Hospital._Manager;

                    }
                }
                else { }
                temp = Hospital.FindEmployee(id);
                if (temp != null)
                {
                    if (password == temp.Password)
                    {

                        Console.WriteLine("Succesfull. You are being redirected.....");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Password is not correct");
                    }
                }
                else
                {
                    Console.WriteLine("ID is not found");
                }
            }
        }
 


        static void Main(String[] Args)
        {
            Manager manager = Manager.InstanceManager("Ali", "1234", "123456", "Male", 45, DateTime.Now, "İzmir", DateTime.Now, ("09.00-12.00", "13.00-17.00"), "university");
            Hospital hospital = new Hospital("hospital", "İzmir", "1234578", manager);
            Department Emergency = new Department("Emergency", "First Floor A block", "911");
            Department CheckUp = new Department("Check Up", "Third Floor B block ", "911");
            Department Dermotology = new Department("Dermotology", "Second Floor C block", "911");
            Hospital.Departments.Add(Emergency);
            Hospital.Departments.Add(CheckUp);
            Hospital.Departments.Add(Dermotology);
            DateTime employeeBirthDate = new DateTime(1995, 8, 24);
            DateTime employee2BirthDate = new DateTime(1991, 5, 13);
            DateTime employee3BirthDate = new DateTime(1992, 12, 18);
            DateTime employeeJoinedDate = new DateTime(2017, 1, 4);
            DateTime employee2JoinedDate = new DateTime(2018, 5, 11);
            DateTime employee3JoinedDate = new DateTime(2020, 2, 29);
            Doctor employee = new Doctor("John", "1", "0555 421 7290", "Male", 28, employeeBirthDate, "İzmir", employeeJoinedDate,"Emergency", "Proffessor", "university", ("8.30-12.00", "13.00-18.00"),"Emergency","c201");
            Employee employee2 = new Employee("Arthur", "2", "0534 729 3329", "Male", 32, employee2BirthDate, "İstanbul", employee2JoinedDate, "Other","Check Up", "Licence", "university", ("8.30-12.00", "13.00-18.00"));
            Doctor employee3 = new Doctor("Adriana", "3", "0532 610 8123", "Female", 31, employee3BirthDate, "Ankara", employee3JoinedDate ,"Dermotology", "Master Degree", "university", ("8.30-12.00", "13.00-18.00"), "Dermotology","c204");
            manager.EditAppointmentTimes(employee, 30);
            manager.EditAppointmentTimes(employee3, 30);
            Hospital.FindDepartment(employee.DepartmantName).MyEmployees.Add(employee);
            Hospital.FindDepartment(employee2.DepartmantName).MyEmployees.Add(employee2);
            Hospital.FindDepartment(employee3.DepartmantName).MyEmployees.Add(employee3);
            Hospital.FindDepartment(employee.DepartmantName).MyDoctors.Add(employee);
            Hospital.FindDepartment(employee3.DepartmantName).MyDoctors.Add(employee3);
            Hospital.Employees.Add(employee2);
            Hospital.Employees.Add(employee3);
            Hospital.Employees.Add(employee);

            while (true)
            {
                try
                {
                    Console.WriteLine("--------------------------Welcome ------------------------");
                    Console.WriteLine("Select which page you want to see [1-Home Page, 2-IT Page]");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1)
                    {
                        HomePageMenu();
                        int option = Convert.ToInt32(Console.ReadLine());
                        if (option == 1)
                        {
                            hospital.PrintInfo();
                        }
                        else if (option == 2)
                        {
                            hospital.DisplayDepartments(true);
                            Console.WriteLine("Enter department name: ");
                            string depName = Console.ReadLine();
                            Hospital.FindDepartment(depName).DisplayMyDoctors(true);
                        }
                        else
                        {
                            Patient p = new Patient();
                            while (true)
                            {
                                Console.Write("ID: ");
                                string id = Console.ReadLine();
                                Console.Write("Password: ");
                                string password = Console.ReadLine();
                                if (Hospital.FindPatient(id) != null)
                                {
                                    p = Hospital.FindPatient(id);
                                    if (password == p.Password)
                                    {
                                        Console.WriteLine("Succesfull. You are being redirected.....");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Password is not correct");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ID is not found! Do you want to Register?");
                                    if (Console.ReadLine() == "yes")
                                    {
                                        ITFacade IT = new ITFacade();
                                        IT.AddPatient();
                                        Console.WriteLine("Password: ");
                                        string newPassword = Console.ReadLine();
                                        Hospital.FindPatient(id).Password = newPassword;
                                        p = Hospital.FindPatient(id);
                                        Console.WriteLine("Succesfull. You are being redirected Login.....");

                                    }
                                }
                                
                            }
                            if (option == 3)
                            {
                                ExaminationFacade examination = new ExaminationFacade(p);
                                examination.ArrangeExamination();
                            }
                            else if (option == 4)
                            {
                                p.DisplayAllAppointments();

                            }
                            else if(option == 5)
                            {
                                p.PrintInfo();
                            }

                        }
                        Console.WriteLine();

                    }

                    else if (choice == 2)
                    {
                        Person p = new Person();
                        Console.WriteLine("Select position: (employee,manager)");
                        string position = Console.ReadLine();
                        ITLoginPage(position, p);
                        ITFacade IT = new ITFacade();
                        ITPageMenu();
                        int num = Convert.ToInt32(Console.ReadLine());
                        EditPage();
                        int operateSelection = Convert.ToInt32(Console.ReadLine());
                        if (num == 1)
                        {
                            if (operateSelection == 1)
                            {
                                IT.AddPatient();
                            }
                            else if (operateSelection == 2)
                            {
                                IT.EditPatient();
                            }
                            else
                            {
                                IT.DeletePatient();
                            }
                        }
                        else if (num == 2)
                        {
                            if (operateSelection == 1)
                            {
                                IT.AddEmployee();
                            }
                            else if (operateSelection == 2)
                            {
                                IT.EditEmployee();
                            }
                            else
                            {
                                IT.DeleteEmployee();
                            }
                        }
                        else
                        {
                            if (operateSelection == 1)
                            {
                                IT.AddEquipment();
                            }
                            else if (operateSelection == 2)
                            {
                                IT.EditEquipment();
                            }
                            else
                            {
                                IT.DeleteEquipment();
                            }
                        }
                        Console.WriteLine();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine();
                }

            }
        }
    }
}
