using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reminder
{
    internal class Task
    {
        
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDateTime { get; set; }


        public Task(string taskName, string taskDescription, DateTime taskDateTime)
        {
            TaskName = taskName;
            TaskDescription = taskDescription;
            TaskDateTime = taskDateTime;
        }
    }
}
