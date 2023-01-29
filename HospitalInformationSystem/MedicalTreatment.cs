using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class MedicalTreatment : Treatment  
    {
        public List<(string, string)> Drugs { get; set; }

        public MedicalTreatment(DateTime time, string diagnoses, string doctorName, (string, string) patientInfo, List<(string, string)> drugs) : base(time,  diagnoses, doctorName, patientInfo)
        {
            Drugs = drugs;  
        }


        public override void Display()
        {
            Console.WriteLine("Patient info : " + PatientInfo.Item1 + PatientInfo.Item2 + "\nDoctor name : " + DoctorName + "\nDiagnoses : " + Diagnosis +  "\nTime : " + Time);
            Console.WriteLine(GetPrescription());
        }

        public override string getType()
        {
            return "Medical";
        }
        public string GetPrescription()
        {
            string prescription = "Patient info : " + PatientInfo.Item1 + PatientInfo.Item2 + "\nDoctor name : " + DoctorName + "\nDiagnoses : " + Diagnosis;
            prescription += "\nDrugs-------------------------";
            for (int i = 0; i < Drugs.Count; i++)
            {
                prescription += "\n" + (i + 1) + ") Name: " + Drugs[i].Item1 + " -  Dose: " + Drugs[i].Item2;
            }
            return prescription;
        }
        public override void Operate(Patient patient)
        {
            base.Operate(patient);
            patient.Prescriptions.Add((Time, GetPrescription()));          
           
        }

        public static implicit operator string(MedicalTreatment v)
        {
            throw new NotImplementedException();
        }
    }
}
