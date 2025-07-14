using System.Collections.ObjectModel;
using System.Text.Json;
using TaskTracker.Models;
using TaskTracker.ViewModels;

namespace TaskTracker;

public partial class AddTaskPage : ContentPage
{
    public AddTaskPage()
    {
        InitializeComponent();
    }
    public bool validateInput()
    {
        if (string.IsNullOrEmpty(nameEntry.Text) || datePicker.Date == null)
            return false;
        return true;
    }
    public void clearInput()
    {
        nameEntry.Text = null;
        descriptionEntry.Text = null;
        datePicker.Date = DateTime.Today;
    }

    private void addTaskBtn_Clicked(object sender, EventArgs e)
    {
        if (!validateInput())
        {
            DisplayAlert("Invalid", "Please Input a Name and Date", "Return");
        }
        else
        {
            int currentId = Preferences.Get("task_id_counter", 0);
            currentId++; 
            Preferences.Set("task_id_counter", currentId);

            TaskItem newTask = new TaskItem()
            {
                Id = currentId,
                Name = nameEntry.Text,
                Description = descriptionEntry.Text,
                Date = datePicker.Date,
            };

            var taskListJson = Preferences.Get("@task_list", string.Empty);
            var taskListOc = new ObservableCollection<TaskItem>();
            if (!string.IsNullOrEmpty(taskListJson))
            {
                taskListOc = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(taskListJson);
                taskListOc.Add(newTask);

            }
            else
            {
                taskListOc = new ObservableCollection<TaskItem>();
                taskListOc.Add(newTask);
            }

            taskListJson = JsonSerializer.Serialize(taskListOc);
            Preferences.Set("@task_list", taskListJson);
            DisplayAlert("Success", "Task Added", "Return");
            clearInput();
        }
    }

    private async void cancelBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}