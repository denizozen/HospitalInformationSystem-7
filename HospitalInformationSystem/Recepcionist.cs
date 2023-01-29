using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class Recepcionist : Employee
    {
        public Recepcionist(string fullName, string id, string phoneNumber, string gender, int age, DateTime birthDate, string homeAddress, DateTime joinedDate,
            string depName, string degree, string education, (string, string) workingHours)
            : base(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, joinedDate, "Recepcionist", depName, degree, education, workingHours)
        {           
        }
        public Recepcionist(){ }



        public void RegisterPatient(Patient patient)
        {
            
            Hospital.Patients.Add(patient);
        }

        public void EditPatientInfo(Patient patient, int number)
        {
            if (number == 1)
            {
                Console.Write("Enter new name : ");
                patient.FullName = Console.ReadLine();
            }
            else if (number == 2)
            {
                Console.Write("Enter new id : ");
                patient.ID = Console.ReadLine();
            }
            else if (number == 3)
            {
                Console.Write("Enter new phone number : ");
                patient.PhoneNumber = Console.ReadLine();
            }
            else if (number == 4)
            {
                Console.Write("Enter new gender : ");
                patient.PhoneNumber = Console.ReadLine();
            }
            else if (number == 5)
            {
                Console.Write("Enter new age : ");
                patient.Age = Convert.ToInt32(Console.ReadLine());
            }
            else if (number == 6)
            {
                Console.Write("Enter new birth date : (ex. dd/mm/yyyy)");
                string line = Console.ReadLine();
                List<string> date_components = line.Split('/').ToList();
                DateTime birthDate = new DateTime(Convert.ToInt32(date_components[2]), Convert.ToInt32(date_components[1]), Convert.ToInt32(date_components[0]));
                patient.BirthDate = birthDate;
            }
            else if (number == 7)
            {
                Console.Write("Enter new home address : ");
                patient.HomeAddress = Console.ReadLine();
            }
            else if (number == 8)
            {
                Console.Write("Select Insurance: (state-special) ");
                string line = Console.ReadLine();
                HealthInsurance insurance = new HealthInsurance();
                if (line == "state")
                {
                    insurance = HealthInsurance.state;
                }
                else
                {
                    insurance = HealthInsurance.special;
                }
                patient._HealthInsurance=insurance;
            }
            else if (number == 9)
            {
                patient.DisplaySpecialInfo(1);
                for (; ; )
                {
                    Console.WriteLine("Enter 1 to add new alergies : \n2 to remove : \n3 to terminate : ");
                    int selection = Convert.ToInt32(Console.ReadLine());
                    if (selection == 3)
                    {
                        return;
                    }
                    Console.WriteLine("Enter the names of alergies seperated by commas: ");
                    List<string> alergies = Console.ReadLine().Split(',').ToList();
                    for (int i = 0; i < alergies.Count; i++)
                    {
                        if (selection == 1)
                        {
                            patient.Alergies.Add(alergies[i]);
                        }
                        else if (selection == 2)
                        {
                            for (int j = 0; j < patient.Alergies.Count; j++)
                            {
                                if (patient.Alergies[j] == alergies[i])
                                {
                                    patient.Alergies.Remove(patient.Alergies[j]);
                                }
                            }
                        }
                    }
                }
            }
            else if (number == 10)
            {
                patient.DisplaySpecialInfo(2);
                for (; ; )
                {
                    Console.WriteLine("Enter 1 to add new diseases : \n2 to remove : \n3 to terminate : ");
                    int selection = Convert.ToInt32(Console.ReadLine());
                    if (selection == 3)
                    {
                        return;
                    }
                    Console.WriteLine("Enter the names of diseases seperated by commas: ");
                    List<string> diseases = Console.ReadLine().Split(',').ToList();
                    for (int i = 0; i < diseases.Count; i++)
                    {
                        if (selection == 1)
                        {
                            patient.Diseases.Add(diseases[i]);
                        }
                        else if (selection == 2)
                        {
                            for (int j = 0; j < patient.Diseases.Count; j++)
                            {
                                if (patient.Diseases[j] == diseases[i])
                                {
                                    patient.Diseases.Remove(patient.Diseases[j]);
                                }
                            }
                        }
                    }
                }
            }
            else { }

        }
        public void DeletePatient(String patientID)
        {
            try
            {
                Patient patient = Hospital.FindPatient(patientID);
                Console.WriteLine("Here is the Patient.. ");
                patient.PrintInfo();
                Console.WriteLine("Do you want to delete this Patient? ");
                string selection = Console.ReadLine();
                if (selection == "yes")
                {
                    Hospital.Patients.Remove(patient);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Patient not found!");
            }
        }

    }
}
