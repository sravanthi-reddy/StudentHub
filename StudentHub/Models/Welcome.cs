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
        public static void DisplayOptions()

        {
            string path = @"C:\Users\Sravanthi Nuthula\Desktop\";
            string login_filepath = path + "StudentLoginCreds.json";
            string Info_filepath = path + "StudentInformation.json";

            if (!Directory.Exists(login_filepath))
            {

            }

            Console.WriteLine("Hey Grizzlie!! Welcome to Georgian Student Management System\n");
            Console.WriteLine("Enter a number corresponding to below Menu Options");
            Console.WriteLine("1. New user - Enter 1 to Register \n2. Existing User - Enter 2 to Login \n3. " +
                "Forgot Password - Enter 3 to reset your account");
            int inputkey = Convert.ToInt32(Console.ReadLine());


            var creds_read = File.ReadAllText(login_filepath);
            var creds = JsonConvert.DeserializeObject<List<StudentLogin>>(creds_read);

            var sInfo_read = File.ReadAllText(Info_filepath);
            var sInformations = JsonConvert.DeserializeObject<List<StudentInformation>>(sInfo_read);
            bool isAuthenticated;
            Welcome w = new Welcome();

            if (inputkey == 1)
            {
                Register R = new Register();
                R.RegisterMethod(creds, login_filepath, sInformations, Info_filepath);
            }
            else if (inputkey == 2)
            {

                Login L = new Login();
                Console.WriteLine("Welcome to Login Section\n");
                Console.WriteLine("Enter your username :");
                string username = Console.ReadLine();
                Console.WriteLine("Enter your password :");
                string password = Console.ReadLine();
                isAuthenticated = L.AuthMethod(creds, username, password);
                if (isAuthenticated)
                {
                    L.DisplayMethod(sInformations, username);
                    L.Upd_Del_Change(sInformations, creds, username, Info_filepath, login_filepath);

                }
                else
                {
                    Welcome.DisplayOptions();
                }
            }
            else if (inputkey == 3)
            {
                RecoverAccount RA = new RecoverAccount();
                RA.RecoverAccountMethod();
            }
        }
    }
}

   
