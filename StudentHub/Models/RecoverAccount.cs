﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    class RecoverAccount
    {
        public void RecoverAccountMethod()
        {
            GetRandomNumber RG = new GetRandomNumber();
            bool NoAccountExists = true;
            string login_filepath = @"C:\Users\Sravanthi Nuthula/Desktop\StudentLoginCreds.json";
            Console.WriteLine("Enter your UserName : ");
            string u = Console.ReadLine();

            var creds_read = File.ReadAllText(login_filepath);
            var creds = JsonConvert.DeserializeObject<List<StudentLogin>>(creds_read);
            Welcome w = new Welcome();
            foreach (var cred in creds)
            {
                if (cred.username == u)
                {
                    NoAccountExists = false;
                    int SQ_ID = RG.RandomNumber(1, 3);
                    string SQ = (SQ_ID == 1) ? "What is your favourite colour ?" : (SQ_ID == 2)
                        ? "What is your favourite food ?" : "3. What is your mother's maiden name ?";
                    Console.WriteLine(SQ);
                    string ans = Console.ReadLine();

                    switch (SQ_ID)
                    {
                        case 1:
                            if (ans == cred.FavouriteColour)
                            {
                                Console.WriteLine($"Thanks for confirming. Your password is {cred.password}\n" +
                                $"Driving you to main menu \n");
                                Welcome.DisplayOptions();
                            }
                            else
                            {
                                Console.WriteLine("The entered answer does not match. Driving you to main menu again\n");
                                Welcome.DisplayOptions();
                            }
                            break;
                        case 2:
                            if (ans == cred.FavouriteFood)
                            {
                                Console.WriteLine($"Thanks for confirming. Your password is {cred.password}\n" +
                                  $"Driving you to main menu \n");
                                Welcome.DisplayOptions();
                            }
                            else
                            {
                                Console.WriteLine("The entered answer does not match. Driving you to main menu \n");
                                Welcome.DisplayOptions();
                            }
                            break;
                        case 3:
                            if (ans == cred.MotherMaidenName)
                            {
                                Console.WriteLine($"Thanks for confirming. Your password is {cred.password}\n" +
                                  $"Driving you to main menu \n");
                                Welcome.DisplayOptions();
                            }
                            else
                            {
                                Console.WriteLine("The entered answer does not match. Driving you to main menu \n");
                                Welcome.DisplayOptions();
                            }
                            break;
                    }
                }

            }
            if (NoAccountExists)
            {
                Console.WriteLine("Sorry, the entered user name is not registered with us. " +
                    "Sending you back to main menu.\n\n");
                Welcome.DisplayOptions();

            }



        }
    }
}
