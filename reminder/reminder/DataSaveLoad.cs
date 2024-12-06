using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reminder
{
    class DataSaveLoad
    {
        public void SaveTasks(Tasks tasks)
        {
            try
            {
                string jsonString = System.Text.Json.JsonSerializer.Serialize(tasks);
                File.WriteAllText("tasks.json", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine("DATASAVELOAD");
                Console.WriteLine(e.Message);
            }

        }

        public Tasks LoadTasks()
        {
            try
            {
                string jsonString = File.ReadAllText("tasks.json");
                return System.Text.Json.JsonSerializer.Deserialize<Tasks>(jsonString);
            }
            catch (FileNotFoundException)
            {
                return new Tasks();
            }

        }
    }
}
