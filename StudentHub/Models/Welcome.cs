using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    public class Welcome
    {
        public static void DisplayMenu()

        {
            string path = Environment.CurrentDirectory.Split(new string[] { "StudentHub" }, StringSplitOptions.None)[0];
            string login_filepath = path + "StudentLoginCreds.json";
            string Info_filepath = path + "StudentInformation.json";

            Console.WriteLine("===========================================================*****========================================================");
            Console.WriteLine();
            Console.WriteLine("Hey Grizzlie!! Welcome to Student Hub\n");
            Console.WriteLine("Enter a number corresponding to below Menu Options");
            Console.WriteLine("1. New user - Enter 1 to Register \n2. Existing User - Enter 2 to Login \n3. " +
                "Forgot Password - Enter 3 to reset your account \n4. "+ "Exit - Enter 4 to close the application \n5. "+ "Clear - Enter 5 to clear the log");

            List<StudentLogin> creds = new List<StudentLogin>();
            if (File.Exists(login_filepath))
            {
                var creds_read = File.ReadAllText(login_filepath);
               creds = JsonConvert.DeserializeObject<List<StudentLogin>>(creds_read);

            }
            List<StudentInformation> sInformations = new List<StudentInformation>();
            if (File.Exists(Info_filepath))
            {
                var sInfo_read = File.ReadAllText(Info_filepath);
                sInformations = JsonConvert.DeserializeObject<List<StudentInformation>>(sInfo_read);
            }
           
             bool isAuthenticated;

            string inputkey = Console.ReadLine();
            int number;
            if (Int32.TryParse(inputkey, out number) &&  number >=1  && number <= 5)
            {
                switch (number)
                {
                    case 1:
                        Register register = new Register();
                        register.StudentRegistration(creds, login_filepath, sInformations, Info_filepath);
                        break;
                    case 2:
                        Login login = new Login();
                        Console.WriteLine("Welcome to Login Section\n");
                        Console.WriteLine("Enter your username :");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter your password :");
                        string password = Console.ReadLine();
                        isAuthenticated = login.Authentication(creds, username, password);
                        if (isAuthenticated)
                        {
                            login.ShowUserInfo(sInformations, username);
                            login.ModifyInfo(sInformations, creds, username, Info_filepath, login_filepath);
                        }
                        else
                        {
                            Welcome.DisplayMenu();
                        }
                        break;

                    case 3:

                        RecoverAccount recovery = new RecoverAccount();
                        recovery.RecoverySecurityCheck(creds);
                        break;
                    case 4:

                        Console.WriteLine("Exiting the Application");
                        Environment.Exit(1);
                        break;
                    case 5:

                        Console.WriteLine("Clearing the console");
                        Console.Clear();
                        Welcome.DisplayMenu();
                        break;
                    default:

                        Console.WriteLine("Please enter the correct input");
                        Welcome.DisplayMenu();
                        break;
                }
            }else
            {
                Console.WriteLine("Please choose an integer from 1 to 5");
                Welcome.DisplayMenu();
            }
            Console.ReadKey();       
        }
    }
}

   
