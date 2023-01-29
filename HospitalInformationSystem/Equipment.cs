using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem
{
    class Equipment
    {
        public String Name { get; set; }
        public double Price { get; set; }
        public int AvailableAmount { get; set; }
        private int RequiredAmount { get; set; }

        public Equipment(string name, double price, int availableAmount, int requiredAmount)
        {
            Name = name;
            Price = price;
            AvailableAmount = availableAmount;
            RequiredAmount = requiredAmount;
        }

        public void IncreaseAmount(int number)
        {
            AvailableAmount = AvailableAmount - number;
        }

        public void DecreaseAmount(int number)
        {
            AvailableAmount = AvailableAmount - number;
        }

        public void PrintInfo()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Name: " + Name + "\nPrice: " + Price + "\nAvailable Amount: " + AvailableAmount + "\nRequired Amoount: " + RequiredAmount);
            Console.WriteLine("---------------------------------------------");
        }


    }
}
