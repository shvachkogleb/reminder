using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace reminder
{
    class TaskSchedulerHelper
    {
        public static void ScheduleEmailTask(DateTime runTime, string subject, string body, string to)
        {
            try
            {
                string exePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Email Sender.exe");
                Console.WriteLine(exePath);

                if (!File.Exists(exePath))
                {
                    throw new FileNotFoundException("Nie znaleziono pliku 'Email Sender.exe'.", exePath);
                }

                string time = runTime.ToString("HH:mm");
                string date = runTime.ToString("dd/MM/yyyy").Replace(".", "/");


                string command = $"/create /tn \"{subject}\" /tr \"\\\"{exePath}\\\" \\\"{subject}\\\" \\\"{body}\\\" \\\"{to}\\\"\" /sc once /st {time} /sd {date} /f";
                Console.WriteLine(command);


                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "schtasks",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Błąd przy tworzeniu zadania: {error}");
                    }

                    Console.WriteLine(output);
                    Console.WriteLine($"Task scheduled to send email at {runTime}.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd: {e.Message}");
            }
        }

        public static void DeleteTask(string taskName)
        {
            try
            {
                string command = $"/delete /tn \"{taskName}\" /f";


                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "schtasks",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                }
            }
            catch 
            {
                
            }
            

        }
    }
}
