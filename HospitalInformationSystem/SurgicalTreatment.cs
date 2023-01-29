using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    enum InOutEnum
    {
        Inpatient,
        Outpatient
    }
    class SurgicalTreatment : Treatment
    {
        public InOutEnum TreatmentType { get; set; } 
        public string Description { get; set; }
        public DateTime TreatmentTime { get; set; }

        public SurgicalTreatment(DateTime time, string diagnoses, string doctorName, (string, string) patientInfo, InOutEnum treatmentType, string description, DateTime treatmentTime) : base(time, diagnoses, doctorName, patientInfo)
        {
            TreatmentType = treatmentType;
            Description = description;
            TreatmentTime = treatmentTime;
            
        }

        public void EditBill(HealthInsurance patientInsurance)
        {
            if(TreatmentType== InOutEnum.Inpatient)
            {
                Bill = 10000;
            }
            else
            {
                Bill = 600;
            }
            if (patientInsurance .Equals(HealthInsurance.special))
            {
                Bill = Bill - (Bill * 80) / 100;
            }
            else
            {
                Bill = Bill - (Bill * 20) / 100;
            }
        }
        public override void Display()
        {
            Console.WriteLine("Patient info : " + PatientInfo.Item1 + PatientInfo.Item2 + "\nDoctor name : " + DoctorName + "\nDiagnoses : " + Diagnosis + "\nDescription : " + Description + "\nTime : " + Time);
        }

        public override string getType()
        {
            return "Surgical";
        }
        public override void Operate(Patient patient)
        {
            base.Operate(patient);
            EditBill(patient._HealthInsurance);
        }

        public static implicit operator string(SurgicalTreatment v)
        {
            throw new NotImplementedException();
        }
    }
}
