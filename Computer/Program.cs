using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer
{
    class Program
    {
        static int CloudStorage = 500;
        static int NetworkSpeed = 10000;
        static void Main(string[] args)
        {
            Computer defaultPrototype = new Computer("Standard PC", 2000, 100, true, null);
            Computer userPrototype = null;
            Computer[] computers = null;
            Console.WriteLine("What is the maximum number of computers you will be tracking?");
            int numOfComputers;
            if (!GetIntFromUser(out numOfComputers, 5, 20, 10))
                Console.WriteLine("Number of computers was set to default 10");
            computers = new Computer[numOfComputers];
            String choice;
            do
            {
                Console.WriteLine("Would you like to...\n" +
                    "(1) Add a computer\n" +
                    "(2) Specify your prototype computer\n" +
                    "(3) Remove your prototype computer\n" +
                    "(4) Upgrade your cloud storage\n" +
                    "(5) Downgrade your cloud storage\n" +
                    "(6) Upgrade your network speed\n" +
                    "(7) Downgrade your network speed\n" +
                    "(8) Get a summary of a specific computer\n" +
                    "(9) Get a summary of statistics on all computers\n" +
                    "(10) Get a summary of statistics of specific computers\n" +
                    "Press 'Q' to quit");
                choice = Console.ReadLine().ToUpper();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter the computer ID," +
                            " \nfollowed by the amount of RAM, " +
                            "\nfollowed by the amount of storage, " +
                            "\nfollowed by (true/false) if there is an intena or not," +
                            "\nfollowed by the number of licenses for each one of the 5 extra pices of software" +
                            " sepersted by commas" +
                            " \nall seperated by commas. Enter null if any are not applicable");
                        String[] input = Console.ReadLine().Split(',');
                        String id = input[0];
                        int ram = int.Parse(input[1]);
                        int? storageCapacity;
                        int?[] licenses;
                        if (input[2] == "null")
                            storageCapacity = null;
                        else
                            storageCapacity = int.Parse(input[2]);
                        bool? intena;
                        if (input[3] == "null")
                            intena = null;
                        else
                            intena = bool.Parse(input[3]);
                        if (input[4] == "null")
                            licenses = null;
                        else
                        {
                            licenses = new int?[5];
                            for (int x = 0; x < licenses.Length - 1; x++)
                                licenses[x] = int.Parse(input[x + 5]);
                        }   
                        for (int x = 0; x < computers.Length - 1; x++)
                            if (computers[x] == null)
                                computers[x] = new Computer(id, ram, storageCapacity, intena, licenses);
                        break;
                    case "2":
                        Console.WriteLine("Enter the computer ID," +
                            " \nfollowed by the amount of RAM, " +
                            "\nfollowed by the amount of storage, " +
                            "\nfollowed by (true/false) if there is an intena or not, " +
                            "\nfollowed by the number of licenses for each one of the 5 extra pices of software" +
                            " sepersted by commas" +
                            " \nall seperated by commas. Enter 'null' if any are not applicable");
                        input = Console.ReadLine().Split(',');
                        id = input[0];
                        ram = int.Parse(input[1]);
                        if (input[2] == "null")
                            storageCapacity = null;
                        else
                            storageCapacity = int.Parse(input[2]);
                        if (input[3] == "null")
                            intena = null;
                        else
                            intena = bool.Parse(input[2]);
                        if (input[4] == "null")
                            licenses = null;
                        else
                        {
                            licenses = new int?[5];
                            for (int x = 0; x < licenses.Length - 1; x++)
                                licenses[x] = int.Parse(input[x + 5]);
                        }
                        userPrototype = new Computer(id, ram, storageCapacity, intena, licenses);
                        break;
                    case "3":
                        userPrototype = null;
                        break;
                    case "4":
                        if (!DoubleIntNotPastMax(ref CloudStorage, 16000, false))
                            Console.WriteLine("Cloud storage can't be upgraded double is over the max");
                        break;
                    case "5":
                        if (!HalveValueNotPastMin(ref CloudStorage, 500, true))
                            Console.WriteLine("Cloud storage was downgraded to a minimum of 500");
                        break;
                    case "6":
                        if (!DoubleIntNotPastMax(ref NetworkSpeed, 250000, true))
                            Console.WriteLine("Cloud storage can't be upgraded double is over the max");
                        break;
                    case "7":
                        if (!HalveValueNotPastMin(ref NetworkSpeed, 10000, false))
                            Console.WriteLine("Cloud storage was downgraded to a minimum of 500");
                        break;
                    case "8":
                        Console.WriteLine("Enter the index number of the computer you would like a summary of");
                        Console.WriteLine(computers[int.Parse(Console.ReadLine())].ToString());
                        break;
                    case "9":
                        int?[] averages = Averages(computers);
                        Console.WriteLine("\nThe average RAM is: " + averages[0] +
                            "\nThe average storage capacity is: " + averages[1] +
                            "\nThe average number of inetenas are: " + averages[2] +
                            "\nThe average number of licenses per device is: " + averages[3] +
                            "\nThe average number of licenses per software is: " + averages[4] +
                            "\nYour current cloud storage is: " + CloudStorage +
                            "\nYour current network speed is: " + NetworkSpeed);
                        break;
                    case "10":
                        Console.WriteLine("Enter the index numbers of the computers you would like a summary of seperated by commas.");
                        input = Console.ReadLine().Split(',');
                        int[] pcIndex = Array.ConvertAll(input, int.Parse);
                        Computer[] pcs = new Computer[pcIndex.Length];
                        for (int i = 0; i < pcIndex.Length - 1; i++)
                            pcs[i] = computers[pcIndex[i]];
                        averages = Averages(pcs);
                        Console.WriteLine("\nThe average RAM is: " + averages[0] +
                            "\nThe average storage capacity is: " + averages[1] +
                            "\nThe average number of inetenas are: " + averages[2] +
                            "\nThe average number of licenses per device is: " + averages[3] +
                            "\nThe average number of licenses per software is: " + averages[4] +
                            "\nYour current cloud storage is: " + CloudStorage +
                            "\nYour current network speed is: " + NetworkSpeed);
                        break;
                    case "Q":
                        Console.WriteLine("Quitting....");
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            while (choice != "Q");
        }
        static int?[] Averages(Computer[] computers)
        {
            int RAM = 0;
            double? storage = 0;
            int nullStorage = 0;
            int intenas = 0;
            int nullIntenas = 0;
            int? licenses = 0;
            int nullLicenses = 0;
            for (int x = 0; x < computers.Length -1; x++)
            {
                RAM += computers[x].RAM;
                if (computers[x].StorageCapacity == null)
                    nullStorage++;
                else
                    storage += computers[x].StorageCapacity;
                if (computers[x].celularIntena == null)
                    nullIntenas++;
                if (computers[x].celularIntena == true)
                    intenas++;
                if (computers[x].Licenses == null)
                    nullLicenses++;
                else
                    for (int i = 0; i < computers[x].Licenses.Length -1; i++)
                        licenses += computers[x].Licenses[i];
            }
            int avgRAM = RAM / computers.Length;
            double? avgStorage = storage / (computers.Length - nullStorage);
            int avgIntenas = intenas / (computers.Length - nullIntenas);
            int? avgLicensesPerPC = licenses / (computers.Length - nullLicenses);
            int? avgLicensesPerSoftware = licenses / ((computers.Length - nullLicenses) * 5);
            int?[] averages = { avgRAM, (int)avgStorage, avgIntenas, avgLicensesPerPC, avgLicensesPerSoftware };
            return averages;
        }
        static bool GetIntFromUser(out int number, int min, int max, int Default)
        {
            Console.WriteLine("Enter a number between {0} and {1} (inclusive)", min, max);
            int i = int.Parse(Console.ReadLine());
            if (i >= min && i <= max)
            {
                number = i;
                return true;
            }
            else
            {
                number = Default;
                return false;
            }
        }
        static bool DoubleIntNotPastMax(ref int number, int max, bool setToMax)
        {
            if (number * 2 > max)
            {
                if (setToMax)
                    number = max;

                return false;
            }
            else
            {
                number = number * 2;
                return true;
            }
        }
        static bool HalveValueNotPastMin(ref int number, int min, bool setToMin)
        {
            if (number / 2 < min)
            {
                if (setToMin)
                    number = min;

                return false;
            }
            else
            {
                number = number / 2;
                return true;
            }
        }
    }
    public class Computer
    {
        readonly string id;
        public string ID { get { return id; } }
        public Computer(String id, int RAM, int? StorageCapacity, bool? intena, int?[] Licenses)
        {
            this.id = id;
            this.RAM = RAM;
            this.StorageCapacity = StorageCapacity;
            celularIntena = intena;
            this.Licenses = Licenses;
        }
        public bool? celularIntena { get; set; }
        private double? storageCapacity;
        public double? StorageCapacity
        {
            get
            {
                return storageCapacity;
            }
            set
            {
                storageCapacity = !(value < 0) ? value : throw new Exception("storage capacity cannot be negative");
            }
        }
        private int?[] licenses = new int?[5];
        public int?[] Licenses
        {
            get
            {
                return licenses;
            }
            set
            {
                licenses = Licenses; 
            }
        }
        private int ram;
        public int RAM
        {
            get
            {
                return (celularIntena != null ? 50 : 100) + licenses.Length * 10;
            }
            set
            {
                ram = !(value < 1000) ? value : throw new Exception("RAM cannot be less than 1000");
            }
        }
        public override string ToString()
        {
            StringBuilder computer = new StringBuilder("ID: " + ID);
            computer.AppendLine("\nRAM: " + RAM);
            computer.AppendLine(StorageCapacity == null ? "Device does not support hard drive." : "Storage capacity: " + StorageCapacity);
            if (celularIntena == null)
                computer.AppendLine("intena not applicable for this device");
            if(celularIntena == true)
                computer.AppendLine("Has intena");
            if(celularIntena == false)
                computer.AppendLine("Does not have intena");
            computer.AppendLine(licenses == null ? "Device is not equiped for extra software." : "Software licenses: " + licenses.ToString());
            return computer.ToString();
        }

    }
}
