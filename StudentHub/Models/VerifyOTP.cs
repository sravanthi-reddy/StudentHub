using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models
{
    class VerifyOTP
    {
        public string OTPverifyMethod(string Temp_PEmail, string PEmail, string Fname)
        {
            EmailOTP email = new EmailOTP();
            int otp = email.EmailOTPMethod(Temp_PEmail, Fname);
            Console.WriteLine($"A 6 digit OTP has been sent to {Temp_PEmail}. Kindly check your email and enter the OTP below");
            int UserOTP = Convert.ToInt32(Console.ReadLine());
            if (UserOTP == otp) { PEmail = Temp_PEmail; }
            else
            {
                Console.WriteLine("your entered OTP did not match with the sent OTP\n\n" +
                        "Enter 1 to resend OTP\n" +
                        "Enter 2 to change the entered Email");
                int Einp = Convert.ToInt32(Console.ReadLine());
                if (Einp == 1)
                {
                    otp = email.EmailOTPMethod(Temp_PEmail, Fname);
                    Console.WriteLine($"A new OTP has been sent to {Temp_PEmail}. Enter the OTP below");
                    UserOTP = Convert.ToInt32(Console.ReadLine());
                    if (UserOTP == otp) { PEmail = Temp_PEmail; }
                    else if (UserOTP != otp)
                    {
                        Console.WriteLine("You have entered wrong OTP again." +
                            "Proceeding to Save other details, Please update your Email later Using Update details in Login Section of main menu");
                    }
                }
                else if (Einp == 2)
                {
                    Console.WriteLine("Enter New Personal Email ID :");
                    Temp_PEmail = Console.ReadLine();
                    otp = email.EmailOTPMethod(Temp_PEmail, Fname);
                    Console.WriteLine($"A new OTP has been sent to {Temp_PEmail}. Enter the OTP below");
                    UserOTP = Convert.ToInt32(Console.ReadLine());
                    if (UserOTP == otp) { PEmail = Temp_PEmail; }
                    else if (UserOTP != otp)
                    {
                        Console.WriteLine("You have entered wrong OTP again." +
                            "Proceeding to Save other details, Please update your Email later Using Update details in Login Section of main menu");
                    }
                    else { Console.WriteLine("Invalid Input"); Environment.Exit(1); }
                }

            }
            return PEmail;

        }
    }
}
