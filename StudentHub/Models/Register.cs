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
        public void RegisterMethod(List<StudentLogin> creds, string login_filepath, List<StudentInformation> sInformations, string Info_filepath)
        {
            Console.WriteLine("Enter your Student ID (This will be your UserName) : ");
            string u = Console.ReadLine();
            Console.WriteLine("Enter a Password for the above user : ");
            string p = Console.ReadLine();
            foreach (var cred in creds)
            {
                if (cred.username == u)
                {
                    Console.WriteLine("The entered username already exists. If you forgot password to account, " +
                        "try Forgot Password (3) option in the main menu ");
                    Environment.Exit(1);
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

            creds.Add(new StudentLogin
            {
                username = u,
                password = p,
                FavouriteColour = Fav_clr,
                FavouriteFood = Fav_food,
                MotherMaidenName = maiden_name
            });


            var options = new JsonSerializerOptions { WriteIndented = true };
            string Jsonstring = System.Text.Json.JsonSerializer.Serialize(creds, options);


            using (StreamWriter sw = new StreamWriter(login_filepath))
            {
                sw.WriteLine(Jsonstring);
            }

            Console.WriteLine("Yayyy!! We have registered you in the student management system.\n\n" +
                "Please Provide your answers for the below questions\n" +
                "By Providing answers to below questions you are explicitly providing consent to Georgian College to " +
                "store your Personal Information\n\n");
            AddStudentInfo ISI = new AddStudentInfo();
            ISI.InsertStudentInfoMethod(sInformations, u, Info_filepath);
            Welcome.DisplayOptions();

        }
    }
}
