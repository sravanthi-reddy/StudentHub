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
        static int  tableWidth = 80;

        public bool Authentication(List<StudentLogin> creds, string u, string p)
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

        public void ShowUserInfo(List<StudentInformation> sInformations, string u)
        {
            Console.WriteLine("Please find your details below : \n");
            foreach (var sInfo in sInformations)
            {
                if (sInfo.StudentID == u)
                {
                    PrintLine();
                    PrintRow("StudentId", sInfo.StudentID);
                    PrintRow("First Name", sInfo.Name.FirstName);
                    PrintRow("Middle Name", sInfo.Name.MiddleName);
                    PrintRow("Last Name", sInfo.Name.LastName);
                    PrintRow("Social Insurance Number", sInfo.SocialInsuranceNumber);
                    PrintRow("Personal Email", sInfo.Email.PersonalEmail);
                    PrintRow("Student Email", sInfo.Email.StudentEmail);
                    PrintRow("Mobile Number", sInfo.Mobile);
                    PrintRow("Emergency Contact Name", sInfo.Emergency.EmergencyContactName);
                    PrintRow("Emergency Contact Relation", sInfo.Emergency.EmergencyRelation);
                    PrintRow("Emergency Contact Mobile Number", sInfo.Emergency.EmergencyMobileNumber);
                    PrintRow("Nationality", sInfo.Nationality);
                    PrintRow("Term", sInfo.Term);
                    PrintRow("Course Enrolled In", sInfo.CourseEnrolledIn);
                    PrintRow("Course Time Table", sInfo.TimeTable);
                    PrintRow("Skills", sInfo.Skills);
                    PrintRow("Other Course Interests",sInfo.OtherCourseInterests);
                    PrintRow("Work Experience", sInfo.WorkExperience);
                    PrintRow("LinkedInURL", sInfo.LinkedInURL);
                    PrintRow("Passion", sInfo.Passion);
                    PrintRow("GithubURL", sInfo.GithubURL);
                    PrintRow("Unit Number", sInfo.MailingAddress.UnitNumber);
                    PrintRow("Street Number", sInfo.MailingAddress.StreetNumber);
                    PrintRow("Street Name", sInfo.MailingAddress.StreetName);
                    PrintRow("City", sInfo.MailingAddress.city);
                    PrintRow("Province", sInfo.MailingAddress.Province);
                    PrintRow("Country", sInfo.MailingAddress.Country);
                    PrintRow("ZipCode", sInfo.MailingAddress.ZipCode);
                }
            }

        }
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
            PrintLine();
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        public void ModifyInfo(List<StudentInformation> sInformations, List<StudentLogin> creds, string u, string Info_filepath, string login_filepath)
        {
            Console.WriteLine("\nEnter 1 to Update any of your above details in the system\nEnter 2 to Change Your Password\nEnter 3 to Delete your account\nEnter 4 to go back to the Main Menu\nEnter 5 to exit the application");
            int userinput = Convert.ToInt32(Console.ReadLine());
            switch (userinput)
            {
                case 1:
                    {
                        updateMethod(sInformations, u, Info_filepath);
                        Console.WriteLine("Here are your details after the update \n");
                        ShowUserInfo(sInformations, u);
                        ModifyInfo(sInformations,creds,u,Info_filepath,login_filepath);
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
                        Console.WriteLine("Sending back to Main Menu");
                        Welcome.DisplayMenu();
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Sending you back to Menu Menu");
                        Welcome.DisplayMenu();
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Exiting Application");
                        Environment.Exit(1);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Input. Sending you back to Main Menu");
                        Welcome.DisplayMenu();
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
            string Jsonstring = JsonSerializer.Serialize(creds, options);
            using (StreamWriter sw = new StreamWriter(login_filepath))
            {
                sw.WriteLine(Jsonstring);
            }
            Console.WriteLine("Congrats. We have update your password successfully. Sending you to Main menu");
            Welcome.DisplayMenu();
        }

        public void updateMethod(List<StudentInformation> sInformations, string u, string Info_filepath)
        {

            Console.WriteLine("Enter the Numbers coresponding to the detail you would like to update with comma seperation\n" +
         "1. First Name\n2. Middle Name\n3. LastName\n4. SocialInsuranceNumber\n5. Personal Email\n"+
         "6. Mobile Number\n7. Emergency Contact Name\n8. Relation With EMergency Contact\n9. Emergency Mobile Number" +
         "10. Nationality\n11. Term\n12. Course Enrolled In\n13. Course Time Table\n14. Skills\n15. Other Course Interests" +
         "16. Work Experience\n17. LinkedInURL\n18. Passion\n19. GithubURL\n20. Unit Number\n21. Street Number\n22. StreetName\n" +
         "23. City\n24. Province\n25. Country\n26. ZipCode");
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
                                Console.WriteLine("Enter your Mobile Number :");
                                sInfo.Mobile = Console.ReadLine();
                                break;
                            case 7:
                                Console.WriteLine("Enter your Emergency Contact Name :");
                                sInfo.Emergency.EmergencyContactName = Console.ReadLine();
                                break;
                            case 8:
                                Console.WriteLine("Enter Your Relation with Emergency Contact :");
                                sInfo.Emergency.EmergencyRelation = Console.ReadLine();
                                break;
                            case 9:
                                Console.WriteLine("Enter Your Emergency Contact Mobile Number :");
                                sInfo.Emergency.EmergencyMobileNumber = Console.ReadLine();
                                break;
                            case 10:
                                Console.WriteLine("Enter Your Nationality :");
                                sInfo.Nationality = Console.ReadLine();
                                break;
                            case 11:
                                Console.WriteLine("Enter Your Term (example: Fall 2021):");
                                sInfo.Term = Console.ReadLine();
                                break;
                            case 12:
                                Console.WriteLine("In which Course are you Enrolled In :");
                                sInfo.CourseEnrolledIn = Console.ReadLine();
                                break;
                            case 13:
                                Console.WriteLine("Enter Your Course TimeTable:");
                                sInfo.TimeTable = Console.ReadLine();
                                break;
                            case 14:
                                Console.WriteLine("Enter Your Skills (Seperated by Comma) :");
                                sInfo.Skills = Console.ReadLine();
                                break;
                            case 15:
                                Console.WriteLine("Enter any other Course you are intrested in (If applicable):");
                                sInfo.OtherCourseInterests = Console.ReadLine();
                                break;
                            case 16:
                                Console.WriteLine("How many years of work experience do you have :");
                                sInfo.WorkExperience = Console.ReadLine();
                                break;
                            case 17:
                                Console.WriteLine("Enter your LinkedInURL :");
                                sInfo.LinkedInURL = Console.ReadLine();
                                break;
                            case 18:
                                Console.WriteLine("Enter your Passion Or Hobbies :");
                                sInfo.Passion = Console.ReadLine();
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
            string JsonInfostring = JsonSerializer.Serialize(sInformations, options);
            using (StreamWriter swinfo = new StreamWriter(Info_filepath))
            {
                swinfo.WriteLine(JsonInfostring);
            }
        }
    }
}
