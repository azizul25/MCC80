using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DatabaseConnectivity.Menu;

namespace DatabaseConnectivity;


public class Program
{
    public static void Main()
    {
        MainMenu();
    }

    public static void MainMenu()
    {

        Console.WriteLine("Menu Utama");
        Console.WriteLine("1. Employees");
        Console.WriteLine("2. Departments");
        Console.WriteLine("3. Jobs");
        Console.WriteLine("4. Histories");
        Console.WriteLine("5. Locations");
        Console.WriteLine("6. Countries");
        Console.WriteLine("7. Regions");
        Console.WriteLine("8. Exit");
        Console.WriteLine("Pilihlah: ");

        try
        {
            int pilihMenu = Int32.Parse(Console.ReadLine());

            switch (pilihMenu)
            {

                case 1:
                    Console.Clear();
                    employe.MenuEmployees();
                    break;
                case 2:
                    Console.Clear();
                    deparmens.MenuDepartment();
                    MainMenu();
                    break;
                case 3:
                    Console.Clear();
                    jobs.MenuJobs();
                    MainMenu();
                    break;
                case 4:
                    Console.Clear();
                    histories.MenuHistories();
                    MainMenu();
                    break;
                case 5:
                    Console.Clear();
                    location.MenuLocation();
                    MainMenu();
                    break;
                case 6:
                    Console.Clear();
                    countries.MenuCountry();
                    MainMenu();
                    break;
                case 7:
                    Console.Clear();
                    regions.MenuRegion();
                    MainMenu();
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("8. Exit");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("maaf hanya yang ad menu saja pilihan");
                    MainMenu();
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Input Hanya diantara 1-7!");
            MainMenu();
        }
    }

    //regions

   

    //country
   



    //location
    


    //Deparment
   

    //jobs
    

    //historiees
   

    //employee
}
