using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class Test
    {
        private String DepartmantName { get; set; }
        private String TestType { get; set; }
        public DateTime Time { get; set; }
        public String Result { get; set; }
        public Double Bill { get; set; }
        private (String, String) PatientInfo { get; set; }

        private string EmployeeName { get; set; }



        public Test(String departmantName, String testType, DateTime time, (String, String) patientInfo, string employeeName)
        {
            TestType = testType;
            Time = time;
            Result = string.Empty;
            PatientInfo = patientInfo;
            EmployeeName = employeeName;
            DepartmantName = departmantName;
            if (testType == "Lab")
            {
                Bill = 250;
            }else if (testType == "Radiology")
            {
                Bill = 750;
            }
        }

        public void Display()
        {
            Console.WriteLine("Time : " + Time+ "\nPatient Info : " + PatientInfo.Item1 + PatientInfo.Item2+ "\nDepartmant name : " + DepartmantName + "\nEmployee name: " + EmployeeName+"\nTest type : " + TestType + "\nResult : " + Result   );
        }

        public void setResult(String ID)
        {
            Console.WriteLine("Enter patient ID : ");
            Patient patient = Hospital.FindPatient(ID);
            if (patient == null)
            {
                Console.WriteLine("Patient could not found ");
            }
            else
            {
                patient.PrintInfo();
                for (int i = 0; i < patient.Tests.Count; i++)
                {
                    patient.Tests[i].Display();
                }
                Console.WriteLine("Enter number to choose test : ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Result : ");
                String result = Console.ReadLine();
                Console.WriteLine("Enter Test Type : ");
                String testType = Console.ReadLine();
                patient.Tests[choice].Result = result;
                patient.Tests[choice].TestType = testType;

            }

        }


    }
}
