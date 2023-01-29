using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalInformationSystem
{
    class Doctor : Employee
    {
        public String Specialty;
        public String Room;
        public List<TimeOnly> AppointmentTimes { get; set; }
        public List<Patient> MyPatients { get; set; }
        public List<(DateTime, string)> Appointments { get; set; }

        public Doctor() : base()
        {
            Specialty = string.Empty;
            Room= string.Empty;
            AppointmentTimes = new List<TimeOnly>();
            MyPatients = new List<Patient>();
            Appointments = new List<(DateTime, string)>();
        }

        public Doctor(string fullName, string id, string phoneNumber, string gender, int age, DateTime birthDate, string homeAddress, DateTime joinedDate, string depName,string degree, string education, (string, string) workingHours,
            string specialty, string room)
            : base(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, joinedDate, "Doctor", depName, degree, education, workingHours)
        {
            Specialty = specialty;
            Room = room;
            AppointmentTimes= new List<TimeOnly>();
            MyPatients = new List<Patient>();
            Appointments = new List<(DateTime, string)>();
        }

        public Test Direct(String departmantName, String testType, DateTime time, (String, String) patientInfo, string employeeName)
        {
            Test test = new Test(departmantName, testType, time, patientInfo, employeeName);
            return test;
        }

        
        public Boolean HasAvailableAppointment(string date)
        {
            List<string> components = date.Split('/').ToList();
            DateTime datee = new DateTime(Convert.ToInt32(components[2]), Convert.ToInt32(components[1]), Convert.ToInt32(components[0]));
            int num_availableTime = AppointmentTimes.Count;
            for (int j = 0; j < AppointmentTimes.Count; j++)
            {
                for (int i = 0; i < Appointments.Count; i++)
                {
                    if (Appointments[i].Item1.Date.Equals(datee) && Appointments[i].Item1.TimeOfDay.Equals(AppointmentTimes[j]))
                    {
                        num_availableTime--;
                        break;
                    }
                }
            }
            if (num_availableTime == 0)
            {
                return false;
            }
            return true;
        }   
        
        public Patient FindMyPatient(String patientID)
        {
            for (int i = 0; i < MyPatients.Count; i++)
            {
                if (MyPatients[i].ID == patientID)
                {
                    return MyPatients[i];
                }
            }
            throw new Exception();
        }
        public void PrintDailyAppointments(string date, Boolean authorization)
        {
            List<string> components = date.Split('/').ToList();
            DateOnly datee = new DateOnly(Convert.ToInt32(components[2]), Convert.ToInt32(components[1]), Convert.ToInt32(components[0]));            
           
            
            for (int j = 0; j < AppointmentTimes.Count; j++)
            {
                Boolean found = false;
                for (int i = 0; i < Appointments.Count; i++)
                {
                    if (DateOnly.FromDateTime(Appointments[i].Item1) == datee && TimeOnly.FromDateTime(Appointments[i].Item1) == AppointmentTimes[j])
                    {
                        if (authorization)
                        {
                            Console.WriteLine(AppointmentTimes[j] + "--" + Appointments[i].Item2);                           
                        }
                        else
                        {
                            Console.WriteLine(AppointmentTimes[j] + " ***** ******");
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("**"+ AppointmentTimes[j] + "--   NO PATIENT");                    
                   
                }
            }
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("Specialty : " + Specialty + "\nRoom: "+ Room);
            Console.WriteLine("---------------------------------------------");
        }
        

    }

}
