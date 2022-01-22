using LabCorpEmployee.Model;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace LabCorpEmployee
{
	class Program
	{
		public static IConfigurationRoot _config;

		static void Main(string[] args)
		{
			ServiceCollection serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			int workingDays = Convert.ToInt32(_config.GetSection("MaxWorkingDays").Value);

			HourlyEmployee hourly = new HourlyEmployee(Convert.ToInt32(_config.GetSection("HourlyEmpVacationDays").Value), workingDays);			
			SalariedEmployee salaried = new SalariedEmployee(Convert.ToInt32(_config.GetSection("SalariedEmpVacationDays").Value),workingDays);			
			Manager mgr = new Manager(Convert.ToInt32(_config.GetSection("ManagerVacationDays").Value), workingDays);

            MainMenu(hourly, salaried, mgr);
		}

		private static void MainMenu(HourlyEmployee hourly, SalariedEmployee salaried, Manager mgr)
        {   
            Console.Clear();
            Console.WriteLine("Choose Employee Type:");
            Console.WriteLine("1) Hourly Employee");
            Console.WriteLine("2) Salaried Employee");
            Console.WriteLine("3) Manager");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");
            
            var option = Console.ReadLine().Trim();
            if ((option == "1" || option == "2" || option == "3") &&  Int32.TryParse(option,out int number))
            {                
                bool showMenu = true;
                while (showMenu)
                {
                    showMenu = SubMenu(Convert.ToInt32(option), hourly,salaried,mgr);
                }
			}			
            else
            { 
                MainMenu(hourly, salaried, mgr); 
            }
        }


        private static bool SubMenu(int mainMenuOption, HourlyEmployee hourly, SalariedEmployee salaried, Manager mgr)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1) Enter working days");
            Console.WriteLine("2) Enter vacation days taken");
            Console.WriteLine("3) Main Menu");
            Console.Write("\r\nSelect an option: ");
            try
            {
                var option = (Console.ReadLine());
                if (Int32.TryParse(option, out int number))
                {
                    int selected = Convert.ToInt32(option);
                    switch (selected)
                    {
                        case 1:
                        case 2:
                            if (mainMenuOption == 1) HourlyEmp(hourly, selected);
                            else if (mainMenuOption == 2) SalariedEmp(salaried, selected);
                            else if (mainMenuOption == 3) ManagerEmp(mgr, selected);
                            return true;
                        case 3:
                            MainMenu(hourly, salaried, mgr);
                            return false;
                        default:
                            return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
			{
                Console.WriteLine("Error occured during selecting the options," + ex.Message + ", Please try again.");
                return true;
			}
        }

        private static void HourlyEmp(HourlyEmployee hourly, int option)
        {
            Console.WriteLine("Total Vacation Days accrued: " + hourly.totalVacationDaysAccumulated);
            Console.WriteLine("Total Employee working days: " + hourly.employeeWorkingDays); 
            if (option == 1)
            {
                Console.WriteLine("Enter no. of working days to calculate vacation accrued: ");
                int noOfWorkingDays = Convert.ToInt32(Console.ReadLine().Trim());
                Console.WriteLine("Total Vacation Accrued: " + hourly.Work(noOfWorkingDays));
            }
			else
			{
                Console.WriteLine("Enter no. of vacations taken: ");
                float noOfVacationTaken = float.Parse(Console.ReadLine().Trim());
                Console.WriteLine("Total Vacation Balance: "+hourly.TakeVacation(noOfVacationTaken));
            }
        }

        private static void SalariedEmp(SalariedEmployee salaried, int option)
        {
            Console.WriteLine("Total Vacation Days accrued: " + salaried.totalVacationDaysAccumulated);
            Console.WriteLine("Total Employee working days: " + salaried.employeeWorkingDays); 
            if (option == 1)
            {
                Console.WriteLine("Enter no. of working days to calculate vacation accrued: ");
                int noOfWorkingDays = Convert.ToInt32(Console.ReadLine().Trim());
                Console.WriteLine("Total Vacation Accrued: " + salaried.Work(noOfWorkingDays));
            }
            else
            {
                Console.WriteLine("Enter no. of vacations taken: ");
                float noOfVacationTaken = float.Parse(Console.ReadLine().Trim());
                Console.WriteLine("Total Vacation Balance: " + salaried.TakeVacation(noOfVacationTaken));
            }
        }

        private static void ManagerEmp(Manager mgr, int option)
        {
            Console.WriteLine("Total Vacation Days accrued: " + mgr.totalVacationDaysAccumulated);
            Console.WriteLine("Total Employee working days: " + mgr.employeeWorkingDays);
            if (option == 1)
            {
                Console.WriteLine("Enter no. of working days to calculate vacation accrued: ");
                int noOfWorkingDays = Convert.ToInt32(Console.Read());
                Console.WriteLine("Total Vacation Accrued: " + mgr.Work(noOfWorkingDays));
            }
            else
            {
                Console.WriteLine("Enter no. of vacations taken: ");
                float noOfVacationTaken = float.Parse(Console.ReadLine());
                Console.WriteLine("Total Vacation Balance: " + mgr.TakeVacation(noOfVacationTaken));
            }
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
		{
			_config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

			serviceCollection.AddSingleton<IConfigurationRoot>(_config);
		}
	}
}
