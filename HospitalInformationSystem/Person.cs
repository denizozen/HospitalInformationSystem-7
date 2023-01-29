using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
     class Person
    {
        public String FullName { get; set; }
        public String ID { get; set; }
        public String PhoneNumber {  get; set; }
        public String Gender {  get; set; }
        public int Age {  get; set; }
        public DateTime BirthDate {  get; set; }
        public String HomeAddress {  get; set; }
        public String Password {  get; set; }
        public DateTime JoinedDate;  

        public Person() 
        {
            FullName = String.Empty;
            Gender= String.Empty;
            Age = 0;
            BirthDate = DateTime.MinValue;
            HomeAddress = String.Empty;
            ID = String.Empty;
            Password = String.Empty;
            JoinedDate = DateTime.MinValue;
        }

        public Person(String fullName, String id, String phoneNumber, String gender, int age, DateTime birthDate, String homeAddress, DateTime joinedDate)
        {
            FullName = fullName;
            ID = id;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Age = age;
            BirthDate = birthDate;
            HomeAddress = homeAddress;
            Password = "";
            JoinedDate = joinedDate;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Name : " + FullName + "\nAge : " + Age + "\nBirthdate : " + BirthDate + 
                                "\nGender : " + Gender + "\nAdress : " + HomeAddress + "\nID :" + ID);
            Console.WriteLine("Joined Date: "+JoinedDate.ToString());
            
        }

        public virtual bool Login(string id, string password)
        {
            return false;
        }

        
    }
}
