using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    class Register
    {
        public void StudentRegistration(List<StudentLogin> creds, string login_filepath, List<StudentInformation> sInformations, string Info_filepath)
        {
            Console.WriteLine("Enter your Student ID (This will be your UserName) : ");
            string StudentId = Console.ReadLine();
            Console.WriteLine("Enter a Password for the above user : ");
            string password = Console.ReadLine();
            foreach (var cred in creds)
            {
                if (cred.username == StudentId)
                {
                    Console.WriteLine("The entered username already exists. If you forgot password to account, " +
                        "try Forgot Password (3) option in the main menu ");
                    Welcome.DisplayMenu();
                }
            }

            Console.WriteLine("Great! Please answer below security questions." +
                " These will aid in recovering your forgot password");
            Console.WriteLine("1. What is your favourite colour ?");
            string Fav_clr = Console.ReadLine();
            Console.WriteLine("2. What is your favourite food ?");
            string Fav_food = Console.ReadLine();
            Console.WriteLine("3. What is your mother's maiden name ?");
            string maiden_name = Console.ReadLine();

            var options = new JsonSerializerOptions { WriteIndented = true };

            if (creds == null)
            {
                List<StudentLogin> login = new List<StudentLogin>();
                login.Add(new StudentLogin
                {
                    username = StudentId,
                    password = password,
                    FavouriteColour = Fav_clr,
                    FavouriteFood = Fav_food,
                    MotherMaidenName = maiden_name
                });
                string Jsonstring = JsonSerializer.Serialize(login, options);


                using (StreamWriter sw = new StreamWriter(login_filepath))
                {
                    sw.WriteLine(Jsonstring);
                }

            }
            else
            {
                creds.Add(new StudentLogin
                {
                    username = StudentId,
                    password = password,
                    FavouriteColour = Fav_clr,
                    FavouriteFood = Fav_food,
                    MotherMaidenName = maiden_name
                });
                string Jsonstring = JsonSerializer.Serialize(creds, options);


                using (StreamWriter sw = new StreamWriter(login_filepath))
                {
                    sw.WriteLine(Jsonstring);
                }

            }



            Console.WriteLine("Yayyy!! We have registered you in the Student Hub.\n\n" +
                "Please Provide your answers for the below questions\n" +
                "**By Providing answers to below questions you are explicitly providing consent to College to " +
                "store your Personal Information\n\n");
            AddStudentInfo addStudent = new AddStudentInfo();
            addStudent.AddStudentInformation(sInformations, StudentId, Info_filepath);
            Welcome.DisplayMenu();

        }
    }
}
