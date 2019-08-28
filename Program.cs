using System;
using System.Net.Mail;
using System.IO;
using System.IO.Compression;



namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the location of the folder that you want to ZIP, then hit ENTER");
            Console.WriteLine("(Ex: C:\\pics\\BA500_Assignment1):");
            Console.WriteLine("");
           
            // The path of the folder which needs to be backed up
            string pathToZip =  Console.ReadLine();
            //string pathToZip =  "C:\\pics\\BA500_Assignment1";


            Console.WriteLine("");
            Console.WriteLine("Please enter the location of the folder where you would like to copy the zipped file, then hit ENTER");
            Console.WriteLine("(Ex: C:\\pics):");
            Console.WriteLine("");

            // The path of the folder where the Zip filed can be copied into
            string pathWhereToCopy = Console.ReadLine();
            //string pathToZip =  "C:\\pics";

            ZiptheFile(pathToZip, pathWhereToCopy);

            Console.WriteLine("");
            Console.WriteLine("Please enter the emails you want to send the Zipped file, then hit ENTER:");
            Console.WriteLine("Ex: bandaru11@gmail.com; gadha-file-keya-karega@gmail.com");
            Console.WriteLine("");

            string Email = Console.ReadLine();
           // string Email = "mbandaru11@gmail.com; sougataroy@gmail.com";

            SendEmail(Email, pathWhereToCopy);
        }
        public static void SendEmail(string Emails, string PathWhereToCopyTo)
        {
            try
            {
                const string TextBody = "Please see attached .\r\n\n";
                SmtpClient Myserver = new SmtpClient
                {
                    Host = "google.com",
                    Port = 25
                };
                MailAddress fromAddress = new MailAddress("mbandaru11@gmail.com", "OCCG Support");
                // var fromAddress = new MailAddress("mbandaru11@gmail.com", "OCCG Support");
                

                //2.The Destination email Addresses
                MailAddressCollection TO_addressList = new MailAddressCollection();

                //3.Prepare the Destination email Addresses list
                foreach (var curr_address in Emails.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress mytoAddress = new MailAddress(curr_address);
                    TO_addressList.Add(mytoAddress);
                }

                var toAddress = new MailAddress(Emails);
                const string subject = "xyz for WE 8/27/2019";

                using (var message = new MailMessage()
                {
                    From = fromAddress,
                    Subject = subject,
                    Body = TextBody                   
                })
                {
                    //string ZippedFileToAttach = @"C:\pics\BA500_Assignment1";

                    string ZippedFileToAttach = @PathWhereToCopyTo;
                    //string ZippedFileToAttach = @"C:\Projects\myzip.zip";
                    message.Attachments.Add(new Attachment(ZippedFileToAttach));
                    message.To.Add(TO_addressList.ToString());
                    Myserver.Send(message);
                    Console.WriteLine("Message sent to the emails provided.");
                }

            }
            catch (Exception ex)
            {

            }
        }

        public static void ZiptheFile(string ZipPath, string CopyPath)
        {
          
            //var zipFile = @"C:\pics\myzip.zip";
          
            var zipFile = @CopyPath + "\\myzip.zip";
            
            // var files = Directory.GetFiles(@"C:\pics\BA500_Assignment1");
            var files = Directory.GetFiles(@ZipPath);

            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                foreach (var fPath in files)
                {
                    archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
                }
            }
        }


    }
}

