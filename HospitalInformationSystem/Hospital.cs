using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class Hospital
    {
        private static String Name;
        private static String Address;
        private static String CentralPhone;
        public  static Manager _Manager { set; get; }

        public  static List<Patient> Patients ;
        public  static List<Equipment> Equipments;
        public  static List<Employee> Employees ;
        public  static List<Department> Departments;

        public Hospital(String name, String address, String centralPhone, Manager manager)
        {
            Name = name;
            Address = address;
            CentralPhone = centralPhone;
            _Manager= manager;
            Patients= new List<Patient>();
            Equipments = new List<Equipment>();
            Employees= new List<Employee>();
            Departments= new List<Department>();
        }
        public static Department FindDepartment(String depName)
        {
            for (int i = 0; i < Departments.Count; i++)
            {
                if (Departments[i].Name == depName)
                {
                    return Departments[i];
                }
            }
            throw new Exception();
        }

        public static Employee FindEmployee(String empID) 
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].ID == empID)
                {
                    return Employees[i];
                }
            }
            return null;
        }
        public static Patient FindPatient(String patientID)
        {
            for (int i = 0; i < Patients.Count; i++)
            {
                if (Patients[i].ID == patientID)
                {
                    return Patients[i];
                }
            }
            return null;
        }

        public static Equipment FindEquipment(String equipmentName)
        {
            for (int i = 0; i < Equipments.Count; i++)
            {
                if (Equipments[i].Name == equipmentName)
                {
                    return Equipments[i];
                }
            }
            return null;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Address: " + Address);
            Console.WriteLine("Central Phone: " + CentralPhone);
        }

        public void DisplayDepartments(bool detailed)
        {
            if (detailed)
            {
                for (int i = 0; i < Departments.Count; i++)
                {
                    Departments[i].PrintInfo();
                }
            }
            else
            {
                for (int i = 0; i < Departments.Count; i++)
                {
                    Console.WriteLine((i+1)+") "+Departments[i].Name);
                }
            }
        }
    }
}
