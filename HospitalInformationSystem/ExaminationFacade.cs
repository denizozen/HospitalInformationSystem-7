using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class ExaminationFacade
    {

        private Doctor _Doctor { set; get; }
        private Patient _Patient { set; get; }
        private Treatment _Treatment { set; get; }
        private TreatmentFactory _TreatmentFactory { set; get; }


        public ExaminationFacade(Patient p)
        {
            _Doctor = new Doctor();
            _Patient = p;
            _TreatmentFactory = new TreatmentFactory();
        }

        public void Menu()
        {
            Console.WriteLine("Press 1 to see patient information");
            Console.WriteLine("Press 2 to direct Patient to Test");
            Console.WriteLine("Press 3 to set Test Results");
            Console.WriteLine("Press 4 to see Test Results and enter diognasis");
            Console.WriteLine("Press 5 to add Treatment");
            Console.WriteLine("Press 6 for payment");
            Console.WriteLine("Press -1 to skip");
        }

        public void ArrangeExamination()
        {
            Console.Write("Enter department name: ");
            string departmentName = Console.ReadLine();
            Console.Write("On which date would you like your appointment? (ex. dd/mm/yyyy) ");
            string date = Console.ReadLine();
            List<Doctor> availableDoctors = Hospital.FindDepartment(departmentName).GetAvailableDoctors(date);
            Console.WriteLine("Enter num of the doctor to take appointment: ");
            int selection = Convert.ToInt32(Console.ReadLine());
            Doctor doctor = availableDoctors[selection - 1];
            doctor.PrintDailyAppointments(date, false);
            Console.WriteLine("Select an available time of the doctor according to the appointment list: (ex. hh:mm)");
            string time = Console.ReadLine();
            List<string> date_components = date.Split('/').ToList();
            List<string> time_components = time.Split(':').ToList();
            DateTime dateTime = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]),
               Convert.ToInt32(time_components[0]), Convert.ToInt32(time_components[1]), 0);
            _Patient.TakeAppointment(dateTime, doctor);
        }
        public void DisplayPatient()
        {
            _Patient.PrintInfo();
        }

        public void Direct()
        {
            Console.Write("Department Name: ");
            string depName = Console.ReadLine();
            Console.Write("Specialist name: ");
            string empName = Console.ReadLine();
            Console.Write("Test Type (Lab or Radiology)");
            string type = Console.ReadLine();
            Console.Write("Date: ");
            string date = Console.ReadLine();
            Console.Write("Time: ");
            string time = Console.ReadLine();
            List<string> date_components = date.Split('/').ToList();
            List<string> time_components = time.Split('.').ToList();
            DateTime dateTime = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]),
               Convert.ToInt32(time_components[0]), Convert.ToInt32(time_components[1]), 0);
            Test test = _Doctor.Direct(depName, type, dateTime, (_Patient.ID,_Patient.FullName),empName);
            _Patient.Tests.Add(test);
        }

        public void SetTestResult()
        {
            Console.Write("Date: ");
            string date = Console.ReadLine();
            Console.Write("Time: ");
            string time = Console.ReadLine();
            List<string> date_components = date.Split('/').ToList();
            List<string> time_components = time.Split('.').ToList();
            DateTime dateTime = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]),
               Convert.ToInt32(time_components[0]), Convert.ToInt32(time_components[1]), 0);
            for (int i=0;i<_Patient.Tests.Count;i++)
            {
                if (_Patient.Tests[i].Time == dateTime)
                {
                    Console.WriteLine("Write result: ");
                    string result = Console.ReadLine() ;
                    _Patient.Tests[i].Result = result;
                    break;
                }
            }
        }

        public void DisplayTest()
        {
            Console.WriteLine("Date: ");
            string date = Console.ReadLine();
            Console.WriteLine("Time: ");
            string time = Console.ReadLine();
            List<string> date_components = date.Split('/').ToList();
            List<string> time_components = time.Split('.').ToList();
            DateTime dateTime = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]),
               Convert.ToInt32(time_components[0]), Convert.ToInt32(time_components[1]), 0);
            for (int i = 0; i < _Patient.Tests.Count; i++)
            {
                if (_Patient.Tests[i].Time == dateTime)
                {
                    _Patient.Tests[i].Display();
                    break;
                }
            }
            Console.WriteLine("Write diognasis: ");
            string diognasis = Console.ReadLine();
            _Patient.CurrentDiagnosis = diognasis;
        }

        public void _DecideTreatment()
        {
            Console.WriteLine("Doctor Name: ");
            string doctorName=Console.ReadLine();
            Console.WriteLine("Press 1 for create prescription, 2 for create surgical treatment");
            int selection = Convert.ToInt32(Console.ReadLine());
            string treatment = "";
            if (selection == 1)
            {
                treatment = "Medical";
            }
            else if(selection == 2)
            {
                treatment = "Surgical";
            }           
            _Treatment = _TreatmentFactory.GetTreatment(treatment,_Patient,doctorName, DateTime.Now);
            _Treatment.Operate(_Patient);
        }
        public void Payment()
        {
            Console.WriteLine("Date: ");
            string date = Console.ReadLine();
            List<string> date_components = date.Split('/').ToList();
            DateTime datee = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]));
            _Patient.CalculateBill(datee);
            Console.WriteLine("Bill: " + _Patient.DailyBill);
            Console.WriteLine("If Payment received, Press 1");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                _Patient.DailyBill = 0;
            }

        }

    }
}
