using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class StockController : Employee
    {
        public StockController() { }
        public StockController(string fullName, string id, string phoneNumber, string gender, int age, DateTime birthDate, string homeAddress, DateTime joinedDate,
            string depName, string degree, string education, (string, string) workingHours)
            : base(fullName, id, phoneNumber, gender, age, birthDate, homeAddress, joinedDate, "Stock Controller", depName, degree, education, workingHours)
        {
        }
        public void AddEquipment(Equipment equipment)
        {
            Hospital.Equipments.Add(equipment);         
        }
        public void DeleteEquipment(Equipment equipment)
        {
            Hospital.Equipments.Remove(equipment);
        }
        public void Increase(Equipment equipment, int amount)
        {
            equipment.IncreaseAmount(amount);
        }

        public void Decrease(Equipment equipment, int amount) 
        {
            equipment.DecreaseAmount(equipment.AvailableAmount);
        }

        public void EditEquipmentInfo(Equipment equipment, int number)
        {
            if (number == 1)
            {
                Console.Write("New Name: ");
                String newName = Console.ReadLine();
                equipment.Name = newName;
            }
            else if (number == 2)
            {
                Console.Write("New Price: ");
                double newPrice = Convert.ToDouble(Console.ReadLine());
                equipment.Price = newPrice;
            }
            else if(number == 3)
            {
                Console.Write("Amount to decrease: ");
                int num = Convert.ToInt32(Console.ReadLine());
                equipment.DecreaseAmount(num);
            }
            else if(number == 4)
            {
                Console.Write("Amount to increase: ");
                int num = Convert.ToInt32(Console.ReadLine());
                equipment.IncreaseAmount(num);
            }
        }

        
    }
}
