using System.Text.Json;
using System.Text.RegularExpressions;

namespace reminder
{
    internal class Program
    {
        static void Main(string[] args)
        {


            string choice;
            
            DataSaveLoad dataSaveLoad = new DataSaveLoad();
            Tasks tasks = dataSaveLoad.LoadTasks();

            bool program = true;

            while (program)
            {
                Console.WriteLine("Hello! What you want to do?");
                Console.WriteLine("1. Add a task");
                Console.WriteLine("2. Remove task");
                Console.WriteLine("3. View tasks");
                Console.WriteLine("4. Exit");

                do
                {
                    Console.WriteLine("Your choice (1 - 3) : ");
                    choice = Console.ReadLine();
                }
                while (choice != "1" && choice != "2" && choice != "3" && choice != "4");

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("--------- Add a task ------------");
                        Console.WriteLine(
                            "Enter task name, task description and task date and time in the format (yyyy-MM-dd HH:mm:ss) : ");
                        Console.WriteLine("Task name : ");
                        string taskName = Console.ReadLine();

                        // sprawdzanie czy taka nazwa juz istnieje
                        foreach (var task in tasks.TaskList)
                        {
                            if (task.TaskName == taskName)
                            {
                                Console.WriteLine("Task with this name already exists");
                                break;
                            }
                        }

                        Console.WriteLine("Task description: ");
                        string taskDescription = Console.ReadLine();
                        Console.WriteLine("Task Date : ");
                        DateTime taskDateTime = DateTime.Parse(Console.ReadLine());
                        tasks.TaskList.Add(new Task(taskName, taskDescription, taskDateTime));
                        Console.WriteLine("Email : ");
                        string email = Console.ReadLine();

                        if (!IsValidEmail(email))
                        {
                            Console.WriteLine("Invalid email address");
                            break;
                        }

                        TaskSchedulerHelper.ScheduleEmailTask(taskDateTime, taskName, taskDescription, email);
                        Console.WriteLine(
                            $"Task scheduled to send email at {taskDateTime}.");




                        break;

                    case "2":
                        RemoveTask(tasks);
                        // TODO : Save tasks to a file
                        break;
                    case "3":
                        ShowTasks(tasks);
                        break;
                    case "4":
                        Console.WriteLine("Bye..");
                        program = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;

                    
                }

                dataSaveLoad.SaveTasks(tasks);

            }


        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        private static void RemoveTask(Tasks tasks)
        {
            try
            {
                Console.WriteLine("----------- List of tasks : ----------------");
                ShowTasks(tasks);
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Enter the task number to remove : ");
                Console.WriteLine("Lenght : " + tasks.TaskList.Count);
                int taskNumber = int.Parse(Console.ReadLine());
                tasks.TaskList.RemoveAt(taskNumber - 1);
                TaskSchedulerHelper.DeleteTask(tasks.TaskList[taskNumber - 1].TaskName);
                Console.WriteLine("Task removed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
            
        }

        private static void ShowTasks(Tasks tasks)
        {
            Console.WriteLine("----------- List of tasks : ----------------");
            if (tasks.TaskList.Count == 0)
            {
                Console.WriteLine("No tasks found");
                return;
            }

            for (int i = 0; i < tasks.TaskList.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"------------------ Task {i + 1} -------------------------");
                Console.WriteLine($"Task Name : {tasks.TaskList[i].TaskName}");
                Console.WriteLine($"Task Description : {tasks.TaskList[i].TaskDescription}");
                Console.WriteLine($"Task Date and Time : {tasks.TaskList[i].TaskDateTime}");
                Console.WriteLine("----------------------------------------------------------");
            }

        }
    }
}
