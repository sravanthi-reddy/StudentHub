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
        public void AddStudentInformation(List<StudentInformation> sInfo, string username, string Info_filepath)
        {
            try
            {
                string u = username;
                var sInformations = sInfo;
                string PEmail = "";
                Console.WriteLine("1. Enter Your First Name :");
                string Fname = Console.ReadLine();
                Console.WriteLine("2. Enter Your Middle Name :");
                string Mname = Console.ReadLine();
                Console.WriteLine("3. Enter Your Last Name :");
                string Lname = Console.ReadLine();
                Console.WriteLine("4. Enter Your Social Insurance Number :");
                string SIN = Console.ReadLine();
                try
                {
                    Console.WriteLine("5. Enter Your Personal Email ID :");
                    string Temp_PEmail = Console.ReadLine();
                    VerifyOTP OV = new VerifyOTP();
                    PEmail = OV.OTPverifyMethod(Temp_PEmail, PEmail, Fname);
                } catch (Exception exc)
                {
                    Console.WriteLine("Please enter valid email id");
                    Console.WriteLine("5. Enter Your Personal Email ID :");
                    string Temp_PEmail = Console.ReadLine();
                    VerifyOTP OV = new VerifyOTP();
                    PEmail = OV.OTPverifyMethod(Temp_PEmail, PEmail, Fname);

                }
               
                Console.WriteLine("6. Enter Your Mobile Number :");
                string Mobile = Console.ReadLine();
                Console.WriteLine("7. Emergency Details \nEnter Your Emergency Contact Name :");
                string EcontactName = Console.ReadLine();
                Console.WriteLine("8. Enter Your Relation with above Emergency Contact :");
                string EcontactRel = Console.ReadLine();
                Console.WriteLine("9. Enter Your Emergency Contact Mobile Number :");
                string Emobile = Console.ReadLine();
                Console.WriteLine("10. Enter Your Nationality :");
                string Ntnlty = Console.ReadLine();
                Console.WriteLine("11. Enter Your Term (example: Fall 2021):");
                string term = Console.ReadLine();
                Console.WriteLine("12. In which Course are you Enrolled In :");
                string CourseEnroled = Console.ReadLine();
                Console.WriteLine("13. Enter Your Course TimeTable:");
                string TimeTable = Console.ReadLine();
                Console.WriteLine("14. Enter Your Skills (Seperated by Comma) :");
                string skills = Console.ReadLine();
                Console.WriteLine("15. Enter any other Course you are intrested in (If applicable):");
                string Ocourse = Console.ReadLine();
                Console.WriteLine("16. How many years of work experience do you have :");
                string wrkexp = Console.ReadLine();
                Console.WriteLine("17. Enter your LinkedInURL :");
                string LinkedInURL = Console.ReadLine();
                Console.WriteLine("18. Enter your hobbies or passion :");
                string passion = Console.ReadLine();
                Console.WriteLine("19. Enter your GitHub URL (if applicable):");
                string git = Console.ReadLine();
                Console.WriteLine("20. Mailing Address Details\n");
                Console.WriteLine("Unit Number :");
                string Uno = Console.ReadLine();
                Console.WriteLine("21. Street Number :");
                string Sno = Console.ReadLine();
                Console.WriteLine("22. Street Name :");
                string sName = Console.ReadLine();
                Console.WriteLine("23. City :");
                string city = Console.ReadLine();
                Console.WriteLine("24. Province :");
                string prov = Console.ReadLine();
                Console.WriteLine("25. Country :");
                string cntry = Console.ReadLine();
                Console.WriteLine("26. ZipCode :");
                string zip = Console.ReadLine();
                if (sInformations == null)
                {
                    List<StudentInformation> info = new List<StudentInformation>();
                    info.Add(new StudentInformation
                    {
                        StudentID = u,
                        Name = new Name { FirstName = Fname, MiddleName = Mname, LastName = Lname },
                        SocialInsuranceNumber = SIN,
                        Email = new Email { PersonalEmail = PEmail, StudentEmail = $"{u}@student.college.on.ca" },
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

                    string Jsonstring = JsonSerializer.Serialize(info, options);


                    using (StreamWriter sw = new StreamWriter(Info_filepath))
                    {
                        sw.WriteLine(Jsonstring);
                    }

                }
                else
                {
                    sInformations.Add(new StudentInformation
                    {
                        StudentID = u,
                        Name = new Name { FirstName = Fname, MiddleName = Mname, LastName = Lname },
                        SocialInsuranceNumber = SIN,
                        Email = new Email { PersonalEmail = PEmail, StudentEmail = $"{u}@student.college.on.ca" },
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
                    string JsonInfostring = JsonSerializer.Serialize(sInformations, options);
                    using (StreamWriter swinfo = new StreamWriter(Info_filepath))
                    {
                        swinfo.WriteLine(JsonInfostring);
                    }
                }

                Console.WriteLine("Your Details are Saved Successfully. Sending you to Main Menu");
            }
            catch (Exception exc)
            {
                Console.WriteLine("Please Give Proper Details");
                Welcome.DisplayMenu();

            }
        }
    }
}
