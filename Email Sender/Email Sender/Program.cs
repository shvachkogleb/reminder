using System.Net.Mail;
using System.Net;

namespace Email_Sender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length < 2)
            {
                return;
            }
            
            string subject = args[0];
            string body = args[1];
            string to = args[2];
            
            SendEmail(subject, body, to);
            Console.ReadKey();
            
        }

        static void SendEmail(string subject, string body, string to)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("reminderr90@gmail.com", "dmuq xthb cqng eipp"), // Use app password
                    EnableSsl = true, // Enable SSL
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                MailMessage mail = new MailMessage("reminderr90@gmail.com", to)
                {
                    Subject = subject,
                    Body = body
                };

                client.Send(mail);
                Console.WriteLine("Email sent successfully");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                Console.WriteLine("SMTP server response: " + ex.StatusCode);
            }
        }
    }
    
}