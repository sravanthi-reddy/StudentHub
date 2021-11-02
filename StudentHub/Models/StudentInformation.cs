using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    public class StudentInformation
    {
        public string StudentID { get; set; }
        public Name Name { get; set; }
        public string SocialInsuranceNumber { get; set; }
        public Email Email { get; set; }
        public string Mobile { get; set; }
        public Emergency Emergency { get; set; }
        public string Nationality { get; set; }
        public string Term { get; set; }
        public string CourseEnrolledIn { get; set; }
        public string TimeTable { get; set; }
        public string Skills { get; set; }
        public string OtherCourseInterests { get; set; }
        public string WorkExperience { get; set; }
        public string LinkedInURL { get; set; }
        public string Passion { get; set; }
        public string GithubURL { get; set; }
        public MailingAddress MailingAddress { get; set; }
    }
    public class Name
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
    public class Email
    {
        public string PersonalEmail { get; set; }
        public string StudentEmail { get; set; }
    }
    public class Emergency
    {
        public string EmergencyContactName { get; set; }
        public string EmergencyRelation { get; set; }
        public string EmergencyMobileNumber { get; set; }
    }
    public class MailingAddress
    {
        public string UnitNumber { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string city { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
