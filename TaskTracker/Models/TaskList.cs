using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTracker.Models
{
    public class TaskList
    {
        public static ObservableCollection<TaskItem> taskList = new ObservableCollection<TaskItem>();

    }
}
