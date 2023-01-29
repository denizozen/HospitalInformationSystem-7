using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class ITFacade
    {
        private Manager _Manager { get; set; }
        private Recepcionist _Recepcionist { get; set; }
        private StockController _StockController { get; set; }
        public ITFacade()
        {
            _Manager = Hospital._Manager;
            _Recepcionist = new Recepcionist();
            _StockController = new StockController();
        }

        public void AddEmployee()
        {
            Console.WriteLine("Fill required information...");
            Console.Write("Department Name: ");
            string depName = Console.ReadLine();
            Console.Write("Job: ");
            string job = Console.ReadLine();
            Console.Write("Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("ID: ");
            string id = Console.ReadLine();
            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Gender: ");
            string gender = Console.ReadLine();
            Console.Write("Age: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Birthdate: (ex. dd/mm/yyyy) ");
            string line = Console.ReadLine();
            List<string> date_components = line.Split('/').ToList();
            DateTime birthDate = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]));
            Console.Write("Home Address: ");
            string homeAddress = Console.ReadLine();
            Console.Write("Degree: ");
            string degree = Console.ReadLine();
            Console.Write("Education: ");
            string education = Console.ReadLine();
            Console.Write("Working Hours: (ex. 09.00-12.00/13.00-17.00)");
            line= Console.ReadLine();
            string beforeLunch = line.Split('/').ToList()[0];
            string afterLunch =line.Split('/').ToList()[1];
            (string, string) workingHours = (beforeLunch, afterLunch);
            Employee e = new Employee(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, DateTime.Now, job,depName, degree, education, workingHours);
            
            _Manager.HireEmployee(e);
        }

        public void AddPatient()
        {
            Console.WriteLine("Fill required information...");
            Console.Write("Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("ID: ");
            string id = Console.ReadLine();
            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Gender: ");
            string gender = Console.ReadLine();
            Console.Write("Age: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Birthdate: (ex. dd/mm/yyyy) ");
            string line = Console.ReadLine();
            List<string> date_components = line.Split('/').ToList();
            DateTime birthDate = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]));
            Console.Write("Home Address: ");
            string homeAddress = Console.ReadLine();
            Console.Write("Select Insurance: (state-special) ");
            line =  Console.ReadLine();
            HealthInsurance insurance = new HealthInsurance();
            if (line == "state")
            {
                insurance = HealthInsurance.state;
            }
            else
            {
                insurance = HealthInsurance.special;
            }           
            Patient p = new Patient(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, DateTime.Now,insurance);
            Console.Write("Enter alergies seperated by commas: ");
            p.Alergies = Console.ReadLine().Split(',').ToList();
            Console.Write("Enter diseases seperated by commas: ");
            p.Diseases = Console.ReadLine().Split(',').ToList();
            _Recepcionist.RegisterPatient(p);

        }

        public void AddEquipment()
        {
            Console.WriteLine("Fill required information...");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Available Amount: ");
            int availableAmount = Convert.ToInt32(Console.ReadLine());  
            Console.Write("Required Amount: ");
            int requiredAmount = Convert.ToInt32(Console.ReadLine());
            Equipment e = new Equipment(name, price, availableAmount, requiredAmount);
            _StockController.AddEquipment(e);
        }
        public bool IsFound(String type, String entry)
        {
            if(type == "Emp")
            {
                Employee emp = Hospital.FindEmployee(entry);
                if (emp != null) 
                {
                    Console.WriteLine("Here is the Employee.. ");     
                    if(emp.Job == "Doctor")
                    {
                        emp = Hospital.FindDepartment(emp.DepartmantName).FindDoctor(entry);
                    }
                    emp.PrintInfo();
                    Console.WriteLine("Salary: " + emp.Salary);
                    return true; 
                } else
                {
                    Console.WriteLine("The employee not found!");
                    return false;
                }
            }
           
            else if (type == "Eq")
            {
                Equipment eq = Hospital.FindEquipment(entry);
                if (eq != null) 
                {
                    Console.WriteLine("Here is the Equipment.. ");
                    eq.PrintInfo();
                    return true; 
                }
                else
                {
                    Console.WriteLine("The equipment not found!");
                    return false;
                }
            }
            else if(type == "P")
            {
                Patient p = Hospital.FindPatient(entry);
                if (p != null) 
                {
                    Console.WriteLine("Here is the Patient.. ");
                    p.PrintGeneralInfo();
                    return true; 
                }
                else
                {
                    Console.WriteLine("The patient not found!");
                    return false;
                }
            }
            return false;
        }
        public void EditEmployee()
        {
            Console.Write("Enter employee ID : ");
            String employeeID = Console.ReadLine();
            if (IsFound("Emp", employeeID))
            {
                Employee employee = Hospital.FindEmployee(employeeID);
                string option = "";
                if (employee.Job == "Doctor")
                {
                    option = ",11-specialty, 12-room, 13-Appointment Times";
                }
                Console.WriteLine("Enter the num of the information to arrange: (1-name,2-id,3-phone,4-Age,5-Birthdate,6-HomeAdress,7-Degree,8-education,9-Working Hours,10-Salary" + option + "), PRESS -1 TO SKIP");
                int number = Convert.ToInt32(Console.ReadLine());
                while (number != -1)
                {
                    _Manager.EditEmployee(employee,number);
                    Console.WriteLine("Enter the num of the information to arrange: (1-name,2-id,3-phone,4-Age,5-Birthdate,6-HomeAdress,7-Degree,8-education,9-Working Hours,10-Salary" + option + "), PRESS -1 TO SKIP");
                    number = Convert.ToInt32(Console.ReadLine());
                }                              
            }         
        }

        public void EditPatient()
        {
            Console.Write("Enter patient ID : ");
            String patientID = Console.ReadLine();
            if (IsFound("P", patientID))
            {
                Patient patient = Hospital.FindPatient(patientID);
                Console.WriteLine("Press 1 to arrange, Press 2 to operate examination process, Press -1 to skip");
                int selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 1)
                {
                    Console.WriteLine("Enter the num of the information to arrange: (1-name,2-id,3-phone,4-gender,5-Age,6-Birthdate,7-HomeAdress,8-insurance,9-Alergies,10-Diseases), PRESS -1 TO SKIP");
                    int number = Convert.ToInt32(Console.ReadLine());
                    while (number != -1)
                    {
                        _Recepcionist.EditPatientInfo(patient, number);
                        Console.WriteLine("Enter the num of the information to arrange: (1-name,2-id,3-phone,4-gender,5-Age,6-Birthdate,7-HomeAdress,8-insurance,9-Alergies,10-Diseases), PRESS -1 TO SKIP");
                        number = Convert.ToInt32(Console.ReadLine());
                    }
                }
                else if(selection == 2) 
                {
                    ExaminationFacade examination = new ExaminationFacade(patient);
                    examination.Menu();
                    int option = Convert.ToInt32(Console.ReadLine());
                    while (option != -1)
                    {
                        if (option == 1)
                        {
                            examination.DisplayPatient();
                        }
                        else if (option == 2)
                        {
                            examination.Direct();
                        }
                        else if (option == 3)
                        {
                            examination.SetTestResult();
                        }
                        else if (option == 4)
                        {
                            examination.DisplayTest();
                        }
                        else if (option == 5)
                        {
                            examination._DecideTreatment();
                        }
                        else if (option == 6)
                        {
                            examination.Payment();
                        }
                        else { }
                        examination.Menu();
                        option = Convert.ToInt32(Console.ReadLine());
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EditEquipment()
        {
            Console.Write("Enter equipment name : ");
            String equipmentName = Console.ReadLine();
            if (IsFound("Eq", equipmentName))
            {
                Equipment equipment = Hospital.FindEquipment(equipmentName);
                Console.WriteLine("Enter 1 to edit Name, 2 to edit Price, 3 to decrease amount, 4 to increase amount, PRESS -1 TO SKIP");
                int number = Convert.ToInt16(Console.ReadLine());
                while (number != -1)
                {
                    _StockController.EditEquipmentInfo(equipment, number);
                    Console.WriteLine("Enter 1 to edit Name, 2 to edit Price,3 to decrease amount, 4 to increase amount, PRESS -1 TO SKIP");
                    number = Convert.ToInt16(Console.ReadLine());
                }                   
            }
        }

        public void DeleteEmployee()
        {            
            Console.WriteLine("Enter employee ID : ");
            String id = Console.ReadLine();
            if (IsFound("Emp", id))
            {
                Employee e = Hospital.FindEmployee(id);
                e.PrintInfo();
                Console.WriteLine("Do you want to delete this Employee? ");
                string selection = Console.ReadLine();
                if (selection == "yes")
                {
                    _Manager.DischargeEmployee(e);
                }
            }
        }

        public void DeletePatient()
        {
            Console.WriteLine("Enter patient ID : ");
            String id = Console.ReadLine();
            if (IsFound("Emp", id))
            {
                Patient p = Hospital.FindPatient(id);
                p.PrintInfo();
                Console.WriteLine("Do you want to delete this Patient? ");
                string selection = Console.ReadLine();
                if (selection == "yes")
                {
                    _Recepcionist.DeletePatient(id);
                }
                
            }
            
        }

        public void DeleteEquipment()
        {
            Console.Write("Enter the name of the equipment: ");
            String name = Console.ReadLine();
            if (IsFound("Eq", name))
            {
                Equipment e = Hospital.FindEquipment(name);
                e.PrintInfo();
                Console.WriteLine("Do you want to delete this Equipment? ");
                string selection = Console.ReadLine();
                if (selection == "yes")
                {
                    _StockController.DeleteEquipment(e);
                }
            }       
        }

    }
}
