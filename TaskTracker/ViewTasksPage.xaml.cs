using System.Collections.ObjectModel;
using System.Text.Json;
using TaskTracker.Models;
using TaskTracker.ViewModels;

namespace TaskTracker;

public partial class ViewTasksPage : ContentPage
{

	TaskPageViewModel TaskPageViewModel = new TaskPageViewModel();
	public ViewTasksPage()
	{
		InitializeComponent();

		BindingContext = TaskPageViewModel;

    }

    private void taskCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var obj = sender as CheckBox;

        var task = obj.BindingContext as TaskItem;

        removeTask(task);

        TaskPageViewModel.loadTaskList();
    }

    public void removeTask(TaskItem task)
    {
        var taskListString = Preferences.Get("@task_list", string.Empty);

        if (!string.IsNullOrEmpty(taskListString))
        {
            var taskListOc = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(taskListString);

            var itemToRemove = taskListOc.FirstOrDefault(t => t.Id == task.Id);

            if (itemToRemove != null)
            {
                taskListOc.Remove(itemToRemove);

                var taskListJson = JsonSerializer.Serialize(taskListOc);
                Preferences.Set("@task_list", taskListJson);
            }
        }
    }
}