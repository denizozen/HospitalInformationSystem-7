using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    abstract class Treatment
    {
        public DateTime Time { get; set; } 
        protected String Diagnosis;
        protected String DoctorName;
        protected (String, String) PatientInfo;
        public double Bill { get; set; }

        protected Treatment(DateTime time, string diagnosis, string doctorName, (string, string) patientInfo) 
        {
            Time = time;          
            Diagnosis = diagnosis;
            DoctorName = doctorName;
            PatientInfo = patientInfo;
            Bill = 0;
        }

        public virtual void Operate(Patient patient)
        {
            patient.Treatments.Add(this);
        }
        public abstract void Display();
        public abstract string getType();

        
    }
}
