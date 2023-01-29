using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalInformationSystem
{
    enum HealthInsurance
    {
        state,
        special        
    }
    class Patient : Person
    {
        public String CurrentDiagnosis { get; set; }
        public List<String> Alergies { get; set; }
        public List<String> Diseases { get; set; }
        public List<(DateTime, String)> Prescriptions { get; set; }
        public List<Test> Tests { get; set; }
        public List<Treatment> Treatments { get; set; }
        public List<(DateTime, Doctor)> Appointments { get; set; }
        public Double DailyBill { get; set; }

        public HealthInsurance _HealthInsurance { get; set; }

        public Patient() : base()
        {
            CurrentDiagnosis = string.Empty;
            Alergies = new List<string>();
            Diseases = new List<string>();
            Prescriptions = new List<(DateTime, string)>();
            Tests = new List<Test>();
            Treatments = new List<Treatment>();
            Appointments = new List<(DateTime, Doctor)>();
            DailyBill = 0;
        }
        public Patient(string fullName, string id, string phoneNumber, string gender, int age, DateTime birthDate, string homeAddress, DateTime acceptedDate, HealthInsurance healthInsurance) 
            : base(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, acceptedDate)
        {
            CurrentDiagnosis = string.Empty;
            Alergies = new List<string>();
            Diseases = new List<string>();
            Prescriptions = new List<(DateTime, string)>();          
            Tests = new List<Test>();
            Treatments = new List<Treatment>();
            Appointments = new List<(DateTime, Doctor)>();
            DailyBill = 0;
            _HealthInsurance = healthInsurance;

        }

        public void TakeAppointment(DateTime time, Doctor doctor)
        {
            for(int i = 0; i < doctor.Appointments.Count; i++)
            {
                if (doctor.Appointments[i].Item1 == time)
                {
                    Console.WriteLine("This appointment time has been already creaed.");
                    return;
                }
            }

            Appointments.Add((time, doctor));
            doctor.Appointments.Add((time, FullName));
            doctor.MyPatients.Add(this);
            Console.WriteLine("Your appointment is successfully created.");
         

        }
        public void CancelAppointment(DateTime time)
        {
            for (int i = 0; i < Appointments.Count; i++)
            {
                if (Appointments[i].Item1 == time)
                {
                    Appointments.Remove(Appointments[i]);
               //     Doctor doctor = Appointments[i].Item2;
               //     doctor.Appointments.Remove((time, FullName));
                    break;
                }
            }
        }
        public void DisplayAppointment(DateTime time)
        {
            for(int i = 0; i < Appointments.Count; i++)
            {
                if (Appointments[i].Item1 == time)
                {
                    Console.WriteLine("Details of your appointment are below.");
                    Console.WriteLine("Date and Time: " + time.ToString());
                    Doctor doctor = Appointments[i].Item2;
                    Console.WriteLine("Department: " + Hospital.FindDepartment(doctor.DepartmantName).Name);
                    Console.WriteLine("Doctor Name: " + doctor.FullName);                   
                }
            }
            
        }

        public void DisplayAllAppointments()
        {
            for (int i = 0; i < Appointments.Count; i++)
            {
                Console.WriteLine((i + 1) + ") Date and Time: "+ Appointments[i].Item1.ToString() + " Department: " + Hospital.FindDepartment(Appointments[i].Item2.DepartmantName).Name);
            }
            Console.WriteLine("Enter the num of appointment to see");
            int num = Convert.ToInt32(Console.ReadLine());
            DisplayAppointment(Appointments[num - 1].Item1);
            Console.WriteLine("Press 1 to cancel the appointment, Press -1 to skip");
            num = Convert.ToInt32(Console.ReadLine());
            if (num == -1)
            {
                return;
            }
            else
            {
                CancelAppointment(Appointments[num - 1].Item1);
            }
        }
        public void CalculateBill(DateTime date)
        {
            for (int i = 0; i < Appointments.Count; i++)
            {
                
                if (Appointments[i].Item1.Date == date.Date)
                {
                    if (_HealthInsurance.Equals(HealthInsurance.special))
                    {
                        DailyBill += 500 - (500 * 80) / 100;
                    }
                    else
                    {
                        DailyBill += 500 - (500 * 20) / 100;
                    }
                    
                }
            }
            for(int i = 0; i < Tests.Count; i++)
            {
                if (Tests[i].Time.Date == date.Date)
                {
                    if (_HealthInsurance.Equals(HealthInsurance.special))
                    {
                        DailyBill += Tests[i].Bill - (Tests[i].Bill * 80) / 100;
                    }
                    else
                    {
                        DailyBill += Tests[i].Bill - (Tests[i].Bill * 20) / 100;
                    }
                }
            }
            for (int i = 0; i < Treatments.Count; i++)
            {
                if (Treatments[i].Time.Date == date.Date)
                {
                    DailyBill += Treatments[i].Bill;
                }
            }
        }

        public override void PrintInfo()
        {
            Console.WriteLine("General Information");
            base.PrintInfo();
            int choice = 1;
            while (choice==1)
            {
                Console.WriteLine("Press the num of the information which you want to see");
                Console.WriteLine("1) Alergies \n2)Diseases \n3)Prescriptions \n4)Tests \n5)Treatments ");
                int selection = Convert.ToInt32(Console.ReadLine());
                if(selection == -1)
                {
                    return;
                }
                else
                {
                    DisplaySpecialInfo(selection);
                    Console.WriteLine("Press 1 to see others, press -1 to skip");
                    choice= Convert.ToInt32(Console.ReadLine());
                }                
            }
        }
        public void PrintGeneralInfo()
        {
            Console.WriteLine("General Information");
            base.PrintInfo();
            DisplaySpecialInfo(1);
            DisplaySpecialInfo(2);
            Console.WriteLine("---------------------------------------------");
        }

        public void DisplaySpecialInfo(int choice)
        {
            if (choice == 1)
            {
                Console.Write("Alergies : ");
                for (int i = 0; i < Alergies.Count; i++)
                {
                    Console.Write(Alergies[i]+", ");
                }
            }
            else if (choice == 2)
            {
                Console.Write("Diseases");
                for (int i = 0; i < Diseases.Count; i++)
                {
                    Console.Write(Diseases[i]+",");
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Prescriptions");
                for (int i = 0; i < Prescriptions.Count; i++)
                {
                    Console.WriteLine(Prescriptions[i].Item1.ToString());
                    Console.WriteLine(Prescriptions[i].Item2);
                    Console.WriteLine("-------------");
                }
            }
            else if (choice == 4)
            {
                Console.WriteLine("Tests : ");
                for (int i = 0; i < Tests.Count; i++)
                {
                    Tests[i].Display();
                }
            }
            else if (choice == 5)
            {
                Console.WriteLine("Treatment history : ");
                for (int i = 0; i < Treatments.Count; i++)
                {
                    Treatments[i].Display();
                }
            }
            else if (choice == 6)
            {
                Console.WriteLine("Appointments : ");
                for (int i = 0; i < Appointments.Count; i++)
                {
                    Console.WriteLine(Appointments[i].Item1.ToString() + " - " + Appointments[i].Item2);
                }
            }
            else { }
            Console.WriteLine();
        }             

    }
    
}
