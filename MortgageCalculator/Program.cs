using System;
using System.Collections.Generic;
using System.Threading;

namespace MortgateCalculator
{
    public class Program
    {
        // Interface
        public interface IProperties
        {
            double Interest { get; set; }
            string PropertyName { get; set; }
            void Main();
        }

        // This class asks the basic questions such as name, age.
        public class Basic
        {
            public string name;
            public string age;
            public string nationality;

            public void BasicQuestions()
            {
                Console.WriteLine("Welcome to the Interactive Mortgage Calculator!");
                Thread.Sleep(2000); // Used to create a more intereactive calculator - introduces pauses between writing.
                Console.WriteLine("First things first, I am going to ask you a few basic questions.");
                Thread.Sleep(2000);
                Console.WriteLine("What is your name?");
                Console.WriteLine();
                name = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Hello, " + name + ". Would you mind telling me your age?");
                Console.WriteLine();
                age = Console.ReadLine();
                while (!int.TryParse(age, out _)) // Exception handling: handles entering anything but an integer e.g. letters, decimals etc.
                {
                    Console.WriteLine();
                    Console.WriteLine("I am sorry, that is not a valid number. Please try again: ");
                    Console.WriteLine();
                    age = Console.ReadLine();
                }
                Console.WriteLine();
                Console.WriteLine("Are you a UK Citizen? Enter Y or N: ");
                Console.WriteLine();
                nationality = Console.ReadLine();
                while (nationality != "Y" && nationality != "N")
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry, that is not one of the options, try again: ");
                    Console.WriteLine();
                    nationality = Console.ReadLine();
                }
                if (Convert.ToInt32(age) < 18 && nationality == "N")
                {
                    Console.WriteLine("Sorry, but you must be 18 and a UK Citizen to have a mortgage.");
                    Environment.Exit(0); // Exit the program, as the user cannot apply for a mortgage.
                }
                else if (Convert.ToInt32(age) < 18)
                {
                    Console.WriteLine("Sorry, but you must be 18 or over to apply for a mortgage."); // Must be >= 18 years old.
                    Environment.Exit(0);
                }
                else if (nationality == "N")
                {
                    Console.WriteLine("Sorry, but you must be a UK Citizen to apply for a mortgage."); // Must be a UK Citizen
                    Environment.Exit(0);
                }
            }
        }

        // This class handles the questions to get the user's annual income:
        public class Income
        {
            public string answer;
            // Using static here so income stays user defined throughout the remainder of the code.
            public static int income;
            public static double MaxCredit;

            public void Incomeinput()
            {
                Console.WriteLine();
                Console.WriteLine("Okay, everything checks out so far. Could you please tell me your annual income? ");
                answer = Console.ReadLine();
                // Nice loop which doesn't accept an answer which isn't above 15,000 and an integer.
                bool Valid = false;
                while (Valid == false)
                {
                    if (int.TryParse(answer, out _) && Convert.ToInt32(answer) >= 15000)
                    {
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Unfortunately, that's not a valid response. Please enter an integer, with a minimum income of at least £15,000: ");
                        Console.WriteLine();
                        answer = Console.ReadLine();
                    }
                }
                income = Convert.ToInt32(answer);
                Console.WriteLine();
                Console.WriteLine("Thank you. Please hold for a momemnt while I see which mortgages we are currently offering.");
                Console.WriteLine();
                MaxCredit = Math.Round(0.3 * income, 0);
            }
        }

        // This class handles which properties the calculator can offer:
        public class Properties : Income, IProperties
        {
            public double Interest { get; set; }
            public string PropertyName { get; set; }

            public string abode;
            public string Time;
            public string Credit;

            public decimal creditTotal;


            public void Main()
            {

                IList<Properties> propertiesList = new List<Properties>()
                // Use of List<T>, can be useful if we want to expand further e.g. adding more specific properties (beach houses, etc.)
                {
                    // Flat has an interest rate of 0.75%.
                    new Properties(){ Interest=0.75, PropertyName="Flat"},
                    // House has an interest rate of 1.25%.
                    new Properties(){ Interest=1.25, PropertyName="House"},};

                Console.WriteLine("We are currently offering two types of mortgages: ");
                Console.WriteLine();
                // Prints the information stored in the List<T>
                foreach (Properties properties in propertiesList)
                {
                    Console.WriteLine("A mortgage for a " + properties.PropertyName +
                                      " has an interest rate of " + properties.Interest + " %.");
                }
                Console.WriteLine();
                Console.WriteLine("Would you like to apply for a mortgage for a flat or a house? Enter 'F' for a flat and 'H' for a house: ");
                Console.WriteLine();
                abode = Console.ReadLine();
                while (abode != "F" && abode != "H") // Handles if any answer is neither a house or a flat.
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry, that is not one of the options. Please enter 'F' for a flat or 'H' for a house: ");
                    Console.WriteLine();
                    abode = Console.ReadLine();
                }
                // Interest and credit issued for a Flat.
                if (abode == "F")
                {
                    Console.WriteLine();
                    Console.WriteLine("Okay, for someone with an income of £" + income + " wanting to " +
                        "get a mortgage on a flat, we can offer you a maximum of £" + MaxCredit + ".");
                }
                // Interest and credit issued for a House.
                else if (abode == "H")
                {
                    Console.WriteLine();
                    Console.WriteLine("Okay, for someone with an income of £" + income + " wanting to " +
                        "get a mortgage on a house, we can offer you a maximum of £" + MaxCredit + ".");
                }
                Console.WriteLine();
                Console.WriteLine("How much would you like to borrow? ");
                Console.WriteLine();
                Credit = Console.ReadLine();
                // Another loop which doesn't accpet an answer unless it is between 0 and MaxCredit and also an integer.
                bool Valid = false;
                while (Valid == false)
                {
                    if (int.TryParse(Credit, out _) && Convert.ToInt32(Credit) <= MaxCredit && Convert.ToInt32(Credit) >= 0)
                    {
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Unfortunately, that is not an acceptable answer. Please note that you can only borrow an integer amount" +
                            " between £0 and £" + MaxCredit + ". Please try again: ");
                        Console.WriteLine();
                        Credit = Console.ReadLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("How many years would you like a mortgage for? ");
                Console.WriteLine();
                Time = Console.ReadLine();
                // Another loop which doesn't accept an answer unless it is integer and between 5 and 40.
                Valid = false;
                while (Valid == false)
                {
                    if (int.TryParse(Time, out _) && Convert.ToInt32(Time) <= 40 && Convert.ToInt32(Time) >= 5)
                    {
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Unfortunately, that is not an acceptable response. Please note that the mortgage must be between 5 and 40 years long. Please try again: ");
                        Console.WriteLine();
                        Time = Console.ReadLine();
                    }
                }
                // Repayment fdetails for a flat.
                if (abode == "F")
                {
                    Console.WriteLine();

                    //Figures out the total repayment amount over the time period, plus annual repayments.
                    creditTotal = Convert.ToDecimal(string.Format("{0:0.00}", (Convert.ToDouble(Credit) *
                        (Math.Pow((1 + Convert.ToDouble(propertiesList[0].Interest) / 100), Convert.ToDouble(Time))))));

                    Console.WriteLine("Your application has been accepted. The total amount of credit issued is £" + Convert.ToDecimal(string.Format("{0:0.00}", Credit)) +
                        " for " + Time + " years, with a fixed annual interest rate of " + propertiesList[0].Interest + "%.");
                    Console.WriteLine("The total amount you will pay back is £" + creditTotal + ".");
                }
                // Repayment fdetails for a house.
                else if (abode == "H")
                {
                    Console.WriteLine();

                    //Figures out the total repayment amount over the time period, plus annual repayments.
                    creditTotal = Convert.ToDecimal(string.Format("{0:0.00}", (Convert.ToDouble(Credit) *
                        (Math.Pow((1 + Convert.ToDouble(propertiesList[1].Interest) / 100), Convert.ToDouble(Time))))));

                    Console.WriteLine("Your application has been accepted. The total amount of credit issued is £" + Convert.ToDecimal(string.Format("{0:0.00}", Credit)) +
                        " for " + Time + " years, with a fixed annual interest rate of " + propertiesList[1].Interest + "%.");
                    Console.WriteLine("The total amount you will pay back is £" + creditTotal + ".");
                }

            }
        }

        class Tester
        {

            static void Main(string[] args)
            {
                Basic t1 = new Basic();
                Income t2 = new Income();
                Properties t3 = new Properties();
                t1.BasicQuestions();
                t2.Incomeinput();
                Thread.Sleep(2000);
                t3.Main();

                // Allows user to see mortgage amount before pressing ESC to exit the program:
                Thread.Sleep(3000);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Thank you for using the Interactive Mortgage Calculator.");
                Console.WriteLine("Please press ESC to exit.");
                ConsoleKeyInfo k;
                while (true)
                {
                    k = Console.ReadKey(true);
                    if (k.Key == ConsoleKey.Escape)
                        break;

                    Console.WriteLine("{0} --- ", k.Key);
                }
            }
        }
    }
}
