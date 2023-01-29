using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class TreatmentFactory
    {
        public Treatment GetTreatment(string type, Patient p, string doctorName, DateTime t)
        {
            if (type == null)
            {
                return null;
            }
            if (type == "Surgical")
            {
                Console.WriteLine("Enter treatment description: ");
                string description = Console.ReadLine();
                DateTime treatmentTime = DateTime.Now;
                Console.WriteLine("Enter treatment type: (inpatient or outpatient)");
                string treatmentType= Console.ReadLine();
                InOutEnum en = new InOutEnum();
                if(treatmentType== null)
                {

                }
                else if(treatmentType == "inpatient")
                {
                    en = InOutEnum.Inpatient;

                    Console.WriteLine("Enter the treatment time: (ex. dd/mm/yyyy , hh.mm)");
                    string line = Console.ReadLine();
                    List<string> date_components = line.Split(',').ToList()[0].Split('/').ToList();
                    List<string> time_components = line.Split(',').ToList()[1].Split('.').ToList();
                    DateTime time = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]),
                        Convert.ToInt32(time_components[0]), Convert.ToInt32(time_components[1]),0);
                    treatmentTime = time ;
                }
                else
                {
                    en = InOutEnum.Outpatient;
                }              
                return new SurgicalTreatment(t, p.CurrentDiagnosis, doctorName, (p.FullName, p.ID),en,description,treatmentTime);
            }
            else if(type == "Medical")
            {
                List<(string,string)> drugs = new List<(string,string)> (); 
                while (true)
                {
                    Console.WriteLine("Enter drug name, press -1 to skip.");
                    string line = Console.ReadLine();
                    if (line.Equals("-1"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Enter dose: ");
                        string dose = Console.ReadLine();
                        drugs.Add((line, dose));
                    }
                }
                
                return new MedicalTreatment(t, p.CurrentDiagnosis, doctorName, (p.FullName, p.ID),drugs);
            }
            return null;
           
        }
    }
}
