using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class Department
    {
        public String Name { set; get; }
        private String Location { set; get; }
        private String Phone { set; get; }

        public List<Employee> MyEmployees = new List<Employee>();

        public List<Doctor> MyDoctors = new List<Doctor>();
        public Department(string name, string location, string phone)
        {
            Name = name;
            Location = location;
            Phone = phone;
            MyEmployees = new List<Employee>();
            MyDoctors = new List<Doctor>();
            
        }

        public Doctor FindDoctor(string id)
        {
            for (int i = 0; i < MyDoctors.Count; i++)
            {
                if (MyDoctors[i].ID == id)
                {
                    return MyDoctors[i];
                }
            }
            throw new Exception();
        }
        public void DisplayMyDoctors(bool detailed)
        {
            for (int i = 0; i < MyDoctors.Count; i++)
            {
                Console.WriteLine((i + 1) + ") " + MyDoctors[i].Degree +" "+ MyDoctors[i].FullName);
            }
            if (detailed)
            {
                Console.WriteLine("Enter the num of the doctor to see detailed information, otherwise enter -1");
                int selection = Convert.ToInt32(Console.ReadLine());
                while (selection != -1)
                {
                    MyDoctors[selection - 1].PrintInfo();
                    Console.WriteLine("Enter the num of another doctor to see detailed information, otherwise enter -1");
                    selection = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
        public List<Doctor> GetAvailableDoctors(string date)
        {
            int j = 1;
            List<Doctor> availableDoctors = new List<Doctor>();
            for (int i = 0; i < MyDoctors.Count; i++)
            {
                if (MyDoctors[i].HasAvailableAppointment(date))
                {
                    Console.WriteLine(j + ") " + MyDoctors[i].Degree + MyDoctors[i].FullName);
                    availableDoctors.Add(MyDoctors[i]);
                    j++;
                }
            }
            
            return availableDoctors;
        }

        public void PrintInfo() 
        {
            Console.WriteLine("Name: " + Name + "\nLocation: " + Location + "Phone: " + Phone);
        }
    }

}
