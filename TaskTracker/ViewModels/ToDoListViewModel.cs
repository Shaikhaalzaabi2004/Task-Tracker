using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.ViewModels
{
    public class ToDoListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> Items { get; set; }

        public ToDoListViewModel()
        {

            loadTaskList();
        }

        public void loadTaskList()
        {
            if (Items == null)
                Items = new ObservableCollection<TaskItem>();

            Items.Clear();

            var taskListJson = Preferences.Get("@task_list", string.Empty);
            if (!string.IsNullOrEmpty(taskListJson))
            {
                var taskListOc = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(taskListJson);
                foreach (var task in taskListOc)
                    if(task.Date.Date == DateTime.Now.Date)
                    Items.Add(task);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
