using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    class Login
    {
        public bool AuthMethod(List<StudentLogin> creds, string u, string p)
        {
            bool IsAuthenticated = false;
            foreach (var cred in creds)
            {
                if (cred.username == u) { IsAuthenticated = (p == cred.password) ? true : false; }
            }

            if (IsAuthenticated == false)
            {
                Console.WriteLine("The Username or Password is incorrect\nYou can make use of Forgot Password " +
                    "option in the main menu\n");
            }
            return IsAuthenticated;
        }

        public void DisplayMethod(List<StudentInformation> sInformations, string u)
        {
            const string UNDERLINE = "\x1B[4m";
            const string RESET = "\x1B[0m";
            foreach (var sInfo in sInformations)
            {
                if (sInfo.StudentID == u)
                {
                    Console.WriteLine("Your Saved Details in the system \n\n" +
                        $"Student ID:{sInfo.StudentID}\n" + UNDERLINE + $"Name\n\t" + RESET + $"First Name : {sInfo.Name.FirstName}" +
                        $"\n\tMiddle Name :{sInfo.Name.MiddleName}\n\tLast Name : {sInfo.Name.LastName}\nSocial Insurance Number : {sInfo.SocialInsuranceNumber}\n" +
                        UNDERLINE + $"Email" + RESET + $"\n\tPersonal Email : {sInfo.Email.PersonalEmail}\n\tStudent Email : {sInfo.Email.StudentEmail}\n" +
                        $"Mobile : {sInfo.Mobile}\n" + UNDERLINE + "Emergency" + RESET + $"\n\tEmergency Contact Name : {sInfo.Emergency.EmergencyContactName}\n\tEmergency Contact Relation : {sInfo.Emergency.EmergencyRelation}\n\tEmergency Contact Mobile Number : {sInfo.Emergency.EmergencyMobileNumber}\n" +
                        $"Nationality : {sInfo.Nationality}\nTerm : {sInfo.Term}\nCourse Enrolled In : {sInfo.CourseEnrolledIn}\nCourse Time Table : {sInfo.TimeTable}\nSkills : {sInfo.Skills}\nOther Course Interests : {sInfo.OtherCourseInterests}\nWork Experience : {sInfo.WorkExperience}\nLinkedInURL : {sInfo.LinkedInURL}\nPassion : {sInfo.Passion}\n" +
                        $"Github URL : {sInfo.GithubURL}\n" + UNDERLINE + "MailingAddress" + RESET + $"\n\tUnitNumber : {sInfo.MailingAddress.UnitNumber}\n\tStreet Number: {sInfo.MailingAddress.StreetName}\n\tStreet Name :  {sInfo.MailingAddress.StreetName}\n\tCity : {sInfo.MailingAddress.city}\n\tProvince : {sInfo.MailingAddress.Province}\n\tCountry : {sInfo.MailingAddress.Country}\n\tZip COde : {sInfo.MailingAddress.ZipCode}");

                }
            }

        }

        public void Upd_Del_Change(List<StudentInformation> sInformations, List<StudentLogin> creds, string u, string Info_filepath, string login_filepath)
        {
            Console.WriteLine("\nEnter 1 to Update any of your above details in the system\nEnter 2 to Change Your Password\nEnter 3 to Delete your account\nEnter 4 to exit the application");
            int userinput = Convert.ToInt32(Console.ReadLine());
            switch (userinput)
            {
                case 1:
                    {
                        updateMethod(sInformations, u, Info_filepath);
                        Console.WriteLine("Here are your details after the update \n");
                        DisplayMethod(sInformations, u);
                        break;
                    }
                case 2:
                    {
                        ChangePasswordMethod(creds, u, login_filepath);
                        break;
                    }
                case 3:
                    {
                        DeleteAccountMethod(creds, login_filepath, sInformations, Info_filepath, u);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Exiting Application");
                        Environment.Exit(1);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Input. Exiting Application");
                        Environment.Exit(1);
                        break;
                    }
            }

        }

        private void DeleteAccountMethod(List<StudentLogin> creds, string login_filepath, List<StudentInformation> sInformations, string Info_filepath, string u)
        {
            int counter_cred = 0;
            int counter_sInfo = 0;
            lock (creds)
            {
                foreach (var cred in creds)
                {
                    if (cred.username == u)
                    {
                        break;
                    }
                    counter_cred++;
                }
            }
            creds.Remove(creds[counter_cred]);
            lock (sInformations)
            {
                foreach (var sInfo in sInformations)
                {
                    if (sInfo.StudentID == u)
                    {
                        break;
                    }
                    counter_sInfo++;
                }
            }
            sInformations.Remove(sInformations[counter_sInfo]);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string Jsonstring = System.Text.Json.JsonSerializer.Serialize(creds, options);
            using (StreamWriter sw = new StreamWriter(login_filepath))
            {
                sw.WriteLine(Jsonstring);
            }
            string JsonInfostring = System.Text.Json.JsonSerializer.Serialize(sInformations, options);
            using (StreamWriter swinfo = new StreamWriter(Info_filepath))
            {
                swinfo.WriteLine(JsonInfostring);
            }
            Console.WriteLine("\nYou Account has been deleted successfully");

        }

        public void ChangePasswordMethod(List<StudentLogin> creds, string u, string login_filepath)
        {
            Console.WriteLine("Enter new password for your Account :");
            string upd_password = Console.ReadLine();
            foreach (var cred in creds)
            {
                if (cred.username == u) { cred.password = upd_password; }
            }
            var options = new JsonSerializerOptions { WriteIndented = true };
            string Jsonstring = System.Text.Json.JsonSerializer.Serialize(creds, options);
            using (StreamWriter sw = new StreamWriter(login_filepath))
            {
                sw.WriteLine(Jsonstring);
            }
            Console.WriteLine("Congrats. We have update your password successfully. Sending you to Main menu");
            Welcome w = new Welcome();
            Welcome.DisplayOptions();
        }

        public void updateMethod(List<StudentInformation> sInformations, string u, string Info_filepath)
        {

            Console.WriteLine("Enter the Numbers coresponding to the detail you would like to update with comma seperation\n" +
         "1. First Name\n2. Middle Name\n3. LastName\n4. SocialInsuranceNumber\n5. Personal Email\n6. Passion or Hobbies" +
         "7. Mobile Number\n8. Emergency Contact Name\n9. Relation With EMergency Contact\n10. Emergency Mobile Number" +
         "11. Nationality\n12. Term\n13. Course Enrolled In\n14. Course Time Table\n15. Skills\n16. Other Course Interests" +
         "17. Work Experience\n18. LinkedInURL\n19. GithubURL\n20. Unit Number\n21. Street Number\n22. StreetName\n23. City\n24. Province\n25. Country\n26. Zip Code");
            string inputkeys = Console.ReadLine();
            string[] updatekeys = inputkeys.Split(',');

            foreach (var sInfo in sInformations)
            {
                if (sInfo.StudentID == u)
                {
                    foreach (var key in updatekeys)
                    {
                        switch (Int32.Parse(key))
                        {
                            case 1:
                                Console.WriteLine("Enter your First Name :");
                                sInfo.Name.FirstName = Console.ReadLine();
                                break;
                            case 2:
                                Console.WriteLine("Enter your Middle Name :");
                                sInfo.Name.MiddleName = Console.ReadLine();
                                break;
                            case 3:
                                Console.WriteLine("Enter your Last Name :");
                                sInfo.Name.LastName = Console.ReadLine();
                                break;
                            case 4:
                                Console.WriteLine("Enter Your Social Insurance Number :");
                                sInfo.SocialInsuranceNumber = Console.ReadLine();
                                break;
                            case 5:
                                Console.WriteLine("Enter Your Personal Email ID :");
                                VerifyOTP OV = new VerifyOTP();
                                string Temp = OV.OTPverifyMethod(Console.ReadLine(), "", sInfo.Name.FirstName);
                                sInfo.Email.PersonalEmail = (Temp == "") ? sInfo.Email.PersonalEmail : Temp;
                                break;
                            case 6:
                                Console.WriteLine("Enter your Passion Or Hobbies :");
                                sInfo.Passion = Console.ReadLine();
                                break;
                            case 7:
                                Console.WriteLine("Enter your Mobile Number :");
                                sInfo.Mobile = Console.ReadLine();
                                break;
                            case 8:
                                Console.WriteLine("Enter your Emergency Contact Name :");
                                sInfo.Emergency.EmergencyContactName = Console.ReadLine();
                                break;
                            case 9:
                                Console.WriteLine("Enter Your Relation with Emergency Contact :");
                                sInfo.Emergency.EmergencyRelation = Console.ReadLine();
                                break;
                            case 10:
                                Console.WriteLine("Enter Your Emergency Contact Mobile Number :");
                                sInfo.Emergency.EmergencyMobileNumber = Console.ReadLine();
                                break;
                            case 11:
                                Console.WriteLine("Enter Your Nationality :");
                                sInfo.Nationality = Console.ReadLine();
                                break;
                            case 12:
                                Console.WriteLine("Enter Your Term (example: Fall 2021):");
                                sInfo.Term = Console.ReadLine();
                                break;
                            case 13:
                                Console.WriteLine("In which Course are you Enrolled In :");
                                sInfo.CourseEnrolledIn = Console.ReadLine();
                                break;
                            case 14:
                                Console.WriteLine("Enter Your Course TimeTable:");
                                sInfo.TimeTable = Console.ReadLine();
                                break;
                            case 15:
                                Console.WriteLine("Enter Your Skills (Seperated by Comma) :");
                                sInfo.Skills = Console.ReadLine();
                                break;
                            case 16:
                                Console.WriteLine("Enter any other Georgian COllege Course you are intrested in (If applicable):");
                                sInfo.OtherCourseInterests = Console.ReadLine();
                                break;
                            case 17:
                                Console.WriteLine("How many years of work experience do you have :");
                                sInfo.WorkExperience = Console.ReadLine();
                                break;
                            case 18:
                                Console.WriteLine("Enter your LinkedInURL :");
                                sInfo.LinkedInURL = Console.ReadLine();
                                break;
                            case 19:
                                Console.WriteLine("Enter your GitHub URL :");
                                sInfo.GithubURL = Console.ReadLine();
                                break;
                            case 20:
                                Console.WriteLine("Enter your Unit Number :");
                                sInfo.MailingAddress.UnitNumber = Console.ReadLine();
                                break;
                            case 21:
                                Console.WriteLine("Enter your Street Number :");
                                sInfo.MailingAddress.StreetNumber = Console.ReadLine();
                                break;
                            case 22:
                                Console.WriteLine("Enter your Street Name :");
                                sInfo.MailingAddress.StreetName = Console.ReadLine();
                                break;
                            case 23:
                                Console.WriteLine("Enter your City :");
                                sInfo.MailingAddress.city = Console.ReadLine();
                                break;
                            case 24:
                                Console.WriteLine("Enter your Province :");
                                sInfo.MailingAddress.Province = Console.ReadLine();
                                break;
                            case 25:
                                Console.WriteLine("Enter your Country :");
                                sInfo.MailingAddress.Country = Console.ReadLine();
                                break;
                            case 26:
                                Console.WriteLine("Enter your Zip Code :");
                                sInfo.MailingAddress.ZipCode = Console.ReadLine();
                                break;
                            default:
                                Console.WriteLine("Invalid Input. Exiting Application");
                                Environment.Exit(1);
                                break;
                        }
                    }
                }
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string JsonInfostring = System.Text.Json.JsonSerializer.Serialize(sInformations, options);
            using (StreamWriter swinfo = new StreamWriter(Info_filepath))
            {
                swinfo.WriteLine(JsonInfostring);
            }
        }
    }
}
