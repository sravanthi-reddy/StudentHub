using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace StudentHub.Models
{
    class AddStudentInfo
    {
        public void InsertStudentInfoMethod(List<StudentInformation> sInfo, string username, string Info_filepath)
        {
            string u = username;
            var sInformations = sInfo;
            string PEmail = "";
            Console.WriteLine("1. Enter Your First Name :");
            string Fname = Console.ReadLine();
            Console.WriteLine("Enter Your Last Name :");
            string Lname = Console.ReadLine();
            Console.WriteLine("Enter Your Middle Name :");
            string Mname = Console.ReadLine();
            Console.WriteLine("Enter Your Social Insurance Number :");
            string SIN = Console.ReadLine();
            Console.WriteLine("Enter Your Personal Email ID :");
            string Temp_PEmail = Console.ReadLine();
            VerifyOTP OV = new VerifyOTP();
            PEmail = OV.OTPverifyMethod(Temp_PEmail, PEmail, Fname);
            Console.WriteLine("Enter Your Mobile Number :");
            string Mobile = Console.ReadLine();
            Console.WriteLine("Emergency Details \nEnter Your Emergency Contact Name :");
            string EcontactName = Console.ReadLine();
            Console.WriteLine("Enter Your Relation with above Emergency Contact :");
            string EcontactRel = Console.ReadLine();
            Console.WriteLine("Enter Your Emergency Contact Mobile Number :");
            string Emobile = Console.ReadLine();
            Console.WriteLine("Enter Your Nationality :");
            string Ntnlty = Console.ReadLine();
            Console.WriteLine("Enter Your Term (example: Fall 2021):");
            string term = Console.ReadLine();
            Console.WriteLine("In which Course are you Enrolled In :");
            string CourseEnroled = Console.ReadLine();
            Console.WriteLine("Enter Your Course TimeTable:");
            string TimeTable = Console.ReadLine();
            Console.WriteLine("Enter Your Skills (Seperated by Comma) :");
            string skills = Console.ReadLine();
            Console.WriteLine("Enter any other Georgian COllege Course you are intrested in (If applicable):");
            string Ocourse = Console.ReadLine();
            Console.WriteLine("How many years of work experience do you have :");
            string wrkexp = Console.ReadLine();
            Console.WriteLine("Enter your LinkedInURL :");
            string LinkedInURL = Console.ReadLine();
            Console.WriteLine("Enter your hobbies or passion :");
            string passion = Console.ReadLine();
            Console.WriteLine("Enter your GitHub URL (if applicable):");
            string git = Console.ReadLine();
            Console.WriteLine("Mailing Address Details\n");
            Console.WriteLine("Unit Number :");
            string Uno = Console.ReadLine();
            Console.WriteLine("Street Number :");
            string Sno = Console.ReadLine();
            Console.WriteLine("Street Name :");
            string sName = Console.ReadLine();
            Console.WriteLine("City :");
            string city = Console.ReadLine();
            Console.WriteLine("Province :");
            string prov = Console.ReadLine();
            Console.WriteLine("Country :");
            string cntry = Console.ReadLine();
            Console.WriteLine("Zip Code :");
            string zip = Console.ReadLine();

            sInformations.Add(new StudentInformation
            {
                StudentID = u,
                Name = new Name { FirstName = Fname, MiddleName = Mname, LastName = Lname },
                SocialInsuranceNumber = SIN,
                Email = new Email { PersonalEmail = PEmail, StudentEmail = $"{u}@student.georgianc.on.ca" },
                Mobile = Mobile,
                Emergency = new Emergency
                {
                    EmergencyContactName = EcontactName,
                    EmergencyMobileNumber = Emobile,
                    EmergencyRelation = EcontactRel
                },
                Nationality = Ntnlty,
                Term = term,
                CourseEnrolledIn = CourseEnroled,
                TimeTable = TimeTable,
                Skills = skills,
                OtherCourseInterests = Ocourse,
                WorkExperience = wrkexp,
                LinkedInURL = LinkedInURL,
                Passion = passion,
                GithubURL = git,
                MailingAddress = new MailingAddress
                {
                    UnitNumber = Uno,
                    StreetName = sName,
                    StreetNumber = Sno,
                    city = city,
                    Province = prov,
                    Country = cntry,
                    ZipCode = zip
                }
            });
            var options = new JsonSerializerOptions { WriteIndented = true };
            string JsonInfostring = System.Text.Json.JsonSerializer.Serialize(sInformations, options);
            using (StreamWriter swinfo = new StreamWriter(Info_filepath))
            {
                swinfo.WriteLine(JsonInfostring);
            }

            Console.WriteLine("Your Details are Saved Successfully. Sending you to Main Menu");
        }
    }
}
